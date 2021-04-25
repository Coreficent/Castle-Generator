namespace Coreficent.Tile
{
    public class PathBend : TileBase
    {
        public PathBend()
        {
            north.Add(Socket.Grass);

            east.Add(Socket.Path);

            south.Add(Socket.Path);

            west.Add(Socket.Grass);
        }
    }
}
