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

            precalculateNormal = false;
            outlineDarkness = 0.0f;
            outlineThickness = 0.5f;
            shadingDarkness = 0.5f;
            shadowThreshold = 0.5f;
            shadeThreshold = 0.5f;
        }

        public override int Weight => 1024;
    }
}
