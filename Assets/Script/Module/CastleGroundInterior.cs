namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class CastleGroundInterior : ModuleBase
    {
        public CastleGroundInterior()
        {
            north.Add(Face.CastleFloorSide);

            west.Add(Face.CastleFloorSide);

            south.Add(Face.CastleFloorSide);

            east.Add(Face.CastleFloorSide);

            top.Add(Face.Air);
            top.Add(Face.CastleFloorInterior);

            bottom.Add(Face.Dirt);

            precalculateNormal = false;
            outlineDarkness = 0.0f;
            outlineThickness = 0.0125f;
            shadingDarkness = 0.5f;
            shadowThreshold = 0.5f;
            shadeThreshold = 0.5f;
        }

        public override int Weight => 1024;
    }
}
