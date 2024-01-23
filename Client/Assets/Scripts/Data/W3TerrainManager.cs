using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.Collections;

public class W3TerrainManager : SingletonMono< W3TerrainManager >
{
    public override void initSingletonMono()
    {
    }

	public int width = 0;
	public int height = 0;

	public float offsetX = 0.0f;
	public float offsetY = 0.0f;

	public int groundTileSetsCount = 0;
	public int cliffTileSetsCount = 0;

	public string[] groundTileSets = null;
	public string[] cliffTileSets = null;

	public bool isLoaded = false;

	public string mainTitleSet = "";
    public int customTileSetIsUsed = 0;

    public W3TerrainNode[][] nodes;
    public W3TerrainSmallNode[][] smallNodes;

#if UNITY_BLOCK_TEST
    public W3TerrainBlock[][] terrainBlocks;
#endif

    public List< GameObject > cliffs = new List< GameObject >();
    public List< GameObject > cliffTrans = new List< GameObject >();

    public List<W3TerrainSprite> spriteObjects1 = new List<W3TerrainSprite>();
    public List<W3TerrainSprite> spriteObjects2 = new List<W3TerrainSprite>();

    Texture2D terrainTexture0;

    public void updateShadow()
    {


    }

    public void setDoodadID( int x , int z , int id )
    {
        smallNodes[ z ][ x ].doodadID = id;
    }


    public void removeUnitBuilding( int x , int z , int id , W3UnitDataConfigData unitData )
    {
        W3PathFinder.instance.removeUnitSizeBuilding( x , z , id , unitData );
    }

    public void setUnitBuilding( int x , int z , int id , W3UnitDataConfigData unitData )
    {
        W3PathFinder.instance.setUnitSizeBuilding( x , z , id , unitData );
    }

    public void removeUnit( int x , int z , int id , int size )
    {
#if UNITY_BLOCK_TEST

        int n = size / 2;
        int n1 = size % 2;

        for ( int i = -n ; i < n + n1 ; ++i )
        {
            for ( int j = -n ; j < n + n1 ; ++j )
            {
                unsafe
                {
                    if ( W3PathFinder.instance.unit[ z + i ][ x + j ] == id &&
                        terrainBlocks[ z + i ][ x + j ] != null )
                        terrainBlocks[ z + i ][ x + j ].setBlock( 0 );
//                     else
//                     {
//                         int n111 = W3PathFinder.instance.unit[ z + i ][ x + j ];
//                         n111 = n111;
//                     }
                }
            }
        }
#endif

        W3PathFinder.instance.removeUnitSize( x , z , id , size );
    }

    public void setBuildingPath( int x , int z , W3UnitDataConfigData unitData )
    {
        int wh = unitData.pathW / 2;
        int hh = unitData.pathH / 2;

        int c = 0;
        for ( int i = z - hh ; i < z + unitData.pathH - hh ; i++ )
        {
            for ( int j = x - wh ; j < x + unitData.pathW - wh ; j++ )
            {
                setBuildPath( j , i , unitData.pathData[ c ] );
                c++;
            }
        }
    }

    public void removeBuildingPath( int x , int z , W3UnitDataConfigData unitData )
    {
        int wh = unitData.pathW / 2;
        int hh = unitData.pathH / 2;

        int c = 0;
        for ( int i = z - hh ; i < z + unitData.pathH - hh ; i++ )
        {
            for ( int j = x - wh ; j < x + unitData.pathW - wh ; j++ )
            {
                removeBuildPath( j , i , unitData.pathData[ c ] );
                c++;
            }
        }
    }


