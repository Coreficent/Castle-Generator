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
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
            "LightMode" = "ForwardBase"
            // new
            // "LightMode" = "ForwardAdd"
	        // "PassFlags" = "OnlyDirectional"
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

		Pass
		{
			Tags { "LightMode" = "ForwardAdd" }
			// pass for additional light sources
			Blend One One // additive blending 

			CGPROGRAM

			#pragma vertex vert 
			#pragma fragment frag 

			#include "UnityCG.cginc" 
			uniform float4 _LightColor0;
			// color of light source (from "Lighting.cginc")

			// User-specified properties
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				float4x4 modelMatrix = unity_ObjectToWorld;
				float4x4 modelMatrixInverse = unity_WorldToObject;

				output.posWorld = mul(modelMatrix, input.vertex);
				output.normalDir = normalize(
				mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
				output.pos = UnityObjectToClipPos(input.vertex);
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				float3 normalDirection = normalize(input.normalDir);

				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - input.posWorld.xyz);
				float3 lightDirection;
				float attenuation;

				if (0.0 == _WorldSpaceLightPos0.w) // directional light?
				{
					attenuation = 1.0; // no attenuation
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}
				else // point or spot light
				{
					float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - input.posWorld.xyz;
					float distance = length(vertexToLightSource);
					attenuation = 1.0 / distance; // linear attenuation 
					lightDirection = normalize(vertexToLightSource);
				}

				float3 diffuseReflection =
				attenuation * _LightColor0.rgb * _Color.rgb
				* max(0.0, dot(normalDirection, lightDirection));

				float3 specularReflection;
				
				if (dot(normalDirection, lightDirection) < 0.0)
				// light source on the wrong side?
				{
					specularReflection = float3(0.0, 0.0, 0.0);
				// no specular reflection
				}
				else // light source on the right side
				{
					specularReflection = attenuation * _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);
				}

				return float4(diffuseReflection
				+ specularReflection, 1.0);
				// no ambient lighting in this pass
			}

			ENDCG
		}





    }

    FallBack "Standard"
}
