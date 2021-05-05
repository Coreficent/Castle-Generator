﻿namespace Coreficent.Generation
{
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class World : IAnimatable
    {
        private readonly Dictionary<string, Superposition> worldMap = new Dictionary<string, Superposition>();

        private GameObject board;
        private Superposition superposition;

        private int x = 0;
        private int y = 0;
        private int z = 0;
        private int index = 0;

        public World(Superposition superposition, GameObject board)
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
                        Superposition position = UnityEngine.Object.Instantiate(superposition, board.transform);
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

        public Superposition Find(int x, int y, int z)
        {
            if (!worldMap.ContainsKey(Hash(x, y, z)))
            {
                Test.Warn("position not found", Hash(x, y, z));
                return null;
            }

            Superposition result = worldMap[Hash(x, y, z)];

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
                foreach (KeyValuePair<string, Superposition> entry in worldMap)
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
                HashSet<Superposition> uncollapsedPositions = Collect(superposition => !superposition.Collapsed);

                int entropy = uncollapsedPositions.ToList().Min(superposition => superposition.Entropy);
                var minimumEntropy = uncollapsedPositions.ToList().Where(superposition => superposition.Entropy == entropy);

                int depth = minimumEntropy.ToList().Max(superposition => superposition.Z);
                var maximumDepth = minimumEntropy.ToList().Where(superposition => superposition.Z == depth).ToList();

                //int entropy = int.MaxValue;

                //foreach (Superposition superposition in Collect(superposition => !superposition.Collapsed))
                //{
                //    if (superposition.Entropy < entropy)
                //    {
                //        minimumEntropy.Clear();
                //        entropy = superposition.Entropy;
                //        minimumEntropy.Add(superposition);
                //    }
                //    else if (superposition.Entropy == entropy)
                //    {
                //        minimumEntropy.Add(superposition);
                //    }
                //}

                //List<Superposition> maximumDepth = new List<Superposition>();
                //int depth = int.MinValue;

                //foreach (Superposition superposition in minimumEntropy)
                //{
                //    if (superposition.Z > depth)
                //    {
                //        maximumDepth.Clear();
                //        depth = superposition.Z;
                //        maximumDepth.Add(superposition);
                //    }
                //    else if (superposition.Z == depth)
                //    {
                //        maximumDepth.Add(superposition);
                //    }
                //}

                if (maximumDepth.Count == 0)
                {
                    Test.Warn("minimum entropy not found");
                }

                return maximumDepth[UnityEngine.Random.Range(0, maximumDepth.Count)];
            }
        }

        public Superposition NextRandomUncollapsedPosition
        {
            get
            {
                List<Superposition> superpositions = Collect(superposition => !superposition.Collapsed).ToList();

                return superpositions[UnityEngine.Random.Range(0, superpositions.Count)];
            }
        }

        public HashSet<Superposition> Collect(Func<Superposition, bool> filter)
        {
            HashSet<Superposition> result = new HashSet<Superposition>();

            foreach (KeyValuePair<string, Superposition> entry in worldMap)
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
