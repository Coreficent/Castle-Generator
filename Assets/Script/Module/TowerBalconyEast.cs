namespace Coreficent.Module
{
    public class TowerBalconyEast : ModuleBase
    {
        public TowerBalconyEast()
        {
            north.Add(Face.Air);

            west.Add(Face.TowerBalconyCenterEast);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.TowerEast);
        }

        public override int Weight => 1024;
    }
}
