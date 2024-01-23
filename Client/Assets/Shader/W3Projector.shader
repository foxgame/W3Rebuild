
Shader "W3/Projector" 
{

	Properties 
	{
		_Color ( "Main Color" , Color ) = (1,1,1,1)
//		_SelectionTex ("SelectionTex", 2D) = "" {}
		_MainTex("ShadowTex", 2D) = "" {}
	}
	
	Subshader 
	{
		Tags { "Queue" = "Geometry-190" }

		ZWrite Off
		Blend Off
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha
		Offset -1, -1

		Pass 
	    {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct v2f 
			{
				float4 pos : SV_POSITION;
				float4 uvShadow : TEXCOORD0;
//				float4 uvFalloff : TEXCOORD1;
			};
			
			float4x4 unity_Projector;
//			float4x4 unity_ProjectorClip;
			
			v2f vert ( float4 vertex : POSITION )
			{
				v2f o;
				o.pos = UnityObjectToClipPos( vertex );
				o.uvShadow = mul( unity_Projector , vertex );
//				o.uvFalloff = mul( unity_Projector , vertex );

				return o;
			}
			
			fixed4 _Color;
//			sampler2D _SelectionTex;
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 texS = tex2Dproj(_MainTex , UNITY_PROJ_COORD( i.uvShadow ) );
				return texS;
			}
			ENDCG
		}
	}
}
