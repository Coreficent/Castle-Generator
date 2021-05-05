namespace Coreficent.Generation
{
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class WaveFunctionCollapse : IAnimatable
    {
        private Stack<Superposition> dequeue = new Stack<Superposition>();
        private HashSet<Superposition> track = new HashSet<Superposition>();

        private int uncollapseCount = 0;

        private readonly World world;

        public WaveFunctionCollapse(World world)
        {
            this.world = world;
        }

        public bool HasNext()
        {
            return !world.Collapsed;
        }

        public void Next()
        {
            if (dequeue.Count > 0)
            {
                Superposition superposition = dequeue.Pop();

                Propagation state = superposition.Propagate();

                switch (state)
                {
                    case Propagation.Reduced:
                        foreach (Superposition i in FindNeighbors(superposition.X, superposition.Y, superposition.Z))
                        {
                            if (!track.Contains(i))
                            {
                                dequeue.Push(i);
                                track.Add(i);
                            }
                        }
                        break;

                    case Propagation.Unchanged:
                        Next();
                        break;

                    case Propagation.Collapsed:
                        Next();
                        break;

                    case Propagation.Uncollapsible:
                        dequeue.Push(world.NextRandomUncollapsedPosition);

                        ++uncollapseCount;

                        break;
                }
            }
            else
            {
                Superposition superposition = world.NextMinimumEntropyPosition;

                superposition.Collapse();

                //Test.Pause();

                foreach (Superposition i in FindNeighbors(superposition.X, superposition.Y, superposition.Z))
                {
                    dequeue.Push(i);
                }

                track.Clear();
                track.Add(superposition);
            }
        }

        public void PrintStatistics()
        {
            Test.Log("statistics: uncollapse count, width, height, depth", uncollapseCount, Tuning.Width, Tuning.Height, Tuning.Depth);
        }

        private List<Superposition> FindNeighbors(int x, int y, int z)
        {
            List<Superposition> result = new List<Superposition>();

            result.Add(world.Find(x, y + 1, z));
            result.Add(world.Find(x - 1, y, z));
            result.Add(world.Find(x, y - 1, z));
            result.Add(world.Find(x + 1, y, z));
            result.Add(world.Find(x, y, z - 1));
            result.Add(world.Find(x, y, z + 1));

            return result.OrderBy(a => Random.Range(0, 1024)).ToList(); ;
        }
    }
}
