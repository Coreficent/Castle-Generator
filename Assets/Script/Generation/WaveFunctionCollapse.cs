namespace Coreficent.Generation
{
    using Coreficent.Utility;
    using System.Collections.Generic;

    public class WaveFunctionCollapse : IAnimatable
    {
        private Stack<Superposition> dequeue = new Stack<Superposition>();
        private HashSet<Superposition> track = new HashSet<Superposition>();

        private readonly World world;
        public WaveFunctionCollapse(World world)
        {
            this.world = world;
        }

        public void QueueUncollapsedModules()
        {
            foreach (Superposition superposition in world.Collect(superposition => !superposition.Collapsed))
            {
                dequeue.Push(superposition);
            }
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

                if (superposition.Propagate(world))
                {

                    foreach (Superposition i in FindNeighbors(superposition.X, superposition.Y, superposition.Z))
                    {
                        if (!track.Contains(i))
                        {
                            dequeue.Push(i);
                            track.Add(i);
                        }
                    }
                }
                else
                {
                    Next();
                }
            }
            else
            {
                Superposition superposition = world.NextMinimumEntropyPosition;

                superposition.Collapse(world);

                //Test.Pause();

                foreach (Superposition i in FindNeighbors(superposition.X, superposition.Y, superposition.Z))
                {
                    dequeue.Push(i);
                }

                track.Clear();
                track.Add(superposition);
            }
        }

        List<Superposition> FindNeighbors(int x, int y, int z)
        {
            List<Superposition> result = new List<Superposition>();

            result.Add(world.Find(x, y + 1, z));
            result.Add(world.Find(x - 1, y, z));
            result.Add(world.Find(x, y - 1, z));
            result.Add(world.Find(x + 1, y, z));
            result.Add(world.Find(x, y, z - 1));
            result.Add(world.Find(x, y, z + 1));

            return result;
        }

    }
}
