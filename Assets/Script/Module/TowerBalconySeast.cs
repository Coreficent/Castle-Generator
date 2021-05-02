namespace Coreficent.Module
{
    public class TowerBalconySeast : ModuleBase
    {
        public TowerBalconySeast()
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
