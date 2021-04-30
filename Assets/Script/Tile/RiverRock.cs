namespace Coreficent.Tile
{
    public class RiverRock : TileBase
    {
        public RiverRock()
        {
            north.Add(Socket.River);

            west.Add(Socket.Grass);

            south.Add(Socket.River);

            east.Add(Socket.Grass);
        }
    }
}
