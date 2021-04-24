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

        protected virtual void Start()
        {
            int index = 0;
            foreach (TileBase tileBase in positions)
            {
                TileBase result = Instantiate(tileBase, transform);
                result.transform.localPosition = new Vector3(0.0f, 0.0f, 0.1f * index++);
            }
        }


        public bool Collapsed
        {
            get
            {
                return positions.Count == 1;
            }
        }

        public List<SuperPosition> Collapse()
        {
            throw new NotImplementedException();
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
