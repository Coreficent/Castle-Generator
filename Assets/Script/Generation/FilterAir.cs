using Coreficent.Setting;

namespace Coreficent.Generation
{
    public class FilterAir : IFilter
    {
        public bool filtered(Superposition superposition)
        {
            int x = superposition.X;
            int y = superposition.Y;
            int z = superposition.Z;

            if (x == 0 || x == Tuning.Width - 1 || y == 0 || y == Tuning.Height - 1 || z == 0 || z == Tuning.Depth - 1)
            {
                superposition.Collapse(superposition.air);
                superposition.Immutable = true;

                return true;
            }

            return false;
        }
    }
}
