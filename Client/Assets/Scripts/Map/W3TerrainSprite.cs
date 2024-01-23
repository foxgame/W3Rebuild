using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;


public class W3TerrainSprite : MonoBehaviour
{
    public MeshFilter meshFilter = null;
    public MeshRenderer meshRender = null;
    public MeshCollider meshCollider = null;

    bool inited = false;

    int posX0 = 0;
    int posZ0 = 0;

    int posX1 = 0;
    int posZ1 = 0;
    

    Vector3[] vertices;
    int[] triangles;
    Vector4[] uv0;
    Vector4[] uv1;
    Vector4[] uv2;


    void Start()
    {
    }

    static GameObject spriteObject = null;
    static GameObject terrainObject = null;
    public static GameObject Create()
    {
        if ( spriteObject == null )
        {
            spriteObject = (GameObject)Resources.Load( "W3TerrainSprite" );
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
            mesh.name = "W3SpriteMesh1" + "_" + x0 + "_" + z0;

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

                    if ( node.textureTypeSize != 0 )
                        c++;
                }
            }

            if ( c == 0 )
            {
                return;
            }

            int sizeV = 4 * c;
            int sizeT = 6 * c;

            vertices = new Vector3[ sizeV ];
            uv0 = new Vector4[ sizeV ];
            uv1 = new Vector4[ sizeV ];
            uv2 = new Vector4[ sizeV ];
            triangles = new int[ sizeT ];
            
