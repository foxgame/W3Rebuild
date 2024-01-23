using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.AI;


public class W3BuildManager : SingletonMono< W3BuildManager >
{
    bool inited = false;

    GameObject[] objs = null;

    static GameObject maskObject = null;
    static Material matGreen = null;
    static Material matRed = null;

    W3UnitDataConfigData unitData = null;
    W3UnitUIConfigData unitUI = null;


    GameObject buildingObj = null;
    W3Unit unit = null;

    Transform unitObjTrans = null;

    public enum Type
    {
        Green,
        Red,
    }

    public override void initSingletonMono()
    {
        W3Unit.shaderDic.Add( "W3/UnitSelection" , "W3/BuildBase" );

        W3Unit.shaderDic.Add( "W3/MeshBillboard" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/MeshAlphaColor3" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/MeshBase" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/MeshBaseAlpha" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/MeshBaseAlphaCutoff" , "W3/BuildAlphaCutoff" );
//        W3Unit.shaderDic.Add( "W3/MeshColor" , "W3/BuildAlpha" );

        W3Unit.shaderDic.Add( "W3/SkinnedMeshAlphaColor3" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/SkinnedMeshBase" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/SkinnedMeshBaseAlpha" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/SkinnedMeshBaseAlphaCutoff" , "W3/BuildAlphaCutoff" );
//        W3Unit.shaderDic.Add( "W3/SkinnedMeshColor" , "W3/BuildAlpha" );

        W3Unit.shaderDic.Add( "W3/UnitMeshAlphaColor3" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/W3UnitMeshBase" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/UnitMeshBaseAlpha" , "W3/BuildAlpha" );
        W3Unit.shaderDic.Add( "W3/UnitMeshBaseAlphaCutoff" , "W3/BuildAlphaCutoff" );
//        W3Unit.shaderDic.Add( "W3/UnitMeshColor" , "W3/BuildAlpha" );

        maskObject = (GameObject)Resources.Load( "W3TerrainMask" );
        matGreen = (Material)Resources.Load( "Materials/TerrainMaskGreen" );
        matRed = (Material)Resources.Load( "Materials/TerrainMaskRed" );

        unitObjTrans = GameObject.Find( "Map" ).transform.Find( "Units" ).transform;
    }

    public void build( int x , int z )
    {
        if ( buildingObj == null )
        {
            return;
        }

        int xx = -x / GameDefine.TERRAIN_SIZE_PER + unitData.pathX - unitData.pathW / 2;
        int zz = -z / GameDefine.TERRAIN_SIZE_PER + unitData.pathZ - unitData.pathH / 2;

        if ( !canBuild( xx , zz ) )
        {
            return;
        }

        unit.updateFogUV();
        unit.updateShader( true );
        unit.setuberSplatPos( x / GameDefine.TERRAIN_SIZE_PER * GameDefine.TERRAIN_SIZE_PER ,
            z / GameDefine.TERRAIN_SIZE_PER * GameDefine.TERRAIN_SIZE_PER );

        buildingObj.transform.parent = unitObjTrans.parent;
        buildingObj = null;

//         W3TerrainManager.instance.setUnitBuilding( -x / GameDefine.TERRAIN_SIZE_PER , -z / GameDefine.TERRAIN_SIZE_PER , unit.baseID , unitData );
 
         W3TerrainManager.instance.setBuildingPath( 
             -x / GameDefine.TERRAIN_SIZE_PER , 
             -z / GameDefine.TERRAIN_SIZE_PER , 
             unitData );

        if ( W3UnitManager.instance.selectUnitList.Count > 0 )
        {
            W3UnitManager.instance.selectUnitList[ 0 ].readyBuild( unit ,
                -x / GameDefine.TERRAIN_SIZE_PER , -z / GameDefine.TERRAIN_SIZE_PER ,
                x / GameDefine.TERRAIN_SIZE_PER * GameDefine.TERRAIN_SIZE_PER ,
            z / GameDefine.TERRAIN_SIZE_PER * GameDefine.TERRAIN_SIZE_PER );
        }

        clear();

        //buildingObj.transform.localPosition = new Vector3( -unitData.pathX , W3MapManager.instance.smallNodes[ sz ][ sx ].ym , -unitData.pathZ );
    }

    public void readyToBuild( W3UnitDataConfigData d , W3UnitUIConfigData d1 )
    {
        unitData = d;
        unitUI = d1;

        initMask();

        string name1 = "Prefabs\\" + d1.file;

        buildingObj = (GameObject)GameObject.Instantiate( (GameObject)Resources.Load( name1 ) , transform );
        unit = buildingObj.GetComponent<W3Unit>();
        unit.unitData = d;
        unit.unitUI = d1;

//         W3SkinnedMeshColor[] mc = buildingObj.GetComponentsInChildren<W3SkinnedMeshColor>();
//         for ( int i = 0 ; i < mc.Length ; i++ )
//         {
//             mc[ i ].noColor = true;
//         }
    }

    

    public void setPos( int x , int z )
    {
        if ( !inited )
        {
            return;
        }

        int xx = (int)( x / GameDefine.TERRAIN_SIZE_PER );
        int zz = (int)( z / GameDefine.TERRAIN_SIZE_PER );

        transform.position = new Vector3( xx * GameDefine.TERRAIN_SIZE_PER , 0.0f , zz * GameDefine.TERRAIN_SIZE_PER );

        xx = -xx + unitData.pathX - unitData.pathW / 2;
        zz = -zz + unitData.pathZ - unitData.pathH / 2;


        //         if ( x == (int)transform.position.x && 
        //             z == (int)transform.position.z )
        //         {
        //             return;
        //        }

        updateMask( xx , zz );
    }

    public void clear()
    {
        if ( inited )
        {
            for ( int i = 0 ; i < objs.Length ; i++ )
            {
                if ( objs[ i ] != null )
                    Destroy( objs[ i ] );
                objs[ i ] = null;
            }

            if ( buildingObj != null )
                Destroy( buildingObj );

            buildingObj = null;
            objs = null;
            inited = false;

            unit = null;
        }
    }

    public void initMask()
    {
        clear();
        
        objs = new GameObject[ unitData.pathH * unitData.pathW ];

        int c = 0;
        for ( int i = 0 ; i < unitData.pathH ; i++ )
        {
            for ( int j = 0 ; j < unitData.pathW ; j++ )
            {
                if ( unitData.pathData[ c ].r > 0 )
                {
                    objs[ i * unitData.pathW + j ] = (GameObject)Instantiate( maskObject );
                    objs[ i * unitData.pathW + j ].transform.parent = transform;
                    objs[ i * unitData.pathW + j ].transform.localPosition = new Vector3( ( -j - unitData.pathX + unitData.pathW / 2 ) * GameDefine.TERRAIN_SIZE_PER ,
                        0.0f , ( -i - unitData.pathZ + unitData.pathH / 2 ) * GameDefine.TERRAIN_SIZE_PER );

                    Mesh mesh = new Mesh();

                    Vector3[] vertices = new Vector3[ 4 ];
                    Vector2[] uv0 = new Vector2[ 4 ];
                    int[] triangles = new int[ 6 ];

                    vertices[ 0 ].x = -GameDefine.TERRAIN_SIZE_PER;
                    vertices[ 0 ].z = 0;
                    vertices[ 1 ].x = 0;
                    vertices[ 1 ].z = 0;
                    vertices[ 2 ].x = -GameDefine.TERRAIN_SIZE_PER;
                    vertices[ 2 ].z = -GameDefine.TERRAIN_SIZE_PER;
                    vertices[ 3 ].x = 0;
                    vertices[ 3 ].z = -GameDefine.TERRAIN_SIZE_PER;

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
                    mesh.uv = uv0;
                    mesh.triangles = triangles;

                    MeshFilter f = objs[ i * unitData.pathW + j ].GetComponent<MeshFilter>();
                    f.mesh = mesh;
                }

                c++;
            }
        }

        inited = true;
    }


    public void updateMask( int x , int z )
    {
        for ( int i = 0 ; i < unitData.pathH ; i++ )
        {
            for ( int j = 0 ; j < unitData.pathW ; j++ )
            {
                Type t = W3TerrainManager.instance.isPath( x + j , z + i , W3PathType.NOBUILD ) ? Type.Red : Type.Green;
                setMask( j , i , x + j , z + i , t );
            }
        }
    }

    public bool canBuild( int x , int z )
    {
        for ( int i = 0 ; i < unitData.pathH ; i++ )
        {
            for ( int j = 0 ; j < unitData.pathW ; j++ )
            {
                if ( W3TerrainManager.instance.isPath( x + j , z + i , W3PathType.NOBUILD ) )
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void setMask( int x , int z , int sx , int sz , Type t )
    {
        if ( objs[ z * unitData.pathW + x ] == null )
        {
            return;
        }

        MeshRenderer r = objs[ z * unitData.pathW + x ].GetComponent<MeshRenderer>();
        r.sharedMaterial = ( t == Type.Green ? matGreen : matRed );

        MeshFilter f = objs[ z * unitData.pathW + x ].GetComponent< MeshFilter >();

        Vector3[] v3 = new Vector3[ f.mesh.vertexCount ];
        v3[ 0 ] = f.mesh.vertices[ 0 ];
        v3[ 1 ] = f.mesh.vertices[ 1 ];
        v3[ 2 ] = f.mesh.vertices[ 2 ];
        v3[ 3 ] = f.mesh.vertices[ 3 ];

        v3[ 0 ].y = W3TerrainManager.instance.smallNodes[ sz ][ sx + 1 ].y + GameDefine.TERRAIN_SIZE_PER_HALF;
        v3[ 1 ].y = W3TerrainManager.instance.smallNodes[ sz ][ sx ].y + GameDefine.TERRAIN_SIZE_PER_HALF;
        v3[ 2 ].y = W3TerrainManager.instance.smallNodes[ sz + 1 ][ sx + 1 ].y + GameDefine.TERRAIN_SIZE_PER_HALF;
        v3[ 3 ].y = W3TerrainManager.instance.smallNodes[ sz + 1 ][ sx ].y + GameDefine.TERRAIN_SIZE_PER_HALF;

        f.mesh.vertices = v3;

        if ( x == unitData.pathW / 2 && z == unitData.pathH / 2 )
        {
            buildingObj.transform.localPosition = new Vector3( -unitData.pathX * GameDefine.TERRAIN_SIZE_PER ,
                W3TerrainManager.instance.smallNodes[ sz ][ sx ].ym , -unitData.pathZ * GameDefine.TERRAIN_SIZE_PER );
        }
    }




}




