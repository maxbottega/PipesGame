Shader "ReaShader/UberShader_Opaque" {
	Properties{
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_EmStr ("Emission Intensity", float) = 0
	_EColor ("Emission Color", Color) = (1,1,1,1)
	_Mask ("SplatMask(R)emission(G)Reflection", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
//	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	_RefStr ("Reflection strenght", float) = 0
	_Frn ("Reflection Fresnel", float) = 0
	_FrnTw ("Reflection Fresnel Tweak", float) = 0
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
	_EmissionLM ("Emission (Lightmapper)", float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 400
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma exclude_renderers d3d11_9x
		#pragma target 3.0
//		#pragma debug
		
			sampler2D _MainTex;
			sampler2D _BumpMap;
			sampler2D _Mask;
			samplerCUBE _Cube;
			fixed4 _Color;
//			fixed4 _ReflectColor;
			fixed4 _EColor;
			fixed _EmStr;
			fixed _RefStr;
			fixed _Frn;
			fixed _FrnTw;
			
			struct Input {
				fixed2 uv_MainTex;
//				fixed2 uv_Mask;
//				fixed2 uv_BumpMap;
				fixed3 worldRefl;
				fixed3 viewDir;
				INTERNAL_DATA	
			};
			
			void surf (Input IN, inout SurfaceOutput o) {
				fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
				fixed3 m = tex2D(_Mask, IN.uv_MainTex);
				fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
				
				o.Normal = n;
				fixed3 rim = 1-saturate(dot(normalize(IN.viewDir),o.Normal));
				rim = pow(rim,_Frn) * _FrnTw;
				
				fixed3 worldRefl = WorldReflectionVector(IN, o.Normal);
				fixed3 reflcol = texCUBE (_Cube, worldRefl);
					reflcol = (reflcol  * _RefStr * m.g)*rim;


				o.Albedo = tex.rgb * _Color;
				o.Emission = (tex.rgb * _EmStr * _EColor * m.r) + reflcol;

			}
			ENDCG
		}
	FallBack "Self-Illumin/Specular"
}
