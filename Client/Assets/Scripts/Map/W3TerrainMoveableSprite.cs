using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;


public class W3TerrainMoveableSprite : MonoBehaviour
{
    public MeshFilter meshFilter = null;
    public MeshRenderer meshRender = null;
    public Mesh mesh = null;

    public bool inited = false;

    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uv0;
    public Vector2[] uv2;
    public Vector2[] puv;

    public float textureWidth;
    public float textureHeight;
    public int textureWidth1;
    public int textureHeight1;
    public float textureWidthHalf;
    public float textureHeightHalf;

    public Vector3 posReal;
    public Vector3Int pos;

    public void Awake()
    {
        mesh = new Mesh();
        mesh.name = "W3MoveableSpriteMesh";

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }

    public void updateMaterial( string name )
    {
        meshRender.material = W3MaterialConfig.instance.getMaterial( name );
    }

    public void movePosReal( float x , float z )
    {
        posReal.x = x;
        posReal.z = z;

        movePos( (int)( ( posReal.x + textureWidthHalf ) / GameDefine.TERRAIN_SIZE ) , 
            (int)( ( posReal.z + textureHeightHalf ) / GameDefine.TERRAIN_SIZE ) );

        updateUV();
    }

    public void movePos( int x , int z )
    {
        if ( x == pos.x && z == pos.z )
        {
            return;
        }

        transform.position = new Vector3( x * GameDefine.TERRAIN_SIZE , 0.0f , z * GameDefine.TERRAIN_SIZE );

        pos.x = x;
        pos.z = z;
                
        int c = 0;
        for ( int i = 0 ; i < textureHeight1 + 1 ; i++ )
        {
            for ( int j = 0 ; j < textureWidth1 + 1 ; j++ )
            {
                vertices[ 0 + c * 4 ].y = W3TerrainManager.instance.nodes[ -pos.z + i ][ -pos.x + j + 1 ].y + 10.1f;
                vertices[ 1 + c * 4 ].y = W3TerrainManager.instance.nodes[ -pos.z + i ][ -pos.x + j ].y + 10.1f;
                vertices[ 2 + c * 4 ].y = W3TerrainManager.instance.nodes[ -pos.z + i + 1 ][ -pos.x + j + 1 ].y + 10.1f;
                vertices[ 3 + c * 4 ].y = W3TerrainManager.instance.nodes[ -pos.z + i + 1 ][ -pos.x + j ].y + 10.1f;

                c++;
            }
        }

        meshFilter.mesh.vertices = vertices;
    }

    public void updateUV1()
    {
        int c = 0;
        for ( int i = 0 ; i < textureHeight1 + 1 ; i++ )
        {
            for ( int j = 0 ; j < textureWidth1 + 1 ; j++ )
            {
                uv2[ 0 + c * 4 ].x = (float)( -pos.x + j + 1 ) / W3TerrainManager.instance.width;
                uv2[ 0 + c * 4 ].y = (float)( -pos.z + i ) / W3TerrainManager.instance.height;
                uv2[ 1 + c * 4 ].x = (float)( -pos.x + j ) / W3TerrainManager.instance.width;
                uv2[ 1 + c * 4 ].y = (float)( -pos.z + i ) / W3TerrainManager.instance.height;
                uv2[ 2 + c * 4 ].x = (float)( -pos.x + j + 1 ) / W3TerrainManager.instance.width;
                uv2[ 2 + c * 4 ].y = (float)( -pos.z + i + 1 ) / W3TerrainManager.instance.height;
                uv2[ 3 + c * 4 ].x = (float)( -pos.x + j ) / W3TerrainManager.instance.width;
                uv2[ 3 + c * 4 ].y = (float)( -pos.z + i + 1 ) / W3TerrainManager.instance.height;

                c++;
            }
        }

        meshFilter.mesh.uv2 = uv2;
    }

