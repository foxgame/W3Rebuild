Shader "W3/SkinnedMeshBaseAlpha"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)

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


			v2f vert(a2v v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

				o.uv0 = v.uv0;
				o.uv1.x = -worldPos.x / W3FogWidth;
				o.uv1.y = -worldPos.z / W3FogHeight;

				return o;
			}

			fixed4 frag( v2f i ) : SV_Target
			{
				fixed4 col = tex2D( _MainTex , i.uv0 );
				col.rgb *= W3FogTex(i.uv1) * W3ColorDay0.rgb;

				return col;
			}

			ENDCG
		}

	}

}
