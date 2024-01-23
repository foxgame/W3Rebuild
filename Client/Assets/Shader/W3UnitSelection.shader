Shader "W3/UnitSelection" 
{

	Properties 
	{
		_Color ( "Main Color" , Color ) = (1,1,1,1)
		_MainTex( "SelectionTex" , 2D ) = "" {}
	}
	
	Subshader 
	{
		Tags { "Queue" = "Geometry-149" "RenderType" = "Transparent" "IgnoreProjector" = "True" }

		ZWrite Off
		Blend Off
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass 
	    {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct a2v
			{
				float4 vertex : POSITION;
				float2 uv0 : TEXCOORD0;
			};

			struct v2f 
			{
				float4 pos : SV_POSITION;
				float2 uv0 : TEXCOORD0;
			};
			

			v2f vert ( a2v v )
			{
				v2f o;
				o.pos = UnityObjectToClipPos( v.vertex );
				o.uv0 = v.uv0;

				return o;
			}
			
			fixed4 _Color;
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D( _MainTex , i.uv0 );
				return col * _Color;
			}
			ENDCG
		}
	}
}
