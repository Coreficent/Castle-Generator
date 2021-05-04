namespace Coreficent.Module
{
    public class TowerSquareRoof : ModuleBase
    {
        public TowerSquareRoof()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.TowerSquareTop);

            bottom.Add(Face.WallTop);
        }

        public override int Weight => 500;
    }
}
