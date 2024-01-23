Shader "W3/UnitMeshColor" 
{
	Properties 
	{
		_MainTex ("Particle Texture", 2D) = "white" {}
		_Color1 ("Color1", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "Queue"="Geometry+199" "IGNOREPROJECTOR"="true" "RenderType"="Opaque" }
		
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
			fixed4 _Color1;

			v2f vert( a2v v )
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv0 = v.uv0;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex , i.uv0);
				col.rgb = ( col.rgb + ( 1 - col.a ) * _Color1.rgb );
//				col.rgb = col.rgb * (0.6f, 0.7f, 0.7f);
				col.rgb *= W3ColorDay0.rgb;

				return col;
			}

			ENDCG
		}

		

	}

}

