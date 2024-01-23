using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;


[ System.Serializable ]
public class W3UnitFuncConfigData
{
	public string unitID;

    public string[] makeItems;

    public string[] trains;
	public string[] requires;
	public string[] researches;
	public string art;
	public byte[] buttonPos;

	public string[] upgrade;
	public string[] builds;
	public string[] sellUnits;
	public string[] sellItems;

	public string[] animProps;

	public string missileArt;
	public float[] missileArc;
	public short[] missileSpeed;
	public string buildingSoundLabel;

	public short loopingSoundFadeIn;
	public short loopingSoundFadeOut;

	public string specialArt;
	public string[] attachmentAnimProps;

	public string attachmentLinkProps;
	public string boneProps;

	public string movementSoundLabel;
	public string randomSoundLabel;

	public string casterUpgradeArt;
	public string targetArt;

	public string dependencyOr;
    public byte missileHoming = 0;
    public byte revive = 0;
}


public class W3UnitFuncConfig : SingletonMono< W3UnitFuncConfig >
{
	Dictionary< string , W3UnitFuncConfigData > data = new Dictionary< string , W3UnitFuncConfigData >();
    Dictionary< int , W3UnitFuncConfigData > data1 = new Dictionary< int , W3UnitFuncConfigData >();

    public List< W3UnitFuncConfigData > list;

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
		list = new List< W3UnitFuncConfigData >();
	}

	public void clearConfig()
	{
		list.Clear();
	}

    public W3UnitFuncConfigData getData( string uid )
    {
        if ( data.ContainsKey( uid ) )
        {
            return data[ uid ];
        }

        return null;
    }

    public W3UnitFuncConfigData getData( int uid )
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

		string[] lineArray = text.Split( '\n' );

		for ( int i = 0 ; i < lineArray.Length ; i++ )
		{
			lineArray[ i ] = lineArray[ i ].TrimEnd( '\r' );

			if ( lineArray[ i ].Contains( "//" ) )
			{
				continue;
			}

			if ( lineArray[ i ].Length > 0 && lineArray[ i ].Contains( "[" ) && lineArray[ i ].Contains( "]" )  )
			{
				string unitID = lineArray[ i ].Substring( lineArray[ i ].IndexOf( "[" ) + 1 , lineArray[ i ].IndexOf( "]" ) - 1 );

				W3UnitFuncConfigData d = new W3UnitFuncConfigData();
				list.Add( d );
				list[ list.Count - 1 ].unitID = unitID;
				continue;
			}

			if ( lineArray[ i ].Contains( "Trains=" ) )
			{
				list[ list.Count - 1 ].trains = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
			}
			else if ( lineArray[ i ].Contains( "Attachmentanimprops=" ) )
			{
				list[ list.Count - 1 ].attachmentAnimProps = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
			}
			else if ( lineArray[ i ].Contains( "Attachmentlinkprops=" ) )
			{
				list[ list.Count - 1 ].attachmentLinkProps = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "Boneprops=" ) )
			{
				list[ list.Count - 1 ].boneProps = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "Requirescount=" ) || lineArray[ i ].Contains( "RequiresCount=" ) )
			{
				list[ list.Count - 1 ].requires = new string[ int.Parse( lineArray[ i ].Split( '=' )[ 1 ] ) ];
			}
			else if ( lineArray[ i ].Contains( "Requires=" ) )
			{
				if ( list[ list.Count - 1 ].requires != null )
				{
					list[ list.Count - 1 ].requires[ 0 ] = lineArray[ i ].Split( '=' )[ 1 ];
				}
				else
				{
					list[ list.Count - 1 ].requires = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
				}
			}
			else if ( lineArray[ i ].Contains( "Requires1=" ) )
			{
				list[ list.Count - 1 ].requires[ 1 ] = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "Requires2=" ) )
			{
				list[ list.Count - 1 ].requires[ 2 ] = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "Art=" ) )
			{
				list[ list.Count - 1 ].art = lineArray[ i ].Split( '=' )[ 1 ].Replace( ".blp" , "" ).Replace( ".tga" , "" );
			}
			else if ( lineArray[ i ].Contains( "Researches=" ) )
			{
				list[ list.Count - 1 ].researches = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
			}
			else if ( lineArray[ i ].Contains( "Upgrade=" ) )
			{
				list[ list.Count - 1 ].upgrade = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
			}
			else if ( lineArray[ i ].Contains( "Builds=" ) )
			{
				list[ list.Count - 1 ].builds = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
			}
			else if ( lineArray[ i ].Contains( "Sellunits=" ) )
			{
				list[ list.Count - 1 ].sellUnits = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
			}
			else if ( lineArray[ i ].Contains( "Sellitems=" ) )
			{
				list[ list.Count - 1 ].sellItems = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
			}
			else if ( lineArray[ i ].Contains( "Buttonpos=" ) )
			{
				list[ list.Count - 1 ].buttonPos = new byte[ 2 ];
				list[ list.Count - 1 ].buttonPos[ 0 ] = byte.Parse( lineArray[ i ].Split( '=' )[ 1 ].Split( ',' )[ 0 ] );
				list[ list.Count - 1 ].buttonPos[ 1 ] = byte.Parse( lineArray[ i ].Split( '=' )[ 1 ].Split( ',' )[ 1 ] );
			}
			else if ( lineArray[ i ].Contains( "Missileart=" ) )
			{
				list[ list.Count - 1 ].missileArt = lineArray[ i ].Split( '=' )[ 1 ].Replace( ".mdl" , "" );
			}
			else if ( lineArray[ i ].Contains( "Missilearc=" ) )
			{
				string[] str1 = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
				list[ list.Count - 1 ].missileArc = new float[ str1.Length ];

				for ( int j = 0 ; j < str1.Length ; j++ ) 
				{
					list[ list.Count - 1 ].missileArc[ j ] = float.Parse( str1[ j ] );
				}
			}
			else if ( lineArray[ i ].Contains( "Missilespeed=" ) || lineArray[ i ].Contains( "Missilspeed=" ) )
			{
				string[] str1 = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
				list[ list.Count - 1 ].missileSpeed = new short[ str1.Length ];

				for ( int j = 0 ; j < str1.Length ; j++ ) 
				{
					list[ list.Count - 1 ].missileSpeed[ j ] = short.Parse( str1[ j ] );
				}
			}
			else if ( lineArray[ i ].Contains( "LoopingSoundFadeIn=" ) )
			{
				list[ list.Count - 1 ].loopingSoundFadeIn = short.Parse( lineArray[ i ].Split( '=' )[ 1 ] );
			}
			else if ( lineArray[ i ].Contains( "LoopingSoundFadeOut=" ) )
			{
				list[ list.Count - 1 ].loopingSoundFadeOut = short.Parse( lineArray[ i ].Split( '=' )[ 1 ] );
			}
			else if ( lineArray[ i ].Contains( "BuildingSoundLabel=" ) )
			{
				list[ list.Count - 1 ].buildingSoundLabel = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "MovementSoundLabel=" ) )
			{
				list[ list.Count - 1 ].movementSoundLabel = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "RandomSoundLabel=" ) )
			{
				list[ list.Count - 1 ].randomSoundLabel = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "Casterupgradeart=" ) )
			{
				list[ list.Count - 1 ].casterUpgradeArt = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "Targetart=" ) )
			{
				list[ list.Count - 1 ].targetArt = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "DependencyOr=" ) )
			{
				list[ list.Count - 1 ].dependencyOr = lineArray[ i ].Split( '=' )[ 1 ];
			}
			else if ( lineArray[ i ].Contains( "Animprops=" ) )
			{
				list[ list.Count - 1 ].animProps = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
			}
			else if ( lineArray[ i ].Contains( "Specialart=" ) )
			{
				list[ list.Count - 1 ].specialArt = lineArray[ i ].Split( '=' )[ 1 ].Replace( ".mdl" , "" );
			}
            else if ( lineArray[ i ].Contains( "MissileHoming=" ) || lineArray[ i ].Contains( "Missilehoming=" ) )
            {
                list[ list.Count - 1 ].missileHoming = (byte)int.Parse( lineArray[ i ].Split( '=' )[ 1 ] );
            }
            else if ( lineArray[ i ].Contains( "Revive=" ) )
            {
                list[ list.Count - 1 ].revive = (byte)int.Parse( lineArray[ i ].Split( '=' )[ 1 ] );
            }
            else if( lineArray[ i ].Contains( "Makeitems=" ) )
            {
                list[ list.Count - 1 ].makeItems = lineArray[ i ].Split( '=' )[ 1 ].Split( ',' );
            }
            else if ( lineArray[ i ].Contains( "=" ) )
			{
				Debug.LogError( "error Unit Func " + lineArray[ i ] );
			}



		}

		Debug.Log( "W3 Unit Func loaded num=" + list.Count );
	}

	#endif




}
