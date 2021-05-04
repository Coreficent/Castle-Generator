namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class FlagBlue : ModuleBase
    {
        public FlagBlue()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.TowerSquareTop);

        }

        public override int Weight => 1024;
    }
}
