namespace Coreficent.Generation
{
    using Coreficent.Utility;
    using UnityEngine;

    public class Style : Script
    {
        [SerializeField]
        private Material grass;


        private readonly string shaderColor = "_Color";

        protected override void Start()
        {
            base.Start();
        }

        public void Randomize()
        {
            int style = Random.Range(0, 100);

            if (style < 50)
            {
                // winter
                grass.SetColor(shaderColor, new Color(1.0f, 1.0f, 1.0f, 1.0f));

            }
            else
            {
                // normal
                grass.SetColor(shaderColor, new Color(0.317f, 0.772f, 0.572f, 1.0f));
            }

            Test.Debug("run");
        }
    }
}

