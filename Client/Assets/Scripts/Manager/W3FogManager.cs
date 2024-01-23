using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.Collections;

public class W3FogManager : SingletonMono< W3FogManager >
{

    public class W3FogStart
    {
        public short startX;
        public short startZ;
        public float visionRange;
        public float upVision;

        public short rangeXMin;
        public short rangeZMin;
        public short rangeXMax;
        public short rangeZMax;
    }

    public class W3FogChange
    {
        public int index;
        public byte oldValue;
        public float step;
    }


    Thread fovThread = null;
    Thread fovThreadUpdae = null;


    public Texture2D shadowTex;
    public Texture2D fogTex;

    NativeArray< byte > rawFogTex;
    NativeArray< byte > rawShadowTex;

    public int fogWidth;
    public int fogHeight;

    public byte[] shadowBuffer;

    public byte[] fogPixelBuffer;
    public byte[] fogBuffer;
    public byte[] fogBufferBlur;

    public bool isLoaded = false;

    bool fogCalculateStart = false;
    bool fogUpdateStart = false;
    bool fogUpdating = false;
    bool fogCalculating = false;

    bool updateFogTex = false;
//    bool updateShadowTex = false;

    int fogCalculateTime = 0;
    int fogUpdateTime = 0;
    
    List< W3FogStart > fogStartList = new List< W3FogStart >();
    List< W3FogChange > fogChangeList = new List< W3FogChange >();

    List< int > fogUnitIDAdd = new List< int >();
    List< int > fogUnitIDRemove = new List< int >();

    List< int > fogUnitID = new List< int >();
    List< int > fogUnitIDOld = new List< int >();

    List< int > fogDoodadID = new List< int >();

    public Color dayColor = new Color( 1.0f , 1.0f , 1.0f , 0.0f );

    public void setShadow( int x , int z , int w , int h , byte[] b )
    {
        for ( int i = 0 ; i < h ; i++ )
        {
            for ( int j = 0 ; j < w ; j++ )
            {
                int index = z * fogWidth + i * fogWidth + x + j;

                rawShadowTex[ index ] = shadowBuffer[ index ];

                if ( rawShadowTex[ index ] > b[ i * w + j ] )
                    rawShadowTex[ index ] = b[ i * w + j ];
            }
        }

        shadowTex.Apply();
    }

    public void clearShadow( int x , int z , int w , int h )
    {
        for ( int i = 0 ; i < h ; i++ )
        {
            for ( int j = 0 ; j < w ; j++ )
            {
                int index = z * fogWidth + i * fogWidth + x + j;

                rawShadowTex[ index ] = shadowBuffer[ index ];
            }
        }

        shadowTex.Apply();
    }


    byte blurFog( int x , int z )
    {
        int blurRadius = 1;
        int count = blurRadius * 2 + 1;
        count *= count;

        int a = 0;

        for ( int i = -blurRadius ; i <= blurRadius ; i++ )
        {
            for ( int j = -blurRadius ; j <= blurRadius ; j++ )
            {
                a += fogBuffer[ ( z + i ) * fogWidth + ( x + j ) ];
            }
        }

        return (byte)( a / count );
    }

    public void calculateFog()
    {
        fogCalculateStart = true;
    }

    public void readyCaulateFog()
    {
        fogStartList.Clear();

        List< W3Unit > units = W3PlayerManager.instance.getLocalPlayer().units;

        for ( int i = 0 ; i < units.Count ; i++ )
        {
            W3Unit unit = units[ i ];
            Vector3 pos = unit.getPosition();

            W3FogStart s = new W3FogStart();
            s.startX = (short)( -pos.x / GameDefine.TERRAIN_SIZE_PER );
            s.startZ = (short)( -pos.z / GameDefine.TERRAIN_SIZE_PER );
            s.upVision = unit.upVision;
            s.visionRange = unit.visionRange();

            s.rangeXMin = (short)( s.startX - s.visionRange - 1 );
            if ( s.rangeXMin < 0 )
                s.rangeXMin = 0;
            s.rangeZMin = (short)( s.startZ - s.visionRange - 1 );
            if ( s.rangeZMin < 0 )
                s.rangeZMin = 0;

            s.rangeXMax = (short)( s.startX + s.visionRange + 1 );
            if ( s.rangeXMax >= fogWidth )
                s.rangeXMax = (short)( fogWidth - 1 );
            s.rangeZMax = (short)( s.startZ + s.visionRange + 1 );
            if ( s.rangeZMax >= fogHeight )
                s.rangeZMax = (short)( fogHeight - 1 );

            fogStartList.Add( s );
        }

    }

