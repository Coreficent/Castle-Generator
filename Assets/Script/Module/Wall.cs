namespace Coreficent.Module
{
    using Coreficent.Utility;
    public class Wall : ModuleBase
    {
        public Wall()
        {
            north.Add(Face.WallNorth);
            north.Add(Face.WallWest);

            west.Add(Face.WallInvisible);

            south.Add(Face.WallNorth);
            south.Add(Face.WallWest);

            east.Add(Face.Air);

            top.Add(Face.WallTop);
            top.Add(Face.Air);

            bottom.Add(Face.CastleFloorInterior);
        }

        public override int Weight => 1024;
    }
}
