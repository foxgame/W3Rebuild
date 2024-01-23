Shader "W3/UnitMeshAlphaColor3" 
{
	Properties 
	{
		_MainTex ("Particle Texture", 2D) = "black" {}
		_SubTex1 ("Particle Texture", 2D) = "black" {}
		_SubTex2 ("Particle Texture", 2D) = "black" {}
	}

	SubShader 
	{ 
		Tags { "Queue"="Geometry" "IgnoreProjector"="true" "RenderType"="Opaque" }
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

				float3 normal : NORMAL;
				float4 color  : COLOR;

				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;

				float3 normal : NORMAL;
				float4 color  : COLOR;

				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
			};

			sampler2D _MainTex;
			sampler2D _SubTex1;
			sampler2D _SubTex2;

			v2f vert( a2v v )
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv0 = v.uv0;
				o.uv1 = v.uv1;
				o.uv2 = v.uv2;
				o.normal = v.normal;
				o.color = v.color;

				return o;
			}

			fixed4 frag( v2f i ) : SV_Target
			{
				fixed4 col = tex2D( _MainTex , i.uv0 );
				col.a = 1;

				fixed4 c1 = tex2D( _SubTex1 , i.uv1 );
				col.rgb = ( c1.rgb * c1.a ) + ( col.rgb * ( 1 - c1.a ) );

				fixed4 c2 = tex2D( _SubTex2 , i.uv2 );
				col.rgb = ( c2.rgb * c2.a ) + ( col.rgb * ( 1 - c2.a ) );

				col.rgb *= W3ColorDay0.rgb;

				return col;
			}

			ENDCG
		}


	}

}