    public bool canBuild( int x , int z , W3UnitDataConfigData unit )
    {
        int wh = unit.pathW / 2;
        int hh = unit.pathH / 2;

        for ( int i = z - hh ; i < z + unit.pathH - hh ; i++ )
        {
            for ( int j = x - wh ; j < x + unit.pathW - wh ; j++ )
            {
                if ( W3TerrainManager.instance.isPath( j , i , W3PathType.NOBUILD ) )
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void setUnit( int x , int z , int id , int size )
    {
        W3PathFinder.instance.setUnitSize( x , z , id , size );

#if UNITY_BLOCK_TEST

        int n = size / 2;
        int n1 = size % 2;

        for ( int i = -n ; i < n + n1 ; ++i )
        {
            for ( int j = -n ; j < n + n1 ; ++j )
            {
                unsafe
                {
                    if ( W3PathFinder.instance.unit[ z + i ][ x + j ] == id &&
                    terrainBlocks[ z + i ][ x + j ] != null )
                        terrainBlocks[ z + i ][ x + j ].setBlock( GameDefine.NOWALK );
                }
            }
        }
#endif

    }

    public bool isPath( int x , int z , W3PathType p )
    {
        return ( smallNodes[ z ][ x ].path & (byte)p ) == (byte)p;
    }

    public void setPath( int x , int z , Color b )
    {
        if ( b.g > 0.0f )
        {
            smallNodes[ z ][ x ].path |= (byte)W3PathType.NOFLY;
        }
        if ( b.r > 0.0f )
        {
            smallNodes[ z ][ x ].path |= (byte)W3PathType.NOWALK;
            smallNodes[ z ][ x ].unitHeight = GameDefine.TERRAIN_SIZE_PER;
        }
        if ( b.b > 0.0f )
        {
            smallNodes[ z ][ x ].path |= (byte)W3PathType.NOBUILD;
        }
    }

    public void setBuildPath( int x , int z , Color b )
    {
        if ( b.g > 0.0f )
        {
            smallNodes[ z ][ x ].path |= (byte)W3PathType.NOFLY;
        }
        if ( b.r > 0.0f )
        {
            smallNodes[ z ][ x ].path |= (byte)W3PathType.NOWALK;
        }
        if ( b.b > 0.0f )
        {
            smallNodes[ z ][ x ].path |= (byte)W3PathType.NOBUILD;
        }

#if UNITY_BLOCK_TEST
        if ( terrainBlocks[ z ][ x ] != null )
            terrainBlocks[ z ][ x ].setBlock( smallNodes[ z ][ x ].path );
#endif
    }

    public void removeBuildPath( int x , int z , Color b )
    {
        if ( b.g > 0.0f )
        {
            if ( ( smallNodes[ z ][ x ].path & (byte)W3PathType.NOFLY ) == (byte)W3PathType.NOFLY )
            {
                smallNodes[ z ][ x ].path -= (byte)W3PathType.NOFLY;
            }
        }
        if ( b.r > 0.0f )
        {
            if ( ( smallNodes[ z ][ x ].path & (byte)W3PathType.NOWALK ) == (byte)W3PathType.NOWALK )
            {
                smallNodes[ z ][ x ].path -= (byte)W3PathType.NOWALK;
            }
        }
        if ( b.b > 0.0f )
        {
            if ( ( smallNodes[ z ][ x ].path & (byte)W3PathType.NOBUILD ) == (byte)W3PathType.NOBUILD )
            {
                smallNodes[ z ][ x ].path -= (byte)W3PathType.NOBUILD;
            }
        }

#if UNITY_BLOCK_TEST
        if ( terrainBlocks[ z ][ x ] != null )
            terrainBlocks[ z ][ x ].setBlock( smallNodes[ z ][ x ].path );
#endif
    }

    public bool isBlockUp( int x , int z , float up )
    {
        return ( smallNodes[ z ][ x ].path & (byte)W3PathType.NOWALK ) == (byte)W3PathType.NOWALK && 
            ( smallNodes[ z ][ x ].ym + smallNodes[ z ][ x ].unitHeight ) > up;
    }
    
    public void createTerrainTextures()
    {
        terrainTexture0 = new Texture2D( 1024 , 1024 , TextureFormat.RGBA32 , false , true );

        int index = 0;
        for ( int i = 0 ; i < W3TerrainManager.instance.groundTileSetsCount ; ++i )
        {
            int x = index % 4;
            int y = index / 4;

            string name1 = W3TerrainManager.instance.getTexture( i );
            W3Texture w3t = W3TextureConfig.instance.getTexture( name1 );

            Color[] c = w3t.texture2D.GetPixels( 0 , 0 , 256 , 256 );
            terrainTexture0.SetPixels( x * 256 , y * 256 , 256 , 256 , c );

            W3UVConfig.instance.uvOffset[ i ] = index;
            W3UVConfig.instance.uvOffset512[ i ] = ( w3t.texture2D.width == 512 );

            index++;

            if ( w3t.texture2D.width == 512 )
            {
                x = index % 4;
                y = index / 4;

                Color[] c1 = w3t.texture2D.GetPixels( 256 , 0 , 256 , 256 );
                terrainTexture0.SetPixels( x * 256 , y * 256 , 256 , 256 , c1 );
                index++;
            }
            
            W3TextureConfig.instance.releaseTexture( name1 );
        }

        terrainTexture0.Apply();

//         byte[] pngData = terrainTexture0.EncodeToPNG();
//         File.WriteAllBytes( Application.persistentDataPath + "/" + "terrainTexture0.png" , pngData );
 
         Shader.SetGlobalTexture( "W3TerrainTex0" , terrainTexture0 );
    }

    public void clear()
    {
        if ( nodes != null )
        {
            for ( int j = 0 ; j < nodes.Length ; j++ )
            {
                for ( int i = 0 ; i < nodes[ j ].Length ; i++ )
                {
                    nodes[ j ][ i ] = null;
                }

                nodes[ j ] = null;
            }

            nodes = null;
        }

    }


    void createNodes( int w , int h )
    {
        nodes = new W3TerrainNode[ h + 1 ][];

        for ( int j = 0 ; j < h + 1 ; j++ )
        {
            nodes[ j ] = new W3TerrainNode[ w + 1 ];

            for ( int i = 0 ; i < w + 1 ; i++ )
            {
                nodes[ j ][ i ] = new W3TerrainNode();
                nodes[ j ][ i ].x = i;
                nodes[ j ][ i ].z = j;
            }
        }

        smallNodes = new W3TerrainSmallNode[ h * 4 ][];

        for ( int j = 0 ; j < h * 4 ; j++ )
        {
            smallNodes[ j ] = new W3TerrainSmallNode[ w * 4 ];

            for ( int i = 0 ; i < w * 4 ; i++ )
            {
                smallNodes[ j ][ i ] = new W3TerrainSmallNode();
                smallNodes[ j ][ i ].x = i;
                smallNodes[ j ][ i ].z = j;
            }
        }

#if UNITY_BLOCK_TEST
        terrainBlocks = new W3TerrainBlock[ h * 4 ][];
        for ( int j = 0 ; j < h * 4 ; j++ )
        {
            terrainBlocks[ j ] = new W3TerrainBlock[ w * 4 ];
        }
#endif
    }

    void createSprites()
    {
        //         GameObject obj = W3TerrainSprite.Create();
        // 
        //         W3TerrainSprite spr1 = obj.GetComponent<W3TerrainSprite>();
        //         spr1.initSprite( 0 , 0 , 32 );
        //         spr1.transform.localPosition = new Vector3( 0.0f , 0.0f , 0.0f );

        int size = 4;

        for ( int i = 0 ; i < W3TerrainManager.instance.height / size ; i++ )
        {
            for ( int j = 0 ; j < W3TerrainManager.instance.width / size ; j++ )
            {
                GameObject obj = W3TerrainSprite.Create();

                W3TerrainSprite spr1 = obj.GetComponent< W3TerrainSprite >();
                spr1.initSprite( j * size , i * size , size );
                spr1.transform.localPosition = new Vector3( -size * GameDefine.TERRAIN_SIZE * j ,
                    0.0f , -size * GameDefine.TERRAIN_SIZE * i );

                spriteObjects1.Add( spr1 );
            }
        }
    }

    void createSpritesWater()
    {
        int size = 8;

        for ( int i = 0 ; i < W3TerrainManager.instance.height / size ; i++ )
        {
            for ( int j = 0 ; j < W3TerrainManager.instance.width / size ; j++ )
            {
                GameObject obj = W3TerrainSpriteWater.Create();

                W3TerrainSpriteWater spr1 = obj.GetComponent<W3TerrainSpriteWater>();
                spr1.initSprite( j * size , i * size , size );
                spr1.transform.localPosition = new Vector3( -size * GameDefine.TERRAIN_SIZE * j ,
                    0.0f , -size * GameDefine.TERRAIN_SIZE * i );

            }
        }
    }


    public void setTerrainNode1( int x , int z , float y , sbyte lh , short wl , byte fl )
    {
        nodes[ z ][ x ].y = y;
        nodes[ z ][ x ].layerHeight = lh;
        nodes[ z ][ x ].waterLevel = wl;
        nodes[ z ][ x ].flags = fl;
    }

    public void setTerrainTexture1( int x , int z , byte s , byte[] t , byte[] u , byte sd )
    {
        nodes[ z ][ x ].textureTypeSize = s;
        nodes[ z ][ x ].textureType = t;
        nodes[ z ][ x ].textureUV = u;
        nodes[ z ][ x ].shadow = sd;
    }

    public void setTerrainWater1( int x , int z , byte t , float y , Vector2[] c , sbyte s )
    {
        nodes[ z ][ x ].waterType = t;
        nodes[ z ][ x ].waterY = y;
        nodes[ z ][ x ].waterColor = c;
        nodes[ z ][ x ].waterShoreLine = s;
    }
    
    public W3TerrainNode getNode( int x , int z )
    {
        return nodes[ z ][ x ];
    }

    public W3TerrainSmallNode getSmallNode( int x , int z )
    {
        return smallNodes[ z ][ x ];
    }
    

    public void updateCliffs( int x , int z , string name , float y , byte t , float[] vz )
    {
        GameObject cliff = null;

        string cliffID = W3TerrainManager.instance.cliffTileSets[ t ];

        W3CliffTypesConfigData cliffdata = W3CliffTypesConfig.instance.getData( cliffID );

        if ( cliffdata == null )
        {
            return;
        }

        string ramp = cliffdata.cliffModelDir + "/" + cliffdata.cliffModelDir;

        try
        {
            cliff = (GameObject)Instantiate( (GameObject)Resources.Load( "Prefabs/Doodads/Terrain/" + ramp + name + "0" ) );
            cliff.name = name;
        }
        catch ( System.Exception ex )
        {
            return;
        }

        Transform terrainObject = GameObject.Find( "Map" ).transform.Find( "Terrain" ).transform;
        
        cliff.transform.parent = terrainObject;
        cliff.transform.localPosition = new Vector3( -x * GameDefine.TERRAIN_SIZE , y , -z * GameDefine.TERRAIN_SIZE );

        MeshFilter filter = cliff.GetComponentInChildren<MeshFilter>();
        Mesh mesh = filter.mesh;

        Vector3[] v = mesh.vertices;
        Vector2[] uv2 = new Vector2[ mesh.vertexCount ];

        for ( int i = 0 ; i < mesh.vertexCount ; i++ )
        {
            v[ i ].z = vz[ i ];

            Vector3 worldPt = filter.transform.TransformPoint( v[ i ] );

            uv2[ i ].x = -worldPt.x / W3TerrainManager.instance.width / GameDefine.TERRAIN_SIZE;
            uv2[ i ].y = -worldPt.z / W3TerrainManager.instance.height / GameDefine.TERRAIN_SIZE;
        }

        mesh.vertices = v;

        MeshRenderer renderer = cliff.GetComponentInChildren<MeshRenderer>();

        renderer.sharedMaterial = W3MaterialConfig.instance.getMaterial( "Materials/Cliff/" + cliffdata.texFile , "ReplaceableTextures/Cliff/" + W3TerrainManager.instance.mainTitleSet + "_" + cliffdata.texFile );

        renderer.receiveShadows = false;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        mesh.uv2 = uv2;

        cliffs.Add( cliff );
    }


    public void updateCliffsTrans( int x , int z , string name , float y , byte t , float[] vz )
    {
        GameObject cliff = null;

        string cliffID = W3TerrainManager.instance.cliffTileSets[ t ];

        W3CliffTypesConfigData cliffdata = W3CliffTypesConfig.instance.getData( cliffID );

        if ( cliffdata == null )
        {
            return;
        }

        string ramp = cliffdata.rampModelDir + "/" + cliffdata.rampModelDir;

        try
        {
            cliff = (GameObject)Instantiate( (GameObject)Resources.Load( "Prefabs/Doodads/Terrain/" + ramp + name + "0" ) );
            cliff.name = name;
        }
        catch ( System.Exception ex )
        {
            return;
        }

        Transform terrainObject = GameObject.Find( "Map" ).transform.Find( "Terrain" ).transform;

        cliff.transform.parent = terrainObject;
        cliff.transform.localPosition = new Vector3( -x * GameDefine.TERRAIN_SIZE , y , -z * GameDefine.TERRAIN_SIZE );

        MeshFilter filter = cliff.GetComponentInChildren<MeshFilter>();
        Mesh mesh = filter.mesh;

        Vector3[] v = mesh.vertices;
        Vector2[] uv2 = new Vector2[ mesh.vertexCount ];

        for ( int i = 0 ; i < mesh.vertexCount ; i++ )
        {
            v[ i ].z = vz[ i ];

            Vector3 worldPt = filter.transform.TransformPoint( v[ i ] );

            uv2[ i ].x = -worldPt.x / W3TerrainManager.instance.width / GameDefine.TERRAIN_SIZE;
            uv2[ i ].y = -worldPt.z / W3TerrainManager.instance.height / GameDefine.TERRAIN_SIZE;
        }

        mesh.vertices = v;
        
        MeshRenderer renderer = cliff.GetComponentInChildren<MeshRenderer>();

        renderer.sharedMaterial = W3MaterialConfig.instance.getMaterial( "Materials/Cliff/" + cliffdata.texFile , "ReplaceableTextures/Cliff/" + W3TerrainManager.instance.mainTitleSet + "_" + cliffdata.texFile );

        renderer.receiveShadows = false;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        mesh.uv2 = uv2;

        cliffTrans.Add( cliff );
    }

	public int getTextureType( string id )
	{
		for ( int i = 0 ; i < groundTileSetsCount ; i++ )
		{
			if ( groundTileSets[ i ] == id )
			{
				return i;
			}
		}

		return 0;
	}

	public string getTexture( int n )
	{
		W3TerrainConfigData d = W3TerrainConfig.instance.getData( groundTileSets[ n ] );

		return d.dir + "/" + 
			d.file;
	}

    public void createMap( int w , int h )
    {
        width = w;
        height = h;

        Shader.SetGlobalFloat( "W3Width" , width );
        Shader.SetGlobalFloat( "W3Height" , height );

        createNodes( w , h );

        isLoaded = true;
    }

    public void createMapSprites()
    {
        createSprites();
        createSpritesWater();
    }

    public void updateTerrainSmallNode()
    {
        for ( int i = 0 ; i < W3TerrainManager.instance.height ; i++ )
        {
            for ( int j = 0 ; j < W3TerrainManager.instance.width ; j++ )
            {
                float y0 = nodes[ i ][ j + 1 ].y;
                float y1 = nodes[ i ][ j ].y;
                float y2 = nodes[ i + 1 ][ j + 1 ].y;
                float y3 = nodes[ i + 1 ][ j ].y;

                float y01 = ( y0 - y1 ) * 0.25f;
                float y31 = ( y3 - y1 ) * 0.25f;
                float y21 = ( y2 - y1 ) * 0.25f;
                float y20 = ( y2 - y0 ) * 0.25f;
                float y23 = ( y2 - y3 ) * 0.25f;

                smallNodes[ i * 4 ][ j * 4 + 0 ].ym = ( y1 + y1 + y01 + y1 + y31 + y1 + y21 ) * 0.25f;
                smallNodes[ i * 4 ][ j * 4 + 1 ].ym = ( y1 + y01 + y1 + 2 * y01 + y1 + y21 + y1 + y21 + y01 ) * 0.25f;
                smallNodes[ i * 4 ][ j * 4 + 2 ].ym = ( y1 + 2 * y01 + y1 + 3 * y01 + y1 + y21 + y01 + y1 + y21 + 2 * y01 ) * 0.25f;
                smallNodes[ i * 4 ][ j * 4 + 3 ].ym = ( y1 + 3 * y01 + y0 + y1 + y21 + 2 * y01 + y0 + y20 ) * 0.25f;

                smallNodes[ i * 4 + 1 ][ j * 4 + 0 ].ym = ( y1 + y31 + y1 + y21 + y1 + 2 * y31 + y1 + y21 + y31 ) * 0.25f;
                smallNodes[ i * 4 + 1 ][ j * 4 + 1 ].ym = ( y1 + y21 + y1 + y21 + y01 + y1 + y21 + y31 + y1 + 2 * y21 ) * 0.25f;
                smallNodes[ i * 4 + 1 ][ j * 4 + 2 ].ym = ( y1 + y21 + y01 + y1 + y21 + 2 * y01 + y1 + 2 * y21 + y1 + 2 * y21 + y01 ) * 0.25f;
                smallNodes[ i * 4 + 1 ][ j * 4 + 3 ].ym = ( y1 + y21 + 2 * y01 + y0 + y20 + y1 + 2 * y21 + y01 + y0 + 2 * y20 ) * 0.25f;

                smallNodes[ i * 4 + 2 ][ j * 4 + 0 ].ym = ( y1 + 2 * y31 + y1 + y21 + y31 + y1 + 3 * y31 + y1 + y21 + 2 * y31 ) * 0.25f;
                smallNodes[ i * 4 + 2 ][ j * 4 + 1 ].ym = ( y1 + y21 + y31 + y1 + 2 * y21 + y1 + 3 * y31 + y1 + y21 + 2 * y31 ) * 0.25f;
                smallNodes[ i * 4 + 2 ][ j * 4 + 2 ].ym = ( y1 + 2 * y21 + y1 + 2 * y21 + y01 + y1 + 2 * y21 + y31 + y1 + 3 * y21 ) * 0.25f;
                smallNodes[ i * 4 + 2 ][ j * 4 + 3 ].ym = ( y1 + 2 * y21 + y01 + y0 + 2 * y20 + y1 + 3 * y21 + y0 + 3 * y20 ) * 0.25f;

                smallNodes[ i * 4 + 3 ][ j * 4 + 0 ].ym = ( y1 + 3 * y31 + y1 + y21 + 2 * y31 + y3 + y3 + y23 ) * 0.25f;
                smallNodes[ i * 4 + 3 ][ j * 4 + 1 ].ym = ( y1 + y21 + 2 * y31 + y1 + 2 * y21 + y31 + y3 + y23 + y3 + 2 * y23 ) * 0.25f;
                smallNodes[ i * 4 + 3 ][ j * 4 + 2 ].ym = ( y1 + 2 * y21 + y31 + y1 + 3 * y21 + y3 + 2 * y23 + y3 + 3 * y23 ) * 0.25f;
                smallNodes[ i * 4 + 3 ][ j * 4 + 3 ].ym = ( y1 + 3 * y21 + y0 + 3 * y20 + y3 + 3 * y23 + y2 ) * 0.25f;



                y0 = nodes[ i ][ j + 1 ].y;
                y1 = nodes[ i ][ j ].y;
                y2 = nodes[ i + 1 ][ j + 1 ].y;
                y3 = nodes[ i + 1 ][ j ].y;

                y01 = ( y0 - y1 ) * 0.25f;
                y31 = ( y3 - y1 ) * 0.25f;
                y21 = ( y2 - y1 ) * 0.25f;
                y20 = ( y2 - y0 ) * 0.25f;
                y23 = ( y2 - y3 ) * 0.25f;

                smallNodes[ i * 4 ][ j * 4 + 0 ].y = y1;
                smallNodes[ i * 4 ][ j * 4 + 1 ].y = y1 + y01;
                smallNodes[ i * 4 ][ j * 4 + 2 ].y = y1 + 2 * y01;
                smallNodes[ i * 4 ][ j * 4 + 3 ].y = y1 + 3 * y01;
//                smallNodes[ i * 4 ][ j * 4 + 4 ].y = y0;

                smallNodes[ i * 4 + 1 ][ j * 4 + 0 ].y = y1 + y31;
                smallNodes[ i * 4 + 1 ][ j * 4 + 1 ].y = y1 + y21;
                smallNodes[ i * 4 + 1 ][ j * 4 + 2 ].y = y1 + y21 + y01;
                smallNodes[ i * 4 + 1 ][ j * 4 + 3 ].y = y1 + y21 + 2 * y01;
//                smallNodes[ i * 4 + 1 ][ j * 4 + 4 ].y = y0 + y20;

                smallNodes[ i * 4 + 2 ][ j * 4 + 0 ].y = y1 + 2 * y31;
                smallNodes[ i * 4 + 2 ][ j * 4 + 1 ].y = y1 + y21 + y31;
                smallNodes[ i * 4 + 2 ][ j * 4 + 2 ].y = y1 + 2 * y21;
                smallNodes[ i * 4 + 2 ][ j * 4 + 3 ].y = y1 + 2 * y21 + y01;
//                smallNodes[ i * 4 + 2 ][ j * 4 + 4 ].y = y0 + 2 * y20;

                smallNodes[ i * 4 + 3 ][ j * 4 + 0 ].y = y1 + 3 * y31;
                smallNodes[ i * 4 + 3 ][ j * 4 + 1 ].y = y1 + y21 + 2 * y31;
                smallNodes[ i * 4 + 3 ][ j * 4 + 2 ].y = y1 + 2 * y21 + y31;
                smallNodes[ i * 4 + 3 ][ j * 4 + 3 ].y = y1 + 3 * y21;
//                smallNodes[ i * 4 + 3 ][ j * 4 + 4 ].y = y0 + 3 * y20;

//                 smallNodes[ i * 4 + 4 ][ j * 4 + 0 ].y = y3;
//                 smallNodes[ i * 4 + 4 ][ j * 4 + 1 ].y = y3 + y23;
//                 smallNodes[ i * 4 + 4 ][ j * 4 + 2 ].y = y3 + 2 * y23;
//                 smallNodes[ i * 4 + 4 ][ j * 4 + 3 ].y = y3 + 3 * y23;
//                smallNodes[ i * 4 + 4 ][ j * 4 + 4 ].y = y2;


            }
        }

    }










    public int getTerrainCliffLevel( float x , float y )
    {
        return 0;
    }

    public void setWaterBaseColor( int red , int green , int blue , int alpha )
    {
    }





    public void setBlight( int whichPlayer , float x , float y , float radius , bool addBlight )
    {
    }

    public void setBlightRect( int whichPlayer , float minX , float minY , float maxX , float maxY , bool addBlight )
    {
    }

    public void setBlightPoint( int whichPlayer , float x , float y , bool addBlight )
    {
    }

    public int createBlightedGoldmine( int id , float x , float y , float face )
    {
        return 0;
    }




    public void setDoodadAnimation( float x , float y , float radius , int doodadID , bool nearestOnly , string animName , bool animRandom )
    {
    }

    public void setDoodadAnimationRect( float minX , float minY , float maxX , float maxY , int doodadID , string animName , bool animRandom )
    {
    }








    public void startMeleeAI( int num , string script )
    {
    }

    public void startCampaignAI( int num , string script )
    {
    }

    public void commandAI( int num , int command , int data )
    {
    }

    public void pauseCompAI( int p , bool pause )
    {
    }

    public int getAIDifficulty( int num )
    {
        return 0;
    }

    public void removeGuardPosition( int hUnit )
    {
    }

    public void recycleGuardPosition( int hUnit )
    {
    }

    public void removeAllGuardPositions( int num )
    {
    }

    public void cheat( string cheatStr )
    {
    }

    public bool isNoVictoryCheat()
    {
        return false;
    }

    public bool isNoDefeatCheat()
    {
        return false;
    }

    public void Preload( string filename )
    {
    }

    public void PreloadEnd( double timeout )
    {
    }

    public void PreloadStart()
    {
    }

    public void PreloadRefresh()
    {
    }

    public void PreloadEndEx()
    {
    }

    public void PreloadGenClear()
    {
    }

    public void PreloadGenStart()
    {
    }

    public void PreloadGenEnd( string filename )
    {
    }

    public void Preloader( string filename )
    {
    }







}

