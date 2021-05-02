using Coreficent.Utility;

namespace Coreficent.Module
{
    public class WallCornerSouth : ModuleBase
    {
        public WallCornerSouth()
        {
            north.Add(Face.WallCornerCenterSouth);

            west.Add(Face.WallSpace);

            south.Add(Face.Air);

            east.Add(Face.WallCornerSouthSeast);

            top.Add(Face.TowerSouth);

            bottom.Add(Face.Foundation);

            weight = 1024;
        }
    }
}
