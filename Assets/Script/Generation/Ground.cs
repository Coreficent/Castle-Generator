namespace Coreficent.Generation
{
    using Coreficent.Setting;

    public class Ground : IAnimatable
    {
        private int x = 0;
        private int y = 0;

        private readonly World world;

        public Ground(World world)
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
                    Superposition superposition = world.Find(x, y, Tuning.Depth - 1);
                    superposition.Collapse(superposition.dirt);
                    ++y;
                }
                else
                {
                    ++x;
                    y = 0;
                    Next();
                }
            }
        }
    }
}
