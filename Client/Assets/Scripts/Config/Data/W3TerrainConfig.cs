using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[ System.Serializable ]
public class W3TerrainConfigData
{
	public string tileID;

	public string dir;
	public string file;
	public string comment;
	public string name;
	public string[] convertTo;

    public sbyte cliffSet;
    public byte buildAble;
	public byte footPrints;
	public byte walkAble;
	public byte flyAble;
	public byte blightPri;
	public byte InBeta;
    public byte version;
}

public class W3TerrainConfig : SingletonMono< W3TerrainConfig >
{
	Dictionary< string , W3TerrainConfigData > data = new Dictionary< string , W3TerrainConfigData >();
	public List< W3TerrainConfigData > list;

	public override void initSingletonMono()
	{
		for ( int i = 0; i < list.Count ; i++ ) 
		{
			data.Add( list[ i ].tileID , list[ i ] );
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

	public W3TerrainConfigData getData( string str )
	{
		if ( data.ContainsKey( str ) )
		{
			return data[ str ];
		}

		return null;
	}

	#if UNITY_EDITOR

	public void load( byte[] bytes )
	{
		string text = UTF8Encoding.Default.GetString( bytes );

		list = new List< W3TerrainConfigData >();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
		{
			W3TerrainConfigData d = new W3TerrainConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12 )
			{
				continue;
			}

			d.tileID = array[ 0 ];
            d.cliffSet = (sbyte)int.Parse( array[ 1 ] );
            d.dir = array[ 2 ];
			d.file = array[ 3 ];
			d.comment = array[ 4 ];
			d.name = array[ 5 ];

			d.dir = d.dir.Replace( '\\' , '/' );
			d.file = d.file.Replace( '\\' , '/' );

			d.buildAble = (byte)int.Parse( array[ 6 ] );
			d.footPrints = (byte)int.Parse( array[ 7 ] );
			d.walkAble = (byte)int.Parse( array[ 8 ] );
			d.flyAble = (byte)int.Parse( array[ 9 ] );
			d.blightPri = (byte)int.Parse( array[ 10 ] );

			string str = array[ 11 ];
			d.convertTo = str.Split(";" [0]);
			d.InBeta = (byte)int.Parse( array[ 12 ] );
            d.version = (byte)int.Parse( array[ 13 ] );

            list.Add( d );
		}

		Debug.Log( "W3 Terrain loaded num=" + list.Count );
	}

	#endif




}
