namespace Coreficent.Module
{
    public class WallCornerEast : ModuleBase
    {
        public WallCornerEast()
        {
            //north.Add(Face.Air);
            north.Add(Face.WallSpace);

            west.Add(Face.WallCornerCenterEast);

            south.Add(Face.WallCornerEastSeast);

            east.Add(Face.Air);

            top.Add(Face.Air);
            top.Add(Face.TowerEast);

            bottom.Add(Face.CastleFloorEdge);
            
        }

        public override int Weight => 1024;
    }
}
