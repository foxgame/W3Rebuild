using System;
using UnityEngine;

public class W3Material
{
	public Material material;

	public W3Material()
	{
		
	}

	public void initMaterialTerrain( string name , string sub1 = null , string sub2 = null , string sub3 = null )
	{
		if ( !material )
		{
			string shaderName = "W3/TerrainDiffuse";
			Shader shader = Shader.Find( shaderName );

			material = new Material( shader );
			material.name = name;

			W3Texture texture = W3TextureConfig.instance.getTexture( name );
			material.mainTexture = texture.texture2D;

			if ( sub1 != null )
			{
				W3Texture texture1 = W3TextureConfig.instance.getTexture( sub1 );
				material.SetTexture( "_SubTex1" , texture1.texture2D );
			}

			if ( sub2 != null )
			{
				W3Texture texture2 = W3TextureConfig.instance.getTexture( sub2 );
				material.SetTexture( "_SubTex2" , texture2.texture2D );
			}

			if ( sub3 != null )
			{
				W3Texture texture3 = W3TextureConfig.instance.getTexture( sub3 );
				material.SetTexture( "_SubTex3" , texture3.texture2D );
			}
		}
	}
}


