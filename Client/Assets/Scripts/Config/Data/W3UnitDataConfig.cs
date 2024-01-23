using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public enum W3UnitDataRace
{
	human = 0,
	orc,
	nightelf,
	undead,
	creeps,
	demon,
	critters,
	commoner,
    naga,
	other,

}

public enum W3UnitDataMoveType
{
	foot = 0,
	fly,
	horse,
	hover,
    floatf,
    amph,


}


public enum W3UnitDataTargetType
{
	ground = 0,
	air,
	structure,
	ward,

}


public enum W3UnitDataBuffType
{
	factory = 0,
	townhall,
	buffer,
	resource,

}



public enum W3UnitDataType
{
	Mechanical = 0,
	TownHall,
	Ancient,
	Peon,
	Summoned,
	Ward,
	Sapper,
	Undead,
	Standon,
	Neutral,
    Tauren,



}



[ System.Serializable ]
public class W3UnitDataConfigData
{
	public string unitID;

	public string sort;

	public string comment;

	public W3UnitDataRace race;

	public byte prio;
	public byte threat;

	public W3UnitDataType[] type;

	public byte valid;
	public byte deathType;

	public float death;

	public byte canSleep;
	public byte cargoSize;

	public W3UnitDataMoveType movetp;

	public short moveHeight;
	public byte moveFloor;
	public short launchX;
	public short launchY;
	public short launchZ;
	public short impactZ;

	public float turnRate;

	public byte propWin;
	public byte orientInterp;
	public byte formation;

	public float castpt;
	public float castbsw;

	public W3UnitDataTargetType targType;

    public byte pathW;
    public byte pathH;
    public byte pathX;
    public byte pathZ;

    public byte pathMinX;
    public byte pathMinZ;
    public byte pathMaxX;
    public byte pathMaxZ;


    public Color[] pathData;

	public byte fatLOS;
	public byte collision;
	public byte points;

	public W3UnitDataBuffType buffType;
	public byte buffRadius;

	public byte nameCount;

	public byte InBeta;

    public bool isBuilding;
}


public class W3UnitDataConfig : SingletonMono< W3UnitDataConfig >
{
	Dictionary< string , W3UnitDataConfigData > data = new Dictionary< string , W3UnitDataConfigData >();
	Dictionary< int , W3UnitDataConfigData > data1 = new Dictionary< int , W3UnitDataConfigData >();

	public List< W3UnitDataConfigData > list;

