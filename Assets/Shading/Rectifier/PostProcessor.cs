namespace Coreficent.Shading
{
    using Coreficent.Utility;
    using UnityEngine;

    public class PostProcessor : Script
    {
        [SerializeField] private Material material;

        protected void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, material);
        }
    }
}