    public void updateUV()
    {
        float px0 = posReal.x + textureWidthHalf;
        float py0 = posReal.z + textureHeightHalf;

        float px00 = px0 / textureWidth;
        float py00 = py0 / textureHeight;

        float psx = GameDefine.TERRAIN_SIZE / textureWidth;
        float psy = GameDefine.TERRAIN_SIZE / textureHeight;

        float posx = pos.x * GameDefine.TERRAIN_SIZE / textureWidth;
        float posy = pos.z * GameDefine.TERRAIN_SIZE / textureHeight;

        int c = 0;
        for ( int i = 0 ; i < textureHeight1 + 1 ; i++ )
        {
            for ( int j = 0 ; j < textureWidth1 + 1 ; j++ )
            {
                uv0[ 0 + c * 4 ].x = -( puv[ 0 + c * 4 ].x + posx - px00 );
                uv0[ 0 + c * 4 ].y = -( puv[ 0 + c * 4 ].y + posy - py00 );
                uv0[ 1 + c * 4 ].x = -( puv[ 1 + c * 4 ].x + posx - px00 );
                uv0[ 1 + c * 4 ].y = -( puv[ 1 + c * 4 ].y + posy - py00 );
                uv0[ 2 + c * 4 ].x = -( puv[ 2 + c * 4 ].x + posx - px00 );
                uv0[ 2 + c * 4 ].y = -( puv[ 2 + c * 4 ].y + posy - py00 );
                uv0[ 3 + c * 4 ].x = -( puv[ 3 + c * 4 ].x + posx - px00 );
                uv0[ 3 + c * 4 ].y = -( puv[ 3 + c * 4 ].y + posy - py00 );

                c++;
            }
        }

        meshFilter.mesh.uv = uv0;
    }


    public void initSprite( float w , float h , float x , float z )
    {
        if ( !inited )
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRender = GetComponent<MeshRenderer>();

            float width = GameDefine.TERRAIN_SIZE;

            textureWidth = w;
            textureHeight = h;
            textureWidth1 = (int)( w / width );
            textureHeight1 = (int)( h / width );
            if ( w > textureWidth1 * width )
                textureWidth1++;
            if ( h > textureHeight1 * width )
                textureHeight1++;

            textureWidthHalf = x;
            textureHeightHalf = z;

            int posX0 = 0;
            int posZ0 = 0;

            int posX1 = 0;
            int posZ1 = 0;

            posX0 = 0;
            posZ0 = 0;

            posX1 = (int)textureWidth1 + 1;
            posZ1 = (int)textureHeight1 + 1;

            int sx = posX1 - posX0;
            int sz = posZ1 - posZ0;

            // 3---2
            // | / |
            // 1---0

            int c = sz * sx;

            int sizeV = 4 * c;
            int sizeT = 6 * c;

            vertices = new Vector3[ sizeV ];
            uv0 = new Vector2[ sizeV ];
            uv2 = new Vector2[ sizeV ];
            puv = new Vector2[ sizeV ];
            triangles = new int[ sizeT ];


            c = 0;
            for ( int i = 0 ; i < sz ; i++ )
            {
                for ( int j = 0 ; j < sx ; j++ )
                {
                    float ox = j * width;
                    float oz = i * width;

                    vertices[ 0 + c * 4 ].x = -width - ox;
                    vertices[ 0 + c * 4 ].z = -oz;

                    vertices[ 1 + c * 4 ].x = -ox;
                    vertices[ 1 + c * 4 ].z = -oz;

                    vertices[ 2 + c * 4 ].x = -width - ox;
                    vertices[ 2 + c * 4 ].z = -width - oz;

                    vertices[ 3 + c * 4 ].x = -ox;
                    vertices[ 3 + c * 4 ].z = -width - oz;

                    puv[ 0 + c * 4 ].x = vertices[ 0 + c * 4 ].x / w;
                    puv[ 0 + c * 4 ].y = vertices[ 0 + c * 4 ].z / h;
                    puv[ 1 + c * 4 ].x = vertices[ 1 + c * 4 ].x / w;
                    puv[ 1 + c * 4 ].y = vertices[ 1 + c * 4 ].z / h;
                    puv[ 2 + c * 4 ].x = vertices[ 2 + c * 4 ].x / w;
                    puv[ 2 + c * 4 ].y = vertices[ 2 + c * 4 ].z / h;
                    puv[ 3 + c * 4 ].x = vertices[ 3 + c * 4 ].x / w;
                    puv[ 3 + c * 4 ].y = vertices[ 3 + c * 4 ].z / h;


                    triangles[ 0 + c * 6 ] = 0 + c * 4;
                    triangles[ 1 + c * 6 ] = 1 + c * 4;
                    triangles[ 2 + c * 6 ] = 2 + c * 4;
                    triangles[ 3 + c * 6 ] = 2 + c * 4;
                    triangles[ 4 + c * 6 ] = 1 + c * 4;
                    triangles[ 5 + c * 6 ] = 3 + c * 4;

                    c++;
                }



                //                mesh.uv = uv0;

                //                meshFilter.sharedMesh = mesh;

                inited = true;
            }
        }

    }


}