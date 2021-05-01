namespace Coreficent.Generation
{
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class World
    {
        private readonly Dictionary<string, Superposition> map = new Dictionary<string, Superposition>();
        private readonly HashSet<Superposition> uncollapsedPositions = new HashSet<Superposition>();

        private GameObject board;

        public World(Superposition superPosition, GameObject board)
        {
            this.board = UnityEngine.Object.Instantiate(board);
            this.board.transform.position = new Vector3(-(Tuning.Width - 1) / 2.0f, -(Tuning.Height - 1) / 2.0f, -(Tuning.Depth - 1) / 2.0f);

            for (int x = 0; x < Tuning.Width; ++x)
            {
                for (int y = 0; y < Tuning.Height; ++y)
                {
                    for (int z = 0; z < Tuning.Depth; ++z)
                    {
                        Superposition position = UnityEngine.Object.Instantiate(superPosition, this.board.transform);
                        position.transform.localPosition = new Vector3(x, y, z);
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

        public Superposition NextMinimumEntropyPosition
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

        public HashSet<Superposition> Collect(Func<Superposition, bool> filter)
        {
            HashSet<Superposition> result = new HashSet<Superposition>();

            foreach (KeyValuePair<string, Superposition> entry in map)
            {
                if (filter(entry.Value))
                {
                    result.Add(entry.Value);
                }
            }

            return result;
        }
    }
}
