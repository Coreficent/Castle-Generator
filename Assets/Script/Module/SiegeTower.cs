namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class SiegeTower : ModuleBase
    {
        public SiegeTower()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.GrassyTerrain);

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
