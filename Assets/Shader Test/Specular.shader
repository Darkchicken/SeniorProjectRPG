﻿Shader ".ShaderTalk/Specular"
{
	Properties
	{
		_Color ("Main Color", Color) = (1, 1, 1, 1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_SpecColor ("Specular Color", Color) = (1, 1, 1, 1)
		_SpecShininess ("Specular Shininess", Range(1.0, 100.0)) = 2.0
	}
	Subshader 
	{
		Pass
		{
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
			};

			float4 _LightColor0;

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			float4 _SpecColor;
			float _SpecShininess;

			v2f vert (appdata IN)
			{
				v2f OUT;
				OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.posWorld = mul(_Object2World, IN.vertex);
				OUT.normal = mul(float4(IN.normal, 0.0), _Object2World).xyz;
				OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);
				return OUT;
			}

			fixed4 frag(v2f IN) : COLOR
			{
				fixed texColor = tex2D(_MainTex, IN.texcoord);
				//return texColor;
				float3 normalDirection = normalize(IN.normal);
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float3 viewDirection = normalize(_WorldSpaceCameraPos - IN.posWorld.xyz);
				float3 diffuse = _LightColor0.rgb * _Color.rgb * max(0.0, dot(normalDirection, lightDirection));

				float3 specular;
				if(dot(normalDirection, lightDirection) < 0.0)
				{
					specular = float3(0.0, 0.0, 0.0);
				}
				else
				{
					specular = _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _SpecShininess);
				}
				return _Color * texColor * float4(diffuse, 1 );
				//return _Color;
			}
			ENDCG
		}
	}
}
