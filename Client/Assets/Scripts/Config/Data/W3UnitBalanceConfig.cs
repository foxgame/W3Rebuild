using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public enum W3UnitBalanceRegenType
{
	none = 0,
	always,
	night,
	blight,
}


public enum W3UnitBalanceDefType
{
	small = 0,
	medium,
	large,
	fort,
	hero,
	divine,

    none
}


public enum W3UnitBalancePrimary
{
	STR = 0,
	INT,
	AGI,

}


[ System.Serializable ]
public class W3UnitBalanceConfigData
{
	public string unitBalanceID;

	public string sortBalance;
	public string sort2;

	public string comment;

	public byte level;
	public byte summon;

	public short goldCost;
	public short lumberCost;
	public short goldRep;
	public short lumberRep;

	public byte fmade;
	public byte fused;
	public byte bountyDice;
	public byte bountySides;
	public byte bountyPlus;

	public byte stockMax;
	public byte stockRegen;
	public byte stockStart;

	public short HP;
	public short realHP;
	public float regenHP;
	public W3UnitBalanceRegenType regenType;

	public short manaN;
	public short realM;
	public short mana0;
	public float regenMana;

	public byte def;
	public byte defUp;
	public float realDef;
	public W3UnitBalanceDefType defType;
	public float spd;

	public short bldtm;
	public float sight;
	public float nsight;

	public short STR;
	public short INT;
	public short AGI;

	public float STRplus;
	public float INTplus;
	public float AGIplus;
	public float abilTest;

	public W3UnitBalancePrimary Primary;
	public string[] upgrades;

	public byte InBeta;

    public byte collision;

    public byte hero;
}

public class W3UnitBalanceConfig : SingletonMono< W3UnitBalanceConfig >
{
	Dictionary< string , W3UnitBalanceConfigData > data = new Dictionary< string , W3UnitBalanceConfigData >();
    Dictionary< int , W3UnitBalanceConfigData > data1 = new Dictionary< int , W3UnitBalanceConfigData >();

    public List< W3UnitBalanceConfigData > list;

    public W3UnitBalanceConfigData getData( string id )
    {
        if ( data.ContainsKey( id ) )
        {
            return data[ id ];
        }

        return null;
    }

    public W3UnitBalanceConfigData getData( int id )
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
			data.Add( list[ i ].unitBalanceID , list[ i ] );
            data1.Add( GameDefine.UnitId( list[ i ].unitBalanceID ) , list[ i ] );
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

		list = new List< W3UnitBalanceConfigData >();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
		{
			W3UnitBalanceConfigData d = new W3UnitBalanceConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12 )
			{
				continue;
			}

			d.unitBalanceID = array[ 0 ];
			d.sortBalance = array[ 1 ];
			d.sort2 = array[ 2 ];
//			d.comment = array[ 3 ];

            array[ 4 ] = array[ 4 ].Replace( " " , "" ).Replace( "-" , "" );

            if ( array[ 4 ].Length > 0 )
				d.level = (byte)int.Parse( array[ 4 ] );

			d.summon = ( array[ 5 ] == "TRUE" ? (byte)1 : (byte)0 );

			d.goldCost = (short)int.Parse( array[ 6 ] );
			d.lumberCost = (short)int.Parse( array[ 7 ] );
			d.goldRep = (short)int.Parse( array[ 8 ] );
			d.lumberCost = (short)int.Parse( array[ 9 ] );

            array[ 10 ] = array[ 10 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 11 ] = array[ 10 ].Replace( " " , "" ).Replace( "-" , "" );

            if ( array[ 10 ].Length > 0 )
				d.fmade = (byte)int.Parse( array[ 10 ] );
			if ( array[ 11 ].Length > 0 )
				d.fused = (byte)int.Parse( array[ 11 ] );
			
			d.bountyDice = (byte)int.Parse( array[ 12 ] );
			d.bountySides = (byte)int.Parse( array[ 13 ] );
			d.bountyPlus = (byte)int.Parse( array[ 14 ] );

            array[ 15 ] = array[ 15 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 16 ] = array[ 16 ].Replace( " " , "" ).Replace( "-" , "" ).Replace( "#VALUE!" , "" );
            array[ 17 ] = array[ 17 ].Replace( " " , "" ).Replace( "-" , "" );

