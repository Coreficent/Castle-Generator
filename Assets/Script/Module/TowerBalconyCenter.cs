namespace Coreficent.Module
{
    public class TowerBalconyCenter : ModuleBase
    {
        public TowerBalconyCenter()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.TowerBalconyCenterSouth);

            east.Add(Face.TowerBalconyCenterEast);

            top.Add(Face.Air);
            top.Add(Face.TowerCenter);

            bottom.Add(Face.TowerCenter);
        }

        public override int Weight => 1024;
    }
}
