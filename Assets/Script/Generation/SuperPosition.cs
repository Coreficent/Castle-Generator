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
        List<TileBase> positions = new List<TileBase>();

        List<TileBase> children = new List<TileBase>();

        protected virtual void Start()
        {
            foreach (TileBase tileBase in positions)
            {
                children.Add(Instantiate(tileBase, transform));
            }
            Render();
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
            TileBase selection = Instantiate(children[UnityEngine.Random.Range(0, children.Count)], transform);

            foreach (TileBase tileBase in children)
            {
                Destroy(tileBase.gameObject);
            }

            children.Clear();
            children.Add(selection);

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
                // result.transform.localPosition = new Vector3(0.0f, 1.0f * i / children.Count - (0.25f * (children.Count - 1)), 0.0f);
                //result.transform.localPosition = new Vector3(0.0f, 0.0f, -0.5f * i);
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

        public List<SuperPosition> Propagate(World world, Direction direction)
        {
            List<SuperPosition> result = new List<SuperPosition>();

            if (direction == Direction.Up)
            {
                SuperPosition superPosition = world.Find(X, Y + 1);

                if (superPosition.Collapsed)
                {
                    return result;
                }

                HashSet<Socket> socketsUp = FindValidSockets(Direction.Up);

                foreach (TileBase tileBase in superPosition.children.ToList())
                {
                    if (socketsUp.Intersect(tileBase.South).ToList().Count == 0)
                    {
                        superPosition.children.Remove(tileBase);
                        Destroy(tileBase.gameObject);

                        result.Add(superPosition);


                    }
                }

                superPosition.Render();
            }

            return result;
        }

        private HashSet<Socket> FindValidSockets(Direction direction)
        {
            HashSet<Socket> result = new HashSet<Socket>();

            if (direction == Direction.Up)
            {
                foreach (TileBase tileBase in children)
                {
                    result.UnionWith(tileBase.North);
                }
            }

            Test.ToDo("other directions");

            Test.ToDo("use set in tile base instead of list");

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
