using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[ System.Serializable ]
public class W3DestructableDataConfigData 
{
	public string doodID;
	public string dir;
	public string file;

    public string texFile;

    public byte numVar;

    public string pathTex;
}


public class W3DestructableDataConfig : SingletonMono< W3DestructableDataConfig >
{
	Dictionary< string , W3DestructableDataConfigData > data = new Dictionary< string , W3DestructableDataConfigData >();
    Dictionary< int , W3DestructableDataConfigData > data1 = new Dictionary< int , W3DestructableDataConfigData >();

    public List< W3DestructableDataConfigData > list;


	public W3DestructableDataConfigData getData( string id )
	{
		if ( data.ContainsKey( id ) )
		{
			return data[ id ];
		}
        
        return null;
	}

    public W3DestructableDataConfigData getData( int id )
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

		list = new List< W3DestructableDataConfigData >();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
		{
			W3DestructableDataConfigData d = new W3DestructableDataConfigData();

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
            d.texFile = array[ 8 ];
            d.pathTex = array[ 35 ];
            d.pathTex = d.pathTex.Replace( ".tga" , "" );

            //			d.groundTile = array[ 6 ];
            //			d.upperTile = array[ 7 ];
            //			d.cliffClass = array[ 8 ];
            //			d.oldID = (byte)int.Parse( array[ 9 ] );
            //			d.version = (byte)int.Parse( array[ 10 ] );
            //			d.InBeta = (byte)int.Parse( array[ 11 ] );
            d.numVar = (byte)int.Parse( array[ 21 ] );

			list.Add( d );
		}

		Debug.Log( "W3 destructable loaded num=" + list.Count );
	}

	#endif




}
