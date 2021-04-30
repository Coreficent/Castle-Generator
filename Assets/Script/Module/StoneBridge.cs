namespace Coreficent.Module
{
    public class StoneBridge : TileBase
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
