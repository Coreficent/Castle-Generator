namespace Coreficent.Generation
{
    public class Border : IAnimatable
    {
        private int x = 0;
        private int y = 0;
        private int z = 0;

        private World world;

        public Border(World world)
        {
            this.world = world;
        }

        public bool HasNext()
        {
            return x != world.Width;
        }

        public void Next()
        {
            if (x < world.Width)
            {
                if (y < world.Height)
                {
                    if (z < world.Depth)
                    {
                        if (x == 0 || y == 0 || x == world.Width - 1 || y == world.Height - 1)
                        {
                            Superposition superposition = world.Find(x, y, z);
                            superposition.Collapse(superposition.border);
                            ++z;
                        }
                        else
                        {
                            ++z;
                            Next();
                        }

                        return;
                    }
                    ++y;
                    z = 0;
                    return;
                }
                ++x;
                y = 0;
                z = 0;

                return;
            }
        }
    }
}
