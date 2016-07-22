Shader "Hidden/FOWViewStampShader" 
{
	Properties
	{
		_MainTex("Particle Texture", 2D) = "white" {}
	}

		CGINCLUDE

#include "UnityCG.cginc"

	sampler2D _MainTex;
	float4 _MainTex_ST;
	fixed4 _TintColor;

	struct appdata_t
	{
		float4 position : POSITION;
		float4 texcoord : TEXCOORD0;
		fixed4 color : COLOR;
	};

	struct v2f
	{
		float4 position : SV_POSITION;
		float2 texcoord : TEXCOORD0;
		fixed4 color : COLOR;
	};

	v2f vert(appdata_t v)
	{
		v2f o;
		o.position = mul(UNITY_MATRIX_MVP, v.position);
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.color = v.color;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col = 2 * tex2D(_MainTex, i.texcoord);
		if (col.a < 0.5) discard;
		return col;
	}

		ENDCG

		SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

			Blend One One
			Cull Off Lighting Off ZWrite Off Fog{ Mode Off }

			Pass
		{
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
			ENDCG
		}
	}
}
