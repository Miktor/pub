void main
	(
	float2 uv : TEXCOORD,
	float3 blendColor : COLOR,
	uniform sampler2D texture1 : register(s0),
	uniform sampler2D texture2 : register(s1),
	uniform sampler2D texture3 : register(s2),
						  
	out float4 outColor : COLOR
	)
{
	outColor = 
		float4
			(
			blendColor.r * tex2D(texture1, uv).rgb +
			blendColor.g * tex2D(texture2, uv).rgb +
			blendColor.b * tex2D(texture3, uv).rgb,
			1
			);
}

