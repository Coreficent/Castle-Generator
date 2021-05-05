namespace Coreficent.Module
{
    public class GrassEdge : ModuleBase
    {
        public GrassEdge()
        {
            north.Add(Face.Air);

            west.Add(Face.DirtGrass);

            south.Add(Face.DirtGrass);

            east.Add(Face.DirtGrass);

            top.Add(Face.Air);

            bottom.Add(Face.Dirt);

            precalculateNormal = false;
            outlineDarkness = 0.0f;
            outlineThickness = 0.03f;
            shadingDarkness = 0.5f;
            shadowThreshold = 0.5f;
            shadeThreshold = 0.5f;
        }
        public override int Weight => 1024;
    }
}
