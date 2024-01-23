using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System;
using Mono.Xml;
using System.Security;

public class GameFileCreateEditor : EditorWindow
{
//	static private GameFileCreateEditor mInstance = null;
//	static public GameFileCreateEditor instance
//	{
//		get
//		{
//			return mInstance;
//		}
//	}
//
//
//	string[] sizeOptions = { "32" , "64" , "96" , "128" , "160" , "192" , "224" , "256" };
//	string[] heightOptions = { "null" , "high" , "low" , "level" };
//	string[] terrainOptions = null;
//
//	int mapWidthIndex = 1;
//	int mapHeightIndex = 1;
//
//	int terrainIndex = 0;
//
//	float offsetX = 0.0f;
//	float offsetY = 0.0f;
//
//
//	string[] altitudeOptions = { "1" , "2" , "3" , "4" , "5" , "6" , "7" , "8" , "9" , "10" , "11" , "12" , "13" , "14" };
//
//	int altitudeIndex = 1;
//
//	Texture2D[] textureSelect = null;
//
//	Vector2 scrollPosition = new Vector2( 0 , 0.0f );
//
//	void OnGUI()
//	{
//		if ( !GameManager.instance ) 
//		{
//			GUI.color = Color.red;
//			GUILayout.Label( "Please Start WorldEditor Scene." , EditorStyles.boldLabel );
//			GUI.color = Color.white;
//
//			return;
//		}
//
//		if ( GUILayout.Button( W3Localization.instance.g( "open" ) ) )
//		{
//			loadMapTerrain( "C:/work/war3map.xml" , "test" );
//			loadMapDoo( "C:/work/war3mapdoo.xml" , "test" );
//			loadMapShd( "C:/work/war3map.shd" , "test" );
//			return;
//		}
//
//		scrollPosition = EditorGUILayout.BeginScrollView( scrollPosition );
//
//		GUILayout.Label( W3Localization.instance.g( "Create New Map" ) , EditorStyles.boldLabel );
//
//		if ( mInstance == null )
//		{
//			mInstance = this;
//
//			terrainOptions = new string[ W3TerrainConfig.instance.list.Count ];
//
//			for ( int i = 0 ; i < W3TerrainConfig.instance.list.Count ; i++ )
//			{
//				terrainOptions[ i ] = W3TerrainConfig.instance.list[ i ].name;
//			}
//		}
//
//		// size
//		GUI.color = Color.green;
//		EditorGUILayout.LabelField( W3Localization.instance.g( "Map Size:" ) , EditorStyles.boldLabel );
//		GUI.color = Color.white;
//
//		EditorGUILayout.BeginHorizontal();
//
//		GUILayout.Label( W3Localization.instance.g( "Width" ) , EditorStyles.label );
//		mapWidthIndex = EditorGUILayout.Popup( mapWidthIndex , sizeOptions );
//
//		GUILayout.Label( "	" , EditorStyles.label );
//
//		GUILayout.Label( W3Localization.instance.g( "Height" ) , EditorStyles.label );
//		mapHeightIndex = EditorGUILayout.Popup( mapHeightIndex , sizeOptions );
//
//		EditorGUILayout.EndHorizontal();
//
//
//		// adjust height 
//		GUILayout.Label( W3Localization.instance.g( "Adjust Height" ) , EditorStyles.label );
//		W3MapManager.instance.selectAdjustHeight = EditorGUILayout.Popup( W3MapManager.instance.selectAdjustHeight , heightOptions );
//
//
//		// real size
//		GUI.color = Color.green;
//		EditorGUILayout.LabelField( W3Localization.instance.g( "Real Map Size:" ) , EditorStyles.boldLabel );
//		GUI.color = Color.white;
//
//		EditorGUILayout.BeginHorizontal();
//
//		GUILayout.Label( W3Localization.instance.g( "Width" ) , EditorStyles.label );
//		GUILayout.Label( ( Convert.ToInt32( sizeOptions[ mapWidthIndex ] ) - 12 ).ToString()  , EditorStyles.label );
//		GUILayout.Label( "	" , EditorStyles.label );
//		GUILayout.Label( W3Localization.instance.g( "Height" ) , EditorStyles.label );
//		GUILayout.Label( ( Convert.ToInt32( sizeOptions[ mapHeightIndex ] ) - 12 ).ToString()  , EditorStyles.label );
//
//		EditorGUILayout.EndHorizontal();
//
//
//		// size des.
//		GUI.color = Color.green;
//		EditorGUILayout.LabelField( W3Localization.instance.g( "Size Des. :" ), EditorStyles.boldLabel );
//		GUILayout.Label( "	" , EditorStyles.label );
//		GUI.color = Color.white;
//
//		// terrain
//		GUI.color = Color.green;
//		EditorGUILayout.LabelField( W3Localization.instance.g( "Terrain" ) , EditorStyles.boldLabel );
//		GUI.color = Color.white;
//
//		EditorGUILayout.BeginHorizontal();
//		terrainIndex = EditorGUILayout.Popup( terrainIndex , terrainOptions );
//		EditorGUILayout.EndHorizontal();
//
//		// terrain textures
//		int pos = 0;
//
//		if ( textureSelect == null || textureSelect.Length == 0 )
//		{
////			textureSelect = new Texture2D[ W3TerrainConfig.instance.data[ terrainIndex ].data.Length ];
////
////			for ( int i = 0 ; i < W3TerrainConfig.instance.data[ terrainIndex ].data.Length ; i++ )
////			{
////				Texture2D texture = (Texture2D)Resources.Load( W3TerrainConfig.instance.data[ terrainIndex ].data[ i ].dir + "/" +
////					W3TerrainConfig.instance.data[ terrainIndex ].data[ i ].file );
////
////				if ( texture != null )
////				{
////					textureSelect[ i ] = texture;
////
//////					EditorGUILayout.BeginHorizontal();
//////
//////					GUILayoutOption[] opt = { GUILayout.MaxHeight( 50.0f ) };
//////					GUILayout.Label( texture , opt );
//////					textureSelectIndex = i;
//////	//				textureSelect = (Texture2D)EditorGUILayout.ObjectField( W3TerrainConfig.instance.data[ terrainIndex ].data[ i ].comment , texture , typeof( Texture2D ) , false );
//////					pos++;
//////
//////					EditorGUILayout.EndHorizontal();
////				}
////			}
//		}
//
////		GUILayoutOption[] opt = { GUILayout.MaxHeight( 50.0f ) };
//		W3MapManager.instance.selectTexture = GUILayout.SelectionGrid( W3MapManager.instance.selectTexture , textureSelect , 1 );
//
//
//		// terrain
//		GUI.color = Color.green;
//		EditorGUILayout.LabelField( W3Localization.instance.g( "Altitude" ) , EditorStyles.boldLabel );
//		GUI.color = Color.white;
//
//		altitudeIndex = EditorGUILayout.Popup( altitudeIndex , altitudeOptions );
//
//		if ( GUILayout.Button( W3Localization.instance.g( "create" ) ) )
//		{
//			W3MapManager.instance.selectTerrainID = terrainIndex;
//			W3MapManager.instance.selectTextureDefault = W3MapManager.instance.selectTexture;
//			W3MapManager.instance.createMap( Convert.ToInt32( sizeOptions[ mapWidthIndex ] ) , Convert.ToInt32( sizeOptions[ mapHeightIndex ] ) , true );
//		}
//
//		EditorGUILayout.EndScrollView();
//	}
//
//	public void reloadTerrain( int i , int j , ushort tID )
//	{
//
//	}
//
//	public void reloadBuilding( int i , int j , ushort tID )
//	{
//
//	}
//
//	public void saveMap( string path , string name )
//	{
//
//	}
//
//	public void loadMapTerrain( string path , string name )
//	{
//		W3MapManager.instance.selectTerrainID = terrainIndex;
//		W3MapManager.instance.selectTextureDefault = W3MapManager.instance.selectTexture;
//
//		SecurityParser parser = new SecurityParser();
//
//		FileStream fs = File.OpenRead( path );
//		byte[] bytes = new byte[ fs.Length ];
//		fs.Read( bytes , 0 , (int)fs.Length );
//
//		parser.LoadXml( Encoding.UTF8.GetString( bytes ).Trim() );
//
//		SecurityElement node = parser.ToXml();
//
//		if ( node == null || node.Children == null )
//		{
//			return;
//		}
//
//
//		int width = int.Parse( node.Attribute( "width" ) );
//		int height = int.Parse( node.Attribute( "height" ) );
//
//		offsetX = float.Parse( node.Attribute( "offsetX" ) );
//		offsetY = float.Parse( node.Attribute( "offsetY" ) );
//
//		W3MapManager.instance.createMap( width - 1 , height - 1 , true );
//
//
//		foreach( SecurityElement nodeList in node.Children )
//		{
//			if ( nodeList.Tag == "tilepoint" )
//			{
//				int count = 0;
//				foreach( SecurityElement nodeList1 in nodeList.Children )
//				{
//					if ( nodeList1.Tag == "t" )
//					{
//						int x0 = count % width;
//						int y0 = count / width;
//
//						int gt = int.Parse( nodeList1.Attribute( "groundTextureType" ) );
//						int ct = int.Parse( nodeList1.Attribute( "cliffTextureType" ) );
//
//						int t0 = int.Parse( nodeList1.Attribute( "t0" ) );
//						int t1 = int.Parse( nodeList1.Attribute( "t1" ) );
//
//						int layerHeight = int.Parse( nodeList1.Attribute( "layerHeight" ) );
//						int groundHeight = int.Parse( nodeList1.Attribute( "groundHeight" ) );
//						short waterLevel = short.Parse( nodeList1.Attribute( "waterLevel" ) );
//
//						int flags = int.Parse( nodeList1.Attribute( "flags" ) );
//
//						int x1 = t1 % 4;
//						int y1 = t1 / 4;
//
//						float gh = ( groundHeight - 0x2000 ) / 4.0f / 32.0f; // 32  war3 is 128/grid this 4/grid
////						float wl = ( ( waterLevel - 0x2000 ) / 4.0f - 89.6f ) / 32.0f;
//
//						W3TerrainManager.instance.setTerrainNode( x0 , y0 , gh , waterLevel , (byte)flags , (byte)gt , (byte)ct , (sbyte)layerHeight , (byte)t0 , (byte)( ( x1 + 4 ) * 4 + y1 ) );
//
////						W3Selection.instance.setTerrain( terrain.getTerrainSprite( x0 , y0 ) , h , ( x1 + 4 ) * 4 + y1 , (byte)t , (byte)ct1 , (sbyte)layerHeight );
////						W3Selection.instance.setTerrain( terrain.getTerrainSprite( x0 , y0 ) , ( x1 + 4 ) * 4 + y1 , h );
//						count++;
//					}
//				}
//			}
//		}
//
//		W3TerrainManager.instance.updateTerrainSprite();
//
//		GameDataManager.instance.createBuiding( "hcas" , new Vector3( -338.0f , 8.0f , -100.0f ) , W3AnimationType.Stand );
//	}
//
//	public void loadMapShd( string path , string name )
//	{
//		FileStream fs = File.OpenRead( path );
//
//		byte[] bytes = new byte[ fs.Length ];
//		fs.Read( bytes , 0 , (int)fs.Length );
//
//		int count = 0;
//		for ( int j = 0 ; j < W3MapManager.instance.height * 4 ; j++ )
//		{
//			for ( int i = 0 ; i < W3MapManager.instance.width * 4 ; i++ )
//			{
//				if ( bytes[ count ] != 0 )
//				{
//					W3TerrainManager.instance.updateShadow( i , j );
//				}
//				count++;
//			}
//		}
//
//		W3TerrainManager.instance.updateShadows();
//	}
//
//	public void loadMapDoo( string path , string name )
//	{
//		SecurityParser parser = new SecurityParser();
//
//		FileStream fs = File.OpenRead( path );
//		byte[] bytes = new byte[ fs.Length ];
//		fs.Read( bytes , 0 , (int)fs.Length );
//
//		parser.LoadXml( Encoding.UTF8.GetString( bytes ).Trim() );
//
//		SecurityElement node = parser.ToXml();
//
//		if ( node == null || node.Children == null )
//		{
//			return;
//		}
//
//		foreach( SecurityElement nodeList in node.Children )
//		{
//			if ( nodeList.Tag == "Tree" )
//			{
//				if ( nodeList.Children == null )
//				{
//					return;
//				}
//
//				foreach( SecurityElement nodeList1 in nodeList.Children )
//				{
//					if ( nodeList1.Tag == "t" )
//					{
//						string id = nodeList1.Attribute( "id" );
//						int variation = int.Parse( nodeList1.Attribute( "Variation" ) );
//
//						float x = float.Parse( nodeList1.Attribute( "x" ) );
//						float y = float.Parse( nodeList1.Attribute( "y" ) );
//						float z = float.Parse( nodeList1.Attribute( "z" ) );
//
//						float a = float.Parse( nodeList1.Attribute( "a" ) );
//
//						float gx = ( offsetX - x ) / 128 * 4.0f;
//						float gy = z / 128 * 4.0f;
//						float gz = ( offsetY - y ) / 128 * 4.0f;
//
//						W3DoodadsConfigData d = W3DoodadsConfig.instance.getData( id );
//						W3DestructableDataConfigData d1 = W3DestructableDataConfig.instance.getData( id );
//
//						if ( d1 != null )
//						{
//							string name1 = "Prefabs\\" + d1.dir + "\\" + d1.file + "\\" + d1.file + ( d1.numVar > 1 ? "0" : "" );
//							Debug.Log( name1 );
//
//							try
//							{
//								GameObject obj = (GameObject)Instantiate( (GameObject)Resources.Load( name1 ) );
//								obj.transform.position = new Vector3( gx , gy , gz );
//								obj.transform.localEulerAngles = new Vector3( 0.0f , -90.0f - a * Mathf.Rad2Deg , 0.0f );
//							}
//							catch ( Exception ex )
//							{
//								Debug.LogError( name1 );
//							}
//						}
//
//						if ( d != null )
//						{
//							string name1 = "Prefabs\\" + d.dir + "\\" + d.file + "\\" + d.file + ( d.numVar > 1 ? "0" : "" );
//							Debug.Log( name1 );
//
//							try
//							{
//								GameObject obj = (GameObject)Instantiate( (GameObject)Resources.Load( name1 ) );
//								obj.transform.position = new Vector3( gx , gy , gz );
//								obj.transform.localEulerAngles = new Vector3( 0.0f , -90.0f - a * Mathf.Rad2Deg , 0.0f );
//							}
//							catch ( Exception ex )
//							{
//								Debug.LogError( name1 );
//							}
//						}
//
////						return;
//					}
//				}
//			}
//		}
//
//		W3TerrainManager.instance.updateTerrainSprite();
//
//
//	}
//
//
//
//
}

