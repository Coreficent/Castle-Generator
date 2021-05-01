namespace Coreficent.Module
{
    public class GrassEdge : ModuleBase
    {
        public GrassEdge()
        {
            north.Add(Face.Grass);

            west.Add(Face.Grass);

            south.Add(Face.Grass);

            east.Add(Face.Grass);
        }
    }
}
