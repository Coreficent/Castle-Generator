namespace Coreficent.Module
{
    public class WallDoor : ModuleBase
    {
        public WallDoor()
        {
            north.Add(Face.WallNorth);
            north.Add(Face.WallWest);

            west.Add(Face.WallInvisible);

            south.Add(Face.WallNorth);
            south.Add(Face.WallWest);

            east.Add(Face.Air);

            // top.Add(Face.WallTop);
            top.Add(Face.Air);

            bottom.Add(Face.CastleFloorInterior);

            precalculateNormal = false;
            outlineDarkness = 0.0f;
            outlineThickness = 0.5f;
            shadingDarkness = 0.5f;
            shadowThreshold = 0.5f;
            shadeThreshold = 0.5f;
        }

        public override int Weight => 1024 * 16 * 16 * 16;
    }
}
