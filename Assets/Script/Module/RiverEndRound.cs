namespace Coreficent.Module
{
    public class RiverEndRound : TileBase
    {
        public RiverEndRound()
        {
            north.Add(Face.Grass);

            west.Add(Face.Grass);

            south.Add(Face.River);

            east.Add(Face.Grass);
        }
    }
}
