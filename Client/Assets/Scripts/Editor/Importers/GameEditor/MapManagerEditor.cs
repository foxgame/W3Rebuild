using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor( typeof( W3TerrainManager ) )]
public class MapManagerEditor : Editor
{

    void OnSceneGUI()
    {
        if ( W3TerrainManager.instance && 
            W3TerrainManager.instance.isLoaded )
        {
            Ray ray = HandleUtility.GUIPointToWorldRay( Event.current.mousePosition );
            RaycastHit rayHit = new RaycastHit();

            if ( Physics.Raycast( ray , out rayHit ) )
            {
                float CamDist = Vector3.Distance( Camera.current.transform.position , rayHit.point );

                W3TerrainSmallNode tsn = W3TerrainManager.instance.getSmallNode( (int)-rayHit.point.x / GameDefine.TERRAIN_SIZE_PER , (int)-rayHit.point.z / GameDefine.TERRAIN_SIZE_PER );
                W3TerrainNode tn = W3TerrainManager.instance.getNode( (int)-rayHit.point.x / GameDefine.TERRAIN_SIZE , (int)-rayHit.point.z / GameDefine.TERRAIN_SIZE );
                W3TerrainNode tna = W3TerrainManager.instance.getNode( (int)-rayHit.point.x / GameDefine.TERRAIN_SIZE , (int)-rayHit.point.z / GameDefine.TERRAIN_SIZE + 1 );
                W3TerrainNode tnb = W3TerrainManager.instance.getNode( (int)-rayHit.point.x / GameDefine.TERRAIN_SIZE + 1 , (int)-rayHit.point.z / GameDefine.TERRAIN_SIZE + 1 );
                W3TerrainNode tnc = W3TerrainManager.instance.getNode( (int)-rayHit.point.x / GameDefine.TERRAIN_SIZE + 1 , (int)-rayHit.point.z / GameDefine.TERRAIN_SIZE );

                float y = 0.0f;

                if ( -rayHit.point.x / 128.0f - tna.x + tna.z + rayHit.point.z / 128.0f > 1.0f )
                {
                    y = ( -rayHit.point.z / 128.0f - tnc.z ) * ( tnb.y - tnc.y ) +
                    ( tnc.x + rayHit.point.x / 128.0f ) * ( tn.y - tnc.y ) + tnc.y;
                }
                else
                {
                    y = ( -rayHit.point.x / 128.0f - tna.x ) * ( tnb.y - tna.y ) +
                    ( tna.z + rayHit.point.z / 128.0f ) * ( tn.y - tna.y ) + tna.y;
                }

                unsafe
                {
                    byte b = W3PathFinder.instance.block[ tsn.z ][ tsn.x ];

                    byte fog = W3FogManager.instance.fogBuffer[ tsn.z * W3FogManager.instance.fogWidth + tsn.x ];
                    byte fog3 = W3FogManager.instance.fogPixelBuffer[ tsn.z * W3FogManager.instance.fogWidth + tsn.x ];

                    Handles.Label( rayHit.point + Camera.current.transform.rotation * new Vector3( 0.025f , 0.150f , 0f ) * CamDist , "mouse: " + (int)-rayHit.point.x + " " + (int)-rayHit.point.z + " " + y , EditorStyles.whiteLargeLabel );

                    Handles.Label( rayHit.point + Camera.current.transform.rotation * new Vector3( 0.025f , 0.130f , 0f ) * CamDist , "S X: " + Mathf.Floor( tsn.x ).ToString( "f0" ) + " Z: " + Mathf.Floor( tsn.z ).ToString( "f0" ) + " Y: " + tsn.y + " YM:" + tsn.ym + " RY: " + rayHit.point.y , EditorStyles.whiteLargeLabel );
                    Handles.Label( rayHit.point + Camera.current.transform.rotation * new Vector3( 0.025f , 0.110f , 0f ) * CamDist , "path: " + tsn.path + " " + b + " " + W3PathFinder.instance.region[ tsn.z ][ tsn.x ] + " fog: " + fog.ToString() + " " + fog3.ToString() , EditorStyles.whiteLargeLabel );
                    Handles.Label( rayHit.point + Camera.current.transform.rotation * new Vector3( 0.025f , 0.090f , 0f ) * CamDist , " doodad: " + tsn.doodadID.ToString() , EditorStyles.whiteLargeLabel );
                    Handles.Label( rayHit.point + Camera.current.transform.rotation * new Vector3( 0.025f , 0.060f , 0f ) * CamDist , "unit: " + W3PathFinder.instance.getUnitID( tsn.x , tsn.z ) + " " + tsn.unitHeight.ToString() , EditorStyles.whiteLargeLabel );

                    Handles.Label( rayHit.point + Camera.current.transform.rotation * new Vector3( 0.025f , 0.040f , 0f ) * CamDist , "N X: " + Mathf.Floor( tn.x ).ToString( "f0" ) + " Y:" + tn.y + " Z: " + Mathf.Floor( tn.z ).ToString( "f0" ) , EditorStyles.whiteLargeLabel );
                    Handles.Label( rayHit.point + Camera.current.transform.rotation * new Vector3( 0.025f , 0.010f , 0f ) * CamDist , "layer: " + tn.layerHeight , EditorStyles.whiteLargeLabel );

                }

            }

        }
    }

}

