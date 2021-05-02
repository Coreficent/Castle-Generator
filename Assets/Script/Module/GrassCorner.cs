namespace Coreficent.Module
{
    public class GrassCorner : ModuleBase
    {
        public GrassCorner()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Grass);

            east.Add(Face.Grass);

            top.Add(Face.Air);

            bottom.Add(Face.Dirt);
        }

        public override int Weight => 1024;
    }
}
