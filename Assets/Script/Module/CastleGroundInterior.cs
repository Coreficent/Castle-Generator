namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class CastleGroundInterior : ModuleBase
    {
        public CastleGroundInterior()
        {
            north.Add(Face.CastleFloorSide);

            west.Add(Face.CastleFloorSide);

            south.Add(Face.CastleFloorSide);

            east.Add(Face.CastleFloorSide);

            top.Add(Face.Air);
            top.Add(Face.CastleFloorInterior);

            bottom.Add(Face.Dirt);
        }

        public override int Weight => 1024;
    }
}
