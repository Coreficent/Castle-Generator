namespace Coreficent.Module
{
    public class CastleGroundEdge : ModuleBase
    {
        public CastleGroundEdge()
        {
            north.Add(Face.DirtGrass);

            west.Add(Face.CastleFloorSide);

            south.Add(Face.CastleFloorSide);

            east.Add(Face.CastleFloorSide);

            top.Add(Face.CastleFloorEdge);

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
