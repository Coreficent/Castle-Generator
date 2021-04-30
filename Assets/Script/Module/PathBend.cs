namespace Coreficent.Module
{
    public class PathBend : TileBase
    {
        public PathBend()
        {
            north.Add(Face.Grass);

            west.Add(Face.Grass);

            south.Add(Face.Path);

            east.Add(Face.Path);
        }
    }
}
