namespace Coreficent.Module
{
    public class PathEnd : Module
    {
        public PathEnd()
        {
            north.Add(Face.Grass);

            west.Add(Face.Grass);

            south.Add(Face.Path);

            east.Add(Face.Grass);
        }
    }
}
