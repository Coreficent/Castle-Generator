namespace Coreficent.Tile
{
    public class Grass : TileBase
    {
        public Grass()
        {
            north.Add(Socket.Grass);

            east.Add(Socket.Grass);

            south.Add(Socket.Grass);

            west.Add(Socket.Grass);
        }
    }
}