	public override void initSingletonMono()
	{
		for ( int i = 0; i < list.Count ; i++ ) 
		{
			data.Add( list[ i ].unitID , list[ i ] );
            data1.Add(GameDefine.UnitId( list[ i ].unitID ) , list[ i ] );
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

	public W3UnitDataConfigData getData( string uid )
	{
		if ( data.ContainsKey( uid ) )
		{
			return data[ uid ];
		}

		return null;
	}

	public W3UnitDataConfigData getData( int uid )
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

		list = new List< W3UnitDataConfigData >();

		string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

		for ( int i = 1 ; i < lineArray.Length ; i++ )
		{
			W3UnitDataConfigData d = new W3UnitDataConfigData();

			string[] array = lineArray[i].Split( '\t' );

			if ( array.Length < 12 )
			{
				continue;
			}

			d.unitID = array[ 0 ];

//             Debug.Log( d.unitID );

            d.sort = array[ 1 ];
//			d.comment = array[ 2 ];

			d.race = (W3UnitDataRace)Enum.Parse( typeof( W3UnitDataRace ) , array[ 3 ] );
			d.prio = (byte)int.Parse( array[ 4 ] );
			d.threat = (byte)int.Parse( array[ 5 ] );

			string str = array[ 6 ].Replace( "_" , "" ).Replace( "undead" , "Undead" ).Replace( "summoned" , "Summoned" ).Replace( "neutral" , "Neutral" ).Replace( "standon" , "Standon" ).Replace( "ward" , "Ward" ).Replace( "mechanical" , "Mechanical" );
            if ( str.Length > 0 )
			{
				string[] str1 = str.Split( ";"[ 0 ] );
				d.type = new W3UnitDataType[ str1.Length ];
				for ( int j = 0 ; j < str1.Length ; j++ ) 
				{
					d.type[ j ] = (W3UnitDataType)Enum.Parse( typeof( W3UnitDataType ) , str1[ j ] );
				}
			}

			d.valid = (byte)int.Parse( array[ 7 ] );
			d.deathType = (byte)int.Parse( array[ 8 ] );
			d.death = float.Parse( array[ 9 ] );

			d.canSleep = (byte)int.Parse( array[ 10 ] );
			if ( array[ 11 ].Replace( "-" , "" ).Length > 0 )
				d.cargoSize = (byte)int.Parse( array[ 11 ] );

			if ( array[ 12 ].Replace( "_" , "" ).Length > 0 )
                d.movetp = (W3UnitDataMoveType)Enum.Parse( typeof( W3UnitDataMoveType ) , array[ 12 ].Replace( "float" , "floatf" ) );

			d.moveHeight = (short)int.Parse( array[ 13 ] );
			d.moveFloor = (byte)int.Parse( array[ 14 ] );
            d.turnRate = float.Parse( array[ 15 ].Replace( "-" , "0.0" ) );

            d.formation = (byte)int.Parse( array[ 18 ] );

            //			d.launchX = (short)int.Parse( array[ 15 ] );
            // 			d.launchY = (short)int.Parse( array[ 16 ] );
            // 			d.launchZ = (short)int.Parse( array[ 17 ] );
            // 			d.impactZ = (short)int.Parse( array[ 18 ] );
            // 
            // 			if ( array[ 19 ].Length > 0 )
            // 				d.turnRate = float.Parse( array[ 19 ] );
            // 
            // 			d.propWin = (byte)int.Parse( array[ 20 ] );
            // 			d.orientInterp = (byte)int.Parse( array[ 21 ] );
            // 			if ( array[ 23 ].Length > 0 )
            // 				d.castpt = float.Parse( array[ 23 ] );
            // 			d.castbsw = float.Parse( array[ 24 ] );


            d.targType = (W3UnitDataTargetType)Enum.Parse( typeof( W3UnitDataTargetType ) , array[ 19 ] );

			string pathTex = array[ 20 ];

            if ( pathTex.Length > 3 )
            {
                d.isBuilding = true;

                Texture2D texture3232 = Instantiate( Resources.Load<Texture2D>( pathTex.Replace( ".tga" , "" ) ) );

                d.pathW = (byte)texture3232.width;
                d.pathH = (byte)texture3232.height;
                d.pathData = new Color[ texture3232.width * texture3232.height ];

                d.pathX = (byte)( d.pathW / 2 );
                d.pathZ = (byte)( d.pathH / 2 );
                d.pathX %= 2;
                d.pathZ %= 2;

                d.pathMinX = 99;
                d.pathMinZ = 99;
                d.pathMaxX = 0;
                d.pathMaxZ = 0;

                int c = 0;
                for ( int ti = 0 ; ti < texture3232.height ; ti++ )
                {
                    for ( int tj = 0 ; tj < texture3232.width ; tj++ )
                    {
                        d.pathData[ c ] = texture3232.GetPixel( tj , ti );

                        if ( d.pathData[ c ].r > 0 )
                        {
                            if ( tj < d.pathMinX )
                            {
                                d.pathMinX = (byte)tj;
                            }

                            if ( ti < d.pathMinZ )
                            {
                                d.pathMinZ = (byte)tj;
                            }

                            if ( tj > d.pathMaxX )
                            {
                                d.pathMaxX = (byte)tj;
                            }

                            if ( ti > d.pathMaxZ )
                            {
                                d.pathMaxZ = (byte)tj;
                            }
                        }

                        c++;
                    }

                }



            }


// 			d.fatLOS = (byte)int.Parse( array[ 21 ] );
// 			d.collision = (byte)int.Parse( array[ 22 ] );
// 			d.points = (byte)int.Parse( array[ 29 ] );
// 
// 			if ( array[ 30 ].Length > 0 )
// 				d.buffType = (W3UnitDataBuffType)Enum.Parse( typeof( W3UnitDataBuffType ) , array[ 30 ] );
// 
// 			if ( array[ 31 ].Length > 0 )
// 				d.buffRadius = (byte)int.Parse( array[ 31 ] );
// 			if ( array[ 32 ].Length > 0 )
// 				d.nameCount = (byte)int.Parse( array[ 32 ] );
// 			d.InBeta = (byte)int.Parse( array[ 33 ] );


            list.Add( d );
		}

		Debug.Log( "W3 Unit Data loaded num=" + list.Count );
	}

	#endif




}
