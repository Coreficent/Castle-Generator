namespace Coreficent.Generation
{
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using static Coreficent.Tile.TileBase;

    public class WaveFunctionCollapse : IAnimatable
    {
        private World world;

        private Stack<SuperPosition> dequeue = new Stack<SuperPosition>();
        private HashSet<SuperPosition> track = new HashSet<SuperPosition>();

        public WaveFunctionCollapse(SuperPosition superPosition, SuperPosition emptyPosition)
        {
            world = new World(superPosition, emptyPosition);
        }

        public bool HasNext()
        {
            return !world.Collapsed;
        }

        public void Next()
        {

            if (dequeue.Count > 0)
            {
                SuperPosition superPosition = dequeue.Pop();

                // Test.Bug("pro", superPosition.X, superPosition.Y);

                if (superPosition.Propagate(world))
                {

                    foreach (SuperPosition i in FindNeighbors(superPosition.X, superPosition.Y, superPosition.Z))
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
                // Test.Bug("collapse");

                SuperPosition superPosition = world.Next;

                superPosition.Collapse(world);

                //SuperPosition propogate;

                //propogate = world.Find(superPosition.X, superPosition.Y + 1);
                //dequeue.Push(propogate);

                //propogate = world.Find(superPosition.X - 1, superPosition.Y);
                //dequeue.Push(propogate);

                //propogate = world.Find(superPosition.X, superPosition.Y - 1);
                //dequeue.Push(propogate);

                //propogate = world.Find(superPosition.X + 1, superPosition.Y);
                //dequeue.Push(propogate);

                foreach (SuperPosition i in FindNeighbors(superPosition.X, superPosition.Y, superPosition.Z))
                {
                    dequeue.Push(i);
                }

                track.Clear();
                track.Add(superPosition);
            }
        }

        List<SuperPosition> FindNeighbors(int x, int y, int z)
        {
            List<SuperPosition> result = new List<SuperPosition>();

            result.Add(world.Find(x, y + 1, z));
            result.Add(world.Find(x - 1, y, z));
            result.Add(world.Find(x, y - 1, z));
            result.Add(world.Find(x + 1, y, z));

            return result;
        }

    }
}
