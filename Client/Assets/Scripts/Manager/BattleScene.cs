using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;


public class BattleScene : SingletonMono< BattleScene >
{
    public void unloadScene()
    {

    }

    public void loadScene()
    {
        loadMapData();
    }

    void loadMapData()
    {
        TextAsset dat = Resources.Load( "Map/" + W3MapManager.instance.mapFile ) as TextAsset;
        byte[] bytes = dat.bytes;

        int startIndex = 3;
        int version = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
        W3TerrainManager.instance.mainTitleSet = Encoding.ASCII.GetString( bytes , startIndex , 1 ); startIndex += 1;
        W3TerrainManager.instance.customTileSetIsUsed = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;

        W3TerrainManager.instance.groundTileSetsCount = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
        W3TerrainManager.instance.groundTileSets = new string[ 16 ];
        for ( int i = 0 ; i < W3TerrainManager.instance.groundTileSetsCount ; ++i )
        {
            W3TerrainManager.instance.groundTileSets[ i ] = Encoding.ASCII.GetString( bytes , startIndex , 4 ); startIndex += 4;
        }

        W3TerrainManager.instance.cliffTileSetsCount = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
        W3TerrainManager.instance.cliffTileSets = new string[ 16 ];
        for ( int i = 0 ; i < W3TerrainManager.instance.cliffTileSetsCount ; ++i )
        {
            W3TerrainManager.instance.cliffTileSets[ i ] = Encoding.ASCII.GetString( bytes , startIndex , 4 ); startIndex += 4;
        }

        int width = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
        int height = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;

        W3TerrainManager.instance.offsetX = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
        W3TerrainManager.instance.offsetY = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;

        W3TerrainManager.instance.createMap( width - 1 , height - 1 );
        W3FogManager.instance.createMap( width - 1 , height - 1 );

        UnityEngine.Debug.Log( "createMap " + width + " " + height );

        W3TerrainManager.instance.createTerrainTextures();

        for ( int i = 0 ; i < height ; i++ )
        {
            for ( int j = 0 ; j < width ; j++ )
            {
                float v1 = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                int layerHeight = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                int waterLevel = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                int flags = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;

                W3TerrainManager.instance.setTerrainNode1( j , i , v1 , (sbyte)layerHeight , (short)waterLevel , (byte)flags );
            }
        }

        W3TerrainManager.instance.updateTerrainSmallNode();

        W3WaterManager.instance.initWaterTextures();

        // load cliff
        for ( int i = 0 ; i < height - 1 ; i++ )
        {
            for ( int j = 0 ; j < width - 1 ; j++ )
            {
                int textureTypeSize = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                int shadows = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;

                byte[] type = new byte[ textureTypeSize ];
                byte[] uvs = new byte[ textureTypeSize ];

                for ( int k = 0 ; k < textureTypeSize ; k++ )
                {
                    type[ k ] = (byte)BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                    uvs[ k ] = (byte)BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                }

                W3TerrainManager.instance.setTerrainTexture1( j , i , (byte)textureTypeSize , type , uvs , (byte)shadows );

                byte cliff = (byte)BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                if ( cliff < 16 )
                {
                    string name = Encoding.ASCII.GetString( bytes , startIndex , 4 ); startIndex += 4;

                    float y = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    int verticesCount = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                    float[] vz = new float[ verticesCount ];
                    for ( int k = 0 ; k < verticesCount ; k++ )
                    {
                        vz[ k ] = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    }


                    W3TerrainManager.instance.updateCliffs( j , i , name , y , cliff , vz );
                }

                byte cliffTrans = (byte)BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                if ( cliffTrans < 16 )
                {
                    string name = Encoding.ASCII.GetString( bytes , startIndex , 4 ); startIndex += 4;

                    float y = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    int verticesCount = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                    float[] vz = new float[ verticesCount ];
                    for ( int k = 0 ; k < verticesCount ; k++ )
                    {
                        vz[ k ] = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    }

                    W3TerrainManager.instance.updateCliffsTrans( j , i , name , y , cliffTrans , vz );
                }

                int water = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
                if ( water > 0 )
                {
                    float y = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;

                    Vector2[] c = new Vector2[ 4 ];
                    c[ 0 ].x = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    c[ 0 ].y = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    c[ 1 ].x = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    c[ 1 ].y = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    c[ 2 ].x = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    c[ 2 ].y = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    c[ 3 ].x = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
                    c[ 3 ].y = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;

                    int shoreLine = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;

                    W3TerrainManager.instance.setTerrainWater1( j , i , (byte)water , y , c , (sbyte)shoreLine );
                }
            }
        }

        UnityEngine.Debug.Log( "createMapSprites " );

        W3TerrainManager.instance.createMapSprites();

        // load shadow
        int index1 = 0;
        for ( int i = 0 ; i < W3FogManager.instance.fogHeight ; i++ )
        {
            for ( int j = 0 ; j < W3FogManager.instance.fogWidth ; j++ )
            {
                byte sa = W3ShadowConfig.instance.getData( bytes[ startIndex ] ); startIndex++;

                W3TerrainManager.instance.smallNodes[ i ][ j ].path = bytes[ startIndex ];
                startIndex++;

                W3FogManager.instance.shadowBuffer[ index1 ] = sa;
                W3FogManager.instance.fogPixelBuffer[ index1 ] = 64;

                index1++;
            }
        }

        UnityEngine.Debug.Log( "W3PathFinder.instance.initPathRegion " );

        // load path
        W3PathFinder.instance.initPathRegion( W3FogManager.instance.fogWidth , W3FogManager.instance.fogHeight );


        UnityEngine.Debug.Log( "load doo " );

        // load doo
        Transform objTrans = GameObject.Find( "Map" ).transform.Find( "Objects" ).transform;
        Transform DestructableObjTrans = GameObject.Find( "Map" ).transform.Find( "DestructableObjects" ).transform;


#if UNITY_BLOCK_TEST

        GameObject terrainBlock = Resources.Load<GameObject>( "W3TerrainBlock" );

        for ( int i = 0 ; i < ( height - 1 ) * 4 ; i++ )
        {
            for ( int j = 0 ; j < ( width - 1 ) * 4 ; j++ )
            {
                if ( !W3TerrainManager.instance.isPath( j , i , W3PathType.NOWALK ) )
                {
                    GameObject ob = Instantiate( terrainBlock );
                    ob.transform.parent = objTrans;
                    ob.transform.position = new Vector3( -j * 32 - 16 ,
                        W3TerrainManager.instance.smallNodes[ i ][ j ].ym + 10 ,
                        -i * 32 - 16 );

                    W3TerrainManager.instance.terrainBlocks[ i ][ j ] = ob.GetComponent<W3TerrainBlock>();
                    W3TerrainManager.instance.terrainBlocks[ i ][ j ].setBlock( W3TerrainManager.instance.smallNodes[ i ][ j ].path );
                }
            }
        }
#endif

        int dooNum = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;

        for ( int i = 0 ; i < dooNum ; i++ )
        {
            string id = Encoding.ASCII.GetString( bytes , startIndex , 4 ); startIndex += 4;
            int variation = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;

            float x = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
            float y = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
            float z = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;

            float a = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;

            float xs = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
            float ys = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;
            float zs = BitConverter.ToSingle( bytes , startIndex ); startIndex += 4;

            int flags = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;
            int life = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;

            int doodadID = BitConverter.ToInt32( bytes , startIndex ); startIndex += 4;

            W3DoodadsConfigData d = W3DoodadsConfig.instance.getData( id );
            W3DestructableDataConfigData d1 = W3DestructableDataConfig.instance.getData( id );

            GameObject obj = null;

            if ( d1 != null )
            {
                string name1 = "Prefabs\\" + d1.file + ( d1.numVar > 1 ? variation.ToString() : "" );

                try
                {
                    obj = (GameObject)Instantiate( (GameObject)Resources.Load( name1 ) , DestructableObjTrans );
                    obj.transform.position = new Vector3( x , y , z );
                    obj.transform.localEulerAngles = new Vector3( 0.0f , a , 0.0f );
                    //                     obj.transform.localScale = new Vector3( xs , ys , zs );

                    if ( d1.pathTex.Contains( "PathTextures" ) )
                    {
                        Texture2D pathTex = (Texture2D)Resources.Load( d1.pathTex );

                        int w0 = pathTex.width / 2;
                        int h0 = pathTex.height / 2;

                        for ( int i0 = 0 ; i0 < pathTex.height ; i0++ )
                        {
                            for ( int j0 = 0 ; j0 < pathTex.width ; j0++ )
                            {
                                Color c = pathTex.GetPixel( j0 , i0 );
                                W3TerrainManager.instance.setPath( (int)-x / GameDefine.TERRAIN_SIZE_PER - w0 + j0 , (int)-z / GameDefine.TERRAIN_SIZE_PER - h0 + i0 , c );
                                W3TerrainManager.instance.setDoodadID( (int)-x / GameDefine.TERRAIN_SIZE_PER - w0 + j0 , (int)-z / GameDefine.TERRAIN_SIZE_PER - h0 + i0 , doodadID );
                            }
                        }
                    }


                    W3Base w3base = obj.GetComponent<W3Base>();
                    w3base.updateFogUV();
                }
                catch ( Exception ex )
                {
                    UnityEngine.Debug.LogError( name1 );
                }
            }

            if ( d != null )
            {
                string name1 = "Prefabs\\" + d.file + ( d.numVar > 1 ? variation.ToString() : "" );

                try
                {
                    obj = (GameObject)Instantiate( (GameObject)Resources.Load( name1 ) , objTrans );
                    obj.transform.position = new Vector3( x , y , z );
                    obj.transform.localEulerAngles = new Vector3( 0.0f , a , 0.0f );
                    //                     obj.transform.localScale = new Vector3( xs , ys , zs );

                    if ( d.pathTex.Contains( "PathTextures" ) )
                    {
                        Texture2D pathTex = (Texture2D)Resources.Load( d.pathTex );

                        int w0 = pathTex.width / 2;
                        int h0 = pathTex.height / 2;

                        for ( int i0 = 0 ; i0 < pathTex.height ; i0++ )
                        {
                            for ( int j0 = 0 ; j0 < pathTex.width ; j0++ )
                            {
                                Color c = pathTex.GetPixel( j0 , i0 );
                                W3TerrainManager.instance.setPath( (int)-x / GameDefine.TERRAIN_SIZE_PER - w0 + j0 , (int)-z / GameDefine.TERRAIN_SIZE_PER - h0 + i0 , c );
                                W3TerrainManager.instance.setDoodadID( (int)-x / GameDefine.TERRAIN_SIZE_PER - w0 + j0 , (int)-z / GameDefine.TERRAIN_SIZE_PER - h0 + i0 , doodadID );
                            }
                        }
                    }

                    W3Base w3base = obj.GetComponent<W3Base>();
                    w3base.updateFogUV();
                }
                catch ( Exception ex )
                {
                    UnityEngine.Debug.LogError( name1 );
                }
            }

            if ( obj == null )
            {
                obj = GameObject.CreatePrimitive( PrimitiveType.Cube );
                obj.transform.position = new Vector3( x , y , z );
                obj.transform.localEulerAngles = new Vector3( 0.0f , -90.0f - a * Mathf.Rad2Deg , 0.0f );
                //                 obj.transform.localScale = new Vector3( xs , ys , zs );

                UnityEngine.Debug.LogWarning( "doo id " + id );
            }
            else
            {
                W3ReplaceableTexture[] rts = obj.GetComponentsInChildren<W3ReplaceableTexture>();

                if ( rts != null )
                {
                    for ( int i0 = 0 ; i0 < rts.Length ; i0++ )
                    {
                        MeshRenderer r = rts[ i0 ].GetComponent<MeshRenderer>();
                        SkinnedMeshRenderer r1 = rts[ i0 ].GetComponent<SkinnedMeshRenderer>();

                        if ( r != null )
                        {
                            if ( r.sharedMaterial == null )
                            {
                                r.sharedMaterial = W3MaterialConfig.instance.getMaterial( d1.texFile , d1.texFile , "W3/MeshBaseAlphaCutoff" );
                            }
                            else
                            {
                                r1.sharedMaterial.mainTexture = (Texture2D)Resources.Load( d1.texFile );
                            }
                        }
                        else if ( r1 != null )
                        {
                            if ( r1.sharedMaterial == null )
                            {
                                r1.sharedMaterial = W3MaterialConfig.instance.getMaterial( d1.texFile , d1.texFile , "W3/SkinnedMeshBaseAlphaCutoff" );
                            }
                            else
                            {
                                r1.sharedMaterial.mainTexture = (Texture2D)Resources.Load( d1.texFile );
                            }
                        }
                    }
                }

            }

            W3BaseManager.instance.addDoodad( doodadID , obj.GetComponent<W3Doodad>() );
        }

        UnityEngine.Debug.Log( "W3PathFinder.instance.initMap" );

        // init path
        W3PathFinder.instance.initMap( (ushort)W3FogManager.instance.fogWidth ,
            (ushort)W3FogManager.instance.fogHeight );


        Shader.SetGlobalColor( "W3ColorDay0" , W3FogManager.instance.dayColor );


        W3FogManager.instance.shadowTex.LoadRawTextureData( W3FogManager.instance.shadowBuffer );
        W3FogManager.instance.shadowTex.Apply( false );

        W3FogManager.instance.fogTex.LoadRawTextureData( W3FogManager.instance.fogPixelBuffer );
        W3FogManager.instance.fogTex.Apply( false );

        Shader.SetGlobalTexture( "W3FogTex0" , W3FogManager.instance.fogTex );
        Shader.SetGlobalTexture( "W3ShadowTex0" , W3FogManager.instance.shadowTex );

        W3FogManager.instance.calculateFog();


        // create town hall

        W3UnitManager.instance.createUnit( W3PlayerManager.instance.localPlayer , GameDefine.TOWN_HALL ,
            W3MapManager.instance.startLocation[ 0 ].x , W3MapManager.instance.startLocation[ 0 ].y , 0 , false );

        float gx = ( W3TerrainManager.instance.offsetX - (float)W3MapManager.instance.startLocation[ 0 ].x );
        float gz = ( W3TerrainManager.instance.offsetY - (float)W3MapManager.instance.startLocation[ 0 ].y );

        W3CameraMovement.instance.LookAt( new Vector3( gx , 0 , gz ) );

        Resources.UnloadUnusedAssets();
    }

}

