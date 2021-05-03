#ifndef ILLUMINATION_INCLUDED
#define ILLUMINATION_INCLUDED

#include "AutoLight.cginc"
#include "UnityCG.cginc"

fixed4 _Color;
float _ShadingDarkness;
float _ShadeThreshold;
float _ShadowThreshold;
float4 _MainTex_ST;
sampler2D _MainTex;

struct appdata
{
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
    float4 pos : POSITION;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float3 worldNormal : NORMAL;
    float4 pos : SV_POSITION;
    SHADOW_COORDS(1)
    UNITY_FOG_COORDS(2)
};

v2f vert(appdata v)
{
    v2f o;

    o.pos = UnityObjectToClipPos(v.pos);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.worldNormal = UnityObjectToWorldNormal(v.normal);
    TRANSFER_SHADOW(o);
    UNITY_TRANSFER_FOG(o, o.pos);

    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    // calculate coordinates
    fixed4 col = tex2D(_MainTex, i.uv) * _Color;;

    float fullLighting = 1.0;
    float lightIntensity = fullLighting;

    float shadow = SHADOW_ATTENUATION(i);
    // shadow <= _ShadowThreshold
    float shadowIntensity = lerp(fullLighting, _ShadingDarkness, step(shadow, _ShadowThreshold));

    float lightAmount = dot(_WorldSpaceLightPos0, normalize(i.worldNormal)) * 0.5 + 0.5;
    // lightDirection <= _ShadeThreshold
    float shadeIntensity = lerp(fullLighting, _ShadingDarkness, step(lightAmount, _ShadeThreshold));

    // calculate the light intensity using dot product
    return col * min(shadowIntensity, shadeIntensity);  
}

#endif