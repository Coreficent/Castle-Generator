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
        }
        public override int Weight => 1024;
    }
}