    public void calculateFogThread()
    {
        fogDoodadID.Clear();
        fogUnitID.Clear();
        fogChangeList.Clear();

        // first copy pixel buffer
        System.Array.Copy( fogPixelBuffer , fogBuffer , fogPixelBuffer.Length );

        // calculate all units
        for ( int i = 0 ; i < fogStartList.Count ; i++ )
        {
            W3FogStart s = fogStartList[ i ];

            int startCoordinate = s.startZ * fogWidth + s.startX;
            fogBuffer[ startCoordinate ] = 255;

            float uv = W3TerrainManager.instance.smallNodes[ s.startZ ][ s.startX ].ym + s.upVision;

            // Top Left
            castLight( 1 , 1.0f , 0.0f , s.startX , s.startZ , 0 , 1 , -1 , 0 , s.visionRange , uv );
            castLight( 1 , 1.0f , 0.0f , s.startX , s.startZ , 1 , 0 , 0 , -1 , s.visionRange , uv );

            // Top right
            castLight( 1 , 1.0f , 0.0f , s.startX , s.startZ , -1 , 0 , 0 , -1 , s.visionRange , uv );
            castLight( 1 , 1.0f , 0.0f , s.startX , s.startZ , 0 , -1 , -1 , 0 , s.visionRange , uv );

            // Bottom Right
            castLight( 1 , 1.0f , 0.0f , s.startX , s.startZ , 0 , -1 , 1 , 0 , s.visionRange , uv );
            castLight( 1 , 1.0f , 0.0f , s.startX , s.startZ , -1 , 0 , 0 , 1 , s.visionRange , uv );

            // Bottom Left
            castLight( 1 , 1.0f , 0.0f , s.startX , s.startZ , 1 , 0 , 0 , 1 , s.visionRange , uv );
            castLight( 1 , 1.0f , 0.0f , s.startX , s.startZ , 0 , 1 , 1 , 0 , s.visionRange , uv );


            for ( int i0 = s.rangeXMin ; i0 < s.rangeXMax ; i0++ )
            {
                for ( int j0 = s.rangeZMin ; j0 < s.rangeZMax ; j0++ )
                {
                    int index = j0 * fogWidth + i0;

                    float r = GameMath.sqrt( i0 - s.startX , j0 - s.startZ );

                    // update doodad
                    if ( r < s.visionRange &&
                        fogDoodadID.Contains( W3TerrainManager.instance.smallNodes[ j0 ][ i0 ].doodadID ) )
                    {
                        byte a = 255;

                        if ( a > fogBuffer[ index ] )
                        {
                            fogBuffer[ index ] = a;
                        }
                    }

                    // explored, update pixel buffer
                    if ( fogPixelBuffer[ index ] != 96 )
                    {
                        if ( r < s.visionRange )
                        {
                            byte a = (byte)( 96 - ( 96 - 32 ) / ( s.visionRange * 0.25f ) * ( r - s.visionRange * 0.75f ) );

                            if ( r < s.visionRange * 0.75f )
                            {
                                a = 96;
                            }

                            if ( fogPixelBuffer[ index ] < a )
                            {
                                fogPixelBuffer[ index ] = a;
                            }
                        }
                    }

                }
            }

        }


        // blur fog
        for ( int i = 0 ; i < fogStartList.Count ; i++ )
        {
            W3FogStart s = fogStartList[ i ];

            int idx = 0;
            for ( int i0 = s.rangeXMin ; i0 < s.rangeXMax ; i0++ )
            {
                for ( int j0 = s.rangeZMin ; j0 < s.rangeZMax ; j0++ )
                {
                    fogBufferBlur[ idx ] = blurFog( i0 , j0 );
                    idx++;
                }
            }

            idx = 0;
            for ( int i0 = s.rangeXMin ; i0 < s.rangeXMax ; i0++ )
            {
                for ( int j0 = s.rangeZMin ; j0 < s.rangeZMax ; j0++ )
                {
                    fogBuffer[ j0 * fogWidth + i0 ] = fogBufferBlur[ idx ];
                    idx++;
                }
            }
        }

        // check whole map
        for ( int i = 0 ; i < fogBuffer.Length ; i++ )
        {
            if ( fogBuffer[ i ] != rawFogTex[ i ] )
            {
                float step = ( fogBuffer[ i ] - rawFogTex[ i ] ) * 0.05f;

//                 if ( Mathf.Abs( step ) > 0.05f )
//                 {
                    W3FogChange c = new W3FogChange();
                    c.index = i;
                    c.oldValue = rawFogTex[ i ];
                    c.step = step;
                    fogChangeList.Add( c );
//                 }
//                 else
//                 {
//                     // one step
//                     raw[ i ] = fogBuffer[ i ];
//                 }
            }
        }

        // check unit
        for ( int i = 0 ; i < fogUnitIDOld.Count ; )
        {
            if ( fogUnitID.Contains( fogUnitIDOld[ i ] ) )
            {
                fogUnitID.Remove( fogUnitIDOld[ i ] );
                i++;
            }
            else
            {
                fogUnitIDRemove.Add( fogUnitIDOld[ i ] );
                fogUnitIDOld.RemoveAt( i );
            }
        }

        for ( int i = 0 ; i < fogUnitID.Count ; i++ )
        {
            fogUnitIDAdd.Add( fogUnitID[ i ] );
            fogUnitIDOld.Add( fogUnitID[ i ] );
        }

        fogUpdateTime = 0;
        fogUpdateStart = true;

        fogCalculating = false;
    }


