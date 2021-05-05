#ifndef LUMINESCENCE_INCLUDED
#define LUMINESCENCE_INCLUDED

#include "UnityCG.cginc" 
uniform float4 _LightColor0;
// User-specified properties
uniform float4 _Color;
uniform float4 _SpecColor;
uniform float _Shininess;

struct appdata
{
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};

struct v2f
{
	float4 pos : SV_POSITION;
	float4 posWorld : TEXCOORD0;
	float3 normalDirection : TEXCOORD1;
};

v2f vert(appdata v)
{
	v2f output;

	float4x4 modelMatrix = unity_ObjectToWorld;
	float4x4 modelMatrixInverse = unity_WorldToObject;

	output.posWorld = mul(modelMatrix, v.vertex);
	output.normalDirection = normalize(mul(float4(v.normal, 0.0), modelMatrixInverse).xyz);
	output.pos = UnityObjectToClipPos(v.vertex);

	return output;
}

float4 frag(v2f i) : COLOR
{
	float3 normalDirection = normalize(i.normalDirection);
	float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);

	float3 lightDirection;
	float attenuation;

	// determine directional light
	if (0.0 == _WorldSpaceLightPos0.w)
	{
		// no attenuation
		attenuation = 1.0;
		lightDirection = normalize(_WorldSpaceLightPos0.xyz);
	}
	else
	{
		// point or spot light
		float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
		float distance = length(vertexToLightSource);
		// linear attenuation 
		attenuation = 1.0 / distance;
		lightDirection = normalize(vertexToLightSource);
	}

	float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb * max(0.0, dot(normalDirection, lightDirection));

	float3 specularReflection;

	// light source direction
	if (dot(normalDirection, lightDirection) < 0.0)
	{
		// no specular reflection
		specularReflection = float3(0.0, 0.0, 0.0);
	}
	else
	{
		// light source on the right side
		specularReflection = attenuation * _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);
	}

	// no ambient lighting
	return float4(diffuseReflection + specularReflection, 1.0);
}

#endif