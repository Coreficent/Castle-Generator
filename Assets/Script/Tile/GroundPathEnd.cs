namespace Coreficent.Tile
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GroundPathEnd : TileBase
    {
        public GroundPathEnd()
        {
            south.Add(Socket.GrassToGrass);
        }
    }
}
