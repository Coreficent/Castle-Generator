

namespace Coreficent.Generation
{
    using Coreficent.Setting;

    public class Sky : IAnimatable
    {
        private int x = 0;
        private int y = 0;

        private readonly World world;

        public Sky(World world)
        {
            this.world = world;
        }

        public bool HasNext()
        {
            return x != Tuning.Width;
        }

        public void Next()
        {
            if (x < Tuning.Width)
            {
                if (y < Tuning.Height)
                {
                    Superposition superposition = world.Find(x, y, 0);
                    superposition.Collapse(superposition.air);

                    ++y;
                    return;
                }
                else
                {
                    ++x;
                    y = 0;
                    Next();
                }

                return;
            }
        }
    }
}
