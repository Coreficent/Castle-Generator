namespace Coreficent.Module
{
    public class CastleGroundCorner : ModuleBase
    {
        public CastleGroundCorner()
        {
            north.Add(Face.DirtGrass);

            west.Add(Face.DirtGrass);

            south.Add(Face.CastleFloorSide);

            east.Add(Face.CastleFloorSide);

            top.Add(Face.CastleFloorCorner);

            bottom.Add(Face.Dirt);
        }

        public override int Weight => 1024;
    }
}
