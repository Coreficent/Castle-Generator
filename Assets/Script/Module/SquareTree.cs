namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class SquareTree : ModuleBase
    {
        public SquareTree()
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
