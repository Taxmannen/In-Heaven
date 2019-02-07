Shader "Custom/Cel Shader V2" 
{

	Properties 
	{
		[Header(Standard)]
		_MainTex("Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1, 1, 1, 1)
		[HDR] _Emission("Emission", color) = (0 ,0 ,0 , 1)

		[Header(Lighting)]
		_ShadowTint("Shadow Color", Color) = (0.5, 0.5, 0.5, 1)
		[IntRange]_StepAmount("Shadow Steps", Range(1, 16)) = 2
		_StepWidth("Step Size", Range(0, 1)) = 0.25
		
		[Header(Specular)]
		_Specular("Specular Color", Color) = (1,1,1,1)
		_SpecularSize("Specular Size", Range(0, 1)) = 0
		_SpecularFalloff("Specular Falloff", Range(0, 2)) = 1

		[Header(Outline)]
		[Toggle(SHOW_OUTLINE)]
		_ShowOutline("Show Outline", Float) = 0
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
		_OutlineExtrusion("Outline Extrusion", float) = 0.01
	}

	SubShader 
	{
		/*
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 200
		Blend srcAlpha OneMinusSrcAlpha
		ZWrite off
		*/

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

		#pragma surface surf Stepped fullforwardshadows //alpha:fade
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		half3 _Emission;
		fixed4 _Specular;

		float3 _ShadowTint;
		float _StepWidth;
		float _StepAmount;
		float _SpecularSize;
		float _SpecularFalloff;

		struct ToonSurfaceOutput
		{
			fixed3 Albedo;
			half3 Emission;
			fixed3 Specular;
			fixed Alpha;
			fixed3 Normal;
		};

		float4 LightingStepped(ToonSurfaceOutput s, float3 lightDir, half3 viewDir, float shadowAttenuation) 
		{
			float towardsLight = dot(s.Normal, lightDir);
			towardsLight = towardsLight / _StepWidth;

			float lightIntensity = floor(towardsLight);
			float change = fwidth(towardsLight);
			float smoothing = smoothstep(0, change, frac(towardsLight));
			lightIntensity = lightIntensity + smoothing;
			lightIntensity = lightIntensity / _StepAmount;
			lightIntensity = saturate(lightIntensity);

			#ifdef USING_DIRECTIONAL_LIGHT
				float attenuationChange = fwidth(shadowAttenuation) * 0.5;
				float shadow = smoothstep(0.5 - attenuationChange, 0.5 + attenuationChange, shadowAttenuation);
			#else
				float attenuationChange = fwidth(shadowAttenuation);
				float shadow = smoothstep(0, attenuationChange, shadowAttenuation);
			#endif
			lightIntensity = lightIntensity * shadow;

			float3 reflectionDirection = reflect(lightDir, s.Normal);
			float towardsReflection = dot(viewDir, -reflectionDirection);

			float specularFalloff = dot(viewDir, s.Normal);
			specularFalloff = pow(specularFalloff, _SpecularFalloff);
			towardsReflection = towardsReflection * specularFalloff;

			float specularChange = fwidth(towardsReflection);
			float specularIntensity = smoothstep(1 - _SpecularSize, 1 - _SpecularSize + specularChange, towardsReflection);
			specularIntensity = specularIntensity * shadow;

			float4 color;
			color.rgb = s.Albedo * lightIntensity * _LightColor0.rgb;
			color.rgb = lerp(color.rgb, s.Specular * _LightColor0.rgb, saturate(specularIntensity));

			color.a = s.Alpha;
			return color;
		}

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input i, inout ToonSurfaceOutput o) 
		{
			fixed4 col = tex2D(_MainTex, i.uv_MainTex);
			col *= _Color;
			o.Alpha = _Color.a;
			o.Albedo = col.rgb;
			o.Specular = _Specular;

			float3 shadowColor = col.rgb * _ShadowTint;
			o.Emission = _Emission + shadowColor;
		}
		ENDCG

		//Outline 
		Pass 
		{
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
			#pragma shader_feature SHOW_OUTLINE

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
				
				#if SHOW_OUTLINE
					newPos += float4(normal, 0.0) * _OutlineExtrusion;
				#endif

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