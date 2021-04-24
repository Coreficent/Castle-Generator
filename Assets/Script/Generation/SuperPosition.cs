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

            for (int i = 0; i < positions.Count; ++i)
            {
                TileBase result = Instantiate(positions[i], transform);
                result.transform.localScale = new Vector3(1.0f / positions.Count, 1.0f / positions.Count, 1.0f);
                result.transform.localPosition = new Vector3(0.0f, 1.0f * i / positions.Count - 0.5f / positions.Count, 0.0f);
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
