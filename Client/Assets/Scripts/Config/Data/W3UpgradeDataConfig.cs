using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;



[ System.Serializable ]
public class W3UpgradeDataConfigData
{
	public string upgradeID;


}


public class W3UpgradeDataConfig : SingletonMono< W3UpgradeDataConfig >
{
	Dictionary< string , W3UpgradeDataConfigData > data = new Dictionary< string , W3UpgradeDataConfigData >();
	public List< W3UpgradeDataConfigData > list;

	public override void initSingletonMono()
	{
		for ( int i = 0; i < list.Count ; i++ ) 
		{
			data.Add( list[ i ].upgradeID , list[ i ] );
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

		list = new List< W3UpgradeDataConfigData >();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1 ; i < lineArray.Length ; i++ )
		{
			W3UpgradeDataConfigData d = new W3UpgradeDataConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12 )
			{
				continue;
			}

			d.upgradeID = array[ 0 ];

			list.Add( d );
		}

		Debug.Log( "W3 Upgrade Data loaded num=" + list.Count );
	}

	#endif




}
