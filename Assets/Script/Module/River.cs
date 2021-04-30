namespace Coreficent.Module
{
    public class River : Module
    {
        public River()
        {
            north.Add(Face.Grass);

            west.Add(Face.Grass);

            south.Add(Face.Grass);

            east.Add(Face.Grass);
        }
    }
}
