namespace Coreficent.Generation
{
    using Coreficent.Module;
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using static Coreficent.Module.ModuleBase;

    public class Superposition : Script, IComparer<Superposition>
    {
        public ModuleBase air;
        public ModuleBase dirt;

        [SerializeField]
        private List<ModuleBase> positions = new List<ModuleBase>();

        private readonly List<ModuleBase> children = new List<ModuleBase>();

        private bool immutable = false;

        public bool Immutable
        {
            get
            {
                return immutable;
            }
            set
            {
                immutable = value;
            }
        }

        protected virtual void Awake()
        {
            Uncollapse();
        }

        protected override void Start()
        {
            name = ToString();
            base.Start();
        }

        private void Render()
        {
            int volume = 1;

            while (volume < children.Count)
            {
                volume <<= 2;
            }

            int scale = volume >> 1;

            if (scale == 0)
            {
                scale = 1;
            }

            float scaler = 1.0f / scale;
            float sideScale = children.Count == 1 ? scaler : scaler * 1.0f;

            for (int i = 0; i < children.Count; ++i)
            {
                ModuleBase child = children[i];
                child.transform.localScale = new Vector3(sideScale, sideScale, sideScale);
                child.Visible = false;
            }

            int index = 0;

            float offset = 0.5f * scaler * (scale - 1);

            for (int x = 0; x < scale; ++x)
            {
                for (int y = 0; y < scale; ++y)
                {
                    for (int z = 0; z < scale; ++z)
                    {
                        if (children.Count >= Tuning.MaximumRenderCount)
                        {
                            return;
                        }
                        else
                        {
                            if (index < children.Count)
                            {
                                ModuleBase child = children[index];

                                child.Visible = true;
                                child.transform.localPosition = new Vector3(x * scaler - offset, y * scaler - offset, z * scaler - offset);
                            }
                            else
                            {
                                return;
                            }
                        }

                        index++;
                    }
                }
            }
        }

        public bool Collapsed
        {
            get
            {
                return children.Count <= 1;
            }
        }

        public bool Uncollapsible
        {
            get
            {
                return children.Count == 0;
            }
        }

        public void Collapse(World world)
        {
            Bind(world);

            if (!Uncollapsible)
            {


                int totalWeight = children.Select(module => module.Weight).Sum();

                if (totalWeight <= 0)
                {
                    Test.Warn("unexpected total weight");
                }

                // Test.Debug("wet", totalWeight);

                int pick = Random.Range(0, totalWeight);

                int score = 0;

                ModuleBase selectedModule = null;

                for (int i = 0; i < children.Count; ++i)
                {
                    score += children[i].Weight;

                    if (score >= pick)
                    {
                        selectedModule = Instantiate(children[i], transform);
                        break;
                    }
                }

                if (selectedModule == null)
                {
                    Test.Warn("fail to select module based on weight");
                }

                DeleteChildren();
                AddChild(selectedModule);
            }

            Render();
        }

        public void Collapse(ModuleBase selection)
        {
            DeleteChildren();

            AddChild(Instantiate(selection, transform));

            Render();
        }

        public void Uncollapse()
        {
            foreach (ModuleBase tileBase in positions)
            {
                HashSet<ModuleBase> filter = new HashSet<ModuleBase>();

                for (int i = 0; i < 4; ++i)
                {

                    tileBase.transform.localEulerAngles = new Vector3(0.0f, 0.0f, i * 90.0f);

                    if (!filter.Contains(tileBase))
                    {
                        ModuleBase module = Instantiate(tileBase, transform);

                        AddChild(module);
                        filter.Add(module);
                    }
                }
            }

            Render();
        }


        public bool Propagate(World world)
        {
            if (Collapsed)
            {
                return false;
            }

            int childrenCountStart = children.Count;

            Bind(world);

            if (childrenCountStart == children.Count)
            {
                return false;
            }

            if (children.Count == 0)
            {
                Test.Log("try to uncollapse", this);

                UncollapseIfNotBoundary(world, X, Y + 1, Z);
                UncollapseIfNotBoundary(world, X - 1, Y, Z);
                UncollapseIfNotBoundary(world, X, Y - 1, Z);
                UncollapseIfNotBoundary(world, X + 1, Y, Z);
                UncollapseIfNotBoundary(world, X, Y, Z - 1);
                UncollapseIfNotBoundary(world, X, Y, Z + 1);

                Uncollapse();

                return true;
            }

            Render();

            return true;
        }

