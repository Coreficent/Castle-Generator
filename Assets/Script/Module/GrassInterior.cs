namespace Coreficent.Module
{
    public class GrassInterior : ModuleBase
    {
        public GrassInterior()
        {
            north.Add(Face.Grass);

            west.Add(Face.Grass);

            south.Add(Face.Grass);

            east.Add(Face.Grass);

            top.Add(Face.Air);
            top.Add(Face.Foundation);

            bottom.Add(Face.Dirt);

        }
        public override int Weight => 1024;
    }
}
