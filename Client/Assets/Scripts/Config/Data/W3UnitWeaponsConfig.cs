using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;




[System.Serializable]
public class W3UnitWeaponsConfigData
{
    public string unitID;

    public int rangeN1;

    public float castpt;
    public float cool1;
    public int mindmg1;
    public int maxdmg1;
    
}


public class W3UnitWeaponsConfig : SingletonMono<W3UnitWeaponsConfig>
{
    Dictionary<string , W3UnitWeaponsConfigData> data = new Dictionary<string , W3UnitWeaponsConfigData>();
    Dictionary<int , W3UnitWeaponsConfigData> data1 = new Dictionary<int , W3UnitWeaponsConfigData>();

    public List<W3UnitWeaponsConfigData> list;

    public override void initSingletonMono()
    {
        for ( int i = 0 ; i < list.Count ; i++ )
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

    public W3UnitWeaponsConfigData getData( string uid )
    {
        if ( data.ContainsKey( uid ) )
        {
            return data[ uid ];
        }

        return null;
    }

    public W3UnitWeaponsConfigData getData( int uid )
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

        list = new List<W3UnitWeaponsConfigData>();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
        {
            W3UnitWeaponsConfigData d = new W3UnitWeaponsConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12 )
            {
                continue;
            }

            d.unitID = array[ 0 ];

            array[ 7 ] = array[ 7 ].Replace( "-" , "" ).Replace( " " , "" );
            if ( array[ 7 ].Length > 0 )
                d.castpt = float.Parse( array[ 7 ] );



            array[ 18 ] = array[ 18 ].Replace( "-" , "" ).Replace( " " , "" );
            if ( array[ 18 ].Length > 0 )
                d.rangeN1 = int.Parse( array[ 18 ] );

            array[ 23 ] = array[ 23 ].Replace( "-" , "" ).Replace( " " , "" ).Replace( "#VALUE!" , "" );
            if ( array[ 23 ].Length > 0 )
                d.cool1 = float.Parse( array[ 23 ] );

            array[ 29 ] = array[ 29 ].Replace( "-" , "" ).Replace( " " , "" ).Replace( "#VALUE!" , "" );
            if ( array[ 29 ].Length > 0 )
                d.mindmg1 = int.Parse( array[ 29 ] );

            array[ 31 ] = array[ 31 ].Replace( "-" , "" ).Replace( " " , "" ).Replace( "#VALUE!" , "" );
            if ( array[ 31 ].Length > 0 )
                d.maxdmg1 = int.Parse( array[ 31 ] );

            list.Add( d );
        }

        Debug.Log( "W3 Unit Weapons loaded num=" + list.Count );
    }

#endif




}
