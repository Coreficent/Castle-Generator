namespace Coreficent.Module
{
    public class TowerBalconyCenter : ModuleBase
    {
        public TowerBalconyCenter()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.Tower);

            weight = 1024;
        }
    }
}
