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
                    if (z < Tuning.Depth)
                    {
                        if (x == 0 || x == Tuning.Width - 1 || y == 0 || y == Tuning.Height - 1 || z == 0 || z == Tuning.Depth - 1)
                        {
                            Superposition superposition = world.Find(x, y, z);
                            superposition.Collapse(superposition.air);
                            superposition.Immutable = true;
                            ++z;
                        }
                        else
                        {
                            ++z;
                            Next();
                        }
                    }
                    else
                    {
                        ++y;
                        z = 0;
                        Next();
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
