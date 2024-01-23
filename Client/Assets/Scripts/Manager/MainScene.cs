using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;


public class MainScene : SingletonMono< MainScene >
{
    public void unloadScene()
    {

    }

    public void loadScene()
    {
        
    }

    void Start()
    {
//         lineMaterial = new Material( Shader.Find( "Particles/Alpha Blended" ) );
//         lineMaterial.hideFlags = HideFlags.HideAndDontSave;
//         lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
    }

    //     Material lineMaterial;
    // 
    //     void OnPostRender()
    //     {
    //         GL.PushMatrix();
    // 
    //         lineMaterial.SetPass( 0 );
    // 
    //         GL.Begin( GL.LINES );
    //         GL.Color( Color.green );
    // 
    //         GL.Vertex3( -3800 , 0 + 16 , -3246 );
    //         GL.Vertex3( -4000 , 0 + 16 , -3246 );
    // 
    // 
    //         GL.Vertex3( -3800 , 0 + 16 , -3446 );
    //         GL.Vertex3( -4000 , 0 + 16 , -3446 );
    // 
    //         GL.End();
    // 
    // 
    //         GL.PopMatrix();
    //     }

    string[] selStrings = { "Human" , "Undead" , "Orc" , "NightElf" , "Random" };

    void OnGUI()
    {
        GUI.Label( new Rect( 25 , 25 , 200 , 30 ) , "map name: " + W3MapManager.instance.mapFile );

        if ( GUI.Button( new Rect( 200 , 25 , 100 , 30 ) , "game start" ) )
        {
            W3GameSceneManager.instance.loadScene( GameSceneType.GST_BATTLE );
        }

        GUI.Label( new Rect( 25 , 150 , 200 , 30 ) , "player1: " );
        GUI.Label( new Rect( 25 , 180 , 200 , 30 ) , "player2: " );

        GUI.Label( new Rect( 125 , 150 , 200 , 30 ) , "race: " );
        GUI.Label( new Rect( 125 , 180 , 200 , 30 ) , "race: " );

        W3MapManager.instance.racePreference[ 0 ] = GUI.SelectionGrid( new Rect( 175 , 150 , 300 , 30 ) , W3MapManager.instance.racePreference[ 0 ] , selStrings , 5 );
        W3MapManager.instance.racePreference[ 1 ] = GUI.SelectionGrid( new Rect( 175 , 180 , 300 , 30 ) , W3MapManager.instance.racePreference[ 1 ] , selStrings , 5 );

        GUI.Label( new Rect( 500 , 150 , 200 , 30 ) , "color: " );
        GUI.Label( new Rect( 500 , 180 , 200 , 30 ) , "color: " );

        int.TryParse( GUI.TextField( new Rect( 530 , 150 , 30 , 25 ) , W3MapManager.instance.playerColor[ 0 ].ToString() ) , out W3MapManager.instance.playerColor[ 0 ] );
        int.TryParse( GUI.TextField( new Rect( 530 , 180 , 30 , 25 ) , W3MapManager.instance.playerColor[ 1 ].ToString() ) , out W3MapManager.instance.playerColor[ 1 ] );

    }




}

