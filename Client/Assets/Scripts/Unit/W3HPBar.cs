using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class W3HPBar : MonoBehaviour
{
    public MeshFilter meshFilter = null;
    public MeshRenderer meshRender = null;

    public MeshFilter meshFilterB = null;
    public MeshRenderer meshRenderB = null;


    Vector3[] vertices;
    int[] triangles;
    Vector2[] uv0;
    Color[] colors;

    public int maxHP = 0;
    public int hp = 0;

    float x = 32;
    float z = 4;

    //     void Start()
    //     {
    //         init( 100 , 100 );
    //         setHP( 30 );
    //     }

    static GameObject spriteObject = null;
    public static GameObject Create()
    {
        if ( spriteObject == null )
        {
            spriteObject = (GameObject)Resources.Load( "Prefabs/UI/W3HPBar" );
            DontDestroyOnLoad( spriteObject );
        }

        GameObject obj = (GameObject)Instantiate( spriteObject );
        return obj;
    }



    public void enable( bool b )
    {
        gameObject.SetActive( b );
    }

    public void setHP( int h )
    {
        hp = h;

        float f = (float)hp / maxHP;

        float xx = ( x - f * ( x * 2 ) );
        float r = 1.0f;
        float g = 1.0f;

        if ( f > 0.5f )
        {
            r = 1.0f - ( f - 0.5f ) * 2f;
        }
        else
        {
            g = 1.0f - ( 0.5f - f ) * 2f;
        }

        vertices[ 0 ].x = xx;
        vertices[ 0 ].y = 0f;
        vertices[ 0 ].z = z;
        vertices[ 1 ].x = x;
        vertices[ 1 ].y = 0f;
        vertices[ 1 ].z = z;
        vertices[ 2 ].x = xx;
        vertices[ 2 ].y = 0f;
        vertices[ 2 ].z = -z;
        vertices[ 3 ].x = x;
        vertices[ 3 ].y = 0f;
        vertices[ 3 ].z = -z;

        colors[ 0 ].r = r;
        colors[ 0 ].g = g;
        colors[ 0 ].b = 0.0f;
        colors[ 0 ].a = 1.0f;
        colors[ 1 ].r = r;
        colors[ 1 ].g = g;
        colors[ 1 ].b = 0.0f;
        colors[ 1 ].a = 1.0f;
        colors[ 2 ].r = r;
        colors[ 2 ].g = g;
        colors[ 2 ].b = 0.0f;
        colors[ 2 ].a = 1.0f;
        colors[ 3 ].r = r;
        colors[ 3 ].g = g;
        colors[ 3 ].b = 0.0f;
        colors[ 3 ].a = 1.0f;

        uv0[ 0 ].x = f;
        uv0[ 0 ].y = 0.0f;
        uv0[ 1 ].x = 0.0f;
        uv0[ 1 ].y = 0.0f;
        uv0[ 2 ].x = f;
        uv0[ 2 ].y = 1.0f;
        uv0[ 3 ].x = 0.0f;
        uv0[ 3 ].y = 1.0f;

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.colors = colors;
        meshFilter.mesh.uv = uv0;
    }

    bool inited = false;

    public void init( int hp1 , int maxHP1 )
    {
        if ( !inited )
        {
            hp = hp1;
            maxHP = maxHP1;

            meshFilter = GetComponent<MeshFilter>();
            meshRender = GetComponent<MeshRenderer>();
            meshFilterB = transform.Find( "background" ).GetComponent<MeshFilter>();
            meshRenderB = transform.Find( "background" ).GetComponent<MeshRenderer>();


            Mesh meshB = new Mesh();
            Mesh mesh = new Mesh();
            mesh.name = "W3HPBar" + "_" + hp + "_" + maxHP;
            meshB.name = "W3HPBarB" + "_" + hp + "_" + maxHP;


            // 3---2
            // | / |
            // 1---0


            int sizeV = 4;
            int sizeT = 6;

            vertices = new Vector3[ sizeV ];
            uv0 = new Vector2[ sizeV ];
            colors = new Color[ sizeV ];
            triangles = new int[ sizeT ];

            colors[ 0 ].r = 0.0f;
            colors[ 0 ].g = 1.0f;
            colors[ 0 ].b = 0.0f;
            colors[ 0 ].a = 1.0f;
            colors[ 1 ].r = 0.0f;
            colors[ 1 ].g = 1.0f;
            colors[ 1 ].b = 0.0f;
            colors[ 1 ].a = 1.0f;
            colors[ 2 ].r = 0.0f;
            colors[ 2 ].g = 1.0f;
            colors[ 2 ].b = 0.0f;
            colors[ 2 ].a = 1.0f;
            colors[ 3 ].r = 0.0f;
            colors[ 3 ].g = 1.0f;
            colors[ 3 ].b = 0.0f;
            colors[ 3 ].a = 1.0f;


            vertices[ 0 ].x = -x;
            vertices[ 0 ].y = 0f;
            vertices[ 0 ].z = z;
            vertices[ 1 ].x = x;
            vertices[ 1 ].y = 0f;
            vertices[ 1 ].z = z;
            vertices[ 2 ].x = -x;
            vertices[ 2 ].y = 0f;
            vertices[ 2 ].z = -z;
            vertices[ 3 ].x = x;
            vertices[ 3 ].y = 0f;
            vertices[ 3 ].z = -z;

            uv0[ 0 ].x = 1.0f;
            uv0[ 0 ].y = 0.0f;
            uv0[ 1 ].x = 0.0f;
            uv0[ 1 ].y = 0.0f;
            uv0[ 2 ].x = 1.0f;
            uv0[ 2 ].y = 1.0f;
            uv0[ 3 ].x = 0.0f;
            uv0[ 3 ].y = 1.0f;


            triangles[ 0 ] = 0;
            triangles[ 1 ] = 1;
            triangles[ 2 ] = 2;
            triangles[ 3 ] = 2;
            triangles[ 4 ] = 1;
            triangles[ 5 ] = 3;

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv0;
            mesh.colors = colors;
            meshFilter.mesh = mesh;


            vertices[ 0 ].x = -x - 2;
            vertices[ 0 ].y = 0f;
            vertices[ 0 ].z = z + 2;
            vertices[ 1 ].x = x + 2;
            vertices[ 1 ].y = 0f;
            vertices[ 1 ].z = z + 2;
            vertices[ 2 ].x = -x - 2;
            vertices[ 2 ].y = 0f;
            vertices[ 2 ].z = -z - 2;
            vertices[ 3 ].x = x + 2;
            vertices[ 3 ].y = 0f;
            vertices[ 3 ].z = -z - 2;

            meshB.vertices = vertices;
            meshB.triangles = triangles;
            meshB.uv = uv0;
//            meshB.colors = colors;

            meshFilterB.mesh = mesh;

            inited = true;
        }
    }

}

