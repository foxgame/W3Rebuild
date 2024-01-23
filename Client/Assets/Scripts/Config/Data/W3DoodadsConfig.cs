using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[ System.Serializable ]
public class W3DoodadsConfigData 
{
	public string doodID;
	public string dir;
	public string file;

	public byte numVar;
    public byte shadow;
    public byte showInFog;

    public string pathTex;

}


public class W3DoodadsConfig : SingletonMono< W3DoodadsConfig >
{
	Dictionary< string , W3DoodadsConfigData > data = new Dictionary< string , W3DoodadsConfigData >();
    Dictionary< int , W3DoodadsConfigData > data1 = new Dictionary< int , W3DoodadsConfigData >();

    public List< W3DoodadsConfigData > list;


	public W3DoodadsConfigData getData( string id )
	{
		if ( data.ContainsKey( id ) )
		{
			return data[ id ];
		}

		return null;
	}

    public W3DoodadsConfigData getData( int id )
    {
        if ( data1.ContainsKey( id ) )
        {
            return data1[ id ];
        }

        return null;
    }

    public override void initSingletonMono()
	{
		for ( int i = 0; i < list.Count ; i++ ) 
		{
			data.Add( list[ i ].doodID , list[ i ] );
            data1.Add( GameDefine.UnitId( list[ i ].doodID ) , list[ i ] );
        }

        list.Clear();
		list = null;
	}

	public void initConfig()
	{

	}

	public void clearConfig()
	{
		list.Clear();
	}

	#if UNITY_EDITOR

	public void load( byte[] bytes )
	{
		string text = UTF8Encoding.Default.GetString( bytes );

		list = new List< W3DoodadsConfigData >();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
		{
			W3DoodadsConfigData d = new W3DoodadsConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12 )
			{
				continue;
			}

			d.doodID = array[ 0 ];
//			d.cliffModelDir = array[ 1 ];
//			d.rampModelDir = array[ 2 ];
//			d.texDir = array[ 3 ];
//			d.dir = array[ 4 ];
			d.file = array[ 4 ];
//			d.groundTile = array[ 6 ];
//			d.upperTile = array[ 7 ];
//			d.cliffClass = array[ 8 ];
//			d.oldID = (byte)int.Parse( array[ 9 ] );
//			d.version = (byte)int.Parse( array[ 10 ] );
//			d.InBeta = (byte)int.Parse( array[ 11 ] );

            d.numVar = (byte)int.Parse( array[ 20 ] );
            d.shadow = (byte)int.Parse( array[ 24 ] );
            d.showInFog = (byte)int.Parse( array[ 25 ] );

            d.pathTex = array[ 28 ];
            d.pathTex = d.pathTex.Replace( ".tga" , "" );
            d.pathTex = d.pathTex.Replace( "none" , "" );

            list.Add( d );
		}

		Debug.Log( "W3 doodads loaded num=" + list.Count );
	}

	#endif




}
