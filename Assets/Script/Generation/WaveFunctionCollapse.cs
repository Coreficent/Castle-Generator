namespace Coreficent.Generation
{
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class WaveFunctionCollapse : IAnimatable
    {
        private World world;

        public WaveFunctionCollapse(SuperPosition superPosition)
        {
            world = new World(superPosition);
        }

        public bool HasNext()
        {
            return !world.Collapsed;
        }

        public void Next()
        {
            SuperPosition superPosition = world.Next;
            superPosition.Collapse();
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
