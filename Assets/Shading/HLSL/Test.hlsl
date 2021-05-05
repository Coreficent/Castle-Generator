#ifndef OUTLINE_INCLUDED
#define OUTLINE_INCLUDED

#include "UnityCG.cginc"

float _OutlineDarkness;
float _OutlineThickness;
float _PrecalculatedNormal;
float4 _MainTex_ST;
sampler2D _MainTex;

struct appdata
{
    float2 uv : TEXCOORD0;
    float3 vertexNormal : NORMAL;
    float3 customNormal : TEXCOORD3;
    float4 vertex : POSITION;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 pos : SV_POSITION;
};

v2f vert(appdata v)
{
    v2f o;
    
    float3 normal = lerp(v.vertexNormal, v.customNormal, _PrecalculatedNormal);
    o.pos = UnityObjectToClipPos(normalize(normal) * _OutlineThickness + v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    return o;
}

fixed4 frag(v2f i) : SV_TARGET
{
    fixed4 col = tex2D(_MainTex, i.uv);

    UNITY_APPLY_FOG(i.fogCoord, col);

    return col * _OutlineDarkness;
}

#endif