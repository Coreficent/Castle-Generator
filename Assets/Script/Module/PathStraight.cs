namespace Coreficent.Module
{
    public class PathStraight : ModuleBase
    {
        public PathStraight()
        {
            north.Add(Face.Path);

            west.Add(Face.Grass);

            south.Add(Face.Path);

            east.Add(Face.Grass);
        }
    }
}
