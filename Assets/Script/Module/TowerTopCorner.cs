namespace Coreficent.Module
{
    public class TowerTopCorner : ModuleBase
    {
        public TowerTopCorner()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.TowerBalconyCenterSouth);

            east.Add(Face.TowerBalconyCenterEast);

            top.Add(Face.Air);

            bottom.Add(Face.TowerTopCorner);
        }

        public override int Weight => 1024;
    }
}
