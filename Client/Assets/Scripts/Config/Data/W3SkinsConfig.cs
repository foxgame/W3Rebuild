using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;


[System.Serializable]
public class W3SkinsConfigData
{
    public string skinID;

    public string[] music;

    public List< string > dataKey;
    public List< string > dataValue;

    public Dictionary< string , string > data;
}


public class W3SkinsConfig : SingletonMono< W3SkinsConfig >
{
//     Dictionary< string , W3SkinsConfigData > data = new Dictionary< string , W3SkinsConfigData >();
    public W3SkinsConfigData[] list;

    public override void initSingletonMono()
    {
        for ( int i = 0 ; i < list.Length ; i++ )
        {

            list[ i ].data = new Dictionary< string , string >();

            for ( int j = 0 ; j < list[ i ].dataKey.Count ; j++ )
            {
                list[ i ].data.Add( list[ i ].dataKey[ j ] , list[ i ].dataValue[ j ] );
            }

//             data.Add( list[ i ].skinID , list[ i ] );

//             list[ i ].dataKey = null;
//             list[ i ].dataValue = null;
        }

//         list.Clear();
//         list = null;
    }

    public void initConfig()
    {
        list = new W3SkinsConfigData[ GameDefine.RACE_OTHER.race + 1 ];
    }

    public void clearConfig()
    {
    }

    public string getData( string str )
    {
        int r = W3PlayerManager.instance.getLocalPlayer().race;

        if ( list[ r ].data.ContainsKey( str ) )
        {
            return list[ r ].data[ str ];
        }

        if ( list[ 0 ].data.ContainsKey( str ) )
        {
            return list[ 0 ].data[ str ];
        }

        return "";
    }

#if UNITY_EDITOR

    public void load( byte[] bytes )
    {
        int index = -1;

        string text = UTF8Encoding.Default.GetString( bytes );

        string[] lineArray = text.Split( '\n' );

        for ( int i = 0 ; i < lineArray.Length ; i++ )
        {
            lineArray[ i ] = lineArray[ i ].TrimEnd( '\r' );

            if ( lineArray[ i ].Contains( "//" ) )
            {
                continue;
            }

            if ( lineArray[ i ].Contains( "[Main]" ) )
            {
                continue;
            }

            if ( lineArray[ i ].Length > 0 && lineArray[ i ].Contains( "[" ) && lineArray[ i ].Contains( "]" ) )
            {
                string skinID = lineArray[ i ].Substring( lineArray[ i ].IndexOf( "[" ) + 1 , lineArray[ i ].IndexOf( "]" ) - 1 );

                if ( lineArray[ i ].Contains( "Human" ) )
                {
                    index = GameDefine.RACE_HUMAN.race;
                }
                else if ( lineArray[ i ].Contains( "Orc" ) )
                {
                    index = GameDefine.RACE_ORC.race;
                }
                else if ( lineArray[ i ].Contains( "NightElf" ) )
                {
                    index = GameDefine.RACE_NIGHTELF.race;
                }
                else if ( lineArray[ i ].Contains( "Undead" ) )
                {
                    index = GameDefine.RACE_UNDEAD.race;
                }
                else if ( lineArray[ i ].Contains( "Default" ) )
                {
                    index = 0;
                }


                list[ index ] = new W3SkinsConfigData();
                list[ index ].dataKey = new List<string>();
                list[ index ].dataValue = new List<string>();
                list[ index ].skinID = skinID;
                continue;
            }

            if ( index == -1 )
            {
                continue;
            }

            if ( lineArray[ i ].Contains( "Music=" ) )
            {
                string[] str1 = lineArray[ i ].Split( '=' )[ 1 ].Split( ';' );
                list[ index ].music = new string[ str1.Length ];

                for ( int j = 0 ; j < str1.Length ; j++ )
                {
                    list[ index ].music[ j ] = str1[ j ];
                }
            }
            else if ( lineArray[ i ].Contains( "=" ) )
            {
                list[ index ].dataKey.Add( lineArray[ i ].Split( '=' )[ 0 ] );
                list[ index ].dataValue.Add( lineArray[ i ].Split( '=' )[ 1 ].Replace( ".blp" , "" ).Replace( ".mdl" , "" ).Replace( ".mp3" , "" ) );
            }
 
        }

        Debug.Log( "W3 Skins loaded num=" + list.Length );
    }

#endif




}
