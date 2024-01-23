Shader "W3/BuildAlphaCutoff"
{
	Properties
	{
		_MainTex("Tex (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent+199" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		LOD 200

		Blend One OneMinusSrcAlpha

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
			};

			sampler2D _MainTex;


			v2f vert(a2v v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv0 = v.uv0;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex , i.uv0);
				col.rgb *= 0.5f;
				col.a = 0.5f;

				return col;
			}

			ENDCG
		}

	}

}