    private void castLight( int row , float start , float end , int startX , int startZ , int xx , int xz , int zx , int zz , float visionRange , float upVision )
    {
        float newStart = 0.0f;
        if ( start < end )
        {
            return;
        }

        bool blocked = false;

        for ( int distance = row ; distance <= visionRange && !blocked ; distance++ )
        {
            int deltaZ = -distance;
            for ( int deltaX = -distance ; deltaX <= 0 ; deltaX++ )
            {
                int currentX = startX + deltaX * xx + deltaZ * xz;
                int currentZ = startZ + deltaX * zx + deltaZ * zz;

                int coordinate = Mathf.FloorToInt( currentZ ) * fogWidth + Mathf.FloorToInt( currentX );

                float leftSlope = ( deltaX - 0.5f ) / ( deltaZ + 0.5f );
                float rightSlope = ( deltaX + 0.5f ) / ( deltaZ - 0.5f );

                if ( !( currentX >= 0 && currentZ >= 0 && currentX < fogWidth && currentZ < fogHeight ) || start < rightSlope )
                {
                    continue;
                }
                else if ( end > leftSlope )
                {
                    break;
                }

                float r = GameMath.sqrt( deltaX , deltaZ );

                if ( r <= visionRange )
                {
                    if ( !W3TerrainManager.instance.isBlockUp( currentX , currentZ , upVision ) )
                    {
                        byte a = (byte)( 255 - ( 255 - 32 ) / ( visionRange * 0.25f ) * ( r - visionRange * 0.75f ) );

                        if ( r < visionRange * 0.75f )
                        {
                            a = 255;
                        }

                        if ( a > fogBuffer[ coordinate ] )
                        {
                            fogBuffer[ coordinate ] = a;
                        }

                        unsafe
                        {
                            int uid = W3PathFinder.instance.unit[ currentZ ][ currentX ];


                            if ( uid > 0 && 
                                a >= 240 &&
                                !fogUnitID.Contains( uid ) )
                            {
                                W3Unit unit = W3BaseManager.instance.getUnit( uid );

                                if ( unit.isEnemy( W3PlayerManager.instance.localPlayer ) )
                                {
                                    fogUnitID.Add( uid );
                                }
                            }
                        }

                    }
                }

                if ( blocked )
                {
                    if ( W3TerrainManager.instance.smallNodes[ currentZ ][ currentX ].doodadID > 0 &&
                        !fogDoodadID.Contains( W3TerrainManager.instance.smallNodes[ currentZ ][ currentX ].doodadID ) )
                    {
                        fogDoodadID.Add( W3TerrainManager.instance.smallNodes[ currentZ ][ currentX ].doodadID );
                    }

                    if ( W3TerrainManager.instance.isBlockUp( currentX , currentZ , upVision ) )
                    {
                        newStart = rightSlope;
                        continue;
                    }
                    else
                    {
                        blocked = false;
                        start = newStart;
                    }
                }
                else
                {
                    if ( W3TerrainManager.instance.isBlockUp( currentX , currentZ , upVision ) && distance < visionRange )
                    {
                        blocked = true;
                        castLight( distance + 1 , start , leftSlope , startX , startZ , xx , xz , zx , zz , visionRange , upVision );
                        newStart = rightSlope;
                    }
                }
            }
        }

    }

