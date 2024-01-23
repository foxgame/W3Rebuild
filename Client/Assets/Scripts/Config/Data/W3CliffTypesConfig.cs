using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[ System.Serializable ]
public class W3CliffTypesConfigData 
{
	public string cliffID;
	public string cliffModelDir;
	public string rampModelDir;
	public string texDir;
	public string texFile;
	public string name;
	public string groundTile;
	public string upperTile;
	public string cliffClass;
	public byte oldID;
	public byte version;
	public byte InBeta;
}


public class W3CliffTypesConfig : SingletonMono< W3CliffTypesConfig >
{
	Dictionary< string , W3CliffTypesConfigData > data = new Dictionary< string , W3CliffTypesConfigData >();
	public List< W3CliffTypesConfigData > list;


	public override void initSingletonMono()
	{
		for ( int i = 0; i < list.Count ; i++ ) 
		{
			data.Add( list[ i ].cliffID , list[ i ] );
		}

		list.Clear();
		list = null;
	}

	public W3CliffTypesConfigData getData( string id )
	{
		if ( data.ContainsKey( id ) )
		{
			return data[ id ];
		}

		return null;
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

		list = new List< W3CliffTypesConfigData >();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
		{
			W3CliffTypesConfigData d = new W3CliffTypesConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12 )
			{
				continue;
			}

			d.cliffID = array[ 0 ];
			d.cliffModelDir = array[ 1 ];
			d.rampModelDir = array[ 2 ];
			d.texDir = array[ 3 ];
			d.texFile = array[ 4 ];
			d.name = array[ 5 ];
			d.groundTile = array[ 6 ];
			d.upperTile = array[ 7 ];
			d.cliffClass = array[ 8 ];
			d.oldID = (byte)int.Parse( array[ 9 ] );
			d.version = (byte)int.Parse( array[ 10 ] );
			d.InBeta = (byte)int.Parse( array[ 11 ] );

			list.Add( d );
		}

		Debug.Log( "W3 CliffTypes loaded num=" + list.Count );
	}

	#endif




}
