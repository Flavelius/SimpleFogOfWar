Shader "Hidden/FOWProjectorShader" {
	Properties 
	{
		_MainTex ("Texture", 2D) = "Black" {}
		_Color ("Color", color) = (0,0,0)
		_Blur ("Blur", float) = 0
	}

	Subshader {
		Tags { "RenderType" = "Transparent" "IgnoreProjector" = "True" "Queue" = "Transparent" }
		Pass {
			ZWrite Off
			Blend DstColor Zero
			Offset -1, -1
			Lighting Off
			Fog {Mode Off}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 pos : SV_POSITION;
			};
			

			float4x4 _Projector;
			sampler2D _MainTex;
			half4 _MainTex_TexelSize;
			half3 _Color;
			fixed _Blur;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, vertex);
				o.uvShadow = mul (_Projector, vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 uv = UNITY_PROJ_COORD(i.uvShadow);
				fixed4 c = tex2Dproj (_MainTex, uv);
				if (_Blur > 0)
				{
					fixed spread = _Blur*_MainTex_TexelSize*0.75;
					c += tex2D(_MainTex, uv + half4(spread, spread, spread, spread));
					c += tex2D(_MainTex, uv + half4(-spread, spread, -spread, spread));
					c += tex2D(_MainTex, uv + half4(-spread, -spread, -spread, -spread));
					c += tex2D(_MainTex, uv + half4(spread, -spread, spread, -spread));
					c /= 5;
				}
				c.rgb = saturate(lerp(0, _Color, 1-c.rgb)) + c.rgb;
				c.a = 0;
				return c;
			}
			ENDCG
		}
	}
}
