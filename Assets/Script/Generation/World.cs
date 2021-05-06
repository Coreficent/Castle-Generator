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
        private readonly Superposition[,,] worldMap = new Superposition[Tuning.Width, Tuning.Height, Tuning.Depth];
        private readonly Superposition superposition;
        private readonly GameObject board;

        private List<IFilter> filters = new List<IFilter>();

        private int index = 0;
        private int z = 0;
        private int y = 0;
        private int x = 0;

        public World(Superposition superposition, GameObject board)
        {
            this.board = UnityEngine.Object.Instantiate(board);
            this.board.name = "World";
            this.board.transform.position = new Vector3(-(Tuning.Width - 1) / 2.0f, -(Tuning.Height - 1) / 2.0f, -(Tuning.Depth - 1) / 2.0f);
            this.superposition = superposition;

            filters.Add(new FilterDirt());
            filters.Add(new FilterAir());
        }

        public float Progress
        {
            get
            {
                return 1.0f * index / (Tuning.Width * Tuning.Height * Tuning.Depth);
            }
        }

        public bool Validated
        {
            get
            {
                for (int x = 0; x < Tuning.Width; ++x)
                {
                    for (int y = 0; y < Tuning.Height; ++y)
                    {
                        for (int z = 0; z < Tuning.Depth; ++z)
                        {
                            if (worldMap[x, y, z] == null)
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }
        }

        public bool HasNext()
        {
            return z < Tuning.Depth;
        }

        public void Next()
        {
            if (z < Tuning.Depth)
            {
                if (y < Tuning.Height)
                {
                    if (x < Tuning.Width)
                    {
                        Superposition superpositionClone = UnityEngine.Object.Instantiate(superposition, board.transform);
                        superpositionClone.World = this;
                        superpositionClone.transform.localPosition = new Vector3(x, y, z);
                        worldMap[x, y, z] = superpositionClone;

                        foreach (var filter in filters)
                        {
                            if (filter.filtered(superpositionClone))
                            {
                                break;
                            }
                        }

                        ++index;
                        ++x;
                    }
                    else
                    {
                        ++y;
                        x = 0;
                        Next();
                    }
                }
                else
                {
                    ++z;
                    y = 0;
                    x = 0;
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
            return x >= 0 && y >= 0 && z >= 0 && x < Tuning.Width && y < Tuning.Height && z < Tuning.Depth;
        }

        public Superposition Find(int x, int y, int z)
        {
            if (!Has(x, y, z))
            {
                Test.Warn("position not found", x, y, z);
                return null;
            }

            Superposition result = worldMap[x, y, z];

            if (result == null)
            {
                Test.Warn("null superposition found at", x, y, z);
            }

            return worldMap[x, y, z];
        }

        public bool Collapsed
        {
            get
            {
                for (int x = 0; x < Tuning.Width; ++x)
                {
                    for (int y = 0; y < Tuning.Height; ++y)
                    {
                        for (int z = 0; z < Tuning.Depth; ++z)
                        {
                            if (!worldMap[x, y, z].Collapsed)
                            {
                                return false;
                            }
                        }
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

            for (int x = 0; x < Tuning.Width; ++x)
            {
                for (int y = 0; y < Tuning.Height; ++y)
                {
                    for (int z = 0; z < Tuning.Depth; ++z)
                    {
                        Superposition superposition = worldMap[x, y, z];

                        if (filter(superposition))
                        {
                            result.Add(superposition);
                        }
                    }
                }
            }

            return result;
        }
    }
}
