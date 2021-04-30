namespace Coreficent.Tile
{
    public class River : TileBase
    {
        public River()
        {
            north.Add(Socket.Grass);

            west.Add(Socket.Grass);

            south.Add(Socket.Grass);

            east.Add(Socket.Grass);
        }
    }
}
