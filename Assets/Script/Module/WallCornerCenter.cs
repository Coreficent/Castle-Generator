namespace Coreficent.Module
{
    public class WallCornerCenter : ModuleBase
    {
        public WallCornerCenter()
        {
            north.Add(Face.WallNorth);

            west.Add(Face.WallWest);

            south.Add(Face.WallCornerCenterSouth);

            east.Add(Face.WallCornerCenterEast);

            top.Add(Face.TowerTopCorner);
            top.Add(Face.TowerSegment);

            bottom.Add(Face.CastleFloorInterior);
        }

        public override int Weight => 1024;
    }
}
