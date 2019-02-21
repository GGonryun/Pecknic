Shader "Custom/HeightMapTint"
{
	Properties
	{
	  _MainTex("Base (RGB)", 2D) = "white" {}
	  _HeightMin("Height Min", Float) = -2
	  _HeightMid("Height Mid", Float) = 7
	  _HeightMax("Height Max", Float) = 10
	  _ColorMin("Tint Color At Min", Color) = (11,102,35,1)
	  _ColorMid("Tint Color At Mid", Color) = (112,130,56,1)
	  _ColorMax("Tint Color At Max", Color) = (169,186,157,1)
	}

		SubShader
	  {
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _ColorMin;
		fixed4 _ColorMid;
		fixed4 _ColorMax;
		float _HeightMin;
		float _HeightMid;
		float _HeightMax;

		struct Input
		{
		  float2 uv_MainTex;
		  float3 worldPos;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
		  half4 c = tex2D(_MainTex, IN.uv_MainTex);
		  float h = (_HeightMax - IN.worldPos.y) / (_HeightMax - _HeightMin);
		  fixed4 tintColor = lerp(_ColorMax.rgba, _ColorMin.rgba, h);
		  o.Albedo = c.rgb * tintColor.rgb;
		  o.Alpha = c.a * tintColor.a;
		}
		ENDCG
	  }
		  Fallback "Diffuse"
}