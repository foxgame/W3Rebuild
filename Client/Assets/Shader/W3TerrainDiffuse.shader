Shader "W3/TerrainDiffuse" 
{ 

	Properties 
	{
    }

    SubShader 
	{
		Tags { "Queue" = "Geometry-199" "RenderType" = "Opaque" }
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

				float4 uv0 : TEXCOORD0;
				float4 uv1 : TEXCOORD1;
				float4 uv2 : TEXCOORD2;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;

				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
				float2 uv3 : TEXCOORD3;
				float2 uv4 : TEXCOORD4;
				float2 uvFog : TEXCOORD5;

				float2 uvShadow : TEXCOORD6;
			};

			v2f vert( a2v v )
			{
				v2f o;

				o.vertex = UnityObjectToClipPos( v.vertex );

//				float4 worldPos = mul( unity_ObjectToWorld , v.vertex );
//				o.uvShadow.x = ( -worldPos.x + W3ShadowCameraPosX + 0.5 * W3ShadowCameraWidth ) / W3ShadowCameraWidth;
//				o.uvShadow.y = ( -worldPos.z + W3ShadowCameraPosZ + 0.5 * W3ShadowCameraHeight ) / W3ShadowCameraHeight;

				o.uv0 = v.uv0.xy;
				o.uv1 = v.uv1.xy;
				o.uv2 = v.uv1.zw;
				o.uv3 = v.uv2.xy;
				o.uv4 = v.uv2.zw;
				o.uvFog = v.uv0.zw;
				
				return o;
			}

			fixed4 frag( v2f i ) : SV_Target
			{
				fixed4 col = tex2D( W3TerrainTex0 , i.uv0 );
				col.a = 1;

				if ( i.uv1.x > 0 )
				{
					fixed4 c1 = tex2D( W3TerrainTex0 , i.uv1 );
					col.rgb = ( c1.rgb * c1.a ) + ( col.rgb * ( 1 - c1.a ) );
				}

				if ( i.uv2.x > 0 )
				{
					fixed4 c2 = tex2D( W3TerrainTex0 , i.uv2 );
					col.rgb = ( c2.rgb * c2.a ) + ( col.rgb * ( 1 - c2.a ) );
				}

				if ( i.uv3.x > 0 )
				{
					fixed4 c3 = tex2D( W3TerrainTex0 , i.uv3 );
					col.rgb = ( c3.rgb * c3.a ) + ( col.rgb * ( 1 - c3.a ) );
				}

				if ( i.uv4.x > 0 )
				{
					fixed4 c4 = tex2D( W3TerrainTex0 , i.uv4 );
					col.rgb = ( c4.rgb * c4.a ) + ( col.rgb * ( 1 - c4.a ) );
				}

				fixed4 shadowTexture = tex2D( W3ShadowTex1 , i.uvShadow );
				
				col.rgb *= W3ShadowTex( i.uvFog ) * W3FogTex( i.uvFog ) * W3ColorDay0.rgb;
				//col.rgb = shadowTexture;

				

				return col;
			}

			ENDCG
		}

	}
//    FallBack "Mobile/Diffuse"

}
