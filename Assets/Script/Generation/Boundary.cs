namespace Coreficent.Generation
{
    using Coreficent.Setting;

    public class Boundary : IAnimatable
    {
        private int x = 0;
        private int y = 0;
        private int z = 0;

        private readonly World world;

        public Boundary(World world)
        {
            this.world = world;
        }

        public bool HasNext()
        {
            return x < Tuning.Width;
        }

        public void Next()
        {
            if (x < Tuning.Width)
            {
                if (y < Tuning.Height)
                {
                    if (z < Tuning.Height)
                    {
                        Superposition superposition = world.Find(x, y, z);
                        superposition.Collapse(superposition.air);
                        ++z;
                        return;
                    }
                    else
                    {
                        ++y;
                        z = 0;
                        Next();
                        return;
                    }
                }
                else
                {
                    ++x;
                    y = 0;
                    z = 0;
                    Next();
                }
            }
        }
    }
}
