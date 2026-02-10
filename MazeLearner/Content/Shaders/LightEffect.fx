#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler TextureSampler : register(s0);

float2 RingCenter;
float Radius;
float Thickness;
float4 Color;
float Softness;

float4 MainShaders(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
    float dist = distance(texCoord, RingCenter);

    float inner = Radius - Thickness;
    float outer = Radius;

    // Ring mask (1 = ring area)
    float ringOuter = 1.0 - smoothstep(outer, outer + Softness, dist);
    float ringInner = smoothstep(inner - Softness, inner, dist);
    float ringMask = ringOuter * ringInner;

    // Invert: outside ring = visible
    float alpha = 1.0 - ringMask;

    if (alpha <= 0) discard;

    return float4(Color.rgb, Color.a * alpha);
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainShaders();
    }
}