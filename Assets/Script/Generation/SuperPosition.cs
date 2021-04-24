namespace Coreficent.Generation
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SuperPosition : Script, IComparer<SuperPosition>
    {
        [SerializeField]
        List<TileBase> positions = new List<TileBase>();

        List<TileBase> children = new List<TileBase>();

        protected virtual void Start()
        {
            for (int i = 0; i < positions.Count; ++i)
            {
                TileBase result = Instantiate(positions[i], transform);
                result.transform.localScale = new Vector3(1.0f / positions.Count, 1.0f / positions.Count, 1.0f);
                result.transform.localPosition = new Vector3(0.0f, 1.0f * i / positions.Count - (0.25f * (positions.Count - 1)), 0.0f);
                children.Add(result);
            }
        }


        public bool Collapsed
        {
            get
            {
                return children.Count == 1;
            }
        }

        public List<SuperPosition> Collapse(World world)
        {
            TileBase selection = Instantiate(children[UnityEngine.Random.Range(0, children.Count)], transform);
            selection.transform.localScale = Vector3.one;
            selection.transform.localPosition = Vector3.zero;

            foreach (TileBase tileBase in children)
            {
                Destroy(tileBase.gameObject);
            }

            children.Clear();
            children.Add(selection);

            Test.ToDo("implement neightbors");
            return null;
        }

        public int Compare(SuperPosition x, SuperPosition y)
        {
            return x.Entropy.CompareTo(y.Entropy);
        }

        public int Entropy
        {
            get
            {
                return positions.Count;
            }
        }
    }
}
