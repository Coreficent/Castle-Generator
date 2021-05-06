using Coreficent.Setting;
using UnityEngine;

namespace Coreficent.Generation
{
    public class FilterGrass : IFilter
    {
        public bool filtered(Superposition superposition)
        {
            int x = superposition.X;
            int y = superposition.Y;
            int z = superposition.Z;

            if (z == Tuning.Depth - 2)
            {
                if (x > 1 && x < Tuning.Width - 2 && y > 2 && y < Tuning.Height - 3)
                {
                    int castleReserveX = Tuning.Width / 4;

                    if ((x >= Tuning.Width / 2 + castleReserveX || x < Tuning.Width / 2 - castleReserveX))
                    {
                        if (Random.Range(0, 100) < 50)
                        {
                            superposition.Collapse(superposition.grass);
                            superposition.Immutable = true;

                            return true;
                        }
                    }


                    //if ((x > Tuning.Width / 2 + castleReserveX || x < Tuning.Width / 2 - castleReserveX) && (y > Tuning.Height / 2 + castleReserveY || y < Tuning.Height / 2 - castleReserveY))
                    //{
                    //    superposition.Collapse(superposition.grass);
                    //    superposition.Immutable = true;

                    //    return true;
                    //}
                }
            }

            return false;
        }
    }
}
