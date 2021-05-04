namespace Coreficent.Generation
{
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class World : IAnimatable
    {
        private readonly Dictionary<string, SuperpositionX> worldMap = new Dictionary<string, SuperpositionX>();

        private GameObject board;
        private SuperpositionX superposition;

        private int x = 0;
        private int y = 0;
        private int z = 0;
        private int index = 0;

        public World(SuperpositionX superposition, GameObject board)
        {
            this.board = UnityEngine.Object.Instantiate(board);
            this.board.name = "World";
            this.board.transform.position = new Vector3(-(Tuning.Width - 1) / 2.0f, -(Tuning.Height - 1) / 2.0f, -(Tuning.Depth - 1) / 2.0f);

            this.superposition = superposition;
        }

        public float Progress
        {
            get
            {
                return 1.0f * index / (Tuning.Width * Tuning.Height * Tuning.Depth);
            }
        }

        public bool HasNext()
        {
            return x < Tuning.Width;
        }

        public void Next()
        {
            if (x < Tuning.Width)
            {
                if (y < Tuning.Height)
                {
                    if (z < Tuning.Depth)
                    {
                        SuperpositionX position = UnityEngine.Object.Instantiate(superposition, board.transform);
                        position.World = this;
                        position.transform.localPosition = new Vector3(x, y, z);
                        worldMap.Add(Hash(x, y, z), position);

                        ++index;
                        ++z;
                    }
                    else
                    {
                        ++y;
                        z = 0;
                        Next();
                    }
                }
                else
                {
                    ++x;
                    y = 0;
                    z = 0;
                    Next();
                }
            }
        }

        public void Clear()
        {
            UnityEngine.Object.Destroy(board);
        }

        public bool Has(int x, int y, int z)
        {
            return worldMap.ContainsKey(Hash(x, y, z));
        }

        public SuperpositionX Find(int x, int y, int z)
        {
            if (!worldMap.ContainsKey(Hash(x, y, z)))
            {
                Test.Warn("position not found", Hash(x, y, z));
                return null;
            }

            SuperpositionX result = worldMap[Hash(x, y, z)];

            if (!result || result == null || result.ToString() == "null")
            {
                Test.Warn("null superposition found at", x, y, z);
            }

            return worldMap[Hash(x, y, z)];
        }

        private string Hash(int x, int y, int z)
        {
            return "" + x + "::" + y + "::" + z + "";
        }

        public bool Collapsed
        {
            get
            {
                foreach (KeyValuePair<string, SuperpositionX> entry in worldMap)
                {
                    if (!entry.Value.Collapsed)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public SuperpositionX NextMinimumEntropyPosition
        {
            get
            {
                int entropy = int.MaxValue;
                List<SuperpositionX> result = new List<SuperpositionX>();

                foreach (SuperpositionX superposition in Collect(superposition => !superposition.Collapsed))
                {
                    if (superposition.Entropy < entropy)
                    {
                        entropy = superposition.Entropy;
                        result.Add(superposition);
                    }
                }

                if (result.Count == 0)
                {
                    Test.Warn("minimum entropy not found");
                }

                return result[UnityEngine.Random.Range(0, result.Count)];
            }
        }

        public SuperpositionX NextRandomUncollapsedPosition
        {
            get
            {
                List<SuperpositionX> superpositions = Collect(superposition => !superposition.Collapsed).ToList();

                return superpositions[UnityEngine.Random.Range(0, superpositions.Count)];
            }
        }

        public HashSet<SuperpositionX> Collect(Func<SuperpositionX, bool> filter)
        {
            HashSet<SuperpositionX> result = new HashSet<SuperpositionX>();

            foreach (KeyValuePair<string, SuperpositionX> entry in worldMap)
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
