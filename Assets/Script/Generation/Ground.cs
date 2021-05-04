namespace Coreficent.Generation
{
    using Coreficent.Setting;

    public class Ground : IAnimatable
    {
        private int x = 1;
        private int y = 1;

        private readonly World world;

        public Ground(World world)
        {
            this.world = world;
        }

        public bool HasNext()
        {
            return x != Tuning.Width - 1;
        }

        public void Next()
        {
            if (x < Tuning.Width - 1)
            {
                if (y < Tuning.Height - 1)
                {
                    SuperpositionX superposition = world.Find(x, y, Tuning.Depth - 1);
                    superposition.Collapse(superposition.dirt);
                    superposition.Immutable = true;
                    ++y;
                }
                else
                {
                    ++x;
                    y = 1;
                    Next();
                }
            }
        }
    }
}
