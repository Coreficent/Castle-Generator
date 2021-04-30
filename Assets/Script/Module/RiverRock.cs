namespace Coreficent.Module
{
    public class RiverRock : Module
    {
        public RiverRock()
        {
            north.Add(Face.River);

            west.Add(Face.Grass);

            south.Add(Face.River);

            east.Add(Face.Grass);
        }
    }
}
