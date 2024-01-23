using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[ System.Serializable ]
public class W3WaterConfigData 
{
	public string waterID;

	public string shoreDir;
	public string shoreSFile;
	public string shoreOCFile;
	public string shoreICFile;

	public float height;

	public byte mmAlpha;
	public byte mmRed;
	public byte mmGreen;
	public byte mmBlue;
	public byte numTex;
	public byte texRate;
	public byte texOffset;
	public byte alphaMode;
	public byte lighting;
	public byte cells;
	public byte minX;
	public byte minY;
	public byte minZ;

	public byte maxX;
	public byte maxY;
	public byte maxZ;
	public byte rateX;
	public byte rateY;
	public byte rateZ;
	public byte revX;
	public byte revY;


	public byte shoreSVar;
	public byte shoreOCVar;
	public byte shoreICVar;

	public byte Smin_A;
	public byte Smin_R;
	public byte Smin_G;
	public byte Smin_B;

	public byte Smax_A;
	public byte Smax_R;
	public byte Smax_G;
	public byte Smax_B;

	public byte Dmin_A;
	public byte Dmin_R;
	public byte Dmin_G;
	public byte Dmin_B;

	public byte Dmax_A;
	public byte Dmax_R;
	public byte Dmax_G;
	public byte Dmax_B;

}


public class W3WaterConfig : SingletonMono< W3WaterConfig >
{
	Dictionary< string , W3WaterConfigData > data = new Dictionary< string , W3WaterConfigData >();
	public List< W3WaterConfigData > list;


	public override void initSingletonMono()
	{
		for ( int i = 0; i < list.Count ; i++ ) 
		{
			data.Add( list[ i ].waterID , list[ i ] );
		}

		list.Clear();
		list = null;
	}

	public W3WaterConfigData getData( string id )
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

		list = new List< W3WaterConfigData >();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
		{
			W3WaterConfigData d = new W3WaterConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12 )
			{
				continue;
			}

			d.waterID = array[ 0 ];
			d.height = float.Parse( array[ 1 ] );

			list.Add( d );
		}

		Debug.Log( "W3 Water loaded num=" + list.Count );
	}

	#endif




}
