using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

[ System.Serializable ]
public class W3UnitUIConfigData
{
	public string unitID;
	public string file;

    //#if UNITY_EDITOR


    public string unitShadow;

    public float scale;

    public float run;
    public float selY;

    public short shadowW;
    public short shadowH;
    public short shadowX;
    public short shadowY;

    public string uberSplat;

    public string unitClass;

    public bool isBuilding;


    public byte buildingShadowX = 0;
    public byte buildingShadowZ = 0;
    public byte buildingShadowW = 0;
    public byte buildingShadowH = 0;
    public byte[] buildingShadow = null;

    //#endif
}


public class W3UnitUIConfig : SingletonMono< W3UnitUIConfig >
{
	Dictionary< string , W3UnitUIConfigData > data = new Dictionary< string , W3UnitUIConfigData >();
	Dictionary< int , W3UnitUIConfigData > data1 = new Dictionary< int , W3UnitUIConfigData >();

	public List< W3UnitUIConfigData > list;

	public override void initSingletonMono()
	{
		for ( int i = 0; i < list.Count ; i++ ) 
		{
			data.Add( list[ i ].unitID , list[ i ] );
            data1.Add( GameDefine.UnitId( list[ i ].unitID ) , list[ i ] );
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

	public W3UnitUIConfigData getData( string uid )
	{
		if ( data.ContainsKey( uid ) )
		{
			return data[ uid ];
		}

		return null;
	}
	public W3UnitUIConfigData getData( int uid )
	{
		if ( data1.ContainsKey( uid ) )
		{
			return data1[ uid ];
		}

		return null;
	}

#if UNITY_EDITOR

	public void load( byte[] bytes )
	{
		string text = UTF8Encoding.Default.GetString( bytes );

		list = new List< W3UnitUIConfigData >();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
		{
			W3UnitUIConfigData d = new W3UnitUIConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12 )
			{
				continue;
			}

			d.unitID = array[ 0 ];
			d.file = array[ 2 ];
            d.unitClass = array[ 7 ];

            d.unitShadow = array[ 37 ].Replace( "_" , "" );


            string buildingShadow = array[ 38 ];

            if ( buildingShadow.Length > 3 )
            {
                Texture2D texture3232 = Instantiate( Resources.Load<Texture2D>( "ReplaceableTextures/Shadows/" + buildingShadow ) );

                d.buildingShadowW = (byte)texture3232.width;
                d.buildingShadowH = (byte)texture3232.height;
                d.buildingShadow = new byte[ texture3232.width * texture3232.height ];

                int c = 0;
                for ( int ti = 0 ; ti < texture3232.height ; ti++ )
                {
                    for ( int tj = 0 ; tj < texture3232.width ; tj++ )
                    {
                        Color color = texture3232.GetPixel( tj , ti );

                        if ( color.r > 0 && 
                            d.buildingShadowX == 0 && 
                            d.buildingShadowZ == 0 )
                        {
                            d.buildingShadowX = (byte)tj;
                            d.buildingShadowZ = (byte)ti;
                        }

                        d.buildingShadow[ c ] = color.a > 0 ? (byte)GameDefine.SHADOW_VALUE : (byte)255;
                        c++;
                    }
                }
            }


            if ( array[ 39 ].Length > 0 )
                d.shadowW = (short)int.Parse( array[ 39 ] );
            if ( array[ 40 ].Length > 0 )
                d.shadowH = (short)int.Parse( array[ 40 ] );
            if ( array[ 41 ].Length > 0 )
                d.shadowX = (short)int.Parse( array[ 41 ] );
            if ( array[ 42 ].Length > 0 )
                d.shadowY = (short)int.Parse( array[ 42 ] );
            if ( array[ 25 ].Length > 0 )
                d.run = int.Parse( array[ 25 ] );

            d.uberSplat =  array[ 36 ];
            
            d.selY = int.Parse(array[26]);
            d.scale = float.Parse(array[17]) * GameDefine.W3SCALE1;

            //			//			d.comment = array[ 2 ];
            //
            //			d.race = (W3UnitDataRace)Enum.Parse( typeof( W3UnitDataRace ) , array[ 3 ] );
            //			d.prio = (byte)int.Parse( array[ 4 ] );
            //			d.threat = (byte)int.Parse( array[ 5 ] );
            //
            list.Add( d );
		}

        Debug.Log( "W3 Unit UI loaded num=" + list.Count );
	}

#endif




}
