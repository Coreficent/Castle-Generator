namespace Coreficent.Generation
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using static Coreficent.Tile.TileBase;

    public class SuperPosition : Script, IComparer<SuperPosition>
    {
        [SerializeField]
        private List<TileBase> positions = new List<TileBase>();

        private List<TileBase> children = new List<TileBase>();

        protected virtual void Start()
        {
            foreach (TileBase tileBase in positions)
            {
                if (IsEmptySuperPosition(this))
                {
                    children.Add(Instantiate(tileBase, transform));
                }
                else
                {
                    HashSet<TileBase> filter = new HashSet<TileBase>();

                    for (int i = 0; i < 4; ++i)
                    {
                        TileBase tile = Instantiate(tileBase, transform);

                        tile.transform.eulerAngles = new Vector3(0.0f, 0.0f, i * 90.0f);

                        if (!filter.Contains(tile))
                        {
                            children.Add(tile);
                            filter.Add(tile);
                        }
                        else
                        {
                            Destroy(tile.gameObject);
                        }

                    }
                }
            }
            Render();
        }

        private void Render()
        {
            int scale = 1;

            while (scale < Mathf.Sqrt(children.Count))
            {
                scale <<= 2;
            }

            for (int i = 0; i < children.Count; ++i)
            {
                TileBase result = children[i];
                result.transform.localScale = new Vector3(1.0f / scale, 1.0f / scale, 1.0f);
            }

            int index = 0;

            for (int x = 0; x < Mathf.Sqrt(children.Count); ++x)
            {
                for (int y = 0; y < Mathf.Sqrt(children.Count); ++y)
                {
                    if (index < children.Count)
                    {
                        children[index].transform.localPosition = new Vector3(1.0f * x / scale - 0.25f / scale * (children.Count - 1), 1.0f * y / scale - 0.25f / scale * (children.Count - 1), 0.0f);
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

            TileBase selection = Instantiate(children[UnityEngine.Random.Range(0, children.Count)], transform);

            foreach (TileBase tileBase in children.ToList())
            {
                RemoveChild(tileBase);
            }

            children.Add(selection);

            Render();
        }

        public bool Propagate(World world)
        {
            if (IsEmptySuperPosition(this))
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

        private bool IsEmptySuperPosition(SuperPosition superPosition)
        {
            return superPosition.positions.Count == 1 && superPosition.positions[0] is EmptyTile;
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
            SuperPosition otherPosition;
            HashSet<Socket> originSockets;

            switch (direction)
            {
                case Direction.North:
                    otherPosition = world.Find(X, Y + 1);
                    break;
                case Direction.West:
                    otherPosition = world.Find(X - 1, Y);
                    break;
                case Direction.South:
                    otherPosition = world.Find(X, Y - 1);
                    break;
                case Direction.East:
                    otherPosition = world.Find(X + 1, Y);
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

            foreach (TileBase tileBase in children.ToList())
            {

                HashSet<Socket> tileSockets;

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
                    RemoveChild(tileBase);
                }
            }
        }

        private void RemoveChild(TileBase tileBase)
        {
            children.Remove(tileBase);
            Destroy(tileBase.gameObject);
        }

        private HashSet<Socket> FindValidSockets(Direction direction)
        {
            HashSet<Socket> result = new HashSet<Socket>();

            switch (direction)
            {
                case Direction.North:
                    foreach (TileBase tileBase in children)
                    {
                        result.UnionWith(tileBase.North);
                    }
                    break;

                case Direction.East:
                    foreach (TileBase tileBase in children)
                    {
                        result.UnionWith(tileBase.East);
                    }
                    break;

                case Direction.South:
                    foreach (TileBase tileBase in children)
                    {
                        result.UnionWith(tileBase.South);
                    }
                    break;

                case Direction.West:
                    foreach (TileBase tileBase in children)
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
                return Mathf.RoundToInt(transform.position.x);
            }
        }

        public int Y
        {
            get
            {
                return Mathf.RoundToInt(transform.position.y);
            }
        }

        public int Compare(SuperPosition x, SuperPosition y)
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
