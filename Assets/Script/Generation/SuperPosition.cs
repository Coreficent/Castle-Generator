namespace Coreficent.Generation
{
    using Coreficent.Module;
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

        private List<ModuleBase> children = new List<ModuleBase>();

        protected virtual void Awake()
        {
            foreach (ModuleBase tileBase in positions)
            {
                HashSet<ModuleBase> filter = new HashSet<ModuleBase>();

                for (int i = 0; i < 4; ++i)
                {
                    ModuleBase tile = Instantiate(tileBase, transform);

                    tile.transform.eulerAngles = new Vector3(0.0f, 0.0f, i * 90.0f);

                    if (!filter.Contains(tile))
                    {
                        AddChild(tile);
                        filter.Add(tile);
                    }
                    else
                    {
                        Destroy(tile.gameObject);
                    }
                }
            }

            Render();
        }

        private void Render()
        {
            int volume = 1;

            while (volume < children.Count)
            {
                volume <<= 2;
            }

            int scale = volume >> 2;

            if (scale == 0)
            {
                scale = 1;
            }

            float scaler = 1.0f / scale;
            float sideScale = children.Count == 1 ? scaler : scaler * 1.0f;

            for (int i = 0; i < children.Count; ++i)
            {
                ModuleBase result = children[i];
                result.transform.localScale = new Vector3(sideScale, sideScale, sideScale);
            }

            int index = 0;

            float offset = 0.5f * scaler * (scale - 1);

            for (int x = 0; x < scale; ++x)
            {
                for (int y = 0; y < scale; ++y)
                {
                    for (int z = 0; z < scale; ++z)
                    {
                        if (index < children.Count)
                        {
                            children[index].transform.localPosition = new Vector3(x * scaler - offset, y * scaler - offset, z * scaler - offset);
                        }
                        else
                        {
                            return;
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

        public void Collapse(World world)
        {
            Bind(world);

            ModuleBase selection = Instantiate(children[Random.Range(0, children.Count)], transform);
            DeleteChildren();
            AddChild(selection);

            Render();
        }

        public void Collapse(ModuleBase selection)
        {
            DeleteChildren();

            AddChild(Instantiate(selection, transform));

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
                Test.Warn("unable to collapse");

                return false;
            }

            Render();

            return true;
        }

        private void Bind(World world)
        {
            Constrain(world, Direction.North);
            Constrain(world, Direction.West);
            Constrain(world, Direction.South);
            Constrain(world, Direction.East);
        }

        private void Constrain(World world, Direction direction)
        {
            Superposition otherPosition;
            HashSet<Face> originSockets;

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
                default:
                    Test.Warn("attempt to constrain from an invalid origin");
                    return;
            }

            if (otherPosition.children.Count == 0)
            {
                Test.Log("avoid cllapsing on uncollapsable tile");
                return;
            }

            originSockets = otherPosition.FindValidSockets(InverseDirection(direction));

            foreach (ModuleBase tileBase in children.ToList())
            {

                HashSet<Face> tileSockets;

                switch (direction)
                {
                    case Direction.North:
                        tileSockets = tileBase.North;
                        break;
                    case Direction.East:
                        tileSockets = tileBase.East;
                        break;
                    case Direction.South:
                        tileSockets = tileBase.South;
                        break;
                    case Direction.West:
                        tileSockets = tileBase.West;
                        break;
                    default:
                        Test.Log("unexpected direction");
                        return;
                }

                if (originSockets.Intersect(tileSockets).ToList().Count == 0)
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

        private HashSet<Face> FindValidSockets(Direction direction)
        {
            HashSet<Face> result = new HashSet<Face>();

            switch (direction)
            {
                case Direction.North:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.North);
                    }
                    break;

                case Direction.East:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.East);
                    }
                    break;

                case Direction.South:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.South);
                    }
                    break;

                case Direction.West:
                    foreach (ModuleBase tileBase in children)
                    {
                        result.UnionWith(tileBase.West);
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
    }
}
