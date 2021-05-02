namespace Coreficent.Module
{
    public class TowerBalconySouth : ModuleBase
    {
        public TowerBalconySouth()
        {
            north.Add(Face.TowerBalconyCenterSouth);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.TowerSouth);

            weight = 1024;
        }
    }
}
