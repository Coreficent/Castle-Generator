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

                    SuperPosition candidate;

                    candidate = world.Find(superPosition.X, superPosition.Y + 1);

                    if (!track.Contains(candidate))
                    {
                        dequeue.Push(candidate);
                        track.Add(candidate);
                    }

                    candidate = world.Find(superPosition.X + 1, superPosition.Y);

                    if (!track.Contains(candidate))
                    {
                        dequeue.Push(candidate);
                        track.Add(candidate);
                    }

                    candidate = world.Find(superPosition.X, superPosition.Y - 1);

                    if (!track.Contains(candidate))
                    {
                        dequeue.Push(candidate);
                        track.Add(candidate);
                    }

                    candidate = world.Find(superPosition.X - 1, superPosition.Y);

                    if (!track.Contains(candidate))
                    {
                        dequeue.Push(candidate);
                        track.Add(candidate);
                    }
                }
                else
                {
                    Next();
                }
            }
            else
            {
                Test.Bug("collapse");

                SuperPosition superPosition = world.Next;

                superPosition.Collapse(world);

                SuperPosition propogate;

                propogate = world.Find(superPosition.X, superPosition.Y + 1);
                propogate.PropogateOrigin = Direction.South;
                dequeue.Push(propogate);

                propogate = world.Find(superPosition.X - 1, superPosition.Y);
                propogate.PropogateOrigin = Direction.East;
                dequeue.Push(propogate);

                propogate = world.Find(superPosition.X, superPosition.Y - 1);
                propogate.PropogateOrigin = Direction.North;
                dequeue.Push(propogate);

                propogate = world.Find(superPosition.X + 1, superPosition.Y);
                propogate.PropogateOrigin = Direction.West;
                dequeue.Push(propogate);

                track.Clear();
                track.Add(superPosition);
            }
        }

        /*

        map = graph

        while map.not_fully_collapsed()
            collapsible_chain = dequeue
            collapsible_chain.push(map.minimum_block)

            while collapsible_chain.not_empty()
                block <- collapsible_chain
                collapsible_chain.push(block.collapse())

        class block::collaps -> collapsed block list
        {
            result = list
            for each in north based on rotation
                if not have this north socket given tiles in this block
                    remove
                    result.add(north)

            do the same for other directions
        }

         */

    }
}
