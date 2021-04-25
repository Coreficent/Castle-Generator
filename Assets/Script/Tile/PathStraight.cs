namespace Coreficent.Tile
{
    public class PathStraight : TileBase
    {
        public PathStraight()
        {
            north.Add(Socket.Path);

            east.Add(Socket.Grass);

            south.Add(Socket.Path);

            west.Add(Socket.Grass);
        }
    }
}
