Shader "Coreficent/Anime"
{
    Properties
    {
        [HideInInspector] _PrecalculatedNormal("Use Custom Normals", Range(0.0, 1.0)) = 0.0
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineDarkness ("Outline Darkness", Range(0.0, 1.0)) = 0.0
        _OutlineThickness ("Outline Thickness", Range(0.0, 1.0)) = 0.5
        _ShadingDarkness ("Shading Darkness", Range(0.0, 1.0)) = 0.5
        _ShadowThreshold ("Shadow Threshold", Range(0.0, 1.0)) = 0.5
        _ShadeThreshold ("Shade Threshold", Range(0.0, 1.0)) = 0.5
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Float) = 10
    }

    SubShader
    {
        // normal shading
        Pass
        {
            Tags
            {
                "RenderType" = "Opaque"
                "Queue" = "Geometry"
                "LightMode" = "ForwardBase"
            }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile_fwdbase

            #include "../HLSL/Illumination.hlsl"

            ENDCG
        }

		// outline shading
		Pass
		{
            Tags
            {
                "RenderType" = "Opaque"
                "Queue" = "Geometry"
                "LightMode" = "ForwardBase"
            }

			Cull front
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "../HLSL/Outline.hlsl"

			ENDCG
		}

        // additional lighting
		Pass
		{
			Tags { "LightMode" = "ForwardAdd" }
			// pass for additional light sources
			Blend One One // additive blending 

			CGPROGRAM

			#pragma vertex vert 
			#pragma fragment frag 

            #include "../HLSL/Luminescence.hlsl"

			ENDCG
		}
    }

    FallBack "Standard"
}