            c = 0;
            for ( int i = 0 ; i < sz ; i++ )
            {
                for ( int j = 0 ; j < sx ; j++ )
                {
                    W3TerrainNode node = W3TerrainManager.instance.nodes[ posZ0 + i ][ posX0 + j ];

                    if ( node.textureTypeSize == 0 )
                    {
                        continue;
                    }
                    else
                    {
                        int n = node.textureUV[ 0 ] % 16;

                        Vector2 offset = W3UVConfig.instance.getUVOffset( node.textureType[ 0 ] , node.textureUV[ 0 ] );

                        uv0[ 0 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv0.x * 0.25f + offset.x - 0.001f;
                        uv0[ 0 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv0.y * 0.25f + offset.y + 0.001f;
                        uv0[ 1 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f + offset.x + 0.001f;
                        uv0[ 1 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f + offset.y + 0.001f;
                        uv0[ 2 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv2.x * 0.25f + offset.x - 0.001f;
                        uv0[ 2 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv2.y * 0.25f + offset.y - 0.001f;
                        uv0[ 3 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv3.x * 0.25f + offset.x + 0.001f;
                        uv0[ 3 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv3.y * 0.25f + offset.y - 0.001f;

                        if ( node.textureTypeSize > 1 )
                        {
                            n = node.textureUV[ 1 ] % 16;

                            offset = W3UVConfig.instance.getUVOffset( node.textureType[ 1 ] , node.textureUV[ 1 ] );

                            uv1[ 0 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv0.x * 0.25f + offset.x - 0.001f;
                            uv1[ 0 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv0.y * 0.25f + offset.y + 0.001f;
                            uv1[ 1 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f + offset.x + 0.001f;
                            uv1[ 1 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f + offset.y + 0.001f;
                            uv1[ 2 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv2.x * 0.25f + offset.x - 0.001f;
                            uv1[ 2 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv2.y * 0.25f + offset.y - 0.001f;
                            uv1[ 3 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv3.x * 0.25f + offset.x + 0.001f;
                            uv1[ 3 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv3.y * 0.25f + offset.y - 0.001f;
                        }
                        else
                        {
                            uv1[ 0 + c * 4 ].x = 0.0f;
                            uv1[ 0 + c * 4 ].y = 0.0f;
                            uv1[ 1 + c * 4 ].x = 0.0f;
                            uv1[ 1 + c * 4 ].y = 0.0f;
                            uv1[ 2 + c * 4 ].x = 0.0f;
                            uv1[ 2 + c * 4 ].y = 0.0f;
                            uv1[ 3 + c * 4 ].x = 0.0f;
                            uv1[ 3 + c * 4 ].y = 0.0f;
                        }

                        if ( node.textureTypeSize > 2 )
                        {
                            n = node.textureUV[ 2 ] % 16;

                            offset = W3UVConfig.instance.getUVOffset( node.textureType[ 2 ] , node.textureUV[ 2 ] );

                            uv1[ 0 + c * 4 ].z = W3UVConfig.instance.uv16[ n ].uv0.x * 0.25f + offset.x - 0.001f;
                            uv1[ 0 + c * 4 ].w = W3UVConfig.instance.uv16[ n ].uv0.y * 0.25f + offset.y + 0.001f;
                            uv1[ 1 + c * 4 ].z = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f + offset.x + 0.001f;
                            uv1[ 1 + c * 4 ].w = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f + offset.y + 0.001f;
                            uv1[ 2 + c * 4 ].z = W3UVConfig.instance.uv16[ n ].uv2.x * 0.25f + offset.x - 0.001f;
                            uv1[ 2 + c * 4 ].w = W3UVConfig.instance.uv16[ n ].uv2.y * 0.25f + offset.y - 0.001f;
                            uv1[ 3 + c * 4 ].z = W3UVConfig.instance.uv16[ n ].uv3.x * 0.25f + offset.x + 0.001f;
                            uv1[ 3 + c * 4 ].w = W3UVConfig.instance.uv16[ n ].uv3.y * 0.25f + offset.y - 0.001f;
                        }
                        else
                        {
                            uv1[ 0 + c * 4 ].z = 0.0f;
                            uv1[ 0 + c * 4 ].w = 0.0f;
                            uv1[ 1 + c * 4 ].z = 0.0f;
                            uv1[ 1 + c * 4 ].w = 0.0f;
                            uv1[ 2 + c * 4 ].z = 0.0f;
                            uv1[ 2 + c * 4 ].w = 0.0f;
                            uv1[ 3 + c * 4 ].z = 0.0f;
                            uv1[ 3 + c * 4 ].w = 0.0f;
                        }

                        if ( node.textureTypeSize > 3 )
                        {
                            n = node.textureUV[ 3 ] % 16;

                            offset = W3UVConfig.instance.getUVOffset( node.textureType[ 3 ] , node.textureUV[ 3 ] );

                            uv2[ 0 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv0.x * 0.25f + offset.x - 0.001f;
                            uv2[ 0 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv0.y * 0.25f + offset.y + 0.001f;
                            uv2[ 1 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f + offset.x + 0.001f;
                            uv2[ 1 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f + offset.y + 0.001f;
                            uv2[ 2 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv2.x * 0.25f + offset.x - 0.001f;
                            uv2[ 2 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv2.y * 0.25f + offset.y - 0.001f;
                            uv2[ 3 + c * 4 ].x = W3UVConfig.instance.uv16[ n ].uv3.x * 0.25f + offset.x + 0.001f;
                            uv2[ 3 + c * 4 ].y = W3UVConfig.instance.uv16[ n ].uv3.y * 0.25f + offset.y - 0.001f;
                        }
                        else
                        {
                            uv2[ 0 + c * 4 ].x = 0.0f;
                            uv2[ 0 + c * 4 ].y = 0.0f;
                            uv2[ 1 + c * 4 ].x = 0.0f;
                            uv2[ 1 + c * 4 ].y = 0.0f;
                            uv2[ 2 + c * 4 ].x = 0.0f;
                            uv2[ 2 + c * 4 ].y = 0.0f;
                            uv2[ 3 + c * 4 ].x = 0.0f;
                            uv2[ 3 + c * 4 ].y = 0.0f;
                        }

                        if ( node.textureTypeSize > 4 )
                        {
                            n = node.textureUV[ 4 ] % 16;

                            offset = W3UVConfig.instance.getUVOffset( node.textureType[ 4 ] , node.textureUV[ 4 ] );

                            uv2[ 0 + c * 4 ].z = W3UVConfig.instance.uv16[ n ].uv0.x * 0.25f + offset.x - 0.001f;
                            uv2[ 0 + c * 4 ].w = W3UVConfig.instance.uv16[ n ].uv0.y * 0.25f + offset.y + 0.001f;
                            uv2[ 1 + c * 4 ].z = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f + offset.x + 0.001f;
                            uv2[ 1 + c * 4 ].w = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f + offset.y + 0.001f;
                            uv2[ 2 + c * 4 ].z = W3UVConfig.instance.uv16[ n ].uv2.x * 0.25f + offset.x - 0.001f;
                            uv2[ 2 + c * 4 ].w = W3UVConfig.instance.uv16[ n ].uv2.y * 0.25f + offset.y - 0.001f;
                            uv2[ 3 + c * 4 ].z = W3UVConfig.instance.uv16[ n ].uv3.x * 0.25f + offset.x + 0.001f;
                            uv2[ 3 + c * 4 ].w = W3UVConfig.instance.uv16[ n ].uv3.y * 0.25f + offset.y - 0.001f;
                        }
                        else
                        {
                            uv2[ 0 + c * 4 ].z = 0.0f;
                            uv2[ 0 + c * 4 ].w = 0.0f;
                            uv2[ 1 + c * 4 ].z = 0.0f;
                            uv2[ 1 + c * 4 ].w = 0.0f;
                            uv2[ 2 + c * 4 ].z = 0.0f;
                            uv2[ 2 + c * 4 ].w = 0.0f;
                            uv2[ 3 + c * 4 ].z = 0.0f;
                            uv2[ 3 + c * 4 ].w = 0.0f;
                        }
                    }


                    float ox = j * width;
                    float oz = i * width;

                    vertices[ 0 + c * 4 ].x = -width - ox;
                    vertices[ 0 + c * 4 ].y = W3TerrainManager.instance.nodes[ posZ0 + i ][ posX0 + j + 1 ].y;
                    vertices[ 0 + c * 4 ].z = -oz;

                    vertices[ 1 + c * 4 ].x = -ox;
                    vertices[ 1 + c * 4 ].y = W3TerrainManager.instance.nodes[ posZ0 + i ][ posX0 + j ].y;
                    vertices[ 1 + c * 4 ].z = -oz;

                    vertices[ 2 + c * 4 ].x = -width - ox;
                    vertices[ 2 + c * 4 ].y = W3TerrainManager.instance.nodes[ posZ0 + i + 1 ][ posX0 + j + 1 ].y;
                    vertices[ 2 + c * 4 ].z = -width - oz;

                    vertices[ 3 + c * 4 ].x = -ox;
                    vertices[ 3 + c * 4 ].y = W3TerrainManager.instance.nodes[ posZ0 + i + 1 ][ posX0 + j ].y;
                    vertices[ 3 + c * 4 ].z = -width - oz;

                    uv0[ 0 + c * 4 ].z = (float)( posX0 + j + 1 ) / W3TerrainManager.instance.width;
                    uv0[ 0 + c * 4 ].w = (float)( posZ0 + i ) / W3TerrainManager.instance.height;

                    uv0[ 1 + c * 4 ].z = (float)( posX0 + j ) / W3TerrainManager.instance.width;
                    uv0[ 1 + c * 4 ].w = (float)( posZ0 + i ) / W3TerrainManager.instance.height;

                    uv0[ 2 + c * 4 ].z = (float)( posX0 + j + 1 ) / W3TerrainManager.instance.width;
                    uv0[ 2 + c * 4 ].w = (float)( posZ0 + i + 1 ) / W3TerrainManager.instance.height;

                    uv0[ 3 + c * 4 ].z = (float)( posX0 + j ) / W3TerrainManager.instance.width;
                    uv0[ 3 + c * 4 ].w = (float)( posZ0 + i + 1 ) / W3TerrainManager.instance.height;


                    triangles[ 0 + c * 6 ] = 0 + c * 4;
                    triangles[ 1 + c * 6 ] = 1 + c * 4;
                    triangles[ 2 + c * 6 ] = 2 + c * 4;
                    triangles[ 3 + c * 6 ] = 2 + c * 4;
                    triangles[ 4 + c * 6 ] = 1 + c * 4;
                    triangles[ 5 + c * 6 ] = 3 + c * 4;

                    c++;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            mesh.SetUVs( 0 , new List<Vector4>( uv0 ) );
            mesh.SetUVs( 1 , new List<Vector4>( uv1 ) );
            mesh.SetUVs( 2 , new List<Vector4>( uv2 ) );
            
            meshFilter.mesh = mesh;

            inited = true;

            uv0 = null;
            uv1 = null;
            uv2 = null;
            vertices = null;
            triangles = null;

            meshCollider.sharedMesh = mesh;
        }
    }


//     public void updateMask()
//     {
//         int sx = posX1 - posX0;
//         int sz = posZ1 - posZ0;
// 
//         // 3---2
//         // | / |
//         // 1---0
// 
//         int c = 0;
//         for ( int i = 0 ; i < sz ; i++ )
//         {
//             for ( int j = 0 ; j < sx ; j++ )
//             {
//                 W3TerrainNode node = W3MapManager.instance.nodes[ posZ0 + i ][ posX0 + j ];
// 
//                 if ( node.textureTypeSize != 0 &&
//                     node.shadow != (int)W3ShadowConfig.W3ShadowType.Partial )
//                     c++;
//             }
//         }
// 
//         if ( c == 0 )
//         {
//             return;
//         }
// 
//         c = 0;
//         for ( int i = 0 ; i < sz ; i++ )
//         {
//             for ( int j = 0 ; j < sx ; j++ )
//             {
//                 W3TerrainNode node = W3MapManager.instance.nodes[ posZ0 + i ][ posX0 + j ];
// 
//                 if ( node.textureTypeSize == 0 ||
//                     node.shadow == (int)W3ShadowConfig.W3ShadowType.Partial )
//                 {
//                     continue;
//                 }
// 
//                 fastUVs4[ 0 + c * 4 ].y = W3MapManager.instance.nodes[ posZ0 + i ][ posX0 + j + 1 ].mask > 0 ?
//                     W3MapManager.instance.nodes[ posZ0 + i ][ posX0 + j + 1 ].mask / 255.0f : 0.0f;
//                 fastUVs4[ 1 + c * 4 ].y = W3MapManager.instance.nodes[ posZ0 + i ][ posX0 + j ].mask > 0 ?
//                     W3MapManager.instance.nodes[ posZ0 + i ][ posX0 + j ].mask / 255.0f : 0.0f;
//                 fastUVs4[ 2 + c * 4 ].y = W3MapManager.instance.nodes[ posZ0 + i + 1 ][ posX0 + j + 1 ].mask > 0 ?
//                     W3MapManager.instance.nodes[ posZ0 + i + 1 ][ posX0 + j + 1 ].mask / 255.0f : 0.0f;
//                 fastUVs4[ 3 + c * 4 ].y = W3MapManager.instance.nodes[ posZ0 + i + 1 ][ posX0 + j ].mask > 0 ?
//                     W3MapManager.instance.nodes[ posZ0 + i + 1 ][ posX0 + j ].mask / 255.0f : 0.0f;
// 
// 
//                 c++;
//             }
//         }
// 
//         meshFilter.mesh.uv4 = fastUVs4;
//     }
// 
// 
//     public void updateMaskShadow()
//     {
//         int sx = posX1 - posX0;
//         int sz = posZ1 - posZ0;
// 
//         // 3---2
//         // | / |
//         // 1---0
// 
//         int c = 0;
//         for ( int i = 0 ; i < sz ; i++ )
//         {
//             for ( int j = 0 ; j < sx ; j++ )
//             {
//                 W3TerrainNode node = W3MapManager.instance.nodes[ posZ0 + i ][ posX0 + j ];
// 
//                 if ( node.textureTypeSize != 0 &&
//                     node.shadow == (int)W3ShadowConfig.W3ShadowType.Partial )
//                     c++;
//             }
//         }
// 
//         if ( c == 0 )
//         {
//             return;
//         }
// 
//         c = 0;
//         for ( int i = 0 ; i < sz * 4 ; i++ )
//         {
//             for ( int j = 0 ; j < sx * 4 ; j++ )
//             {
//                 W3TerrainNode node = W3MapManager.instance.nodes[ posZ0 + i / 4 ][ posX0 + j / 4 ];
// 
//                 if ( node.textureTypeSize == 0 ||
//                     node.shadow != (int)W3ShadowConfig.W3ShadowType.Partial )
//                 {
//                     continue;
//                 }
// 
//                 fastUVs4[ 0 + c * 4 ].y = W3MapManager.instance.smallNodes[ posZ0 * 4 + i ][ posX0 * 4 + j + 1 ].mask > 0 ?
//                         W3MapManager.instance.smallNodes[ posZ0 * 4 + i ][ posX0 * 4 + j + 1 ].mask / 255.0f : 0.0f;
//                 fastUVs4[ 1 + c * 4 ].y = W3MapManager.instance.smallNodes[ posZ0 * 4 + i ][ posX0 * 4 + j ].mask > 0 ?
//                     W3MapManager.instance.smallNodes[ posZ0 * 4 + i ][ posX0 * 4 + j ].mask / 255.0f : 0.0f;
//                 fastUVs4[ 2 + c * 4 ].y = W3MapManager.instance.smallNodes[ posZ0 * 4 + i + 1 ][ posX0 * 4 + j + 1 ].mask > 0 ?
//                     W3MapManager.instance.smallNodes[ posZ0 * 4 + i + 1 ][ posX0 * 4 + j + 1 ].mask / 255.0f : 0.0f;
//                 fastUVs4[ 3 + c * 4 ].y = W3MapManager.instance.smallNodes[ posZ0 * 4 + i + 1 ][ posX0 * 4 + j ].mask > 0 ?
//                     W3MapManager.instance.smallNodes[ posZ0 * 4 + i + 1 ][ posX0 * 4 + j ].mask / 255.0f : 0.0f;
// 
//                 c++;
//             }
//         }
// 
//         meshFilter.mesh.uv4 = fastUVs4;
//     }
// 
//     public void initSpriteShadow( int x0 , int z0 , int size )
//     {
//         if ( !inited )
//         {
//             width = 1.0f;
// 
//             meshFilter = GetComponent<MeshFilter>();
//             meshRender = GetComponent<MeshRenderer>();
//             meshCollider = GetComponent<MeshCollider>();
// 
//             posX0 = x0;
//             posZ0 = z0;
// 
//             Mesh mesh = new Mesh();
//             mesh.name = "W3SpriteMesh1" + "_" + x0 + "_" + z0;
// 
//             posX1 = x0 + size;
//             if ( posX1 > W3MapManager.instance.width )
//             {
//                 posX1 = W3MapManager.instance.width;
//             }
//             posZ1 = z0 + size;
//             if ( posZ1 > W3MapManager.instance.height )
//             {
//                 posZ1 = W3MapManager.instance.height;
//             }
// 
//             if ( posX0 == posX1 || posZ0 == posZ1 )
//             {
//                 return;
//             }
// 
//             int sx = posX1 - posX0;
//             int sz = posZ1 - posZ0;
// 
//             // 3---2
//             // | / |
//             // 1---0
// 
//             int c = 0;
//             for ( int i = 0 ; i < sz ; i++ )
//             {
//                 for ( int j = 0 ; j < sx ; j++ )
//                 {
//                     W3TerrainNode node = W3MapManager.instance.nodes[ posZ0 + i ][ posX0 + j ];
// 
//                     if ( node.textureTypeSize != 0 && 
//                         node.shadow == (int)W3ShadowConfig.W3ShadowType.Partial )
//                         c++;
//                 }
//             }
// 
//             if ( c == 0 )
//             {
//                 return;
//             }
// 
//             int sizeV = 4 * c * 16;
//             int sizeT = 6 * c * 16;
// 
//             fastUVs = new Vector2[ sizeV ];
//             fastUVs2 = new Vector2[ sizeV ];
//             fastUVs3 = new Vector2[ sizeV ];
//             fastUVsC = new Color[ sizeV ];
//             fastUVs4 = new Vector2[ sizeV ];
//             fastVertices = new Vector3[ sizeV ];
//             fastTriangles = new int[ sizeT ];
// 
// 
//             c = 0;
//             for ( int i = 0 ; i < sz * 4 ; i++ )
//             {
//                 for ( int j = 0 ; j < sx * 4 ; j++ )
//                 {
//                     W3TerrainNode node = W3MapManager.instance.nodes[ posZ0 + i / 4 ][ posX0 + j / 4 ];
// 
//                     if ( node.textureTypeSize == 0 || 
//                         node.shadow != (int)W3ShadowConfig.W3ShadowType.Partial )
//                     {
//                         continue;
//                     }
//                     else
//                     {
//                         int n = node.textureUV[ 0 ] % 16;
//                         int py = node.textureType[ 0 ] / 4;
//                         int px = node.textureType[ 0 ] % 4;
// 
//                         float px0 = 0.25f * px;
//                         float py0 = 0.25f * py;
// 
//                         float px1 = 0.015625f * ( j % 4 );
//                         float py1 = 0.015625f * ( i % 4 );
// 
//                         float px2 = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f;
//                         float py2 = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f;
// 
//                         fastUVs[ 0 + c * 4 ].x = px2 + px0 + px1 + 0.015625f;
//                         fastUVs[ 0 + c * 4 ].y = py2 + py0 + py1;
//                         fastUVs[ 1 + c * 4 ].x = px2 + px0 + px1;
//                         fastUVs[ 1 + c * 4 ].y = py2 + py0 + py1;
//                         fastUVs[ 2 + c * 4 ].x = px2 + px0 + px1 + 0.015625f;
//                         fastUVs[ 2 + c * 4 ].y = py2 + py0 + py1 + 0.015625f;
//                         fastUVs[ 3 + c * 4 ].x = px2 + px0 + px1;
//                         fastUVs[ 3 + c * 4 ].y = py2 + py0 + py1 + 0.015625f;
// 
//                         if ( node.textureTypeSize > 1 )
//                         {
//                             n = node.textureUV[ 1 ] % 16;
//                             py = node.textureType[ 1 ] / 4;
//                             px = node.textureType[ 1 ] % 4;
// 
//                             px0 = 0.25f * px;
//                             py0 = 0.25f * py;
// 
//                             px2 = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f;
//                             py2 = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f;
// 
//                             fastUVs2[ 0 + c * 4 ].x = px2 + px0 + px1 + 0.015625f;
//                             fastUVs2[ 0 + c * 4 ].y = py2 + py0 + py1;
//                             fastUVs2[ 1 + c * 4 ].x = px2 + px0 + px1;
//                             fastUVs2[ 1 + c * 4 ].y = py2 + py0 + py1;
//                             fastUVs2[ 2 + c * 4 ].x = px2 + px0 + px1 + 0.015625f;
//                             fastUVs2[ 2 + c * 4 ].y = py2 + py0 + py1 + 0.015625f;
//                             fastUVs2[ 3 + c * 4 ].x = px2 + px0 + px1;
//                             fastUVs2[ 3 + c * 4 ].y = py2 + py0 + py1 + 0.015625f;
//                         }
//                         else
//                         {
//                             fastUVs2[ 0 + c * 4 ].x = 0.0f;
//                             fastUVs2[ 0 + c * 4 ].y = 0.0f;
//                             fastUVs2[ 1 + c * 4 ].x = 0.0f;
//                             fastUVs2[ 1 + c * 4 ].y = 0.0f;
//                             fastUVs2[ 2 + c * 4 ].x = 0.0f;
//                             fastUVs2[ 2 + c * 4 ].y = 0.0f;
//                             fastUVs2[ 3 + c * 4 ].x = 0.0f;
//                             fastUVs2[ 3 + c * 4 ].y = 0.0f;
//                         }
// 
//                         if ( node.textureTypeSize > 2 )
//                         {
//                             n = node.textureUV[ 2 ] % 16;
//                             py = node.textureType[ 2 ] / 4;
//                             px = node.textureType[ 2 ] % 4;
// 
//                             px0 = 0.25f * px;
//                             py0 = 0.25f * py;
// 
//                             px2 = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f;
//                             py2 = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f;
// 
//                             fastUVs3[ 0 + c * 4 ].x = px2 + px0 + px1 + 0.015625f;
//                             fastUVs3[ 0 + c * 4 ].y = py2 + py0 + py1;
//                             fastUVs3[ 1 + c * 4 ].x = px2 + px0 + px1;
//                             fastUVs3[ 1 + c * 4 ].y = py2 + py0 + py1;
//                             fastUVs3[ 2 + c * 4 ].x = px2 + px0 + px1 + 0.015625f;
//                             fastUVs3[ 2 + c * 4 ].y = py2 + py0 + py1 + 0.015625f;
//                             fastUVs3[ 3 + c * 4 ].x = px2 + px0 + px1;
//                             fastUVs3[ 3 + c * 4 ].y = py2 + py0 + py1 + 0.015625f;
//                         }
//                         else
//                         {
//                             fastUVs3[ 0 + c * 4 ].x = 0.0f;
//                             fastUVs3[ 0 + c * 4 ].y = 0.0f;
//                             fastUVs3[ 1 + c * 4 ].x = 0.0f;
//                             fastUVs3[ 1 + c * 4 ].y = 0.0f;
//                             fastUVs3[ 2 + c * 4 ].x = 0.0f;
//                             fastUVs3[ 2 + c * 4 ].y = 0.0f;
//                             fastUVs3[ 3 + c * 4 ].x = 0.0f;
//                             fastUVs3[ 3 + c * 4 ].y = 0.0f;
//                         }
// 
//                         if ( node.textureTypeSize > 3 )
//                         {
//                             n = node.textureUV[ 3 ] % 16;
//                             py = node.textureType[ 3 ] / 4;
//                             px = node.textureType[ 3 ] % 4;
// 
//                             px0 = 0.25f * px;
//                             py0 = 0.25f * py;
// 
//                             px2 = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f;
//                             py2 = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f;
// 
//                             fastUVsC[ 0 + c * 4 ].r = px2 + px0 + px1 + 0.015625f;
//                             fastUVsC[ 0 + c * 4 ].g = py2 + py0 + py1;
//                             fastUVsC[ 1 + c * 4 ].r = px2 + px0 + px1;
//                             fastUVsC[ 1 + c * 4 ].g = py2 + py0 + py1;
//                             fastUVsC[ 2 + c * 4 ].r = px2 + px0 + px1 + 0.015625f;
//                             fastUVsC[ 2 + c * 4 ].g = py2 + py0 + py1 + 0.015625f;
//                             fastUVsC[ 3 + c * 4 ].r = px2 + px0 + px1;
//                             fastUVsC[ 3 + c * 4 ].g = py2 + py0 + py1 + 0.015625f;
//                         }
//                         else
//                         {
//                             fastUVsC[ 0 + c * 4 ].r = 0.0f;
//                             fastUVsC[ 0 + c * 4 ].g = 0.0f;
//                             fastUVsC[ 1 + c * 4 ].r = 0.0f;
//                             fastUVsC[ 1 + c * 4 ].g = 0.0f;
//                             fastUVsC[ 2 + c * 4 ].r = 0.0f;
//                             fastUVsC[ 2 + c * 4 ].g = 0.0f;
//                             fastUVsC[ 3 + c * 4 ].r = 0.0f;
//                             fastUVsC[ 3 + c * 4 ].g = 0.0f;
//                         }
// 
//                         if ( node.textureTypeSize > 4 )
//                         {
//                             n = node.textureUV[ 4 ] % 16;
//                             py = node.textureType[ 4 ] / 4;
//                             px = node.textureType[ 4 ] % 4;
// 
//                             px0 = 0.25f * px;
//                             py0 = 0.25f * py;
// 
//                             px2 = W3UVConfig.instance.uv16[ n ].uv1.x * 0.25f;
//                             py2 = W3UVConfig.instance.uv16[ n ].uv1.y * 0.25f;
// 
//                             fastUVsC[ 0 + c * 4 ].b = px2 + px0 + px1 + 0.015625f;
//                             fastUVsC[ 0 + c * 4 ].a = py2 + py0 + py1;
//                             fastUVsC[ 1 + c * 4 ].b = px2 + px0 + px1;
//                             fastUVsC[ 1 + c * 4 ].a = py2 + py0 + py1;
//                             fastUVsC[ 2 + c * 4 ].b = px2 + px0 + px1 + 0.015625f;
//                             fastUVsC[ 2 + c * 4 ].a = py2 + py0 + py1 + 0.015625f;
//                             fastUVsC[ 3 + c * 4 ].b = px2 + px0 + px1;
//                             fastUVsC[ 3 + c * 4 ].a = py2 + py0 + py1 + 0.015625f;
//                         }
//                         else
//                         {
//                             fastUVsC[ 0 + c * 4 ].b = 0.0f;
//                             fastUVsC[ 0 + c * 4 ].a = 0.0f;
//                             fastUVsC[ 1 + c * 4 ].b = 0.0f;
//                             fastUVsC[ 1 + c * 4 ].a = 0.0f;
//                             fastUVsC[ 2 + c * 4 ].b = 0.0f;
//                             fastUVsC[ 2 + c * 4 ].a = 0.0f;
//                             fastUVsC[ 3 + c * 4 ].b = 0.0f;
//                             fastUVsC[ 3 + c * 4 ].a = 0.0f;
//                         }
//                     }
// 
//                     fastUVs4[ 0 + c * 4 ].x = W3ShadowConfig.instance.getData( W3MapManager.instance.smallNodes[ posZ0 * 4 + i ][ posX0 * 4 + j + 1 ].shadow );
//                     fastUVs4[ 0 + c * 4 ].y = 0.0f;
//                     fastUVs4[ 1 + c * 4 ].x = W3ShadowConfig.instance.getData( W3MapManager.instance.smallNodes[ posZ0 * 4 + i ][ posX0 * 4 + j ].shadow );
//                     fastUVs4[ 1 + c * 4 ].y = 0.0f;
//                     fastUVs4[ 2 + c * 4 ].x = W3ShadowConfig.instance.getData( W3MapManager.instance.smallNodes[ posZ0 * 4 + i + 1 ][ posX0 * 4 + j + 1 ].shadow );
//                     fastUVs4[ 2 + c * 4 ].y = 0.0f;
//                     fastUVs4[ 3 + c * 4 ].x = W3ShadowConfig.instance.getData( W3MapManager.instance.smallNodes[ posZ0 * 4 + i + 1 ][ posX0 * 4 + j ].shadow );
//                     fastUVs4[ 3 + c * 4 ].y = 0.0f;
// 
//                     float ox = j * width;
//                     float oz = i * width;
// 
//                     fastVertices[ 0 + c * 4 ].x = -width - ox;
//                     fastVertices[ 0 + c * 4 ].y = W3MapManager.instance.smallNodes[ posZ0 * 4 + i ][ posX0 * 4 + j + 1 ].y;
//                     fastVertices[ 0 + c * 4 ].z = -oz;
//                     fastVertices[ 1 + c * 4 ].x = -ox;
//                     fastVertices[ 1 + c * 4 ].y = W3MapManager.instance.smallNodes[ posZ0 * 4 + i ][ posX0 * 4 + j ].y;
//                     fastVertices[ 1 + c * 4 ].z = -oz;
//                     fastVertices[ 2 + c * 4 ].x = -width - ox;
//                     fastVertices[ 2 + c * 4 ].y = W3MapManager.instance.smallNodes[ posZ0 * 4 + i + 1 ][ posX0 * 4 + j + 1 ].y;
//                     fastVertices[ 2 + c * 4 ].z = -width - oz;
//                     fastVertices[ 3 + c * 4 ].x = -ox;
//                     fastVertices[ 3 + c * 4 ].y = W3MapManager.instance.smallNodes[ posZ0 * 4 + i + 1 ][ posX0 * 4 + j ].y;
//                     fastVertices[ 3 + c * 4 ].z = -width - oz;
// 
//                     fastTriangles[ 0 + c * 6 ] = 0 + c * 4;
//                     fastTriangles[ 1 + c * 6 ] = 1 + c * 4;
//                     fastTriangles[ 2 + c * 6 ] = 2 + c * 4;
//                     fastTriangles[ 3 + c * 6 ] = 2 + c * 4;
//                     fastTriangles[ 4 + c * 6 ] = 1 + c * 4;
//                     fastTriangles[ 5 + c * 6 ] = 3 + c * 4;
// 
//                     c++;
//                 }
//             }
// 
//             mesh.vertices = fastVertices;
//             mesh.uv = fastUVs;
//             mesh.uv2 = fastUVs2;
//             mesh.uv3 = fastUVs3;
//             mesh.uv4 = fastUVs4;
//             mesh.colors = fastUVsC;
//             mesh.triangles = fastTriangles;
// 
//             meshFilter.mesh = mesh;
// 
//             inited = true;
// 
// 
//             fastUVs = null;
//             fastUVs2 = null;
//             fastUVs3 = null;
//             fastUVsC = null;
// //          fastUVs4 = null;
//             fastVertices = null;
//             fastTriangles = null;
// 
//             meshCollider.sharedMesh = mesh;
//         }
//     }



}


