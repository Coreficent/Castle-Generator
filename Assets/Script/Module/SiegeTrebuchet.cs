namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class SiegeTrebuchet : ModuleBase
    {
        public SiegeTrebuchet()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.GrassyTerrain);

        }

        public override int Weight => 1024;
    }
}
