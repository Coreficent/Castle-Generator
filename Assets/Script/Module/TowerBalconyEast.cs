namespace Coreficent.Module
{
    public class TowerBalconyEast : ModuleBase
    {
        public TowerBalconyEast()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.Air);

            weight = 1024;
        }
    }
}
