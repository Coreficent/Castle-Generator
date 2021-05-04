﻿namespace Coreficent.Module
{
    public class WallInvisible : ModuleBase
    {
        public WallInvisible()
        {
            north.Add(Face.WallSpace);

            west.Add(Face.WallInvisible);

            south.Add(Face.WallSpace);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.GrassyTerrain);
        }

        public override int Weight => 1024;
    }
}
