namespace Coreficent.Module
{
    public class WallCornerCenter : ModuleBase
    {
        public WallCornerCenter()
        {
            north.Add(Face.Wall);

            west.Add(Face.Wall);

            south.Add(Face.WallCornerCenterSouth);

            east.Add(Face.WallCornerCenterEast);

            top.Add(Face.TowerTopCorner);
            top.Add(Face.TowerSegment);

            bottom.Add(Face.Foundation);
        }

        public override int Weight => 1024;
    }
}
