namespace Coreficent.Module
{
    public class Dirt : TileBase
    {
        public Dirt()
        {
            north.Add(Face.Grass);

            west.Add(Face.Dirt);

            south.Add(Face.Dirt);

            east.Add(Face.Dirt);
        }
    }
}
