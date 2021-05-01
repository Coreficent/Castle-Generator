namespace Coreficent.Module
{
    public class GrassEdge : ModuleBase
    {
        public GrassEdge()
        {
            north.Add(Face.Air);

            west.Add(Face.Grass);

            south.Add(Face.Grass);

            east.Add(Face.Grass);

            top.Add(Face.Air);

            bottom.Add(Face.Dirt);
        }
    }
}
