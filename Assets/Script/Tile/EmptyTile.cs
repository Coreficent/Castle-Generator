namespace Coreficent.Tile
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EmptyTile : TileBase
    {
        public EmptyTile()
        {
            north.Add(Socket.Grass);

            east.Add(Socket.Grass);

            south.Add(Socket.Grass);

            west.Add(Socket.Grass);
        }
    }
}