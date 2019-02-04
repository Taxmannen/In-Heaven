/* https://www.ronja-tutorials.com/2018/10/20/single-step-toon.html */

Shader "Custom/Cel Shader Final" {
	Properties {
		[Header(Standard)]
		_Color("Tint", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}
		[HDR] _Emission("Emission", color) = (0 ,0 ,0 , 1)

		[Header(Lighting)]
		_ShadowTint("Shadow Color", Color) = (0.5, 0.5, 0.5, 1)

		[Header(Outline)]
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
		_OutlineExtrusion("Outline Extrusion", float) = 0.04
	}

	SubShader {
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }

		//Write to Stencil buffer (so that outline pass can read)
		Stencil
		{
			Ref 4
			Comp always
			Pass replace
			ZFail keep
		}

		CGPROGRAM

		#pragma surface surf Stepped fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		half3 _Emission;

		float3 _ShadowTint;

		float4 LightingStepped(SurfaceOutput s, float3 lightDir, half3 viewDir, float shadowAttenuation) {
			float towardsLight = dot(s.Normal, lightDir);
			float towardsLightChange = fwidth(towardsLight);
			float lightIntensity = smoothstep(0, towardsLightChange, towardsLight);

			#ifdef USING_DIRECTIONAL_LIGHT
				float attenuationChange = fwidth(shadowAttenuation) * 0.5;
				float shadow = smoothstep(0.5 - attenuationChange, 0.5 + attenuationChange, shadowAttenuation);
			#else
				float attenuationChange = fwidth(shadowAttenuation);
				float shadow = smoothstep(0, attenuationChange, shadowAttenuation);
			#endif
			lightIntensity = lightIntensity * shadow;

			float3 shadowColor = s.Albedo * _ShadowTint;
			float4 color;
			color.rgb = lerp(shadowColor, s.Albedo, lightIntensity) * _LightColor0.rgb;
			color.a = s.Alpha;
			return color;
		}


		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input i, inout SurfaceOutput o) {
			fixed4 col = tex2D(_MainTex, i.uv_MainTex);
			col *= _Color;
			o.Albedo = col.rgb;
			o.Emission = _Emission;
		}
		ENDCG

		//Outline 
		Pass {
			Cull OFF
			ZWrite OFF
			ZTest ON
			Stencil
			{
				Ref 4
				Comp notequal
				Fail keep
				Pass replace
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			uniform float4 _OutlineColor;
			uniform float _OutlineSize;
			uniform float _OutlineExtrusion;
			uniform float _OutlineDot;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 color : COLOR;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				float4 newPos = input.vertex;
				float3 normal = normalize(input.normal);
				newPos += float4(normal, 0.0) * _OutlineExtrusion;

				output.pos = UnityObjectToClipPos(newPos);
				output.color = _OutlineColor;

				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				return input.color;
			}

			ENDCG
		}
	}
	FallBack "Standard"
}