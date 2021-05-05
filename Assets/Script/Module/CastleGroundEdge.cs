namespace Coreficent.Module
{
    public class CastleGroundEdge : ModuleBase
    {
        public CastleGroundEdge()
        {
            north.Add(Face.DirtGrass);

            west.Add(Face.CastleFloorSide);

            south.Add(Face.CastleFloorSide);

            east.Add(Face.CastleFloorSide);

            top.Add(Face.CastleFloorEdge);

            bottom.Add(Face.Dirt);
        }

        public override int Weight => 1024;
    }
}
