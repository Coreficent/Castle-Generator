namespace Coreficent.Module
{
    public class WallCornerCenter : ModuleBase
    {
        public WallCornerCenter()
        {
            north.Add(Face.Wall);

            west.Add(Face.Wall);

            south.Add(Face.WallCornerToSouth);

            east.Add(Face.WallCornerToEast);

            top.Add(Face.Air);

            bottom.Add(Face.Foundation);

            weight = 1024;
        }
    }
}
