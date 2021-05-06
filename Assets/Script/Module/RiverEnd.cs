namespace Coreficent.Module
{
    public class RiverEnd : ModuleBase
    {
        public RiverEnd()
        {
            north.Add(Face.DirtGrass);

            west.Add(Face.DirtGrass);

            south.Add(Face.River);

            east.Add(Face.DirtGrass);

            top.Add(Face.Air);
            top.Add(Face.RiverTop);

            bottom.Add(Face.Dirt);

            precalculateNormal = true;
            outlineDarkness = 0.0f;
            outlineThickness = 0.5f;
            shadingDarkness = 0.5f;
            shadowThreshold = 0.5f;
            shadeThreshold = 0.5f;
        }

        public override int Weight => 1024;
    }
}
