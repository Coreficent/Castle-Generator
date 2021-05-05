#ifndef ILLUMINATION_INCLUDED
#define ILLUMINATION_INCLUDED

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
#endif