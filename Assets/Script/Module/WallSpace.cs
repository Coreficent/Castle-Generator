using Coreficent.Utility;

namespace Coreficent.Module
{
    public class WallSpace : ModuleBase
    {
        public WallSpace()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.Foundation);

            weight = 1024;
        }
    }
}
