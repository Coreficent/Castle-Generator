using Coreficent.Utility;

namespace Coreficent.Module
{
    public class WallCornerSouth : ModuleBase
    {
        public WallCornerSouth()
        {
            north.Add(Face.WallCenterSouth);

            west.Add(Face.WallSpace);

            south.Add(Face.Air);

            east.Add(Face.WallSouthTower);

            top.Add(Face.Air);

            bottom.Add(Face.Foundation);

            weight = 1024;
        }
    }
}
