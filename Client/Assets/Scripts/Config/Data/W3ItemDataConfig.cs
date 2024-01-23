using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[System.Serializable]
public class W3ItemDataConfigData
{
    public string itemID;
    public string file;

    public bool perishable;
    public bool droppable;
    public bool pawnable;
    public bool sellable;
    public bool powerup;
}


public class W3ItemDataConfig : SingletonMono< W3ItemDataConfig >
{
    Dictionary<string , W3ItemDataConfigData> data = new Dictionary<string , W3ItemDataConfigData>();
    Dictionary<int , W3ItemDataConfigData> data1 = new Dictionary<int , W3ItemDataConfigData>();

    public List<W3ItemDataConfigData> list;


    public W3ItemDataConfigData getData( string id )
    {
        if ( data.ContainsKey( id ) )
        {
            return data[ id ];
        }

        return null;
    }

    public W3ItemDataConfigData getData( int id )
    {
        if ( data1.ContainsKey( id ) )
        {
            return data1[ id ];
        }

        return null;
    }

    public override void initSingletonMono()
    {
        for ( int i = 0 ; i < list.Count ; i++ )
        {
            data.Add( list[ i ].itemID , list[ i ] );
            data1.Add( GameDefine.UnitId( list[ i ].itemID ) , list[ i ] );
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

        list = new List<W3ItemDataConfigData>();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
        {
            W3ItemDataConfigData d = new W3ItemDataConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12 )
            {
                continue;
            }

            d.itemID = array[ 0 ];
            d.file = array[ 27 ];

            list.Add( d );
        }

        Debug.Log( "W3 item data loaded num=" + list.Count );
    }

#endif




}
