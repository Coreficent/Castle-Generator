﻿namespace Coreficent.Module
{
    public class Dirt : ModuleBase
    {
        public Dirt()
        {
            north.Add(Face.Dirt);

            west.Add(Face.Dirt);

            south.Add(Face.Dirt);

            east.Add(Face.Dirt);

            top.Add(Face.Dirt);

            bottom.Add(Face.Dirt);

        }
        public override int Weight => 1024;
    }
}
