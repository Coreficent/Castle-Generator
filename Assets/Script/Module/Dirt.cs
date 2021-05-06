namespace Coreficent.Module
{
    public class Dirt : ModuleBase
    {
        public Dirt()
        {
            north.Add(Face.Dirt);

            west.Add(Face.Dirt);

            south.Add(Face.Dirt);

            east.Add(Face.Dirt);

            top.Add(Face.Dirt);

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
