namespace Coreficent.Tile
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GroundPathEnd : TileBase
    {
        public GroundPathEnd()
        {
            north.Add(Socket.Grass);

            east.Add(Socket.Grass);

            south.Add(Socket.Path);

            west.Add(Socket.Grass);
        }
    }
}
