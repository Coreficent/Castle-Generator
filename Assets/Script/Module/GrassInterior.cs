namespace Coreficent.Module
{
    public class GrassInterior : ModuleBase
    {
        public GrassInterior()
        {
            north.Add(Face.DirtGrass);

            west.Add(Face.DirtGrass);

            south.Add(Face.DirtGrass);

            east.Add(Face.DirtGrass);

            top.Add(Face.Air);
            top.Add(Face.GrassyTerrain);

            bottom.Add(Face.Dirt);

        }
        public override int Weight => 1024;
    }
}
