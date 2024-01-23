Shader "W3/MeshBillboard"
{
	Properties
	{
		_MainTex("Tex (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Lighting Off

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "W3Base.cginc"

			struct a2v
			{
				float4 vertex : POSITION;

				float2 uv0 : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;

				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
			};

			sampler2D _MainTex;

			void CalcOrthonormalBasis( float3 dir , out float3 right , out float3 up )
			{
				up = abs(dir.y) > 0.999f ? float3(0, 0, 1) : float3(0, 1, 0);
				right = normalize(cross(up, dir));
				up = cross(dir, right);
			}


			v2f vert(a2v v)
			{
				v2f o;

				o.vertex = mul(UNITY_MATRIX_P,
					mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
					+ float4(v.vertex.x, v.vertex.y, 0.0, 0.0)
					* float4(1.0, 1.0, 1.0, 1.0));

				float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

				o.uv0 = v.uv0;
				o.uv1.x = -worldPos.x / W3FogWidth;
				o.uv1.y = -worldPos.z / W3FogHeight;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex , i.uv0);
				col.a = ( col.r * 0.5f + col.g * 0.5f + col.b * 0.5f );
				col.rgb *= W3FogTex(i.uv1) * W3ColorDay0.rgb;

				return col;
			}

			ENDCG
		}

	}

}
