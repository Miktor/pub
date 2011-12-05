void main
	(
	float4 position : POSITION,
	float2 uv : TEXCOORD0,
	float3 blendColor : COLOR,
						  
	out float4 outPosition : POSITION,
	out float2 outUv : TEXCOORD0,
	out float3 outBlendColor : COLOR,

	uniform float4x4 worldViewProj,
	uniform float Time
	)
{
	outPosition = mul(worldViewProj, position);
	outUv = uv;
	outBlendColor = blendColor * Time;
}
