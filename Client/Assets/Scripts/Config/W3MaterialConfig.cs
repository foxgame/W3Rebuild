using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class W3MaterialConfig : SingletonMono< W3MaterialConfig >
{
	Dictionary< string , W3Material > terrainMaterials = new Dictionary< string , W3Material >();
	Dictionary< string , Material > materials = new Dictionary< string , Material >();

	public void clear()
	{

	}

    public Material getMaterial( string name )
    {
        string n = name;

        if ( materials.ContainsKey( n ) )
        {
            return materials[ n ];
        }

        return (Material)Resources.Load( name );
    }

    public Material getMaterial( string name , string tname , string shader = "" )
	{
		string n = name + tname + shader;

		if ( materials.ContainsKey( n ) )
		{
			return materials[ n ];
		}

		return createMaterial( name , tname , shader );
	}

	public Material createMaterial( string name , string tname , string shader = "" )
	{
        Material material = null;

        if ( shader.Length > 1 )
        {
            Shader shader1 = Shader.Find( shader );

            material = new Material( shader1 );
            material.name = name;
        }
        else
        {
            material = (Material)Resources.Load( name );
        }

        Texture2D t2d = (Texture2D)Resources.Load( tname );

        if ( t2d != null )
            material.mainTexture = t2d;

		materials.Add( name + tname + shader , material );

		return material;
	}

	public W3Material getTerrainMaterial( string name , string sub1 = null , string sub2 = null , string sub3 = null , string sub4 = null )
	{
		string n = name;

		if ( sub1 != null )
		{
			n += sub1;
		}
		if ( sub2 != null )
		{
			n += sub2;
		}
		if ( sub3 != null )
		{
			n += sub3;
		}
		if ( sub4 != null )
		{
			n += sub4;
		}

		if ( terrainMaterials.ContainsKey( n ) )
		{
			return terrainMaterials[ n ];
		}

		return createTerrainMaterial( name , sub1 , sub2 , sub3 , sub4 );
	}


	W3Material createTerrainMaterial( string name , string sub1 = null , string sub2 = null , string sub3 = null , string sub4 = null )
	{
		W3Material material = new W3Material();

		material.initMaterialTerrain( name , sub1 , sub2 , sub3 );

		string n = name;

		if ( sub1 != null )
		{
			n += sub1;
		}
		if ( sub2 != null )
		{
			n += sub2;
		}
		if ( sub3 != null )
		{
			n += sub3;
		}
		if ( sub4 != null )
		{
			n += sub4;
		}

		terrainMaterials.Add( n , material );

		return material;
	}





}

