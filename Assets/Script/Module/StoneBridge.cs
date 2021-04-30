namespace Coreficent.Module
{
    public class StoneBridge : Module
    {
        public StoneBridge()
        {
            north.Add(Face.River);

            west.Add(Face.Path);

            south.Add(Face.River);

            east.Add(Face.Path);
        }
    }
}
