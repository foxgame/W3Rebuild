using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class W3UVConfig : SingletonMono< W3UVConfig >
{
    public struct W3UV
    {
        public Vector2 uv0;
        public Vector2 uv1;
        public Vector2 uv2;
        public Vector2 uv3;
    }

    public W3UV[] uv16 = new W3UV[ 16 ];
    public int[] uvOffset = new int[ 16 ];
    public bool[] uvOffset512 = new bool[ 16 ];

    public Vector2 getUVOffset( int i , int j )
    {
        int idx = uvOffset[ i ];

        if ( uvOffset512[ i ] && j > 15 )
        {
            idx++;
        }

        int x = idx % 4;
        int y = idx / 4;

        return new Vector2( x * 0.25f , y * 0.25f );
    }

	public override void initSingletonMono()
	{
        for ( int i = 0 ; i < 16 ; i++ )
        {
            uv16[ i ] = new W3UV();

            uv16[ i ].uv0 = new Vector2();
            uv16[ i ].uv1 = new Vector2();
            uv16[ i ].uv2 = new Vector2();
            uv16[ i ].uv3 = new Vector2();

            float u = i / 4 * 0.25f;
            float v = 0.75f - i % 4 * 0.25f;

            uv16[ i ].uv0.x = u + 0.25f;
            uv16[ i ].uv0.y = v;
            uv16[ i ].uv1.x = u;
            uv16[ i ].uv1.y = v;
            uv16[ i ].uv2.x = u + 0.25f;
            uv16[ i ].uv2.y = v + 0.25f;
            uv16[ i ].uv3.x = u;
            uv16[ i ].uv3.y = v + 0.25f;
        }


    }



}