        private void UncollapseIfNotBoundary(World world, int x, int y, int z)
        {
            Superposition superposition = world.Find(x, y, z); ;
            if (!superposition.Immutable)
            {
                superposition.Uncollapse();
            }
        }

        private void Bind(World world)
        {
            Constrain(world, Direction.North);
            Constrain(world, Direction.West);
            Constrain(world, Direction.South);
            Constrain(world, Direction.East);
            Constrain(world, Direction.Top);
            Constrain(world, Direction.Bottom);
        }

        private void Constrain(World world, Direction direction)
        {
            Superposition otherPosition;
            HashSet<Face> thatFaces;

            switch (direction)
            {
                case Direction.North:
                    otherPosition = world.Find(X, Y + 1, Z);
                    break;
                case Direction.West:
                    otherPosition = world.Find(X - 1, Y, Z);
                    break;
                case Direction.South:
                    otherPosition = world.Find(X, Y - 1, Z);
                    break;
                case Direction.East:
                    otherPosition = world.Find(X + 1, Y, Z);
                    break;
                case Direction.Top:
                    otherPosition = world.Find(X, Y, Z - 1);
                    break;
                case Direction.Bottom:
                    otherPosition = world.Find(X, Y, Z + 1);
                    break;
                default:
                    Test.Warn("attempt to constrain from an invalid origin");
                    return;
            }

            if (otherPosition.Uncollapsible)
            {
                Test.Log("skip collapse", this, otherPosition);
                return;
            }

            thatFaces = otherPosition.FindValidFaces(InverseDirection(direction));

            foreach (ModuleBase tileBase in children.ToList())
            {

                HashSet<Face> thisFaces;

                switch (direction)
                {
                    case Direction.North:
                        thisFaces = tileBase.NorthSet;
                        break;
                    case Direction.East:
                        thisFaces = tileBase.EastSet;
                        break;
                    case Direction.South:
                        thisFaces = tileBase.SouthSet;
                        break;
                    case Direction.West:
                        thisFaces = tileBase.WestSet;
                        break;
                    case Direction.Top:
                        thisFaces = tileBase.TopSet;
                        break;
                    case Direction.Bottom:
                        thisFaces = tileBase.BottomSet;
                        break;
                    default:
                        Test.Log("unexpected direction");
                        return;
                }



                if (thatFaces.Intersect(thisFaces).ToList().Count == 0)
                {
                    DeleteChild(tileBase);
                }
            }
        }

        private void AddChild(ModuleBase tileBase)
        {
            children.Add(tileBase);
        }

        private void RemoveChild(ModuleBase tileBase)
        {
            children.Remove(tileBase);
        }

        private void DeleteChild(ModuleBase tileBase)
        {
            RemoveChild(tileBase);
            Destroy(tileBase.gameObject);
        }

        private void DeleteChildren()
        {
            foreach (ModuleBase tileBase in children.ToList())
            {
                DeleteChild(tileBase);
            }
        }

        private HashSet<Face> FindValidFaces(Direction direction)
        {
            HashSet<Face> result = new HashSet<Face>();

            if (DebugMode.On)
            {
                if (children.Where(child => child == null).Count() > 0)
                {
                    Test.Warn("null child found in children");
                }
            }


            switch (direction)
            {
                case Direction.North:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.NorthSet);
                    }
                    break;

                case Direction.East:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.EastSet);
                    }
                    break;

                case Direction.South:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.SouthSet);
                    }
                    break;

                case Direction.West:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.WestSet);
                    }
                    break;

                case Direction.Top:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.TopSet);
                    }
                    break;

                case Direction.Bottom:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.BottomSet);
                    }
                    break;

                default:
                    Test.Log("unexpected direction");
                    break;

            }

            return result;
        }

        public int X
        {
            get
            {
                return Round(transform.localPosition.x);
            }
        }

        public int Y
        {
            get
            {
                return Round(transform.localPosition.y);
            }
        }

        public int Z
        {
            get
            {
                return Round(transform.localPosition.z);
            }
        }

        private int Round(float Input)
        {
            return Mathf.RoundToInt(Input);
        }

        public int Compare(Superposition x, Superposition y)
        {
            return x.Entropy.CompareTo(y.Entropy);
        }

        public int Entropy
        {
            get
            {
                return children.Count;
            }
        }

        public override string ToString()
        {
            string delimiter = ":";
            return GetType().Name + delimiter + X + delimiter + Y + delimiter + Z;
        }
    }
}
