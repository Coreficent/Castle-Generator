namespace Coreficent.Tile
{
    public class RiverEndRound : TileBase
    {
        public RiverEndRound()
        {
            north.Add(Socket.Grass);

            west.Add(Socket.Grass);

            south.Add(Socket.River);

            east.Add(Socket.Grass);
        }
    }
}
