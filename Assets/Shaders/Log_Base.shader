Shader "Custom/Log_Base" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_Radius ("Radius", Float) = 10.0
		_DiffuseIntensity ("Diffuse Intensity", Range(0, 10.0)) = 1.0
		_RimColor ("Rim Color", Color) = (1, 1, 1, 1)
		_RimIntensity ("Rim Intensity", Range(0, 10.0)) = 1.0
		_RimWidth ("Rim Width", Range(0, 1.0)) = 0.6
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		Pass {
			Tags { "LightMode" = "ForwardBase" }
			
			CGPROGRAM
		
			// Compiler Directives
			#pragma target 3.0
			#pragma vertex VS_MAIN
			#pragma fragment FS_MAIN
			
			// Predefined variables and helper functions (Unity specific)
			#include "UnityCG.cginc"
			
			// User Defined Properties
			uniform fixed4 _Color;
			uniform fixed4 _LightColor0;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _Radius;
			uniform float _DiffuseIntensity;
			uniform fixed4 _RimColor;
			uniform float _RimIntensity;
			uniform float _RimWidth;		
			
			// Input Structs
			struct FS_INPUT {
				float4 pos		: SV_POSITION;
				fixed4 col		: COLOR;
				half2 uv		: TEXCOORD0;
			};
			
			// VERTEX FUNCTION
			FS_INPUT VS_MAIN (appdata_base input) {
				FS_INPUT output;
				
				float pi = 3.14159;
				
				// Transform vertices to world space
				float4 vertex = mul(_Object2World, input.vertex);
				
				// Apply curvature
				float vertY = vertex.y;
				float diameter = _Radius * 2;
				float perimeter = diameter * pi / 2;
				float dist = clamp(vertex.z - _WorldSpaceCameraPos.z, 0, perimeter) / perimeter;
				float theta = radians(180 * dist);
				vertex.z = -cos(theta) * _Radius + _WorldSpaceCameraPos.z + _Radius;
				vertex.y = sin(theta) * _Radius - _Radius;
				float3 O = float3(vertex.x, -_Radius, _WorldSpaceCameraPos.z + _Radius);
				float3 P = vertex;
				float3 OP = normalize(P - O);
				vertex.xyz = OP * vertY + P;
				
				// Rotate normals
				float theta2 = theta - radians(90);
				float4x4 RotationMatrix = float4x4(
					1, 0, 0, 0,
					0, cos(theta2), -sin(theta2), 0,
					0, sin(theta2), cos(theta2), 0,
					0, 0, 0, 1
				);
				float3 normal = mul(RotationMatrix, mul(_Object2World, float4(input.normal, 0.0))).xyz;
				
				// Lighting
				float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - vertex.xyz);
				
				// Diffuse Light
				float3 diffuseLight = _LightColor0 * (_DiffuseIntensity * max(0.0, dot(normal, lightDir)));
				
				// Rim Light
				float rim = smoothstep(1.0 - _RimWidth, 1.0, 1 - max(0.0, dot(normal, viewDir)));
				float3 rimLight = _RimColor * _RimIntensity * float3(rim, rim, rim);
				
				// Final Light
				float3 finalLight = diffuseLight + rimLight;
				
				// Set FS_MAIN input struct
				output.pos = mul(UNITY_MATRIX_MVP, mul (_World2Object, vertex));
				output.col = fixed4(_Color.rgb * finalLight, 1.0);
				output.uv = TRANSFORM_TEX(input.texcoord, _MainTex);
				
				return output;
			}
			
			// FRAGMENT FUNCTION
			fixed4 FS_MAIN (FS_INPUT input) : COLOR {
				return tex2D(_MainTex, input.uv) * input.col;
			}
		
			ENDCG
		}
	} 
	//Fallback "Diffuse"
}