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

            bottom.Add(Face.TowerCenter);

            weight = 1024;
        }
    }
}
