namespace Coreficent.Tile
{
    public class EmptyTile : TileBase
    {
        public EmptyTile()
        {
            north.Add(Socket.Grass);

            west.Add(Socket.Grass);

            south.Add(Socket.Grass);

            east.Add(Socket.Grass);
        }
    }
}