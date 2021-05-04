namespace Coreficent.Module
{
    public class WallCornerSouth : ModuleBase
    {
        public WallCornerSouth()
        {
            north.Add(Face.WallCornerCenterSouth);

            //west.Add(Face.Air);
            west.Add(Face.WallSpace);

            south.Add(Face.Air);

            east.Add(Face.WallCornerSouthSeast);

            top.Add(Face.Air);
            top.Add(Face.TowerSouth);

            bottom.Add(Face.CastleFloor);
        }
        public override int Weight => 1024;
    }
}
