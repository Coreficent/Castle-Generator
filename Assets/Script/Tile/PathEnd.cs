namespace Coreficent.Tile
{
    public class PathEnd : TileBase
    {
        public PathEnd()
        {
            north.Add(Socket.Grass);

            east.Add(Socket.Grass);

            south.Add(Socket.Path);

            west.Add(Socket.Grass);
        }
    }
}
