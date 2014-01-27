Shader "ReaShader/FresnelBumpedSpec" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_SpecTex ("Specular Map", 2D) = "white" {}
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower ("Fresnel", float) = 3.0
		_RimStr("Rim Intensity", float) = 3.0
	}

	SubShader 
	{
		Tags { "RenderType"="Geometry" }
		LOD 300

		CGPROGRAM
		#pragma surface surf BlinnPhong

		sampler2D _MainTex;
		sampler2D _SpecTex;
		sampler2D _BumpMap;
		fixed4 _Color;
		fixed4 _RimColor;
        fixed _RimPower;
        fixed _RimStr;
		fixed _Shininess;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_SpecTex;
			float2 uv_BumpMap;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 Specs = tex2D(_SpecTex, IN.uv_SpecTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Gloss = Specs.rgb;
			o.Specular = _Shininess;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			o.Emission =(pow (rim, _RimPower) * _RimStr) *  _RimColor.rgb;
		}
		ENDCG  
	}

	FallBack "Diffuse"
}
