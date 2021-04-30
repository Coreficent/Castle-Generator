namespace Coreficent.Module
{
    public class Dirt : Module
    {
        public Dirt()
        {
            north.Add(Face.Dirt);

            west.Add(Face.Dirt);

            south.Add(Face.Dirt);

            east.Add(Face.Dirt);
        }
    }
}
