namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class Mushroom : ModuleBase
    {
        public Mushroom()
        {
            north.Add(Face.DirtGrass);

            west.Add(Face.DirtGrass);

            south.Add(Face.DirtGrass);

            east.Add(Face.DirtGrass);

            top.Add(Face.Air);
            top.Add(Face.Plant);

            bottom.Add(Face.Dirt);

            precalculateNormal = false;
            outlineDarkness = 0.0f;
            outlineThickness = 0.05f;
            shadingDarkness = 0.5f;
            shadowThreshold = 0.5f;
            shadeThreshold = 0.5f;
        }

        public override int Weight => 1024;
    }
}
