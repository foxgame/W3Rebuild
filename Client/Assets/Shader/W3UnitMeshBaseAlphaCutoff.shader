
Shader "W3/UnitMeshBaseAlphaCutoff" 
{
	Properties 
	{
		_MainTex ("Tex (RGB)", 2D) = "white" {}
//		_CutOff("Cutoff Value",Range(0,1.0)) = 0.3
	}

	SubShader 
	{
		Tags { "Queue"="Geometry+199" "RenderType"="TransparentCutout" "IgnoreProjector"="True" }
		LOD 200

		Blend Off
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
			float _CutOff;

			v2f vert(a2v v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos( v.vertex );
				o.uv0 = v.uv0;

				return o;
			}

			fixed4 frag( v2f i ) : SV_Target
			{
				fixed4 col = tex2D( _MainTex , i.uv0 );
				
				if ( col.a < 0.3 )
					discard;

				col.rgb = col.rgb * col.a;
				col.rgb *= W3ColorDay0.rgb;

				return col;
			}

			ENDCG
		}

	}

}
