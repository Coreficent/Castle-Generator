namespace Coreficent.Module
{
    public class WallCornerSeast : ModuleBase
    {
        public WallCornerSeast()
        {
            north.Add(Face.WallCornerEastSeast);

            west.Add(Face.WallCornerSouthSeast);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.CastleFloor);
        }

        public override int Weight => 1024;
    }
}
