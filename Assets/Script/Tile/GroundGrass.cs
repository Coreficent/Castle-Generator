namespace Coreficent.Tile
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GroundGrass : TileBase
    {
        public GroundGrass()
        {
            north.Add(Socket.GrassToGrass);
            east.Add(Socket.GrassToGrass);
            south.Add(Socket.GrassToGrass);
            west.Add(Socket.GrassToGrass);
        }
    }
}
