// In Properties :
_UVRemap ("Sprite UV Rect", Vector) = (0, 0, 1, 1)

// In Global HLSL Scope / UnityPerMaterial CBUFFER :
float4 _UVRemap;

// In Vertex/Fragment Shader :
float2 spriteRectPos = _UVRemap.xy;
float2 spriteRectSize = _UVRemap.zw;
float2 localUV = (IN.uv - spriteRectPos) / spriteRectSize;