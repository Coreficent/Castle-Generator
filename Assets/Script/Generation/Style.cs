namespace Coreficent.Generation
{
    using Coreficent.Utility;
    using UnityEngine;

    public class Style : Script
    {
        [SerializeField]
        private Material postProcessing;

        [SerializeField]
        private Material grass;

        [SerializeField]
        private Material leaf;

        [SerializeField]
        private Material roof;

        private readonly string shaderColor = "_Color";
        private Color postProcessingColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);

        public Color PostProcessingColor
        {
            get
            {
                return postProcessingColor;
            }
            set
            {
                postProcessing.SetColor(shaderColor, value);
            }
        }

        protected override void Start()
        {
            base.Start();
        }

        public void Randomize()
        {
            int style = Random.Range(0, 100);

            if (style < 40)
            {
                // winter
                grass.SetColor(shaderColor, new Color(1.0f, 1.0f, 1.0f, 1.0f));
                leaf.SetColor(shaderColor, new Color(1.0f, 1.0f, 1.0f, 1.0f));
                roof.SetColor(shaderColor, new Color(1.0f, 1.0f, 1.0f, 1.0f));
                postProcessingColor = new Color(0.125f, 0.125f, 0.125f, 1.0f);
            }
            else if (style < 80)
            {
                // fall
                grass.SetColor(shaderColor, new Color(1.00f, 0.55f, 0.10f, 1.0f));
                leaf.SetColor(shaderColor, new Color(1.00f, 0.55f, 0.10f, 1.0f));

                postProcessingColor = new Color(1.0f, 1.0f, 0.3f, 1.0f);

            }
            else
            {
                // normal
                grass.SetColor(shaderColor, new Color(0.317f, 0.772f, 0.572f, 1.0f));
                leaf.SetColor(shaderColor, new Color(0.317f, 0.772f, 0.572f, 1.0f));
                postProcessingColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
        }
    }
}
