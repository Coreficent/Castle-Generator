namespace Coreficent.Module
{
    public class EmptyTile : Module
    {
        public EmptyTile()
        {
            north.Add(Face.Grass);

            west.Add(Face.Grass);

            south.Add(Face.Grass);

            east.Add(Face.Grass);
        }
    }
}