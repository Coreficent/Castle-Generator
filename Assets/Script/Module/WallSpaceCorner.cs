namespace Coreficent.Module
{
    public class WallSpaceCorner : ModuleBase
    {
        public WallSpaceCorner()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.Foundation);

            weight = 0;
        }
    }
}
