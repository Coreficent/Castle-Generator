namespace Coreficent.Tile
{
    public class PathEnd : TileBase
    {
        public PathEnd()
        {
            north.Add(Socket.Grass);

            west.Add(Socket.Grass);

            south.Add(Socket.Path);

            east.Add(Socket.Grass);
        }
    }
}
