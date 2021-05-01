namespace Coreficent.Module
{
    public class ModuleTemplate : ModuleBase
    {
        public ModuleTemplate()
        {
            north.Add(Face.Air);

            west.Add(Face.Air);

            south.Add(Face.Air);

            east.Add(Face.Air);

            top.Add(Face.Air);

            bottom.Add(Face.Air);
        }
    }
}
