namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class FlowerBunch : ModuleBase
    {
        public FlowerBunch()
        {
            north.Add(Face.DirtGrass);

            west.Add(Face.DirtGrass);

            south.Add(Face.DirtGrass);

            east.Add(Face.DirtGrass);

            top.Add(Face.Air);
            top.Add(Face.Plant);

            bottom.Add(Face.Dirt);
        }

        public override int Weight => 1024;
    }
}
