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
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
            "LightMode" = "ForwardBase"
            // new
            // "LightMode" = "ForwardAdd"
	        "PassFlags" = "OnlyDirectional"
        }

        // normal shading
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile_fwdbase
            // new
            // #pragma multi_compile_fwdadd_fullshadows

            #include "../HLSL/Illumination.hlsl"

            ENDCG
        }

        // outline shading
        Pass
        {
            Cull front
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "../HLSL/Outline.hlsl"

            ENDCG
        }
    }

    FallBack "Standard"
}
