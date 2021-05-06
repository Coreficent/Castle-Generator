using Coreficent.Setting;

namespace Coreficent.Generation
{
    public class FilterDirt : IFilter
    {
        public bool filtered(Superposition superposition)
        {
            if (superposition.Z == Tuning.Depth - 1)
            {
                superposition.Collapse(superposition.dirt);
                superposition.Immutable = true;

                return true;
            }

            return false;
        }
    }
}
