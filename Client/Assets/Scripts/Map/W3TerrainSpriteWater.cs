using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;


public class W3TerrainSpriteWater : MonoBehaviour
{
    MeshFilter meshFilter = null;
    MeshRenderer meshRender = null;
    MeshCollider meshCollider = null;

    bool inited = false;

    int posX0 = 0;
    int posZ0 = 0;

    int posX1 = 0;
    int posZ1 = 0;

    static Material materialShoreLine = null;
    static bool shoreLineAlpha = false;
    GameObject shoreLine = null;

    Vector3[] vertices;
    Vector4[] uv0;
    Vector2[] uv1;
    int[] triangles;

    void Start()
    {
    }

    static GameObject spriteObject = null;
    static GameObject terrainObject = null;
    public static GameObject Create()
    {
        if ( spriteObject == null )
        {
            spriteObject = (GameObject)Resources.Load( "W3TerrainSpriteWater" );
            DontDestroyOnLoad( spriteObject );

            GameObject o = GameObject.Find( "Map" );
            terrainObject = o.transform.Find( "Terrain" ).gameObject;
        }

        GameObject obj = (GameObject)Instantiate( spriteObject , terrainObject.transform );
        return obj;
    }


    public void initSprite( int x0 , int z0 , int size )
    {
        if ( !inited )
        {
            float width = GameDefine.TERRAIN_SIZE;

            meshFilter = GetComponent<MeshFilter>();
            meshRender = GetComponent<MeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();

            posX0 = x0;
            posZ0 = z0;

            Mesh mesh = new Mesh();
            mesh.name = "W3TerrainSpriteWater" + "_" + x0 + "_" + z0;

            posX1 = x0 + size;
            if ( posX1 > W3TerrainManager.instance.width )
            {
                posX1 = W3TerrainManager.instance.width;
            }
            posZ1 = z0 + size;
            if ( posZ1 > W3TerrainManager.instance.height )
            {
                posZ1 = W3TerrainManager.instance.height;
            }

            if ( posX0 == posX1 || posZ0 == posZ1 )
            {
                return;
            }

            int sx = posX1 - posX0;
            int sz = posZ1 - posZ0;

            // 3---2
            // | / |
            // 1---0

            int c = 0;
            for ( int i = 0 ; i < sz ; i++ )
            {
                for ( int j = 0 ; j < sx ; j++ )
                {
                    W3TerrainNode node = W3TerrainManager.instance.nodes[ posZ0 + i ][ posX0 + j ];

                    if ( node.waterType != 0 )
                        c++;
                }
            }

            if ( c == 0 )
            {
                return;
            }

            int sizeV = 4 * c;
            int sizeT = 6 * c;

            uv0 = new Vector4[ sizeV ];
            uv1 = new Vector2[ sizeV ];
            vertices = new Vector3[ sizeV ];
            triangles = new int[ sizeT ];


            c = 0;
            for ( int i = 0 ; i < sz ; i++ )
            {
                for ( int j = 0 ; j < sx ; j++ )
                {
                    W3TerrainNode node = W3TerrainManager.instance.nodes[ posZ0 + i ][ posX0 + j ];

                    if ( node.waterType == 0 )
                    {
                        continue;
                    }

                    uv0[ 0 + c * 4 ].x = 1.0f;
                    uv0[ 0 + c * 4 ].y = 0.0f;
                    uv0[ 1 + c * 4 ].x = 0.0f;
                    uv0[ 1 + c * 4 ].y = 0.0f;
                    uv0[ 2 + c * 4 ].x = 1.0f;
                    uv0[ 2 + c * 4 ].y = 1.0f;
                    uv0[ 3 + c * 4 ].x = 0.0f;
                    uv0[ 3 + c * 4 ].y = 1.0f;

                    uv0[ 0 + c * 4 ].z = node.waterColor[ 0 ].x;
                    uv0[ 0 + c * 4 ].w = node.waterColor[ 0 ].y;
                    uv0[ 1 + c * 4 ].z = node.waterColor[ 1 ].x;
                    uv0[ 1 + c * 4 ].w = node.waterColor[ 1 ].y;
                    uv0[ 2 + c * 4 ].z = node.waterColor[ 2 ].x;
                    uv0[ 2 + c * 4 ].w = node.waterColor[ 2 ].y;
                    uv0[ 3 + c * 4 ].z = node.waterColor[ 3 ].x;
                    uv0[ 3 + c * 4 ].w = node.waterColor[ 3 ].y;

                    uv1[ 0 + c * 4 ].x = (float)( posX0 + j + 1 ) / W3TerrainManager.instance.width;
                    uv1[ 0 + c * 4 ].y = (float)( posZ0 + i ) / W3TerrainManager.instance.height;
                    uv1[ 1 + c * 4 ].x = (float)( posX0 + j ) / W3TerrainManager.instance.width;
                    uv1[ 1 + c * 4 ].y = (float)( posZ0 + i ) / W3TerrainManager.instance.height;
                    uv1[ 2 + c * 4 ].x = (float)( posX0 + j + 1 ) / W3TerrainManager.instance.width;
                    uv1[ 2 + c * 4 ].y = (float)( posZ0 + i + 1 ) / W3TerrainManager.instance.height;
                    uv1[ 3 + c * 4 ].x = (float)( posX0 + j ) / W3TerrainManager.instance.width;
                    uv1[ 3 + c * 4 ].y = (float)( posZ0 + i + 1 ) / W3TerrainManager.instance.height;

                    float ox = j * width;
                    float oz = i * width;

                    vertices[ 0 + c * 4 ].x = -width - ox;
                    vertices[ 0 + c * 4 ].y = node.waterY;
                    vertices[ 0 + c * 4 ].z = -oz;

                    vertices[ 1 + c * 4 ].x = -ox;
                    vertices[ 1 + c * 4 ].y = node.waterY;
                    vertices[ 1 + c * 4 ].z = -oz;

                    vertices[ 2 + c * 4 ].x = -width - ox;
                    vertices[ 2 + c * 4 ].y = node.waterY;
                    vertices[ 2 + c * 4 ].z = -width - oz;

                    vertices[ 3 + c * 4 ].x = -ox;
                    vertices[ 3 + c * 4 ].y = node.waterY;
                    vertices[ 3 + c * 4 ].z = -width - oz;

                    triangles[ 0 + c * 6 ] = 0 + c * 4;
                    triangles[ 1 + c * 6 ] = 1 + c * 4;
                    triangles[ 2 + c * 6 ] = 2 + c * 4;
                    triangles[ 3 + c * 6 ] = 2 + c * 4;
                    triangles[ 4 + c * 6 ] = 1 + c * 4;
                    triangles[ 5 + c * 6 ] = 3 + c * 4;

                    if ( node.waterShoreLine >= 0 )
                    {
//                         shoreLine = (GameObject)GameObject.Instantiate( (GameObject)Resources.Load( "Prefabs/Doodads/LordaeronSummer/Water/ShorelineWave/ShorelineWave0" ) );
//                         shoreLine.transform.parent = transform;
//                         shoreLine.transform.localEulerAngles = new Vector3( 0.0f , 45.0f * node.waterShoreLine , 0.0f );
// 
//                         switch ( node.waterShoreLine )
//                         {
//                             case 3:
//                                 shoreLine.transform.localPosition = new Vector3( -ox - 4.0f , node.waterY + 0.1f , -oz );
//                                 break;
//                             case 5:
//                                 shoreLine.transform.localPosition = new Vector3( -ox , node.waterY + 0.1f , -oz );
//                                 break;
//                             case 1:
//                                 shoreLine.transform.localPosition = new Vector3( -ox - 4.0f , node.waterY + 0.1f , -oz - 4.0f );
//                                 break;
//                             case 7:
//                                 shoreLine.transform.localPosition = new Vector3( -ox , node.waterY + 0.1f , -oz - 4.0f );
//                                 break;
//                             case 2:
//                             case 4:
//                             case 6:
//                             case 0:
//                                 shoreLine.transform.localPosition = new Vector3( -ox - 2.0f , node.waterY + 0.1f , -oz - 2.0f );
//                                 break;
//                         }

                        if ( shoreLine != null && !shoreLineAlpha )
                        {
//                             shoreLineAlpha = true;
//                             shoreLine.GetComponent<W3AnimationController>().alpha = true;
//                             shoreLine.GetComponent<Animation>().cullingType = AnimationCullingType.AlwaysAnimate;
//                            
//                             materialShoreLine = (Material)GameObject.Instantiate( (Material)Resources.Load( "Prefabs/Doodads/LordaeronSummer/Water/ShorelineWave/Shoreline" ) );
                        }

//                         shoreLine.GetComponentInChildren<Renderer>().sharedMaterial = materialShoreLine;
                    }

                    c++;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.SetUVs( 0 , new List<Vector4>( uv0 ) );
            mesh.uv2 = uv1;
            meshFilter.mesh = mesh;

            inited = true;

            uv0 = null;
            uv1 = null;
            vertices = null;
            triangles = null;

            W3WaterManager.instance.materialObj = meshRender.sharedMaterial;

 //           meshCollider.sharedMesh = mesh;
        }
    }


    

}


