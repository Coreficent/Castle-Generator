namespace Coreficent.Module
{
    public class Grass : Module
    {
        public Grass()
        {
            north.Add(Face.Grass);

            west.Add(Face.Grass);

            south.Add(Face.Grass);

            east.Add(Face.Grass);
        }
    }
}
