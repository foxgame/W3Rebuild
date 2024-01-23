using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class W3GameColorManager : SingletonMono<W3GameColorManager>
{
	Dictionary< string , Material > materialDic = new Dictionary< string , Material >();

	public override void initSingletonMono()
	{
	}

    public Material GetMaterialMeshColor( int t , Texture2D t2d )
    {
        string n = t2d != null ? t2d.name : "" + "_" + t.ToString();

        if ( materialDic.ContainsKey( n ) )
        {
            return materialDic[ n ];
        }

        string c = t < 9 ? "0" + t.ToString() : t.ToString();

        string shaderName = "W3/MeshColor";

        Shader shader = Shader.Find( shaderName );

        Material material = new Material( shader );

        Texture2D t2dc = (Texture2D)Resources.Load( "ReplaceableTextures/TeamColor/TeamColor" + c );

        material.SetColor( "_Color1" , t2dc.GetPixel( 1 , 1 ) );

        material.mainTexture = t2d != null ? t2d : t2dc;
        material.name = n;

        materialDic.Add( n , material );

        return material;
    }

    public Material GetMaterialSkinnedMeshColor( int t , Texture2D t2d )
    {
        string n = t2d != null ? t2d.name + "_skinned" : "" + "_skinned_" + t.ToString();

        if ( materialDic.ContainsKey( n ) )
        {
            return materialDic[ n ];
        }

        string c = t < 9 ? "0" + t.ToString() : t.ToString();

        string shaderName = "W3/SkinnedMeshColor";

        Shader shader = Shader.Find( shaderName );

        Material material = new Material( shader );

        Texture2D t2dc = (Texture2D)Resources.Load( "ReplaceableTextures/TeamColor/TeamColor" + c );

        material.SetColor( "_Color1" , t2dc.GetPixel( 1 , 1 ) );

        material.mainTexture = t2d != null ? t2d : t2dc;
        material.name = n;

        materialDic.Add( n , material );

        return material;
    }

    public Material GetMaterialUnitMeshColor( int t , Texture2D t2d )
	{
        string n = t2d != null ? t2d.name + "_unit_" + t.ToString() : "" + "_unit_" + t.ToString();

		if ( materialDic.ContainsKey( n ) )
		{
			return materialDic[ n ];
		}

        string c = t < 9 ? "0" + t.ToString() : t.ToString();

        string shaderName = "W3/UnitMeshColor";

		Shader shader = Shader.Find( shaderName );

		Material material = new Material( shader );

		Texture2D t2dc = (Texture2D)Resources.Load( "ReplaceableTextures/TeamColor/TeamColor" + c );

		material.SetColor( "_Color1" , t2dc.GetPixel( 1 , 1 ) );

		material.mainTexture = t2d != null ? t2d : t2dc;
		material.name = n;

		materialDic.Add( n , material );

		return material;
	}

	public Material GetMaterialTeamGlow( int t )
	{
		string n = t.ToString();

		if ( materialDic.ContainsKey( n ) )
		{
			return materialDic[ n ];
		}

        string c = t < 9 ? "0" + t.ToString() : t.ToString();

        Material material = (Material)Resources.Load( "ReplaceableTextures/TeamGlow/Materials/TeamGlow" + c );
		 
		material.name = t.ToString();

		materialDic.Add( n , material );

		return material;
	}
}

