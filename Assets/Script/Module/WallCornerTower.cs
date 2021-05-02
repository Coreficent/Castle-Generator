namespace Coreficent.Module
{
    public class WallCornerTower : ModuleBase
    {
        public WallCornerTower()
        {
            north.Add(Face.WallEastToTower);

            west.Add(Face.WallSouthToTower);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.Foundation);

            weight = 1024;
        }
    }
}
