namespace Coreficent.Tile
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GroundPathEnd : TileBase
    {
        public GroundPathEnd()
        {
            north.Add(Socket.GrassToGrass);
            south.Add(Socket.GroundPathEnd);
        }
    }
}
