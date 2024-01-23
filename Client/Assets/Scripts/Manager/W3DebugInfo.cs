using UnityEngine;
// using UnityEditor;
using System.Collections;

public class W3DebugInfo : MonoBehaviour
{

    long frameCount = 0;
    long lastFrameTime = 0;
    long lastFps = 0;

    void Start()
    {
        lineMaterial = new Material( Shader.Find( "Mobile/Particles/Alpha Blended" ) );
        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
    }

    void Update()
    {
        UpdateTick();
    }

    void OnGUI()
    {
        DrawFps();
    }

    Material lineMaterial;


    void OnPostRender()
    {
        GL.PushMatrix();

//        lineMaterial.SetPass( 0 );

        GL.Begin( GL.LINES );

        for ( int i = 0 ; i < GameDefine.MAX_PLAYER_SLOTS ; i++ )
        {
            for ( int j = 0 ; j < W3PlayerManager.instance.players[ i ].units.Count ; j++ )
            {
                W3PlayerManager.instance.players[ i ].units[ j ].drawLine();
            }
        }

        GL.End();
        GL.PopMatrix();
    }

    private void DrawFps()
    {
        GUI.color = new Color( 1.0f , 0 , 0 );
        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;
        bb.normal.textColor = new Color( 1.0f , 0.5f , 0.0f );
        bb.fontSize = 20;
        GUI.Label( new Rect( ( Screen.width ) - 150 , 0 , 200 , 200 ) , "FPS: " + lastFps , bb );

//         for ( int i = 0 ; i < SceneView.GetAllSceneCameras().Length ; i++ )
//         {
//             Camera c = SceneView.GetAllSceneCameras()[ i ];
//             float a = c.fieldOfView;
// 
//             Debug.Log( "a " + a + " p " + c.transform.position.x + " " + c.transform.position.y + " " + c.transform.position.z + " r " 
//                 + c.transform.rotation.x + " " + c.transform.rotation.y + " " + c.transform.rotation.z );
// 
//         }

    }

    
    private void UpdateTick()
    {
        frameCount++;

        long nCurTime = TickToMilliSec( System.DateTime.Now.Ticks );

        if ( lastFrameTime == 0 )
        {
            lastFrameTime = TickToMilliSec( System.DateTime.Now.Ticks );
        }


        if ( ( nCurTime - lastFrameTime ) >= 1000 )
        {
            long fps = (long)( frameCount * 1.0f / ( ( nCurTime - lastFrameTime ) / 1000.0f ) );
            lastFps = fps;
            frameCount = 0;
            lastFrameTime = nCurTime;
        }
    }

    public long TickToMilliSec( long tick )
    {
        return tick / ( 10 * 1000 );
    }

}
