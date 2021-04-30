namespace Coreficent.Generation
{
    using Coreficent.Module;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using static Coreficent.Module.Module;

    public class Superposition : Script, IComparer<Superposition>
    {
        public Module border;

        [SerializeField]
        private List<Module> positions = new List<Module>();

        private List<Module> children = new List<Module>();

        protected virtual void Awake()
        {
            foreach (Module tileBase in positions)
            {
                HashSet<Module> filter = new HashSet<Module>();

                for (int i = 0; i < 4; ++i)
                {
                    Module tile = Instantiate(tileBase, transform);

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
            int scale = 1;

            //while (scale < Mathf.Sqrt(children.Count))
            //{
            //    scale <<= 2;
            //}

            while (scale < Mathf.Sqrt(children.Count))
            {
                ++scale;
            }

            for (int i = 0; i < children.Count; ++i)
            {
                Module result = children[i];
                result.transform.localScale = new Vector3(1.0f / scale, 1.0f / scale, 1.0f);
            }

            int index = 0;

            int length = Mathf.RoundToInt(Mathf.Sqrt(children.Count));

            for (int x = 0; x < length; ++x)
            {
                for (int y = 0; y < length; ++y)
                {
                    if (index < children.Count)
                    {
                        float offset = length == 1 ? 0.0f : 1.0f * length / scale / 2;

                        children[index].transform.localPosition = new Vector3(1.0f * x / scale - offset, 1.0f * y / scale - offset, 0.0f);
                    }

                    ++index;
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

            Module selection = Instantiate(children[UnityEngine.Random.Range(0, children.Count)], transform);
            DeleteChildren();
            AddChild(selection);

            Render();
        }

        public void Collapse(Module selection)
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

            foreach (Module tileBase in children.ToList())
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

        private void AddChild(Module tileBase)
        {
            children.Add(tileBase);
        }

        private void RemoveChild(Module tileBase)
        {
            children.Remove(tileBase);
        }

        private void DeleteChild(Module tileBase)
        {
            RemoveChild(tileBase);
            Destroy(tileBase.gameObject);
        }

        private void DeleteChildren()
        {
            foreach (Module tileBase in children.ToList())
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
                    foreach (Module tileBase in children)
                    {
                        result.UnionWith(tileBase.North);
                    }
                    break;

                case Direction.East:
                    foreach (Module tileBase in children)
                    {
                        result.UnionWith(tileBase.East);
                    }
                    break;

                case Direction.South:
                    foreach (Module tileBase in children)
                    {
                        result.UnionWith(tileBase.South);
                    }
                    break;

                case Direction.West:
                    foreach (Module tileBase in children)
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
