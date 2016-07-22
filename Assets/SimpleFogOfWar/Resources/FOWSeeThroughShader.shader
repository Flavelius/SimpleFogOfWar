Shader "Hidden/FOWSeeThroughShader" 
{	Properties
	{
		_MainTex("Texture", 2D) = "Black" {}
		_Color("Color", color) = (0,0,0)
		_Blur("Blur", float) = 0
	}

	SubShader 
	{
		Tags { "RenderType"="Transparent" "IgnoreProjector" = "True" "Queue"="Transparent" }
		Cull Off
		Lighting Off
		Blend DstColor Zero
		ZWrite Off
		ZTest Always
		Fog {Mode Off}

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		half4 _MainTex_TexelSize;
		fixed _Blur;
		half3 _Color;

		struct Input 
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			if (_Blur > 0)
			{
				fixed spread = _Blur*_MainTex_TexelSize*0.75;
				c += tex2D(_MainTex, IN.uv_MainTex + fixed2(spread, spread));
				c += tex2D(_MainTex, IN.uv_MainTex + fixed2(-spread, spread));
				c += tex2D(_MainTex, IN.uv_MainTex + fixed2(-spread, -spread));
				c += tex2D(_MainTex, IN.uv_MainTex + fixed2(spread, -spread));
				c /= 5;
			}
			c.rgb = saturate((1 - c.rgb)*_Color) + (c.rgb * 2);
			o.Albedo = c.rgb;
		}
		
		ENDCG
	}
	
	FallBack "Transparent/Diffuse"
}
