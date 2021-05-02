namespace Coreficent.Module
{
    public class Air : ModuleBase
    {
        public Air()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.Air);
        }

        public override int Weight => 1024;
    }
}
