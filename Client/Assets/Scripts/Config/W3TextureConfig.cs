using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class W3TextureConfig : SingletonMono< W3TextureConfig >
{
	Dictionary< string , W3Texture > textures = new Dictionary< string , W3Texture >();
    Dictionary< string , Sprite > sprites = new Dictionary< string , Sprite >();

    public void clear()
	{
		
	}

    public Sprite getSprite( string name )
    {
        if ( sprites.ContainsKey( name ) )
        {
            return sprites[ name ];
        }

        return createSprite( name );
    }

    public W3Texture getTexture( string name )
	{
		if ( textures.ContainsKey( name ) )
		{
			return textures[ name ];
		}

		return createTexture( name );
	}

    public void releaseTexture( string name )
    {
        if ( textures.ContainsKey( name ) )
        {
            Resources.UnloadAsset( textures[ name ].texture2D );

            textures.Remove( name );
        }
    }


    Sprite createSprite( string name )
    {
        Sprite s = Resources.Load<Sprite>( name );

        sprites.Add( name , s );

        return s;
    }

    W3Texture createTexture( string name )
	{
		W3Texture t = new W3Texture();
		t.load( name );

		textures.Add( name , t );

		return t;
	}
}

