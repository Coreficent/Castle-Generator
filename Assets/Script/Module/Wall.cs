namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class Wall : ModuleBase
    {
        public Wall()
        {
            north.Add(Face.Wall);

            west.Add(Face.Air);

            south.Add(Face.Wall);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.GrassyTerrain);
        }

        public override int Weight => 1024;
    }
}
