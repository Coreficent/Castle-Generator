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
            top.Add(Face.TowerSouth);

            bottom.Add(Face.TowerSouth);
        }

        public override int Weight => 1024;
    }
}
