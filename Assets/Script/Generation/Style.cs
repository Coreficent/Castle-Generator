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

            if (style < 25)
            {
                // winter
                grass.SetColor(shaderColor, new Color(1.0f, 1.0f, 1.0f, 1.0f));
                leaf.SetColor(shaderColor, new Color(1.0f, 1.0f, 1.0f, 1.0f));
                roof.SetColor(shaderColor, new Color(1.0f, 1.0f, 1.0f, 1.0f));
                postProcessingColor = new Color(0.52f, 0.52f, 0.51f, 1.0f);
            }
            else if (style < 50)
            {
                // fall
                grass.SetColor(shaderColor, new Color(0.90f, 0.45f, 0.00f, 0.1f));
                leaf.SetColor(shaderColor, new Color(0.90f, 0.30f, 0.00f, 1.0f));
                roof.SetColor(shaderColor, new Color(0.10f, 0.40f, 1.00f, 1.0f));
                postProcessingColor = new Color(0.50f, 0.50f, 0.50f, 1.0f);
            }
            else if (style < 75)
            {
                // night
                grass.SetColor(shaderColor, new Color(0.317f, 0.772f, 0.572f, 1.0f));
                leaf.SetColor(shaderColor, new Color(0.317f, 0.772f, 0.572f, 1.0f));
                roof.SetColor(shaderColor, new Color(0.10f, 0.40f, 1.00f, 1.0f));
                postProcessingColor = new Color(0.00f, 0.30f, 0.90f, 1.0f);
            }
            else
            {
                // normal
                grass.SetColor(shaderColor, new Color(0.317f, 0.772f, 0.572f, 1.0f));
                leaf.SetColor(shaderColor, new Color(0.317f, 0.772f, 0.572f, 1.0f));
                roof.SetColor(shaderColor, new Color(0.10f, 0.40f, 1.00f, 1.0f));
                postProcessingColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
        }
    }
}
