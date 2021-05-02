using Coreficent.Utility;

namespace Coreficent.Module
{
    public class WallCornerSouth : ModuleBase
    {
        public WallCornerSouth()
        {
            north.Add(Face.WallCornerToSouth);

            west.Add(Face.WallSpace);

            south.Add(Face.Air);

            east.Add(Face.WallSouthToTower);

            top.Add(Face.Air);

            bottom.Add(Face.Foundation);

            weight = 1024;
        }
    }
}
