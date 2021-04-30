namespace Coreficent.Tile
{
    public class StoneBridge : TileBase
    {
        public StoneBridge()
        {
            north.Add(Socket.River);

            west.Add(Socket.Path);

            south.Add(Socket.River);

            east.Add(Socket.Path);
        }
    }
}
