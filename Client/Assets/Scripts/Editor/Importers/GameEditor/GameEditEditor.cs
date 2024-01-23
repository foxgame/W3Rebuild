using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System;




public class GameEditEditor : EditorWindow
{
	
	static private GameEditEditor mInstance = null;
	static public GameEditEditor instance
	{
		get
		{
			return mInstance;
		}
	}


	void OnGUI()
	{
		if ( !W3GameManager.instance ) 
		{
			GUI.color = Color.red;
			GUILayout.Label( "Please Start WorldEditor Scene." , EditorStyles.boldLabel );
			GUI.color = Color.white;

			return;
		}

		if ( mInstance == null )
		{
			mInstance = this;
		}
		
		GUI.color = Color.green;
		EditorGUILayout.LabelField( "Map Editor" , EditorStyles.boldLabel );
		GUI.color = Color.white;

		EditorGUILayout.BeginHorizontal();

		if ( GUILayout.Button( "load map" ) )
		{
			
		}

		if ( GUILayout.Button( "save map" ) )
		{
			
		}

		EditorGUILayout.EndHorizontal();



//		EditorGUILayout.BeginHorizontal();
//		GUILayout.Label( "texture Info up 25 down 25" );
//		EditorGUILayout.EndHorizontal();
//
////		ushort textureIndexLast = textureIndex;
//		textureIndex = (ushort)EditorGUILayout.IntField( textureIndex );

//		if ( textureIndexLast != textureIndex )
//		{
//			for ( ushort i = (ushort)( textureIndex - 25 ); i < textureIndex ; i++ )
//			{
//				SATexture texture = SATextureConfig.instance.getTerrainTexture( i );
//
//				if ( texture != null )
//				{
//					texture.releaseData();
//				}
//			}
//
//			for ( ushort i = textureIndex ; i < textureIndex + (ushort)25 ; i++ )
//			{
//				SATexture texture = SATextureConfig.instance.getTerrainTexture( i );
//
//				if ( texture != null )
//				{
//					texture.releaseData();
//				}
//			}
//		}


//		scrollInt = EditorGUILayout.IntField( scrollInt );
//
//		scrollPosition = EditorGUILayout.BeginScrollView( scrollPosition );
//
//		int pos = 0;
//
//		EditorGUILayout.BeginHorizontal();

//		for ( ushort i = (ushort)( textureIndex - scrollInt ); i < textureIndex ; i++ )
//		{
//			if ( pos % 3 == 0 )
//			{
//				EditorGUILayout.EndHorizontal();
//				EditorGUILayout.BeginHorizontal();
//			}
//
//			SATexture texture = SATextureConfig.instance.getTerrainTexture( i );
//
//			if ( texture != null )
//			{
//				texture.initData();
//				texture.loadData();
//				EditorGUILayout.ObjectField( i.ToString() , texture.texture2D , typeof( Texture2D ) , true );
//
//				pos++;
//			}
//		}
//
//		for ( ushort i = textureIndex ; i < textureIndex + (ushort)scrollInt ; i++ )
//		{
//			if ( pos % 3 == 0 )
//			{
//				EditorGUILayout.EndHorizontal();
//				EditorGUILayout.BeginHorizontal();
//			}
//
//			SATexture texture = SATextureConfig.instance.getTerrainTexture( i );
//
//			if ( texture != null )
//			{
//				texture.initData();
//				texture.loadData();
//				EditorGUILayout.ObjectField( i.ToString() , texture.texture2D , typeof( Texture2D ) , true );
//
//				pos++;
//			}
//		}

//		EditorGUILayout.EndHorizontal();
//
//		EditorGUILayout.EndScrollView();
	}

	public void reloadTerrain( int i , int j , ushort tID )
	{
		
	}

	public void reloadBuilding( int i , int j , ushort tID )
	{
		
	}

	public void saveMap( string path , string name )
	{
		
	}

	public void loadMap( string path , string name )
	{
		
	}


}

