namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class TowerTopRoof : ModuleBase
    {
        public TowerTopRoof()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.TowerBalconyCenterSouth);

            east.Add(Face.TowerBalconyCenterEast);

            top.Add(Face.Air);

            bottom.Add(Face.TowerTop);
        }

        public override int Weight => 1024;
    }
}
