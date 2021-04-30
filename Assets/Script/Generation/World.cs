namespace Coreficent.Generation
{
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class World
    {
        public int Width = Tuning.Width;
        public int Height = Tuning.Height;
        public int Depth = Tuning.Depth;

        private readonly Dictionary<string, Superposition> map = new Dictionary<string, Superposition>();
        private readonly HashSet<Superposition> uncollapsedPositions = new HashSet<Superposition>();

        public World(Superposition superPosition)
        {
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    for (int z = 0; z < Depth; ++z)
                    {
                        Superposition position = Object.Instantiate(superPosition);
                        position.transform.position = new Vector3(x, y, z);
                        map.Add(Hash(x, y, z), position);

                        //if (x == 0 || y == 0 || x == Tuning.Width - 1 || y == Tuning.Height - 1)
                        //{
                        //    position.Collapse(position.border);
                        //}
                        //else
                        //{
                        //    uncollapsedPositions.Add(position);
                        //}

                        uncollapsedPositions.Add(position);
                    }
                }
            }
        }

        public Superposition Find(int x, int y, int z)
        {
            if (!map.ContainsKey(Hash(x, y, z)))
            {
                Test.Warn("position not found", Hash(x, y, z));
                return null;
            }

            return map[Hash(x, y, z)];
        }

        private string Hash(int x, int y, int z)
        {
            return "" + x + "::" + y + "::" + z + "";
        }

        public bool Collapsed
        {
            get
            {
                foreach (KeyValuePair<string, Superposition> entry in map)
                {
                    if (!entry.Value.Collapsed)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public Superposition Next
        {
            get
            {
                int entropy = int.MaxValue;
                Superposition result = null;

                foreach (Superposition superposition in uncollapsedPositions.ToList())
                {

                    if (superposition.Collapsed)
                    {
                        uncollapsedPositions.Remove(superposition);
                    }
                    else
                    {
                        if (superposition.Entropy < entropy)
                        {
                            entropy = superposition.Entropy;
                            result = superposition;
                        }
                    }
                }

                if (result == null)
                {
                    Test.Warn("unexpected null position for uncollapsed world");
                }

                return result;
            }
        }
    }
}