            if ( array[ 15 ].Length > 0 )
				d.stockMax = (byte)int.Parse( array[ 15 ] );
			if ( array[ 16 ].Length > 0 )
				d.stockRegen = (byte)int.Parse( array[ 16 ] );
			if ( array[ 17 ].Length > 0 )
				d.stockStart = (byte)int.Parse( array[ 17 ] );

			d.HP = (short)int.Parse( array[ 18 ] );
			d.realHP = (short)int.Parse( array[ 19 ] );

            array[ 20 ] = array[ 20 ].Replace( " " , "" ).Replace( "-" , "" );

            if ( array[ 20 ].Length > 0 )
				d.regenHP = float.Parse( array[ 20 ] );

			d.regenType = (W3UnitBalanceRegenType)Enum.Parse( typeof( W3UnitBalanceRegenType ) , array[ 21 ] );

            array[ 22 ] = array[ 22 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 23 ] = array[ 23 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 24 ] = array[ 24 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 25 ] = array[ 25 ].Replace( " " , "" ).Replace( "-" , "" );

            if ( array[ 22 ].Length > 0 )
				d.manaN = (short)int.Parse( array[ 22 ] );
			if ( array[ 23 ].Length > 0 )
				d.realM = (short)int.Parse( array[ 23 ] );
			if ( array[ 24 ].Length > 0 )
				d.mana0 = (short)int.Parse( array[ 24 ] );
			if ( array[ 25 ].Length > 0 )
				d.regenMana = float.Parse( array[ 25 ] );

			d.def = (byte)int.Parse( array[ 26 ] );
			d.defUp = (byte)int.Parse( array[ 27 ] );
			d.realDef = float.Parse( array[ 28 ] );
			d.defType = (W3UnitBalanceDefType)Enum.Parse( typeof( W3UnitBalanceDefType ) , array[ 29 ] );

            array[ 30 ] = array[ 30 ].Replace( " " , "" ).Replace( "-" , "" );

            if ( array[ 30 ].Length > 0 )
				d.spd = int.Parse( array[ 30 ] );

			d.bldtm = (short)int.Parse( array[ 33 ] );

			d.sight = float.Parse( array[ 35 ] ) / GameDefine.TERRAIN_SIZE_PER;
			d.nsight = float.Parse( array[ 36 ] ) / GameDefine.TERRAIN_SIZE_PER;

            array[ 37 ] = array[ 37 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 38 ] = array[ 38 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 39 ] = array[ 39 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 40 ] = array[ 40 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 41 ] = array[ 41 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 42 ] = array[ 42 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 43 ] = array[ 43 ].Replace( " " , "" ).Replace( "-" , "" );
            array[ 44 ] = array[ 44 ].Replace( " " , "" ).Replace( "-" , "" ).Replace( "_" , "" );
            array[ 45 ] = array[ 45 ].Replace( " " , "" ).Replace( "-" , "" ).Replace( "_" , "" );

            if ( array[ 37 ].Length > 0 )
				d.STR = (short)int.Parse( array[ 37 ] );
			if ( array[ 38 ].Length > 0 )
				d.INT = (short)int.Parse( array[ 38 ] );
			if ( array[ 39 ].Length > 0 )
				d.AGI = (short)int.Parse( array[ 39 ] );
			if ( array[ 40 ].Length > 0 )
				d.STRplus = float.Parse( array[ 40 ] );
			if ( array[ 41 ].Length > 0 )
				d.INTplus = float.Parse( array[ 41 ] );
			if ( array[ 42 ].Length > 0 )
				d.AGIplus = float.Parse( array[ 42 ] );

			if ( array[ 43 ].Length > 0 )
				d.abilTest = float.Parse( array[ 43 ] );

			if ( array[ 44 ].Length > 0 )
				d.Primary = (W3UnitBalancePrimary)Enum.Parse( typeof( W3UnitBalancePrimary ) , array[ 44 ] );
            
            d.collision = (byte)int.Parse( array[ 55 ] );

            string str = array[ 45 ];

			if ( str.Length > 0 )
				d.upgrades = str.Split(";" [0]);

			d.InBeta = (byte)int.Parse( array[ 56 ] );

			list.Add( d );
		}

		Debug.Log( "W3 Unit Balance loaded num=" + list.Count );
	}

	#endif




}
