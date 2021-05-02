namespace Coreficent.Module
{
    public class WallCornerEast : ModuleBase
    {
        public WallCornerEast()
        {
            north.Add(Face.WallSpace);

            west.Add(Face.WallCornerToEast);

            south.Add(Face.WallEastToTower);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.Foundation);

            weight = 1024;
        }
    }
}
