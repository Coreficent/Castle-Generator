namespace Coreficent.Module
{
    public class GrassInterior : ModuleBase
    {
        public GrassInterior()
        {
            north.Add(Face.DirtGrass);

            west.Add(Face.DirtGrass);

            south.Add(Face.DirtGrass);

            east.Add(Face.DirtGrass);

            top.Add(Face.Air);
            top.Add(Face.GrassyTerrain);

            bottom.Add(Face.Dirt);

            precalculateNormal = false;
            outlineDarkness = 0.03f;
            outlineThickness = 0.5f;
            shadingDarkness = 0.5f;
            shadowThreshold = 0.5f;
            shadeThreshold = 0.5f;
        }
        public override int Weight => 1024;
    }
}
