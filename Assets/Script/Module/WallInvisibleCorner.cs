namespace Coreficent.Module
{
    public class WallInvisibleCorner : ModuleBase
    {
        public WallInvisibleCorner()
        {
            north.Add(Face.WallSpace);

            west.Add(Face.WallSpace);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.GrassyTerrain);
        }

        public override int Weight => 1024;
    }
}
