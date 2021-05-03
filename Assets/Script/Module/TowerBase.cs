namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class TowerBase : ModuleBase
    {
        public TowerBase()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.TowerBalconyCenterSouth);

            east.Add(Face.TowerBalconyCenterEast);

            top.Add(Face.TowerTop);

            bottom.Add(Face.TowerBalconyBase);

            
        }

        public override int Weight => 1024;
    }
}