    void fogUpdateThread()
    {
        fogUpdateTime++;

        if ( fogUpdateTime == 20 )
        {
            for ( int i = 0 ; i < fogChangeList.Count ; i++ )
            {
                W3FogChange c = fogChangeList[ i ];
                rawFogTex[ c.index ] = fogBuffer[ c.index ];
            }

            fogUpdateStart = false;
            fogUpdateTime = 0;
        }
        else
        {
            for ( int i = 0 ; i < fogChangeList.Count ; i++ )
            {
                W3FogChange c = fogChangeList[ i ];
                rawFogTex[ c.index ] = (byte)( c.oldValue + c.step * fogUpdateTime );
            }
        }

        updateFogTex = true;

        // wait for update ?
//         while ( updateTexture2D )
//         {
//             Thread.Sleep( 1 );
//         }

        fogUpdating = false;
    }


    public void FixedUpdate()
    {
        if ( !isLoaded )
        {
            return;
        }

        if ( updateFogTex )
        {
            fogTex.Apply( false );
            updateFogTex = false;
        }

        if ( fogCalculateStart )
        {
            fogCalculateTime++;

            if ( fogCalculateTime > 5 && 
                !fogUpdating )
            {
                if ( fovThread == null ||
                    !fovThread.IsAlive )
                {
                    fogCalculating = true;
                    fogCalculateStart = false;
                    fogCalculateTime = 0;

                    // main thread
                    readyCaulateFog();

                    // other thread
                    fovThread = new Thread( new ThreadStart( calculateFogThread ) );
                    fovThread.Start();
                }
            }

        }


        if ( fogUpdateStart && 
            !fogCalculating )
        {
            if ( fovThreadUpdae == null ||
                !fovThreadUpdae.IsAlive )
            {
                fogUpdating = true;

                // other thread
                fovThreadUpdae = new Thread( new ThreadStart( fogUpdateThread ) );
                fovThreadUpdae.Start();
            }
        }


        if ( !fogCalculating )
        {
            if ( fogUnitIDAdd.Count > 0 )
            {
                for ( int i = 0 ; i < fogUnitIDAdd.Count ; i++ )
                {
                    W3Unit unit = W3BaseManager.instance.getUnit( fogUnitIDAdd[ i ] );
                    unit.setActive( true );
                }

                fogUnitIDAdd.Clear();
            }

            if ( fogUnitIDRemove.Count > 0 )
            {
                for ( int i = 0 ; i < fogUnitIDRemove.Count ; i++ )
                {
                    W3Unit unit = W3BaseManager.instance.getUnit( fogUnitIDRemove[ i ] );
                    unit.setActive( false );
                }

                fogUnitIDRemove.Clear();
            }
        }


        //        Shader.SetGlobalColor( "W3ColorDay0" , W3MapManager.instance.dayColor );

    }



    public void createMap( int w , int h )
    {
        fogWidth = w * 4;
        fogHeight = h * 4;

        Shader.SetGlobalFloat( "W3FogWidth" , fogWidth * GameDefine.TERRAIN_SIZE_PER );
        Shader.SetGlobalFloat( "W3FogHeight" , fogHeight * GameDefine.TERRAIN_SIZE_PER );

        fogPixelBuffer = new byte[ fogWidth * fogHeight ];
        fogBuffer = new byte[ fogWidth * fogHeight ];
        fogBufferBlur = new byte[ fogWidth * fogHeight ];

        shadowBuffer = new byte[ fogWidth * fogHeight ];

        fogTex = new Texture2D( fogWidth , fogHeight , TextureFormat.Alpha8 , false , true );
        shadowTex = new Texture2D( fogWidth , fogHeight , TextureFormat.Alpha8 , false , true );

        rawFogTex = fogTex.GetRawTextureData< byte >();
        rawShadowTex = shadowTex.GetRawTextureData< byte >();

        isLoaded = true;
    }

}

