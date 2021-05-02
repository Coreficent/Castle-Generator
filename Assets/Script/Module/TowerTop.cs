namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class TowerTop : ModuleBase
    {
        public TowerTop()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.TowerBalconyCenterSouth);

            east.Add(Face.TowerBalconyCenterEast);

            top.Add(Face.Air);

            bottom.Add(Face.TowerCenter);
        }

        public override int Weight => 1024;
    }
}
