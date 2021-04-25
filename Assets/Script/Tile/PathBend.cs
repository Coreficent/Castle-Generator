namespace Coreficent.Tile
{
    public class PathBend : TileBase
    {
        public PathBend()
        {
            north.Add(Socket.Grass);

            west.Add(Socket.Grass);

            south.Add(Socket.Path);

            east.Add(Socket.Path);
        }
    }
}
