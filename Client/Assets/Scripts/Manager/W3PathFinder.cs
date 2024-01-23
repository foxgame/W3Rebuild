using UnityEngine;
using System.Collections;
using System.Runtime;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;



public unsafe class W3PathFinder : SingletonMono< W3PathFinder >
{
    public struct PathNode
    {
        public PathNode* parent;

        public int posX;
        public int posZ;

        public int f;
        public int g;

        public void clear()
        {
            parent = null;

            f = 0;
            g = 0;
        }
    };

    public class SortedSetNode : IComparable< SortedSetNode >
    {
        public PathNode* node;

        public int CompareTo( SortedSetNode other )
        {
            if ( node->f == other.node->f )
            {
                if ( node == other.node )
                    return 0;
                else
                    return node > other.node ? 1 : -1;
            }
            else
                return node->f > other.node->f ? 1 : -1;
        }
    }


    const short MAXCOUNT = 4096;
    const short MAXOUTCOUNT = 4096 * 2;


    SortedSet< SortedSetNode > sortedSetNodes = new SortedSet< SortedSetNode >();
    List< SortedSetNode > closedList = new List< SortedSetNode >();
    List< SortedSetNode > cacheNode = new List< SortedSetNode >();

    PathNode[] neighbors = new PathNode[ 8 ];
    int neighborsCount = 0;

    byte[] startBuffer = new byte[ 8 * 8 ];

    PathNode* resultNode = null;
    PathNode** nodes = null;
    byte** close = null;
    public byte** region = null;
    public byte** block = null;
    public int** unit = null;
    public uint** unitPathTime = null;

    public uint pathTime = 0;
    public bool pathLine = false;

    int direction;
    public int direction4;

    public int nearPosX;
    public int nearPosZ;

    int unitID = 0;
    byte unitSize = 0;
    public byte unitSizeN = 0;
    public byte unitSizeN1 = 0;
    public byte unitSizeN2 = 0;

    byte pathRegion = 0;

    public int* bufferOut;

    int startPosX;
    int startPosZ;
    public int endPosX;
    public int endPosZ;

    public void release()
    {
    }

    public void clearCache()
    {
        cacheNode.Clear();
    }

    public void addCache( int x , int z )
    {
        SortedSetNode ssn = new SortedSetNode();
        ssn.node = &( nodes[ z ][ x ] );
        cacheNode.Add( ssn );
    }


    void clear()
    {
        resultNode = null;

        for ( int i = 0 ; i < closedList.Count ; i++ )
        {
            PathNode* node = closedList[ i ].node;
            node->clear();
            close[ node->posZ ][ node->posX ] = 0;
        }

        closedList.Clear();
        sortedSetNodes.Clear();
    }


    public void initMap( int x , int z )
    {
        IntPtr unitPtr = Marshal.AllocHGlobal( z * sizeof(int*) );
        unit = (int**)unitPtr.ToPointer();

        IntPtr unitPathTimePtr = Marshal.AllocHGlobal( z * sizeof(uint*) );
        unitPathTime = (uint**)unitPathTimePtr.ToPointer();

        IntPtr blockPtr = Marshal.AllocHGlobal( z * sizeof(byte*) );
        block = (byte**)blockPtr.ToPointer();

        IntPtr regionPtr = Marshal.AllocHGlobal( z * sizeof(byte*) );
        region = (byte**)regionPtr.ToPointer();

        IntPtr closePtr = Marshal.AllocHGlobal( z * sizeof(byte*) );
        close = (byte**)closePtr.ToPointer();

        IntPtr bufferOutPtr = Marshal.AllocHGlobal( MAXOUTCOUNT * sizeof(int) );
        bufferOut = (int*)bufferOutPtr.ToPointer();

        for ( int i = 0 ; i < z ; i++ )
        {
            IntPtr unitPtr1 = Marshal.AllocHGlobal( x * sizeof(int) );
            unit[ i ] = (int*)unitPtr1.ToPointer();

            IntPtr unitPathTimePtr1 = Marshal.AllocHGlobal( x * sizeof(uint) );
            unitPathTime[ i ] = (uint*)unitPathTimePtr1.ToPointer();

            IntPtr blockPtr1 = Marshal.AllocHGlobal( x * sizeof(byte) );
            block[ i ] = (byte*)blockPtr1.ToPointer();

            IntPtr regionPtr1 = Marshal.AllocHGlobal( x * sizeof(byte) );
            region[ i ] = (byte*)regionPtr1.ToPointer();

            IntPtr closePtr1 = Marshal.AllocHGlobal( x * sizeof(byte) );
            close[ i ] = (byte*)closePtr1.ToPointer();

            for ( int j = 0 ; j < x ; j++ )
            {
                region[ i ][ j ] = 0;
                close[ i ][ j ] = 0;
                unit[ i ][ j ] = 0;
                unitPathTime[ i ][ j ] = 0;

                byte b = W3TerrainManager.instance.smallNodes[ i ][ j ].path;
                byte bb = ( ( ( b & (byte)W3PathType.NOWALK ) == (byte)W3PathType.NOWALK ) ? (byte)0 : (byte)1 );
                block[ i ][ j ] = bb;
            }
        }

        for ( int i = 8 ; i < z - 8 ; i++ )
        {
            for ( int j = 8 ; j < x - 8 ; j++ )
            {
                block[ i ][ j ] += (byte)getVaildSize( j , i , 2 );
                block[ i ][ j ] += (byte)getVaildSize( j , i , 3 );
                block[ i ][ j ] += (byte)getVaildSize( j , i , 4 );
                block[ i ][ j ] += (byte)getVaildSize( j , i , 5 );
                block[ i ][ j ] += (byte)getVaildSize( j , i , 6 );
                block[ i ][ j ] += (byte)getVaildSize( j , i , 7 );
                block[ i ][ j ] += (byte)getVaildSize( j , i , 8 );
            }
        }

        resultNode = null;
    }

    public void initPathRegion( int x , int z )
    {
        IntPtr regionPtr = Marshal.AllocHGlobal( z * sizeof( byte* ) );
        region = (byte**)regionPtr.ToPointer();

        IntPtr closePtr = Marshal.AllocHGlobal( z * sizeof( byte* ) );
        close = (byte**)closePtr.ToPointer();

        IntPtr nodesPtr = Marshal.AllocHGlobal( z * sizeof( PathNode* ) );
        nodes = (PathNode**)nodesPtr.ToPointer();

        for ( int i = 0 ; i < z ; i++ )
        {
            IntPtr regionPtr1 = Marshal.AllocHGlobal( x * sizeof( byte ) );
            region[ i ] = (byte*)regionPtr1.ToPointer();

            IntPtr closePtr1 = Marshal.AllocHGlobal( x * sizeof( byte ) );
            close[ i ] = (byte*)closePtr1.ToPointer();

            IntPtr nodesPtr1 = Marshal.AllocHGlobal( x * sizeof( PathNode ) );
            nodes[ i ] = (PathNode*)nodesPtr1.ToPointer();

            for ( int j = 0 ; j < x ; j++ )
            {
                nodes[ i ][ j ].clear();

                nodes[ i ][ j ].posX = j;
                nodes[ i ][ j ].posZ = i;

                region[ i ][ j ] = 0;
                close[ i ][ j ] = 0;
            }
        }


        int c = 0;
        byte r = 0;
        for ( int i = 0 ; i < z ; i++ )
        {
            for ( int j = 0 ; j < x ; j++ )
            {
                if ( !W3TerrainManager.instance.isPath( j , i , W3PathType.NOWALK ) &&
                    close[ i ][ j ] == 0 )
                {
                    r++;
                    findRegion( r , j , i );

                    while ( closedList.Count > 0 )
                    {
                        SortedSetNode ssn = closedList[ 0 ];
                        closedList.RemoveAt( 0 );

                        findRegion( r , ssn.node->posX , ssn.node->posZ );
                    }
                }

                c++;
            }
        }

        for ( int i = 0 ; i < z ; i++ )
        {
            for ( int j = 0 ; j < x ; j++ )
            {
                close[ i ][ j ] = 0;
            }
        }
    }

    void findRegion( byte r , int x , int z )
    {
        int posX = x;
        int posZ = z;

        if ( !W3TerrainManager.instance.isPath( posX , posZ , W3PathType.NOWALK ) &&
        close[ posZ ][ posX ] == 0 )
        {
            close[ posZ ][ posX ] = 1;
            region[ posZ ][ posX ] = r;
        }


        // e
        posX = x;
        posZ = z;
        ++posX;
        if ( !W3TerrainManager.instance.isPath( posX , posZ , W3PathType.NOWALK ) &&
        close[ posZ ][ posX ] == 0 )
        {
            close[ posZ ][ posX ] = 1;
            region[ posZ ][ posX ] = r;

            SortedSetNode ssn = new SortedSetNode();
            ssn.node = &( nodes[ posZ ][ posX ] );
            closedList.Add( ssn );
        }

        // ne
        posX = x;
        posZ = z;
        ++posX;
        ++posZ;
        if ( !W3TerrainManager.instance.isPath( posX , posZ , W3PathType.NOWALK ) &&
        close[ posZ ][ posX ] == 0 )
        {
            close[ posZ ][ posX ] = 1;
            region[ posZ ][ posX ] = r;

            SortedSetNode ssn = new SortedSetNode();
            ssn.node = &( nodes[ posZ ][ posX ] );
            closedList.Add( ssn );
        }

        // nw
        posX = x;
        posZ = z;
        --posX;
        ++posZ;
        if ( !W3TerrainManager.instance.isPath( posX , posZ , W3PathType.NOWALK ) &&
        close[ posZ ][ posX ] == 0 )
        {
            close[ posZ ][ posX ] = 1;
            region[ posZ ][ posX ] = r;

            SortedSetNode ssn = new SortedSetNode();
            ssn.node = &( nodes[ posZ ][ posX ] );
            closedList.Add( ssn );
        }

        // n
        posX = x;
        posZ = z;
        ++posZ;
        if ( !W3TerrainManager.instance.isPath( posX , posZ , W3PathType.NOWALK ) &&
        close[ posZ ][ posX ] == 0 )
        {
            close[ posZ ][ posX ] = 1;
            region[ posZ ][ posX ] = r;

            SortedSetNode ssn = new SortedSetNode();
            ssn.node = &( nodes[ posZ ][ posX ] );
            closedList.Add( ssn );
        }

        // se
        posX = x;
        posZ = z;
        ++posX;
        --posZ;
        if ( !W3TerrainManager.instance.isPath( posX , posZ , W3PathType.NOWALK ) &&
        close[ posZ ][ posX ] == 0 )
        {
            close[ posZ ][ posX ] = 1;
            region[ posZ ][ posX ] = r;

            SortedSetNode ssn = new SortedSetNode();
            ssn.node = &( nodes[ posZ ][ posX ] );
            closedList.Add( ssn );
        }

        // s
        posX = x;
        posZ = z;
        --posZ;
        if ( !W3TerrainManager.instance.isPath( posX , posZ , W3PathType.NOWALK ) &&
        close[ posZ ][ posX ] == 0 )
        {
            close[ posZ ][ posX ] = 1;
            region[ posZ ][ posX ] = r;

            SortedSetNode ssn = new SortedSetNode();
            ssn.node = &( nodes[ posZ ][ posX ] );
            closedList.Add( ssn );
        }

        // sw
        posX = x;
        posZ = z;
        --posX;
        --posZ;
        if ( !W3TerrainManager.instance.isPath( posX , posZ , W3PathType.NOWALK ) &&
        close[ posZ ][ posX ] == 0 )
        {
            close[ posZ ][ posX ] = 1;
            region[ posZ ][ posX ] = r;

            SortedSetNode ssn = new SortedSetNode();
            ssn.node = &( nodes[ posZ ][ posX ] );
            closedList.Add( ssn );
        }

        // w
        posX = x;
        posZ = z;
        --posX;
        if ( !W3TerrainManager.instance.isPath( posX , posZ , W3PathType.NOWALK ) &&
        close[ posZ ][ posX ] == 0 )
        {
            close[ posZ ][ posX ] = 1;
            region[ posZ ][ posX ] = r;

            SortedSetNode ssn = new SortedSetNode();
            ssn.node = &( nodes[ posZ ][ posX ] );
            closedList.Add( ssn );
        }
    }

    public int getUnitID( int x , int z )
    {
        return unit[ z ][ x ];
    }

    public int getVaildSize( int x , int z , int size )
    {
        int n = size / 2;
        int n1 = size % 2;

        for ( int i = -n ; i < n + n1 ; ++i )
        {
            for ( int j = -n ; j < n + n1 ; ++j )
            {
                if ( block[ z + i ][ x + j ] == 0 )
                {
                    return 0;
                }
            }
        }

        return 1 << ( size - 1 );
    }

    public bool isUnitSize( int x , int z , int unitID , int size )
    {
        int n = size / 2;
        int n1 = size % 2;

        for ( int i = -n ; i < n + n1 ; ++i )
        {
            for ( int j = -n ; j < n + n1 ; ++j )
            {
                if ( unit[ z + i ][ x + j ] > 0 &&
                    unit[ z + i ][ x + j ] != unitID )
                {
                    return true;
                }
            }
        }

        return false;
    }


    public bool isPathRegion( int x , int z )
    {
        return region[ z ][ x ] == pathRegion;
    }

    public bool isVaild( int x , int z )
    {
        return ( block[ z ][ x ] & 1 ) == 1;
    }

    public bool isBlock( int x , int z )
    {
        return ( block[ z ][ x ] & 1 ) == 0;
    }

    public bool isVaildSize( int x , int z )
    {
        return ( block[ z ][ x ] & unitSizeN ) == unitSizeN;
    }

    public bool isBlockSize( int x , int z )
    {
        return ( block[ z ][ x ] & unitSizeN ) == 0;
    }

    public bool isUnitPathTime( int x , int z , int unitID , int size , uint pathTime )
    {
        int n = size / 2;
        int n1 = size % 2;

        for ( int i = -n ; i < n + n1 ; ++i )
        {
            for ( int j = -n ; j < n + n1 ; ++j )
            {
                if ( unitPathTime[ z + i ][ x + j ] > pathTime )
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void setUnitSizeBuilding( int x , int z , int id , W3UnitDataConfigData unitData )
    {
        int wh = unitData.pathW / 2;
        int hh = unitData.pathH / 2;

        int c = 0;
        for ( int i = z - hh ; i < z + unitData.pathH - hh ; i++ )
        {
            for ( int j = x - wh ; j < x + unitData.pathW - wh ; j++ )
            {
                if ( unitData.pathData[ c ].r > 0 &&
                    unit[ i ][ j ] == 0 )
                {
                    unit[ i ][ j ] = id;
                    unitPathTime[ i ][ j ] = pathTime;

                    // set block
                    block[ i ][ j ] = 0;
                }
                c++;
            }
        }

        for ( int i = z - hh - 4 ; i < z + unitData.pathH - hh + 5 ; i++ )
        {
            for ( int j = x - wh - 4 ; j < x + unitData.pathW - wh + 5 ; j++ )
            {
                byte b = (byte)( getVaildSize( j , i , 1 ) +
                                getVaildSize( j , i , 2 ) +
                                getVaildSize( j , i , 3 ) +
                                getVaildSize( j , i , 4 ) +
                                getVaildSize( j , i , 5 ) +
                                getVaildSize( j , i , 6 ) +
                                getVaildSize( j , i , 7 ) +
                                getVaildSize( j , i , 8 )
                    );

                block[ i ][ j ] = b;
            }
        }

    }

    public void removeUnitSizeBuilding( int x , int z , int id , W3UnitDataConfigData unitData )
    {
        int wh = unitData.pathW / 2;
        int hh = unitData.pathH / 2;

        int c = 0;
        for ( int i = z - hh ; i < z + unitData.pathH - hh ; i++ )
        {
            for ( int j = x - wh ; j < x + unitData.pathW - wh ; j++ )
            {
                if ( unitData.pathData[ c ].r > 0 &&
                    unit[ i ][ j ] == 0 )
                {
                    unit[ i ][ j ] = 0;
                    unitPathTime[ i ][ j ] = 0;

                    // remove block
                    block[ i ][ j ] = 1;
                }
                c++;
            }
        }

        for ( int i = z - hh - 4 ; i < z + unitData.pathH - hh + 5 ; i++ )
        {
            for ( int j = x - wh - 4 ; j < x + unitData.pathW - wh + 5 ; j++ )
            {
                byte b = (byte)( getVaildSize( j , i , 1 ) +
                 getVaildSize( j , i , 2 ) +
                 getVaildSize( j , i , 3 ) +
                 getVaildSize( j , i , 4 ) +
                 getVaildSize( j , i , 5 ) +
                 getVaildSize( j , i , 6 ) +
                 getVaildSize( j , i , 7 ) +
                 getVaildSize( j , i , 8 )
                );

                block[ i ][ j ] = b;
            }
        }

    }


    public void setUnitSize( int x , int z , int id , int size )
    {
        int n = size / 2;
        int n1 = size % 2;

        for ( int i = -n ; i < n + n1 ; ++i )
        {
            for ( int j = -n ; j < n + n1 ; ++j )
            {
                if ( unit[ z + i ][ x + j ] == 0 )
                {
                    unit[ z + i ][ x + j ] = id;
                    unitPathTime[ z + i ][ x + j ] = pathTime;

                    // set block
                    block[ z + i ][ x + j ] = 0;
                }
            }
        }

        for ( int i = -n - 4 ; i < n + n1 + 5 ; i++ )
        {
            for ( int j = -n - 4 ; j < n + n1 + 5 ; j++ )
            {
                byte b = (byte)( getVaildSize( x + j , z + i , 1 ) +
                                getVaildSize( x + j , z + i , 2 ) +
                                getVaildSize( x + j , z + i , 3 ) +
                                getVaildSize( x + j , z + i , 4 ) +
                                getVaildSize( x + j , z + i , 5 ) +
                                getVaildSize( x + j , z + i , 6 ) +
                                getVaildSize( x + j , z + i , 7 ) +
                                getVaildSize( x + j , z + i , 8 )
                    );

                block[ z + i ][ x + j ] = b;
            }
        }
    }

    public void setUnitSize( int x , int z , int n , int n1 , int id , int size )
    {
        for ( int i = -n ; i < n + n1 ; ++i )
        {
            for ( int j = -n ; j < n + n1 ; ++j )
            {
                if ( unit[ z + i ][ x + j ] == 0 )
                {
                    unit[ z + i ][ x + j ] = id;
                    unitPathTime[ z + i ][ x + j ] = pathTime;

                    // set block
                    block[ z + i ][ x + j ] = 0;
                }
            }
        }

        for ( int i = -n - 4 ; i < n + n1 + 5 ; i++ )
        {
            for ( int j = -n - 4 ; j < n + n1 + 5 ; j++ )
            {
                byte b = (byte)( getVaildSize( x + j , z + i , 1 ) +
                                getVaildSize( x + j , z + i , 2 ) +
                                getVaildSize( x + j , z + i , 3 ) +
                                getVaildSize( x + j , z + i , 4 ) +
                                getVaildSize( x + j , z + i , 5 ) +
                                getVaildSize( x + j , z + i , 6 ) +
                                getVaildSize( x + j , z + i , 7 ) +
                                getVaildSize( x + j , z + i , 8 )
                    );

                block[ z + i ][ x + j ] = b;
            }
        }
    }

    public void removeUnitSize( int x , int z , int id , int size )
    {
        int n = size / 2;
        int n1 = size % 2;

        for ( int i = -n ; i < n + n1 ; ++i )
        {
            for ( int j = -n ; j < n + n1 ; ++j )
            {
                if ( unit[ z + i ][ x + j ] == id )
                {
                    unit[ z + i ][ x + j ] = 0;
                    unitPathTime[ z + i ][ x + j ] = 0;

                    // remove block
                    block[ z + i ][ x + j ] = 1;
                }
            }
        }

        for ( int i = -n - 4 ; i < n + n1 + 5 ; i++ )
        {
            for ( int j = -n - 4 ; j < n + n1 + 5 ; j++ )
            {
                byte b = (byte)( getVaildSize( x + j , z + i , 1 ) +
                                getVaildSize( x + j , z + i , 2 ) +
                                getVaildSize( x + j , z + i , 3 ) +
                                getVaildSize( x + j , z + i , 4 ) +
                                getVaildSize( x + j , z + i , 5 ) +
                                getVaildSize( x + j , z + i , 6 ) +
                                getVaildSize( x + j , z + i , 7 ) +
                                getVaildSize( x + j , z + i , 8 )
                                );

                block[ z + i ][ x + j ] = b;
            }
        }
    }


    int manhattan( int apx , int apz , int bpx , int bpz )
    {
        switch ( direction4 )
        {
            case (int)W3TerrainDirectionType.NORTH:
                {
                    if ( apx > bpx )
                        return Math.Abs( apx - bpx ) + Math.Abs( apz - bpz ) + 1;
                    else
                        return Math.Abs( apx - bpx ) + Math.Abs( apz - bpz );
                }
            case (int)W3TerrainDirectionType.SOUTH:
                {
                    if ( apx < bpx )
                        return Math.Abs( apx - bpx ) + Math.Abs( apz - bpz ) + 1;
                    else
                        return Math.Abs( apx - bpx ) + Math.Abs( apz - bpz );
                }
            case (int)W3TerrainDirectionType.EAST:
                {
                    if ( apz < bpz )
                        return Math.Abs( apx - bpx ) + Math.Abs( apz - bpz ) + 1;
                    else
                        return Math.Abs( apx - bpx ) + Math.Abs( apz - bpz );
                }
            case (int)W3TerrainDirectionType.WEST:
                {
                    if ( apz > bpz )
                        return Math.Abs( apx - bpx ) + Math.Abs( apz - bpz ) + 1;
                    else
                        return Math.Abs( apx - bpx ) + Math.Abs( apz - bpz );
                }
            default:
                return Math.Abs( apx - bpx ) + Math.Abs( apz - bpz );
        }
    }

    int euclidean( int apx , int apz , int bpx , int bpz )
    {
        int fx = Math.Abs( apx - bpx );
        int fz = Math.Abs( apz - bpz );

        if ( fx == fz )
        {
            return (int)( fx * 1.4 );
        }
        else
        {
            return fz + fx;
        }
    }

    public void setStartEnd( int sx , int sz , int ex , int ez )
    {
        startPosX = sx;
        startPosZ = sz;

        endPosX = ex;
        endPosZ = ez;
    }

    int generatePath( int step )
    {
        PathNode* next = resultNode;
        PathNode* prev = resultNode->parent;

        int count = 0;

        int lx = next->posX;
        int lz = next->posZ;

        while ( prev != null )
        {
            int x = next->posX;
            int z = next->posZ;

            int dx = prev->posX - x;
            int dz = prev->posZ - z;

            int steps = Math.Max( Math.Abs( dx ) , Math.Abs( dz ) );

            dx /= Math.Max( Math.Abs( dx ) , 1 );
            dz /= Math.Max( Math.Abs( dz ) , 1 );

            dx *= step;
            dz *= step;

            int dxa = 0, dza = 0;
            for ( int i = 0 ; i < steps ; i += step )
            {
                int xx = x + dxa;
                int zz = z + dza;

                // 4 direction move
//                 if ( lx != xx && lz != zz )
//                 {
//                     if ( isBlockSize( xx , lz ) )
//                     {
//                         bufferOut[ count++ ] = lx;
//                         bufferOut[ count++ ] = zz;
//                     }
//                     else if ( isBlockSize( lx , zz ) )
//                     {
//                         bufferOut[ count++ ] = xx;
//                         bufferOut[ count++ ] = lz;
//                     }
//                 }

                bufferOut[ count++ ] = xx;
                bufferOut[ count++ ] = zz;

                lx = x + dxa;
                lz = z + dza;

                dxa += dx;
                dza += dz;
            }

            next = prev;
            prev = prev->parent;
        }

        if ( lx != startPosX && lz != startPosZ )
        {
            // 4 direction move
//             if ( isBlockSize( startPosX , lz ) )
//             {
//                 bufferOut[ count++ ] = lx;
//                 bufferOut[ count++ ] = startPosZ;
//             }
//             else if ( isBlockSize( lx , startPosZ ) )
//             {
//                 bufferOut[ count++ ] = startPosX;
//                 bufferOut[ count++ ] = lz;
//             }
        }

        return count;
    }

    public int linePathBresenham( int x1 , int z1 , int x2 , int z2 )
    {
        int count = 0;

        int lx = x1;
        int lz = z1;

        int dx = x2 - x1;
        int dz = z2 - z1;
        int ux = ( dx > 0 ) ? 1 : -1;
        int uz = ( dz > 0 ) ? 1 : -1;
        int x = x1, z = z1, eps;
        x2 += ux; z2 += uz;
        eps = 0; dx = Math.Abs( dx ); dz = Math.Abs( dz );
        if ( dx > dz )
        {
            for ( x = x1 ; x != x2 ; x += ux )
            {
                if ( isVaild( x , z ) )
                {
                    // 4 direction move
//                     if ( lx != x && lz != z )
//                     {
//                         if ( isBlockSize( x , lz ) )
//                         {
//                             bufferOut[ count++ ] = lx;
//                             bufferOut[ count++ ] = z;
//                         }
//                         else if ( isBlockSize( lx , z ) )
//                         {
//                             bufferOut[ count++ ] = x;
//                             bufferOut[ count++ ] = lz;
//                         }
//                     }

                    bufferOut[ count++ ] = x;
                    bufferOut[ count++ ] = z;

                    lx = x;
                    lz = z;
                }
                else if ( x != x2 && z != z2 )
                    return 0;
                else
                    return count;

                eps += dz;
                if ( ( eps << 1 ) >= dx )
                {
                    z += uz;
                    eps -= dx;
                }
            }
        }
        else
        {
            for ( z = z1 ; z != z2 ; z += uz )
            {
                if ( isVaild( x , z ) )
                {
                    // 4 direction move
//                     if ( lx != x && lz != z )
//                     {
//                         if ( isBlockSize( x , lz ) )
//                         {
//                             bufferOut[ count++ ] = lx;
//                             bufferOut[ count++ ] = z;
//                         }
//                         else if ( isBlockSize( lx , z ) )
//                         {
//                             bufferOut[ count++ ] = x;
//                             bufferOut[ count++ ] = lz;
//                         }
//                     }

                    bufferOut[ count++ ] = x;
                    bufferOut[ count++ ] = z;

                    lx = x;
                    lz = z;
                }
                else if ( x != x2 && z != z2 )
                    return 0;
                else
                    return count;

                eps += dx;
                if ( ( eps << 1 ) >= dz )
                {
                    x += ux;
                    eps -= dz;
                }
            }
        }

        return count;
    }

    public int linePathBresenhamSize( int x1 , int z1 , int x2 , int z2 )
    {
        int count = 0;

        int lx = x1;
        int lz = z1;

        int dx = x2 - x1;
        int dz = z2 - z1;
        int ux = ( dx > 0 ) ? 1 : -1;
        int uz = ( dz > 0 ) ? 1 : -1;
        int x = x1, z = z1, eps;
        x2 += ux; z2 += uz;
        eps = 0; dx = Math.Abs( dx ); dz = Math.Abs( dz );
        if ( dx > dz )
        {
            for ( x = x1 ; x != x2 ; x += ux )
            {
                if ( isVaildSize( x , z ) )
                {
                    // 4 direction move
//                     if ( lx != x && lz != z )
//                     {
//                         if ( isBlockSize( x , lz ) )
//                         {
//                             bufferOut[ count++ ] = lx;
//                             bufferOut[ count++ ] = z;
//                         }
//                         else if ( isBlockSize( lx , z ) )
//                         {
//                             bufferOut[ count++ ] = x;
//                             bufferOut[ count++ ] = lz;
//                         }
//                     }

                    bufferOut[ count++ ] = x;
                    bufferOut[ count++ ] = z;

                    lx = x;
                    lz = z;
                }
                else if ( x != x2 && z != z2 )
                    return 0;
                else
                    return count;

                eps += dz;
                if ( ( eps << 1 ) >= dx )
                {
                    z += uz;
                    eps -= dx;
                }
            }
        }
        else
        {
            for ( z = z1 ; z != z2 ; z += uz )
            {
                if ( isVaildSize( x , z ) )
                {
                    // 4 direction move
//                     if ( lx != x && lz != z )
//                     {
//                         if ( isBlockSize( x , lz ) )
//                         {
//                             bufferOut[ count++ ] = lx;
//                             bufferOut[ count++ ] = z;
//                         }
//                         else if ( isBlockSize( lx , z ) )
//                         {
//                             bufferOut[ count++ ] = x;
//                             bufferOut[ count++ ] = lz;
//                         }
//                     }

                    bufferOut[ count++ ] = x;
                    bufferOut[ count++ ] = z;

                    lx = x;
                    lz = z;
                }
                else if ( x != x2 && z != z2 )
                    return 0;
                else
                    return count;

                eps += dx;
                if ( ( eps << 1 ) >= dz )
                {
                    x += ux;
                    eps -= dz;
                }
            }
        }

        return count;
    }

    public int findPath( int sx , int sz , int ex , int ez , int uid , byte usize )
    {
        pathRegion = W3TerrainManager.instance.smallNodes[ sz ][ sx ].pathRegion;
        unitID = uid;
        unitSize = usize;

        unitSizeN = (byte) ( 1 << ( unitSize - 1 ) );

        unitSizeN1 = (byte)( unitSize / 2 );
        unitSizeN2 = (byte)( unitSize % 2 + unitSizeN1 );

        setStartEnd( sx , sz , ex , ez );
        removeUnitSize( sx , sz , uid , usize );

        direction4 = W3TerrainDirection.getDirection4( sx , sz , ex , ez );
        
        if ( isBlockSize( ex , ez ) ||
            region[ sz ][ sx ] != region[ ez ][ ex ] )
        {
            // find near pos
            if ( findNearPos() )
            {
                endPosX = nearPosX;
                endPosZ = nearPosZ;
            }
            else
            {
                setUnitSize( sx , sz , uid , usize );
                return 0;
            }
        }

        if ( startPosX == endPosX &&
            startPosZ == endPosZ )
        {
            setUnitSize( sx , sz , uid , usize );
            return 0;
        }

        // quick find first
        int count = unitSize > 1 ? 
             linePathBresenhamSize( sx , sz , endPosX , endPosZ ) :
             linePathBresenham( sx , sz , endPosX , endPosZ );

        if ( count > 0 )
        {
            pathLine = true;
            setUnitSize( sx , sz , uid , usize );
            pathTime++;
            return count;
        }
        else
        {
            pathLine = false;

            // jps
            if ( unitSize > 1 )
                findPathJPSSize();
            else
                findPathJPS();
        }

        if ( resultNode == null )
        {
            setUnitSize( sx , sz , uid , usize );
            return 0;
        }

        pathTime++;

        PathNode* node = resultNode;

#if UNITY_EDITOR
        List<Vector2Int> p = new List<Vector2Int>();
#endif
        while ( true )
        {
#if UNITY_EDITOR
            p.Add( new Vector2Int( node->posX , node->posZ ) );
#endif
            if ( node->parent == null )
            {
                break;
            }

            bufferOut[ count++ ] = node->posX;
            bufferOut[ count++ ] = node->posZ;
            node = node->parent;

            if ( MAXCOUNT == count )
            {
                count -= 1;
                break;
            }
        }

        setUnitSize( sx , sz , uid , usize );
        return count;
    }

    public int findPathStep( int sx , int sz , int ex , int ez , int uid , byte usize , bool near )
    {
        pathRegion = W3TerrainManager.instance.smallNodes[ sz ][ sx ].pathRegion;
        unitID = uid;
        unitSize = usize;

        unitSizeN = (byte)( 1 << ( unitSize - 1 ) );
        setStartEnd( sx , sz , ex , ez );
        removeUnitSize( sx , sz , uid , usize );

        direction4 = W3TerrainDirection.getDirection4( sx , sz , ex , ez );

        if ( isBlockSize( ex , ez ) ||
            region[ sz ][ sx ] != region[ ez ][ ex ] )
        {
            // find near pos
            if ( near && findNearPos() )
            {
                endPosX = nearPosX;
                endPosZ = nearPosZ;
            }
            else
            {
                setUnitSize( sx , sz , uid , usize );
                return 0;
            }
        }

        if ( startPosX == endPosX &&
            startPosZ == endPosZ )
        {
            setUnitSize( sx , sz , uid , usize );
            return 0;
        }

        // quick find first
        int count = unitSize > 1 ?
            linePathBresenhamSize( sx , sz , endPosX , endPosZ ) :
            linePathBresenham( sx , sz , endPosX , endPosZ );

        if ( count > 0 )
        {
            pathLine = true;
            setUnitSize( sx , sz , uid , usize );
            pathTime++;
            return count;
        }
        else
        {
            pathLine = false;

            // jps
            if ( unitSize > 1 )
                findPathJPSSize();
            else
                findPathJPS();
        }

        if ( resultNode == null )
        {
            setUnitSize( sx , sz , uid , usize );
            return 0;
        }

        pathTime++;

        count = generatePath( 1 );

        setUnitSize( sx , sz , uid , usize );

        return count;
    }

    void findPathJPSSize()
    {
        resultNode = null;

        clear();

        PathNode* pn = &( nodes[ startPosZ ][ startPosX ] );
        pn->g = 0;
        pn->f = 0;
        pn->parent = null;
        pn->clear();

        close[ startPosZ ][ startPosX ] = 1;

        SortedSetNode ssn = new SortedSetNode();
        ssn.node = pn;
        sortedSetNodes.Add( ssn );
        closedList.Add( ssn );

        int count = 0;

        while ( true )
        {
            count++;

            SortedSetNode node = sortedSetNodes.Min;
            pn = node.node;
            sortedSetNodes.Remove( node );
            int n = findJpsSize( pn );

            if ( n > 0 )
            {
                return;
            }

            if ( sortedSetNodes.Count == 0 )
            {
                resultNode = null;
                return;
            }

            if ( count == 4096 )
            {
                resultNode = null;
                return;
            }

        }
    }

    void findPathJPS()
    {
        resultNode = null;

        clear();

        PathNode* pn = &( nodes[ startPosZ ][ startPosX ] );
        pn->g = 0;
        pn->f = 0;
        pn->parent = null;
        pn->clear();

        close[ startPosZ ][ startPosX ] = 1;

        SortedSetNode ssn = new SortedSetNode();
        ssn.node = pn;
        sortedSetNodes.Add( ssn );
        closedList.Add( ssn );

        int count = 0;

        while ( true )
        {
            count++;

            SortedSetNode node = sortedSetNodes.Min;
            pn = node.node;
            sortedSetNodes.Remove( node );
            int n = findJps( pn );

            if ( n > 0 )
            {
                return;
            }

            if ( sortedSetNodes.Count == 0 )
            {
                resultNode = null;
                return;
            }

            if ( count == 4096 )
            {
                resultNode = null;
                return;
            }

        }
    }

    public bool findNearPos()
    {
        int dis = 1;
        int range = 128;
        int d = 9999;

        int x1 = endPosX - unitSizeN1;
        int z1 = endPosZ - unitSizeN1;
        int x2 = endPosX + unitSizeN2;
        int z2 = endPosZ + unitSizeN2;

        nearPosX = GameDefine.INVALID_ID;
        nearPosZ = GameDefine.INVALID_ID;

        while ( range > dis )
        {
            int x = x1 - dis;
            int z = z1 - dis;
            int fx = x2 + dis;
            int fz = z2 + dis;

            for ( int i = 0 ; i < fx - x ; i++ )
            {
                if ( isVaildSize( x + i , z ) &&
                    isPathRegion( x + i , z ) )
                {
                    int dd = Mathf.Abs( x + i - endPosX ) + Mathf.Abs( z - endPosZ );
                    if ( dd < d )
                    {
                        d = dd;
                        nearPosX = x + i;
                        nearPosZ = z;
                    }

                    break;
                }
            }


            for ( int i = 0 ; i < fx - x ; i++ )
            {
                if ( isVaildSize( x + i , fz - 1 ) &&
                    isPathRegion( x + i , fz - 1 ) )
                {
                    int dd = Mathf.Abs( x + i - endPosX ) + Mathf.Abs( fz - 1 - endPosZ );
                    if ( dd < d )
                    {
                        d = dd;
                        nearPosX = x + i;
                        nearPosZ = fz - 1;
                    }

                    break;
                }
            }


            for ( int i = 1 ; i < fz - z - 1 ; i++ )
            {
                if ( isVaildSize( x , z + i ) &&
                    isPathRegion( x , z + i ) )
                {
                    int dd = Mathf.Abs( x - endPosX ) + Mathf.Abs( z + i - endPosZ );
                    if ( dd < d )
                    {
                        d = dd;
                        nearPosX = x;
                        nearPosZ = z + i;
                    }

                    break;
                }
            }


            for ( int i = 1 ; i < fz - z - 1 ; i++ )
            {
                if ( isVaildSize( fx - 1 , z + i ) &&
                    isPathRegion( fx - 1 , z + i ) )
                {
                    int dd = Mathf.Abs( fx - 1 - endPosX ) + Mathf.Abs( z + i - endPosZ );
                    if ( dd < d )
                    {
                        d = dd;
                        nearPosX = fx - 1;
                        nearPosZ = z + i;
                    }

                    break;
                }
            }

            if ( nearPosX > 0 )
                return true;

            dis++;
        }
        
        return false;
    }


    public bool findNearPosBuild( int px , int pz , int bx , int bz , int n1x , int n1z , int n2x , int n2z , byte sn , out int npx , out int npz )
    {
        int dis = 0;
        int range = 128;
        int d = 9999;

        unitSizeN = (byte)( 1 << ( sn - 1 ) );
        unitSizeN1 = (byte)( sn / 2 );
        unitSizeN2 = (byte)( sn % 2 + unitSizeN1 );

        int x1 = bx - n1x - unitSizeN2 + 1;
        int z1 = bz - n1z - unitSizeN2 + 1;
        int x2 = bx + n2x + unitSizeN1 + 1;
        int z2 = bz + n2z + unitSizeN1 + 1;

        npx = GameDefine.INVALID_ID;
        npz = GameDefine.INVALID_ID;

        while ( range > dis )
        {
            int x = x1 - dis;
            int z = z1 - dis;
            int fx = x2 + dis;
            int fz = z2 + dis;

            for ( int i = 0 ; i < fx - x ; i++ )
            {
                if ( isVaildSize( x + i , z ) &&
                    isPathRegion( x + i , z ) )
                {
                    int dd = Mathf.Abs( x + i - px ) + Mathf.Abs( z - pz );
                    if ( dd < d )
                    {
                        SortedSetNode ssn = new SortedSetNode();
                        ssn.node = &( nodes[ z ][ x + i ] );
                        if ( cacheNode.Contains( ssn ) )
                        {
                            break;
                        }

                        d = dd;
                        npx = x + i;
                        npz = z;
                        //return true;
                    }

                    //break;
                }
            }

            for ( int i = 1 ; i < fz - z - 1 ; i++ )
            {
                if ( isVaildSize( fx - 1 , z + i ) &&
                    isPathRegion( fx - 1 , z + i ) )
                {
                    int dd = Mathf.Abs( fx - 1 - px ) + Mathf.Abs( z + i - pz );
                    if ( dd < d )
                    {
                        SortedSetNode ssn = new SortedSetNode();
                        ssn.node = &( nodes[ z + i ][ fx - 1 ] );
                        if ( cacheNode.Contains( ssn ) )
                        {
                            break;
                        }

                        d = dd;
                        npx = fx - 1;
                        npz = z + i;
                        //return true;
                    }

                    //break;
                }
            }

            for ( int i = fx - x - 1 ; i >= 0 ; i-- )
            {
                if ( isVaildSize( x + i , fz - 1 ) &&
                    isPathRegion( x + i , fz - 1 ) )
                {
                    int dd = Mathf.Abs( x + i - px ) + Mathf.Abs( fz - 1 - pz );
                    if ( dd < d )
                    {
                        SortedSetNode ssn = new SortedSetNode();
                        ssn.node = &( nodes[ fz - 1 ][ x + i ] );
                        if ( cacheNode.Contains( ssn ) )
                        {
                            break;
                        }

                        d = dd;
                        npx = x + i;
                        npz = fz - 1;
                        //return true;
                    }

                    //break;
                }
            }


            for ( int i = fz - z - 1 - 1 ; i >= 1 ; i-- )
            {
                if ( isVaildSize( x , z + i ) &&
                    isPathRegion( x , z + i ) )
                {
                    int dd = Mathf.Abs( x - px ) + Mathf.Abs( z + i - pz );
                    if ( dd < d )
                    {
                        SortedSetNode ssn = new SortedSetNode();
                        ssn.node = &( nodes[ z + i ][ x ] );
                        if ( cacheNode.Contains( ssn ) )
                        {
                            break;
                        }

                        d = dd;
                        npx = x;
                        npz = z + i;
                        //return true;
                    }

                    //break;
                }
            }

            if ( npx > 0 )
                return true;

            dis += 1;
        }

        return false;
    }

    public bool findNearPosGiveWay( int px , int pz , int bx , int bz , int n1x , int n1z , int n2x , int n2z , byte sn , out int npx , out int npz )
    {
        int dis = 0;
        int range = 128;
        int d = 9999;

        unitSizeN = (byte)( 1 << ( sn - 1 ) );
        unitSizeN1 = (byte)( sn / 2 );
        unitSizeN2 = (byte)( sn % 2 + unitSizeN1 );

        int x1 = bx - n1x - unitSizeN2 + 1;
        int z1 = bz - n1z - unitSizeN2 + 1;
        int x2 = bx + n2x + unitSizeN1 + 1;
        int z2 = bz + n2z + unitSizeN1 + 1;

        npx = GameDefine.INVALID_ID;
        npz = GameDefine.INVALID_ID;

        while ( range > dis )
        {
            int x = x1 - dis;
            int z = z1 - dis;
            int fx = x2 + dis;
            int fz = z2 + dis;

            for ( int i = 0 ; i < fx - x ; i++ )
            {
                if ( isVaildSize( x + i , z ) &&
                    isPathRegion( x + i , z ) )
                {
                    int dd = Mathf.Abs( x + i - px ) + Mathf.Abs( z - pz );
                    if ( dd < d )
                    {
                        SortedSetNode ssn = new SortedSetNode();
                        ssn.node = &( nodes[ z ][ x + i ] );
                        if ( cacheNode.Contains( ssn ) )
                        {
                            break;
                        }

                        d = dd;
                        npx = x + i;
                        npz = z;
                        //return true;
                    }

                    //break;
                }
            }

            for ( int i = 1 ; i < fz - z - 1 ; i++ )
            {
                if ( isVaildSize( fx - 1 , z + i ) &&
                    isPathRegion( fx - 1 , z + i ) )
                {
                    int dd = Mathf.Abs( fx - 1 - px ) + Mathf.Abs( z + i - pz );
                    if ( dd < d )
                    {
                        SortedSetNode ssn = new SortedSetNode();
                        ssn.node = &( nodes[ z + i ][ fx - 1 ] );
                        if ( cacheNode.Contains( ssn ) )
                        {
                            break;
                        }

                        d = dd;
                        npx = fx - 1;
                        npz = z + i;
                        //return true;
                    }

                    //break;
                }
            }

            for ( int i = fx - x - 1 ; i >= 0 ; i-- )
            {
                if ( isVaildSize( x + i , fz - 1 ) &&
                    isPathRegion( x + i , fz - 1 ) )
                {
                    int dd = Mathf.Abs( x + i - px ) + Mathf.Abs( fz - 1 - pz );
                    if ( dd < d )
                    {
                        SortedSetNode ssn = new SortedSetNode();
                        ssn.node = &( nodes[ fz - 1 ][ x + i ] );
                        if ( cacheNode.Contains( ssn ) )
                        {
                            break;
                        }

                        d = dd;
                        npx = x + i;
                        npz = fz - 1;
                        //return true;
                    }

                    //break;
                }
            }

            for ( int i = fz - z - 1 - 1 ; i >= 1 ; i-- )
            {
                if ( isVaildSize( x , z + i ) &&
                    isPathRegion( x , z + i ) )
                {
                    int dd = Mathf.Abs( x - px ) + Mathf.Abs( z + i - pz );
                    if ( dd < d )
                    {
                        SortedSetNode ssn = new SortedSetNode();
                        ssn.node = &( nodes[ z + i ][ x ] );
                        if ( cacheNode.Contains( ssn ) )
                        {
                            break;
                        }

                        d = dd;
                        npx = x;
                        npz = z + i;
                        //return true;
                    }

                    //break;
                }
            }

            
            if ( npx > 0 )
                return true;

            dis += 1;
        }

        return false;
    }

    public bool findNearPosTrans( int px , int pz , int n1x , int n1z , int n2x , int n2z , byte sn , out int npx , out int npz )
    {
        int dis = 0;
        int range = 128;
        int d = 9999;

        unitSizeN = (byte)( 1 << ( sn - 1 ) );
        unitSizeN1 = (byte)( sn / 2 );
        unitSizeN2 = (byte)( sn % 2 + unitSizeN1 );

        int x1 = px - n1x - unitSizeN2 + 1;
        int z1 = pz - n1z - unitSizeN2 + 1;
        int x2 = px + n2x + unitSizeN1 + 1;
        int z2 = pz + n2z + unitSizeN1 + 1;

        npx = GameDefine.INVALID_ID;
        npz = GameDefine.INVALID_ID;

        while ( range > dis )
        {
            int x = x1 - dis;
            int z = z1 - dis;
            int fx = x2 + dis;
            int fz = z2 + dis;

            for ( int i = 0 ; i < fx - x ; i++ )
            {
                if ( isVaildSize( x + i , z ) &&
                    isPathRegion( x + i , z ) )
                {
                    int dd = Mathf.Abs( x + i - px ) + Mathf.Abs( z - pz );
                    if ( dd < d )
                    {
                        d = dd;
                        npx = x + i;
                        npz = z;
                        return true;
                    }

                    break;
                }
            }

            for ( int i = 1 ; i < fz - z - 1 ; i++ )
            {
                if ( isVaildSize( fx - 1 , z + i ) &&
                    isPathRegion( fx - 1 , z + i ) )
                {
                    int dd = Mathf.Abs( fx - 1 - px ) + Mathf.Abs( z + i - pz );
                    if ( dd < d )
                    {
                        d = dd;
                        npx = fx - 1;
                        npz = z + i;
                        return true;
                    }

                    break;
                }
            }

            for ( int i = fx - x - 1 ; i >=0  ; i-- )
            {
                if ( isVaildSize( x + i , fz - 1 ) &&
                    isPathRegion( x + i , fz - 1 ) )
                {
                    int dd = Mathf.Abs( x + i - px ) + Mathf.Abs( fz - 1 - pz );
                    if ( dd < d )
                    {
                        d = dd;
                        npx = x + i;
                        npz = fz - 1;
                        return true;
                    }

                    break;
                }
            }

            for ( int i = fz - z - 1 - 1 ; i >= 1 ; i-- )
            {
                if ( isVaildSize( x , z + i ) &&
                    isPathRegion( x , z + i ) )
                {
                    int dd = Mathf.Abs( x - px ) + Mathf.Abs( z + i - pz );
                    if ( dd < d )
                    {
                        d = dd;
                        npx = x;
                        npz = z + i;
                        return true;
                    }

                    break;
                }
            }


            if ( npx > 0 )
                return true;

            dis += sn;
        }

        return false;
    }

    int findJpsSize( PathNode* n )
    {
        findNeighborsSize( n );

#if UNITY_EDITOR
//         List<Vector2Int> v = new List<Vector2Int>();
//         for ( int i = neighborsCount - 1 ; i >= 0 ; --i )
//         {
//             v.Add( new Vector2Int( neighbors[ i ].posX , neighbors[ i ].posZ ) );
//         }
#endif

        int rx = GameDefine.INVALID_ID;
        int rz = GameDefine.INVALID_ID;

        for ( int i = neighborsCount - 1 ; i >= 0 ; --i )
        {
            int nx = neighbors[ i ].posX;
            int nz = neighbors[ i ].posZ;

            jumpPSize( nx , nz , n->posX , n->posZ , ref rx , ref rz );

            if ( rx == GameDefine.INVALID_ID )
                continue;

            PathNode* jn = &( nodes[ rz ][ rx ] );

            int extraG = euclidean( jn->posX , jn->posZ , n->posX , n->posZ );
            int newG = n->g + extraG;

            if ( close[ rz ][ rx ] == 0 
                || newG < jn->g )
            {
                jn->parent = n;
                jn->g = newG;
                jn->f = jn->g + manhattan( jn->posX , jn->posZ , endPosX , endPosZ );

                if ( close[ rz ][ rx ] == 0 )
                {
                    close[ rz ][ rx ] = 1;

                    SortedSetNode ssn = new SortedSetNode();
                    ssn.node = jn;
                    sortedSetNodes.Add( ssn );
                    closedList.Add( ssn );
                }
                                
                if ( jn->posX == endPosX && 
                    jn->posZ == endPosZ )
                {
                    resultNode = jn;
                    return 1;
                }
            }


        }

        return 0;
    }

    int findJps( PathNode* n )
    {
        findNeighbors( n );

#if UNITY_EDITOR
//         List< Vector2Int > v = new List<Vector2Int>();
//         for ( int i = neighborsCount - 1 ; i >= 0 ; --i )
//         {
//             v.Add( new Vector2Int( neighbors[ i ].posX , neighbors[ i ].posZ ) );
//         }
#endif
        int rx = GameDefine.INVALID_ID;
        int rz = GameDefine.INVALID_ID;

        for ( int i = neighborsCount - 1 ; i >= 0 ; --i )
        {
            int nx = neighbors[ i ].posX;
            int nz = neighbors[ i ].posZ;

            jumpP( nx , nz , n->posX , n->posZ , ref rx , ref rz );

            if ( rx == GameDefine.INVALID_ID )
                continue;

            PathNode* jn = &( nodes[ rz ][ rx ] );

            int extraG = euclidean( jn->posX , jn->posZ , n->posX , n->posZ );
            int newG = n->g + extraG;

            if ( close[ rz ][ rx ] == 0 )
            {
                close[ rz ][ rx ] = 1;

                jn->parent = n;
                jn->g = newG;
                jn->f = jn->g + manhattan( jn->posX , jn->posZ , endPosX , endPosZ );

                SortedSetNode ssn = new SortedSetNode();
                ssn.node = jn;
                sortedSetNodes.Add( ssn );
                closedList.Add( ssn );

                if ( jn->posX == endPosX && jn->posZ == endPosZ )
                {
                    resultNode = jn;
                    return 1;
                }
            }


        }

        return 0;
    }


    void findNeighborsSize( PathNode* n )
    {
        neighborsCount = 0;

        int x = n->posX;
        int z = n->posZ;

        if ( n->parent == null )
        {
            if ( isVaildSize( x - 1 , z ) )
            {
                neighbors[ neighborsCount ].posX = x - 1;
                neighbors[ neighborsCount ].posZ = z;
                neighborsCount++;
            }

            if ( isVaildSize( x , z - 1 ) )
            {
                neighbors[ neighborsCount ].posX = x;
                neighbors[ neighborsCount ].posZ = z - 1;
                neighborsCount++;
            }

            if ( isVaildSize( x , z + 1 ) )
            {
                neighbors[ neighborsCount ].posX = x;
                neighbors[ neighborsCount ].posZ = z + 1;
                neighborsCount++;
            }

            if ( isVaildSize( x + 1 , z ) )
            {
                neighbors[ neighborsCount ].posX = x + 1;
                neighbors[ neighborsCount ].posZ = z;
                neighborsCount++;
            }



            if ( isVaildSize( x - 1 , z - 1 ) && ( isVaild( x , z - 1 ) || isVaild( x - 1 , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x - 1;
                neighbors[ neighborsCount ].posZ = z - 1;
                neighborsCount++;
            }

            if ( isVaildSize( x - 1 , z + 1 ) && ( isVaild( x , z + 1 ) || isVaild( x - 1 , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x - 1;
                neighbors[ neighborsCount ].posZ = z + 1;
                neighborsCount++;
            }

            if ( isVaildSize( x + 1 , z - 1 ) && ( isVaild( x , z - 1 ) || isVaild( x + 1 , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x + 1;
                neighbors[ neighborsCount ].posZ = z - 1;
                neighborsCount++;
            }

            if ( isVaildSize( x + 1 , z + 1 ) && ( isVaild( x , z + 1 ) || isVaild( x + 1 , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x + 1;
                neighbors[ neighborsCount ].posZ = z + 1;
                neighborsCount++;
            }

            return;
        }

        // jump directions (both -1, 0, or 1)
        int dx = x - n->parent->posX;
        dx /= Math.Max( Math.Abs( dx ) , 1 );
        int dz = z - n->parent->posZ;
        dz /= Math.Max( Math.Abs( dz ) , 1 );

        if ( dx != 0 && dz != 0 )
        {
            // diagonal
            // natural neighbors
            bool walkX = false;
            bool walkZ = false;
            if ( ( walkX = isVaildSize( x + dx , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x + dx;
                neighbors[ neighborsCount ].posZ = z;
                neighborsCount++;
            }

            if ( ( walkZ = isVaildSize( x , z + dz ) ) )
            {
                neighbors[ neighborsCount ].posX = x;
                neighbors[ neighborsCount ].posZ = z + dz;
                neighborsCount++;
            }

            if ( walkX || walkZ )
            {
                if ( isVaildSize( x + dx , z + dz ) )
                {
                    neighbors[ neighborsCount ].posX = x + dx;
                    neighbors[ neighborsCount ].posZ = z + dz;
                    neighborsCount++;
                }
            }

            // forced neighbors
            if ( walkZ && isBlockSize( x - dx , z ) )
            {
                if ( isVaildSize( x - dx , z + dz ) )
                {
                    neighbors[ neighborsCount ].posX = x - dx;
                    neighbors[ neighborsCount ].posZ = z + dz;
                    neighborsCount++;
                }
            }

            if ( walkX && isBlockSize( x , z - dz ) )
            {
                if ( isVaildSize( x + dx , z - dz ) )
                {
                    neighbors[ neighborsCount ].posX = x + dx;
                    neighbors[ neighborsCount ].posZ = z - dz;
                    neighborsCount++;
                }
            }

        }
        else if ( dx != 0 )
        {
            // along X axis
            if ( isVaildSize( x + dx , z ) )
            {
                neighbors[ neighborsCount ].posX = x + dx;
                neighbors[ neighborsCount ].posZ = z;
                neighborsCount++;

                // Forced neighbors (+ prevent tunneling)
                if ( isBlockSize( x , z + 1 ) )
                {
                    if ( isVaildSize( x + dx , z + 1 ) )
                    {
                        neighbors[ neighborsCount ].posX = x + dx;
                        neighbors[ neighborsCount ].posZ = z + 1;
                        neighborsCount++;
                    }
                }

                if ( isBlockSize( x , z - 1 ) )
                {
                    if ( isVaildSize( x + dx , z - 1 ) )
                    {
                        neighbors[ neighborsCount ].posX = x + dx;
                        neighbors[ neighborsCount ].posZ = z - 1;
                        neighborsCount++;
                    }
                }
            }
        }
        else if ( dz != 0 )
        {
            // along Z axis
            if ( isVaildSize( x , z + dz ) )
            {
                neighbors[ neighborsCount ].posX = x;
                neighbors[ neighborsCount ].posZ = z + dz;
                neighborsCount++;

                // Forced neighbors (+ prevent tunneling)
                if ( isBlockSize( x + 1 , z ) )
                {
                    if ( isVaildSize( x + 1 , z + dz ) )
                    {
                        neighbors[ neighborsCount ].posX = x + 1;
                        neighbors[ neighborsCount ].posZ = z + dz;
                        neighborsCount++;
                    }
                }

                if ( isBlockSize( x - 1 , z ) )
                {
                    if ( isVaildSize( x - 1 , z + dz ) )
                    {
                        neighbors[ neighborsCount ].posX = x - 1;
                        neighbors[ neighborsCount ].posZ = z + dz;
                        neighborsCount++;
                    }
                }
            }
        }

    }

    void findNeighbors( PathNode* n )
    {
        neighborsCount = 0;

        int x = n->posX;
        int z = n->posZ;

        if ( n->parent == null )
        {
            if ( isVaild( x - 1 , z ) )
            {
                neighbors[ neighborsCount ].posX = x - 1;
                neighbors[ neighborsCount ].posZ = z;
                neighborsCount++;
            }

            if ( isVaild( x , z - 1 ) )
            {
                neighbors[ neighborsCount ].posX = x;
                neighbors[ neighborsCount ].posZ = z - 1;
                neighborsCount++;
            }

            if ( isVaild( x , z + 1 ) )
            {
                neighbors[ neighborsCount ].posX = x;
                neighbors[ neighborsCount ].posZ = z + 1;
                neighborsCount++;
            }

            if ( isVaild( x + 1 , z ) )
            {
                neighbors[ neighborsCount ].posX = x + 1;
                neighbors[ neighborsCount ].posZ = z;
                neighborsCount++;
            }



            if ( isVaild( x - 1 , z - 1 ) && ( isVaild( x , z - 1 ) || isVaild( x - 1 , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x - 1;
                neighbors[ neighborsCount ].posZ = z - 1;
                neighborsCount++;
            }

            if ( isVaild( x - 1 , z + 1 ) && ( isVaild( x , z + 1 ) || isVaild( x - 1 , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x - 1;
                neighbors[ neighborsCount ].posZ = z + 1;
                neighborsCount++;
            }

            if ( isVaild( x + 1 , z - 1 ) && ( isVaild( x , z - 1 ) || isVaild( x + 1 , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x + 1;
                neighbors[ neighborsCount ].posZ = z - 1;
                neighborsCount++;
            }

            if ( isVaild( x + 1 , z + 1 ) && ( isVaild( x , z + 1 ) || isVaild( x + 1 , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x + 1;
                neighbors[ neighborsCount ].posZ = z + 1;
                neighborsCount++;
            }

            return;
        }

        // jump directions (both -1, 0, or 1)
        int dx = x - n->parent->posX;
        dx /= Math.Max( Math.Abs( dx ) , 1 );
        int dz = z - n->parent->posZ;
        dz /= Math.Max( Math.Abs( dz ) , 1 );

        if ( dx != 0 && dz != 0 )
        {
            // diagonal
            // natural neighbors
            bool walkX = false;
            bool walkZ = false;
            if ( ( walkX = isVaild( x + dx , z ) ) )
            {
                neighbors[ neighborsCount ].posX = x + dx;
                neighbors[ neighborsCount ].posZ = z;
                neighborsCount++;
            }

            if ( ( walkZ = isVaild( x , z + dz ) ) )
            {
                neighbors[ neighborsCount ].posX = x;
                neighbors[ neighborsCount ].posZ = z + dz;
                neighborsCount++;
            }

            if ( walkX || walkZ )
            {
                if ( isVaild( x + dx , z + dz ) )
                {
                    neighbors[ neighborsCount ].posX = x + dx;
                    neighbors[ neighborsCount ].posZ = z + dz;
                    neighborsCount++;
                }
            }

            // forced neighbors
            if ( walkZ && isBlock( x - dx , z ) )
            {
                if ( isVaild( x - dx , z + dz ) )
                {
                    neighbors[ neighborsCount ].posX = x - dx;
                    neighbors[ neighborsCount ].posZ = z + dz;
                    neighborsCount++;
                }
            }

            if ( walkX && isBlock( x , z - dz ) )
            {
                if ( isVaild( x + dx , z - dz ) )
                {
                    neighbors[ neighborsCount ].posX = x + dx;
                    neighbors[ neighborsCount ].posZ = z - dz;
                    neighborsCount++;
                }
            }

        }
        else if ( dx != 0 )
        {
            // along X axis
            if ( isVaild( x + dx , z ) )
            {
                neighbors[ neighborsCount ].posX = x + dx;
                neighbors[ neighborsCount ].posZ = z;
                neighborsCount++;

                // Forced neighbors (+ prevent tunneling)
                if ( isBlock( x , z + 1 ) )
                {
                    if ( isVaild( x + dx , z + 1 ) )
                    {
                        neighbors[ neighborsCount ].posX = x + dx;
                        neighbors[ neighborsCount ].posZ = z + 1;
                        neighborsCount++;
                    }
                }

                if ( isBlock( x , z - 1 ) )
                {
                    if ( isVaild( x + dx , z - 1 ) )
                    {
                        neighbors[ neighborsCount ].posX = x + dx;
                        neighbors[ neighborsCount ].posZ = z - 1;
                        neighborsCount++;
                    }
                }
            }
        }
        else if ( dz != 0 )
        {
            // along Z axis
            if ( isVaild( x , z + dz ) )
            {
                neighbors[ neighborsCount ].posX = x;
                neighbors[ neighborsCount ].posZ = z + dz;
                neighborsCount++;

                // Forced neighbors (+ prevent tunneling)
                if ( isBlock( x + 1 , z ) )
                {
                    if ( isVaild( x + 1 , z + dz ) )
                    {
                        neighbors[ neighborsCount ].posX = x + 1;
                        neighbors[ neighborsCount ].posZ = z + dz;
                        neighborsCount++;
                    }
                }

                if ( isBlock( x - 1 , z ) )
                {
                    if ( isVaild( x - 1 , z + dz ) )
                    {
                        neighbors[ neighborsCount ].posX = x - 1;
                        neighbors[ neighborsCount ].posZ = z + dz;
                        neighborsCount++;
                    }
                }
            }
        }

    }


    void jumpPSize( int px , int pz , int psx , int psz , ref int rx , ref int rz )
    {
        int dx = px - psx;
        int dz = pz - psz;

        if ( dx != 0 && dz != 0 )
            jumpDSize( px , pz , dx , dz , ref rx , ref rz );
        else if ( dx != 0 )
            jumpXSize( px , pz , dx , ref rx , ref rz );
        else if ( dz != 0 )
            jumpZSize( px , pz , dz , ref rx , ref rz );
    }

    void jumpP( int px , int pz , int psx , int psz , ref int rx , ref int rz )
    {
        int dx = px - psx;
        int dz = pz - psz;

        if ( dx != 0 && dz != 0 )
            jumpD( px , pz , dx , dz , ref rx , ref rz );
        else if ( dx != 0 )
            jumpX( px , pz , dx , ref rx , ref rz );
        else if ( dz != 0 )
            jumpZ( px , pz , dz , ref rx , ref rz );
    }


    void jumpDSize( int px , int pz , int dx , int dz , ref int rx , ref int rz )
    {
        int steps = 0;

        while ( true )
        {
            if ( px == endPosX && pz == endPosZ )
                break;

            ++steps;

            int x = px;
            int z = pz;

            if ( ( isVaildSize( x - dx , z + dz ) && isBlockSize( x - dx , z ) ) ||
                    ( isVaildSize( x + dx , z - dz ) && isBlockSize( x , z - dz ) ) )
                break;

            bool gdx = isVaildSize( x + dx , z );
            bool gdz = isVaildSize( x , z + dz );

            int rx1 = GameDefine.INVALID_ID;
            int rz1 = GameDefine.INVALID_ID;
            jumpXSize( x + dx , z , dx , ref rx1 , ref rz1 );
            if ( gdx && rx1 != GameDefine.INVALID_ID )
                break;

            jumpZSize( x , z + dz , dz , ref rx1 , ref rz1 );
            if ( gdz && rz1 != GameDefine.INVALID_ID )
                break;

            if ( ( gdx || gdz ) &&
                isVaildSize( x + dx , z + dz ) )
            {
                px += dx;
                pz += dz;
            }
            else
            {
                px = GameDefine.INVALID_ID;
                pz = GameDefine.INVALID_ID;
                break;
            }
        }

        rx = px;
        rz = pz;
    }

    void jumpD( int px , int pz , int dx , int dz , ref int rx , ref int rz )
    {
        int steps = 0;

        while ( true )
        {
            if ( px == endPosX && pz == endPosZ )
                break;

            ++steps;

            int x = px;
            int z = pz;

            if ( ( isVaild( x - dx , z + dz ) && isBlock( x - dx , z ) ) ||
                    ( isVaild( x + dx , z - dz ) && isBlock( x , z - dz ) ) )
                break;

            bool gdx = isVaild( x + dx , z );
            bool gdz = isVaild( x , z + dz );

            int rx1 = GameDefine.INVALID_ID;
            int rz1 = GameDefine.INVALID_ID;
            jumpX( x + dx , z , dx , ref rx1 , ref rz1 );
            if ( gdx && rx1 != GameDefine.INVALID_ID )
                break;

            jumpZ( x , z + dz , dz , ref rx1 , ref rz1 );
            if ( gdz && rz1 != GameDefine.INVALID_ID )
                break;

            if ( ( gdx || gdz ) &&
                isVaild( x + dx , z + dz ) )
            {
                px += dx;
                pz += dz;
            }
            else
            {
                px = GameDefine.INVALID_ID;
                pz = GameDefine.INVALID_ID;
                break;
            }
        }

        rx = px;
        rz = pz;
    }

    void jumpXSize( int px , int pz , int dx , ref int rx , ref int rz )
    {
        int a0 = isBlockSize( px , pz + 1 ) ? 0 : 1;
        int a1 = isBlockSize( px , pz - 1 ) ? 0 : 1;

        int a = ~( ( a0 ) | ( ( a1 ) << 1 ) );

        while ( true )
        {
            int xx = px + dx;

            int b0 = isBlockSize( xx , pz + 1 ) ? 0 : 1;
            int b1 = isBlockSize( xx , pz - 1 ) ? 0 : 1;

            int b = ( ( b0 ) | ( ( b1 ) << 1 ) );

            if ( ( b & a ) > 0
                || ( px == endPosX && pz == endPosZ ) )
                break;

            if ( isBlockSize( xx , pz ) )
            {
                px = GameDefine.INVALID_ID;
                pz = GameDefine.INVALID_ID;
                break;
            }

            a = ~b;
            px += dx;
        }

        rx = px;
        rz = pz;
    }

    void jumpX( int px , int pz , int dx , ref int rx , ref int rz )
    {
        int a = ~( ( block[ pz + 1 ][ px ] & 1 ) | ( ( block[ pz - 1 ][ px ] & 1 ) << 1 ) );

        while ( true )
        {
            int xx = px + dx;

            int b = ( ( block[ pz + 1 ][ xx ] & 1 ) | ( ( block[ pz - 1 ][ xx ] & 1 ) << 1 ) );

            if ( ( b & a ) > 0
                || ( px == endPosX && pz == endPosZ ) )
                break;

            if ( isBlock( xx , pz ) )
            {
                px = GameDefine.INVALID_ID;
                pz = GameDefine.INVALID_ID;
                break;
            }

            a = ~b;
            px += dx;
        }

        rx = px;
        rz = pz;
    }

    void jumpZSize( int px , int pz , int dz , ref int rx , ref int rz )
    {
        int a0 = isBlockSize( px + 1 , pz ) ? 0 : 1;
        int a1 = isBlockSize( px - 1 , pz ) ? 0 : 1;

        int a = ~( ( a0 ) | ( ( a1 ) << 1 ) );

        while ( true )
        {
            int zz = pz + dz;

            int b0 = isBlockSize( px + 1 , zz ) ? 0 : 1;
            int b1 = isBlockSize( px - 1 , zz ) ? 0 : 1;

            int b = ( ( b0 ) | ( ( b1 ) << 1 ) );

            if ( ( a & b ) > 0
                || ( px == endPosX && pz == endPosZ ) )
                break;

            if ( isBlockSize( px , zz ) )
            {
                px = GameDefine.INVALID_ID;
                pz = GameDefine.INVALID_ID;
                break;
            }

            a = ~b;
            pz += dz;
        }

        rx = px;
        rz = pz;
    }

    void jumpZ( int px , int pz , int dz , ref int rx , ref int rz )
    {
        int a = ~( ( block[ pz ][ px + 1 ] & 1 ) | ( ( block[ pz ][ px - 1 ] & 1 ) << 1 ) );

        while ( true )
        {
            int zz = pz + dz;

            int b = ( ( block[ zz ][ px + 1 ] & 1 ) | ( ( block[ zz ][ px - 1 ] & 1 ) << 1 ) );

            if ( ( a & b ) > 0
                || ( px == endPosX && pz == endPosZ ) )
                break;

            if ( isBlock( px , zz ) )
            {
                px = GameDefine.INVALID_ID;
                pz = GameDefine.INVALID_ID;
                break;
            }

            a = ~b;
            pz += dz;
        }

        rx = px;
        rz = pz;
    }

    //     public const byte NOWALK = 2;
    //     public const byte NOFLY = 4;
    //     public const byte NOBUILD = 8;
    //     public const byte BLIGHT = 32;
    //     public const byte NOWATER = 64;
    //     public const byte UNKNOW = 128;
    // 
    // 
    // 
    //     public const short MAXBUFF = 1024;
    // 
    // 
    // 	class PathNode
    // 	{
    // 		public int posX = 0;
    // 		public int posZ = 0;
    // 
    // 		public int f = 0;
    // 		public int g = 0;
    // 		public int n = 0;
    // 
    // 		public PathNode parent = null;
    // 
    // 		public void	clear()
    // 		{
    // 			parent = null;
    // 
    // 			f = 0;
    // 			g = 0;
    // 			n = 0;
    // 		}
    // 
    // 		public PathNode()
    // 		{
    // 
    // 		}
    // 	};
    // 
    // 
    // 	private bool fly = false;
    // 
    //     private int resultDistance = 0;
    //     private byte pathRegion = 0;
    // 
    // 	private PathNode resultNode = null;
    // 	private PathNode topNode = null;
    // 
    // 	private PathNode[] openResult = null;
    // 	private PathNode[] open = null;
    // 
    // 	private byte[] close;
    // 
    // 	private int maxX = 0;
    // 	private int maxZ = 0;
    // 
    // 	private int step = 0;
    // 
    // 	private int openCount;
    // 	private int openResultCount;
    // 
    //     private int startPosX;
    // 	private int startPosZ;
    // 	private int endPosX;
    // 	private int endPosZ;
    // 
    //     private int unitSize;
    // 
    //     private int direction;
    //     private int direction4;
    // 
    //     private int nearPosX;
    //     private int nearPosZ;
    // 
    //     int count111 = 0;
    //     int count222 = 0;
    // 
    //     private PathNode[] neighbours = null;
    //     private int neighbourCount;
    // 
    //     int unitID = 0;
    // 
    // 
    // 	private PathNode[] nodes;
    // 
    // 	public W3PathFinder()
    // 	{
    // 
    // 	}
    // 
    // 	public void release()
    // 	{
    // 		resultNode = null;
    // 		topNode = null;
    // 
    // 		openResult = null;
    // 		open = null;
    // 
    // 		close = null;
    // 
    // 		nodes = null;
    // 	}
    // 
    // 	public void initMap( int x , int z )
    // 	{
    // 		maxX = x;
    // 		maxZ = z;
    // 
    // 		close = new byte[ x * z ];
    // 
    // 		open = new PathNode[ x * z ];
    // 		openResult = new PathNode[ x * z ];
    //         neighbours = new PathNode[ 8 ];
    // 
    // 		nodes = new PathNode[ x * z ];
    // 		for ( int i = 0; i < x * z ; i++ )
    // 		{
    // 			nodes[ i ] = new PathNode();
    // 		}
    // 
    // 		int count = 0;
    // 		for ( int i = 0 ; i < z ; i++ )
    // 		{
    // 			for ( int j = 0 ; j < x ; j++ )
    // 			{
    // 				nodes[ count ].posX = j;
    // 				nodes[ count ].posZ = i;
    // 
    // 				count++;
    // 			}
    // 		}
    // 	}
    //     
    // 
    //     public void setStartEnd( int sx , int sz , int ex , int ez )
    // 	{
    // 		startPosX = sx;
    // 		startPosZ = sz;
    // 
    // 		endPosX = ex;
    // 		endPosZ = ez;
    // 	}
    // 
    // 
    //     void findPath()
    // 	{
    // 		resultNode = null;
    // 
    // 		if ( startPosX == endPosX && startPosZ == endPosZ )
    // 		{
    // 			return;
    // 		}
    // 
    // 		if ( W3MapManager.instance.isBlock( endPosX , endPosZ , unitSize ) ||
    //             W3MapManager.instance.isUnit( endPosX , endPosZ , unitID , unitSize ) || 
    //             !W3MapManager.instance.isPathRegion( endPosX , endPosZ , pathRegion ) )
    // 		{
    //             if ( findNearPos() )
    //             {
    //                 endPosX = nearPosX;
    //                 endPosZ = nearPosZ;
    //             }
    //         }
    // 
    // 		clear();
    // 
    // 		open[ openCount ] = nodes[ getIndex( startPosX , startPosZ ) ];
    // 		open[ openCount ].g = 0;
    // 		open[ openCount ].f = 0;
    // 		open[ openCount ].parent = null;
    // 		open[ openCount ].n = 0;
    // 
    // 		openCount++;
    // 
    // 		close[ getIndex( startPosX , startPosZ ) ] = 1;
    // 		topNode = open[ 0 ];
    // 		topNode.clear();
    // 
    // 		step = 1;
    // 
    //         count111 = 0;
    //         count222 = 0;
    // 
    //         while ( true )
    // 		{
    //             count111++;
    // 			PathNode[] v1 = open;
    // 			PathNode[] v2 = openResult;
    // 			int c = openCount;
    // 
    //             count222 += openCount;
    // 
    //             if ( step > 0 )
    // 			{
    // 				step = 0;
    // 			}
    // 			else
    // 			{
    // 				v1 = openResult;
    // 				v2 = open;
    // 
    // 				step = 1;
    // 
    // 				c = openResultCount;
    // 			}
    // 
    // 			for ( int i = 0 ; i < c ; ++i )
    // 			{
    // 				int n = find( v2 , v1[i] );
    // 
    // 				if ( n > 0 )
    // 				{
    // 					return;
    // 				}
    // 			}
    // 
    // 
    // 			if ( step > 0 )
    // 			{
    // 				openResultCount = 0;
    // 
    // 				if ( openCount == 0 )
    // 				{
    // 					return;
    // 				}
    // 			}
    // 			else
    // 			{
    // 				openCount = 0;
    // 
    // 				if ( openResultCount == 0 )
    // 				{
    // 					return;
    // 				}
    // 			}
    // 
    // 
    // 			if ( fly )
    // 			{
    // 				if ( count111 == 10000 )
    // 				{
    // 					resultNode = null;
    // 					return;
    // 				}
    // 			}
    // 			else
    // 			{
    // 				if ( count111 == 5000 )
    // 				{
    // 					resultNode = null;
    // 					return;
    // 				}
    // 			}
    // 
    // 			//        if ( step )
    // 			//        {
    // 			//            qsort( v2 , mOpenCount, sizeof( PathNode* ) , PathNodeLess );
    // 			//        }
    // 			//        else
    // 			//        {
    // 			//            qsort( v2 , mOpenResultCount, sizeof( PathNode* ) , PathNodeLess );
    // 			//        }
    // 
    // 
    // 			//qsort( v2  , v2->size() , sizeof( PathNode* ) , PathNodeLess );
    // 		}
    // 	}
    // 
    // 
    //     int find( PathNode[] v , PathNode parent )
    // 	{
    //         switch ( direction4 )
    //         {
    //             case (int)W3TerrainDirectionType.NORTH:
    //                 {
    //                     // n
    //                     int posX = parent.posX;
    //                     int posZ = parent.posZ;
    //                     ++posZ;
    //                     int find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // nw
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 3 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // w
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // sw
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 1 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // ne
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 4 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // e
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // se
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 2 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // s
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    //                 }
    //                 break;
    //             case (int)W3TerrainDirectionType.SOUTH:
    //                 {
    //                     // s
    //                     int posX = parent.posX;
    //                     int posZ = parent.posZ;
    //                     --posZ;
    //                     int find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // se
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 2 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // e
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // ne
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 4 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    // 
    //                     // sw
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 1 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // w
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    //                   
    //                     // nw
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 3 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // n
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    //                 }
    //                 break;
    //             case (int)W3TerrainDirectionType.WEST:
    //                 {
    //                     // w
    //                     int posX = parent.posX;
    //                     int posZ = parent.posZ;
    //                     --posX;
    //                     int find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // sw
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 1 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // s
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // se
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 2 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // nw
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 3 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // n
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // ne
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 4 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // e
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    //                 }
    //                 break;
    //             case (int)W3TerrainDirectionType.EAST:
    //                 {
    //                     // e
    //                     int posX = parent.posX;
    //                     int posZ = parent.posZ;
    //                     ++posX;
    //                     int find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // ne
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 4 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // nw
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 3 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // n
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posZ;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    // 
    //                     // se
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     ++posX;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 2 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // s
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // sw
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     --posZ;
    //                     find1 = find( v , parent , posX , posZ , 14 , 1 );
    //                     if ( find1 > 0 )
    //                         return find1;
    // 
    //                     // w
    //                     posX = parent.posX;
    //                     posZ = parent.posZ;
    //                     --posX;
    //                     find1 = find( v , parent , posX , posZ , 10 );
    //                     if ( find1 > 0 )
    //                         return find1;
    //                 }
    //                 break;
    //         }
    //         
    // 
    // 		return 0;
    // 	}
    // 
    // 
    // 	int find( PathNode[] v , PathNode parent , int posX , int posZ , int f , int n = 0 )
    // 	{
    // 		int index = getIndex( posX , posZ );
    // 		if ( index != GameDefine.INVALID_ID )
    // 		{
    // 			if ( close[ index ] != 1 )
    // 			{
    // 				if ( !W3MapManager.instance.isBlock( posX , posZ , unitSize ) &&
    //                     !W3MapManager.instance.isUnit( posX , posZ , unitID , unitSize ) )
    // 				{
    // 					switch ( n )
    // 					{
    // 						case 1:
    // 							{
    // 								int posX1 = posX + 1;
    // 								int posZ1 = posZ + 1;
    // 
    //                                 int index1 = getIndex( posX1 , posZ );
    //                                 int index2 = getIndex( posX , posZ1 );
    // 
    //                                 if ( W3MapManager.instance.isBlock( posX1 , posZ ) &&
    //                                     W3MapManager.instance.isBlock( posX , posZ1 ) )
    // 								{
    //                                     //close[ index ] = 1;
    // 									return 0;
    // 								}
    // 
    //                                 if ( W3MapManager.instance.isUnit( posX1 , posZ , unitID ) &&
    //                                     W3MapManager.instance.isUnit( posX , posZ1 , unitID ) )
    //                                 {
    //                                     //close[ index ] = 1;
    //                                     return 0;
    //                                 }
    //                             }
    // 							break;
    // 						case 2:
    // 							{
    // 								int posX1 = posX - 1;
    // 								int posZ1 = posZ + 1;
    // 
    //                                 if ( W3MapManager.instance.isBlock( posX1 , posZ ) &&
    //                                     W3MapManager.instance.isBlock( posX , posZ1 ) )
    //                                 {
    //                                     //close[ index ] = 1;
    //                                     return 0;
    //                                 }
    // 
    //                                 if ( W3MapManager.instance.isUnit( posX1 , posZ , unitID ) &&
    //                                     W3MapManager.instance.isUnit( posX , posZ1 , unitID ) )
    //                                 {
    //                                     //close[ index ] = 1;
    //                                     return 0;
    //                                 }
    //                             }
    // 							break;
    // 						case 3:
    // 							{
    // 								int posX1 = posX + 1;
    // 								int posZ1 = posZ - 1;
    // 
    //                                 if ( W3MapManager.instance.isBlock( posX1 , posZ ) &&
    //                                     W3MapManager.instance.isBlock( posX , posZ1 ) )
    //                                 {
    //                                     //close[ index ] = 1;
    //                                     return 0;
    //                                 }
    // 
    //                                 if ( W3MapManager.instance.isUnit( posX1 , posZ , unitID ) &&
    //                                     W3MapManager.instance.isUnit( posX , posZ1 , unitID ) )
    //                                 {
    //                                     //close[ index ] = 1;
    //                                     return 0;
    //                                 }
    //                             }
    // 							break;
    // 						case 4:
    // 							{
    // 								int posX1 = posX - 1;
    // 								int posZ1 = posZ - 1;
    // 
    //                                 if ( W3MapManager.instance.isBlock( posX1 , posZ ) &&
    //                                     W3MapManager.instance.isBlock( posX , posZ1 ) )
    //                                 {
    //                                     //close[ index ] = 1;
    //                                     return 0;
    //                                 }
    // 
    //                                 if ( W3MapManager.instance.isUnit( posX1 , posZ , unitID ) &&
    //                                     W3MapManager.instance.isUnit( posX , posZ1 , unitID ) )
    //                                 {
    //                                     //close[ index ] = 1;
    //                                     return 0;
    //                                 }
    //                             }
    // 							break;
    // 					}
    // 
    // 					nodes[ index ].n = parent.n + 1;
    // 					nodes[ index ].f = f;
    // 					nodes[ index ].g = parent.g + f;
    // 					nodes[ index ].parent = parent;
    // 
    // 					if ( step != 0 )
    // 					{
    // 						v[ openCount ] = nodes[ index ];
    // 						openCount++;
    // 					}
    // 					else
    // 					{
    // 						v[ openResultCount ] = nodes[ index ];
    // 						openResultCount++;
    // 					}
    // 					//v->push_back( mNodes + index );
    // 
    // 					if ( posX == endPosX && posZ == endPosZ )
    // 					{
    //                         resultDistance = 0;
    //                         resultNode = nodes[ index ];
    // 						return 1;
    // 					}
    //                     else
    //                     {
    //                         int dis = Math.Abs( posZ - endPosZ ) + Math.Abs( posX - endPosX );
    //                         if ( dis < resultDistance )
    //                         {
    //                             resultDistance = dis;
    //                             resultNode = nodes[ index ];
    //                         }
    //                     }
    // 
    //                 }
    // 
    //                 close[ index ] = 1;
    // 			}
    // 
    // 		}
    // 
    // 		return 0;
    // 	}
    // 
    // 
    //     void findRegion( byte r , int x , int z )
    //     {
    //         int c = getIndex( x , z );
    //         if ( c != GameDefine.INVALID_ID &&
    //             !W3MapManager.instance.isBlock( x , z ) &&
    //         close[ c ] == 0 )
    //         {
    //             close[ c ] = 1;
    //             W3MapManager.instance.setPathRegion( x , z , r );
    //         }
    // 
    // 
    //         // e
    //         int posX = x;
    //         int posZ = z;
    //         ++posX;
    //         c = getIndex( posX , posZ );
    //         if ( c != GameDefine.INVALID_ID && 
    //             !W3MapManager.instance.isBlock( posX , posZ ) &&
    //         close[ c ] == 0 )
    //         {
    //             close[ c ] = 1;
    //             open[ openCount ] = nodes[ c ];
    //             openCount++;
    //             W3MapManager.instance.setPathRegion( posX , posZ , r );
    //         }
    // 
    //         // ne
    //         posX = x;
    //         posZ = z;
    //         ++posX;
    //         ++posZ;
    //         c = getIndex( posX , posZ );
    //         if ( c != GameDefine.INVALID_ID && 
    //             !W3MapManager.instance.isBlock( posX , posZ ) &&
    //         close[ c ] == 0 )
    //         {
    //             close[ c ] = 1;
    //             open[ openCount ] = nodes[ c ];
    //             openCount++;
    //             W3MapManager.instance.setPathRegion( posX , posZ , r );
    //         }
    // 
    //         // nw
    //         posX = x;
    //         posZ = z;
    //         --posX;
    //         ++posZ;
    //         c = getIndex( posX , posZ );
    //         if ( c != GameDefine.INVALID_ID && 
    //             !W3MapManager.instance.isBlock( posX , posZ ) &&
    //         close[ c ] == 0 )
    //         {
    //             close[ c ] = 1;
    //             open[ openCount ] = nodes[ c ];
    //             openCount++;
    //             W3MapManager.instance.setPathRegion( posX , posZ , r );
    //         }
    // 
    //         // n
    //         posX = x;
    //         posZ = z;
    //         ++posZ;
    //         c = getIndex( posX , posZ );
    //         if ( c != GameDefine.INVALID_ID && 
    //             !W3MapManager.instance.isBlock( posX , posZ ) &&
    //         close[ c ] == 0 )
    //         {
    //             close[ c ] = 1;
    //             open[ openCount ] = nodes[ c ];
    //             openCount++;
    //             W3MapManager.instance.setPathRegion( posX , posZ , r );
    //         }
    // 
    //         // se
    //         posX = x;
    //         posZ = z;
    //         ++posX;
    //         --posZ;
    //         c = getIndex( posX , posZ );
    //         if ( c != GameDefine.INVALID_ID && 
    //             !W3MapManager.instance.isBlock( posX , posZ ) &&
    //         close[ c ] == 0 )
    //         {
    //             close[ c ] = 1;
    //             open[ openCount ] = nodes[ c ];
    //             openCount++;
    //             W3MapManager.instance.setPathRegion( posX , posZ , r );
    //         }
    // 
    //         // s
    //         posX = x;
    //         posZ = z;
    //         --posZ;
    //         c = getIndex( posX , posZ );
    //         if ( c != GameDefine.INVALID_ID && 
    //             !W3MapManager.instance.isBlock( posX , posZ ) &&
    //         close[ c ] == 0 )
    //         {
    //             close[ c ] = 1;
    //             open[ openCount ] = nodes[ c ];
    //             openCount++;
    //             W3MapManager.instance.setPathRegion( posX , posZ , r );
    //         }
    // 
    //         // sw
    //         posX = x;
    //         posZ = z;
    //         --posX;
    //         --posZ;
    //         c = getIndex( posX , posZ );
    //         if ( c != GameDefine.INVALID_ID && 
    //             !W3MapManager.instance.isBlock( posX , posZ ) &&
    //         close[ c ] == 0 )
    //         {
    //             close[ c ] = 1;
    //             open[ openCount ] = nodes[ c ];
    //             openCount++;
    //             W3MapManager.instance.setPathRegion( posX , posZ , r );
    //         }
    // 
    //         // w
    //         posX = x;
    //         posZ = z;
    //         --posX;
    //         c = getIndex( posX , posZ );
    //         if ( c != GameDefine.INVALID_ID && 
    //             !W3MapManager.instance.isBlock( posX , posZ ) &&
    //         close[ c ] == 0 )
    //         {
    //             close[ c ] = 1;
    //             open[ openCount ] = nodes[ c ];
    //             openCount++;
    //             W3MapManager.instance.setPathRegion( posX , posZ , r );
    //         }
    //     }
    // 
    //     public void updatePathRegion()
    //     {
    //         int c = 0;
    //         byte r = 0;
    //         for ( int i = 0 ; i < maxZ ; i++ )
    //         {
    //             for ( int j = 0 ; j < maxX ; j++ )
    //             {
    //                 if ( !W3MapManager.instance.isBlock( j , i ) && 
    //                     close[ c ] == 0 )
    //                 {
    //                     r++;
    //                     findRegion( r , j , i );
    // 
    //                     while ( openCount > 0 )
    //                     {
    //                         openResultCount = openCount;
    //                         for ( int k = 0 ; k < openCount ; k++ )
    //                             openResult[ k ] = open[ k ];
    // 
    //                         openCount = 0;
    // 
    //                         for ( int k = 0 ; k < openResultCount ; k++ )
    //                             findRegion( r , openResult[ k ].posX , openResult[ k ].posZ );
    //                     }
    //                 }
    // 
    //                 c++;
    //             }
    //         }
    // 
    //         clear();
    //     }
    // 
    // 
    //     void clear()
    // 	{
    // 		resultNode = null;
    // 
    // 		if ( topNode != null )
    // 		{
    // 			topNode.clear();
    // 			topNode = null;
    // 		}
    // 
    // 		openCount = 0;
    // 		openResultCount = 0;
    // 
    // 		for ( int i = 0 ; i < maxX * maxZ ; i++ )
    // 		{
    // 			close[ i ] = 0;
    // 		}
    // 	}
    // 
    //     public int getX( int index )
    // 	{
    // 		return index % maxX;
    // 	}
    // 	public int getZ( int index )
    // 	{
    // 		return index / maxX;
    // 	}
    // 
    // 	public int getIndex( int x , int z )
    // 	{
    // 		if ( x < 0 || z < 0 || x >= maxX || z >= maxZ )
    // 		{
    // 			return GameDefine.INVALID_ID;
    // 		}
    // 
    // 		return z * maxX + x;
    // 	}
    // 
    //     public int linePathBresenham( ref int[] buffer , int x1 , int z1 , int x2 , int z2 )
    //     {
    //         int count = 0;
    // 
    //         int dx = x2 - x1;
    //         int dz = z2 - z1;
    //         int ux = ( dx > 0 ) ? 1 : -1;
    //         int uz = ( dz > 0 ) ? 1 : -1;
    //         int x = x1, z = z1, eps;
    //         x2 += ux; z2 += uz;
    //         eps = 0; dx = Math.Abs( dx ); dz = Math.Abs( dz );
    //         if ( dx > dz )
    //         {
    //             for ( x = x1 ; x != x2 ; x += ux )
    //             {
    //                 if ( !W3MapManager.instance.isBlock( x , z , unitSize ) &&
    //                     !W3MapManager.instance.isUnit( x , z , unitID , unitSize ) )
    //                 {
    //                     buffer[ count ] = getIndex( x , z );
    //                     count++;
    //                 }
    //                 else if ( x != x2 && z != z2 )
    //                     return 0;
    //                 else
    //                     return count;
    //                 
    //                 eps += dz;
    //                 if ( ( eps << 1 ) >= dx )
    //                 {
    //                     z += uz;
    //                     eps -= dx;
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             for ( z = z1 ; z != z2 ; z += uz )
    //             {
    //                 if ( !W3MapManager.instance.isBlock( x , z , unitSize ) &&
    //                     !W3MapManager.instance.isUnit( x , z , unitID , unitSize ) )
    //                 {
    //                     buffer[ count ] = getIndex( x , z );
    //                     count++;
    //                 }
    //                 else if ( x != x2 && z != z2 )
    //                     return 0;
    //                 else
    //                     return count;
    // 
    //                 eps += dx;
    //                 if ( ( eps << 1 ) >= dz )
    //                 {
    //                     x += ux;
    //                     eps -= dz;
    //                 }
    //             }
    //         }
    // 
    //         return count;
    //     }
    // 
    // 
    //     public bool findNearPos()
    //     {
    //         int dis = 1;
    // 
    //         switch ( direction4 )
    //         {
    //             case (int)W3TerrainDirectionType.NORTH:
    //                 while ( true )
    //                 {
    //                     for ( int i = -dis ; i <= dis ; i++ )
    //                     {
    //                         for ( int j = -dis ; j <= dis ; j++ )
    //                         {
    //                             nearPosX = endPosX + j;
    //                             nearPosZ = endPosZ + i;
    //                             if ( !W3MapManager.instance.isBlock( nearPosX , nearPosZ , unitSize ) &&
    //                                 W3MapManager.instance.isPathRegion( nearPosX , nearPosZ , pathRegion ) )
    //                                 return true;
    //                         }
    //                     }
    // 
    //                     dis++;
    //                 }
    //             case (int)W3TerrainDirectionType.SOUTH:
    //                 while ( true )
    //                 {
    //                     for ( int i = dis ; i >= -dis ; i-- )
    //                     {
    //                         for ( int j = -dis ; j <= dis ; j++ )
    //                         {
    //                             nearPosX = endPosX + j;
    //                             nearPosZ = endPosZ + i;
    //                             if ( !W3MapManager.instance.isBlock( nearPosX , nearPosZ , unitSize ) &&
    //                                 W3MapManager.instance.isPathRegion( nearPosX , nearPosZ , pathRegion ) )
    //                                 return true;
    //                         }
    //                     }
    // 
    //                     dis++;
    //                 }
    //             case (int)W3TerrainDirectionType.WEST:
    //                 while ( true )
    //                 {
    //                     for ( int i = -dis ; i <= dis ; i++ )
    //                     {
    //                         for ( int j = -dis ; j <= dis ; j++ )
    //                         {
    //                             nearPosX = endPosX + i;
    //                             nearPosZ = endPosZ + j;
    //                             if ( !W3MapManager.instance.isBlock( nearPosX , nearPosZ , unitSize ) &&
    //                                 W3MapManager.instance.isPathRegion( nearPosX , nearPosZ , pathRegion ) )
    //                                 return true;
    //                         }
    //                     }
    // 
    //                     dis++;
    //                 }
    //             case (int)W3TerrainDirectionType.EAST:
    //                 while ( true )
    //                 {
    //                     for ( int i = dis ; i >= -dis ; i-- )
    //                     {
    //                         for ( int j = -dis ; j <= dis ; j++ )
    //                         {
    //                             nearPosX = endPosX + i;
    //                             nearPosZ = endPosZ + j;
    //                             if ( !W3MapManager.instance.isBlock( nearPosX , nearPosZ , unitSize ) &&
    //                                 W3MapManager.instance.isPathRegion( nearPosX , nearPosZ , pathRegion ) )
    //                                 return true;
    //                         }
    //                     }
    // 
    //                     dis++;
    //                 }
    //         }
    // 
    //         return false;
    //     }
    // 
    //     public int findPath( ref int[] buffer , int sx , int sz , int ex , int ez , int uid , int usize = 1 )
    // 	{
    //         if ( maxX == 0 || maxZ == 0 )  
    // 		{
    // 			return 0;
    // 		}
    // 
    // 		if ( ex >= maxX || ez >= maxZ || 
    // 			sx >= maxX || sz >= maxZ || 
    // 			sx < 0 || sz < 0 || 
    // 			ex < 0 || ez < 0 ) 
    // 		{
    // 			return 0;
    // 		}
    // 
    //         if ( sx == ex && sz == ez )
    //         {
    //             return 0;
    //         }
    // 
    //         pathRegion = W3MapManager.instance.smallNodes[ sz ][ sx ].pathRegion;
    //         resultDistance = 9999;
    //         unitID = uid;
    //         unitSize = usize;
    // 
    //         direction4 = W3TerrainDirection.GetDirection4( sx , sz , ex , ez );
    // 
    //         setStartEnd( sx , sz , ex , ez );
    // 
    //         int count = linePathBresenham( ref buffer , sx , sz , ex , ez );
    // 
    //         if ( count > 0 )
    //         {
    //             buffer[ 1 ] = buffer[ count - 1 ];
    //             return 2;
    //         }
    // 
    // //         System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    // //         sw.Start();
    // 
    //         findPath();
    //         //findPathJPS();
    // 
    // //         sw.Stop();
    //  //        UnityEngine.Debug.Log( string.Format( "{0}ms " , sw.ElapsedMilliseconds ) + " count:" + count111 + " " + count222 );
    // 
    // 
    //         if ( resultNode == null ) 
    // 		{
    // 			return 0;
    // 		}
    // 
    //         count = 0;
    // 
    //         PathNode node = resultNode;
    // 
    // 		int[] buffer1 = new int[ MAXBUFF ];
    // 
    //         while ( node != null )
    // 		{
    // //			Debug.Log( count + " " + node.posX + " " + node.posZ );
    // 
    // 			buffer1[ count ] = getIndex( node.posX , node.posZ );
    // 			node = node.parent;
    // 			count++;
    // 
    // 			if ( MAXBUFF == count )
    // 			{
    // 				count -= 1;
    // 				break;
    // 			}
    // 		}
    // 
    // 		for ( int i = 0 ; i < count ; i++ )
    // 		{
    // 			buffer[ i ] = buffer1[ count - i - 1 ];
    // 		}
    // 
    // 		return count;
    // 	}
    // 
    // 
    // 
    // 
    // 
    // 	public List< int > PathCache = new List< int >();
    // 
    // 	public bool compressePath( int[] buffer , int l )
    // 	{
    // 		PathCache.Clear();
    // 
    // 		if ( l < 2 )
    // 		{
    // 			return false;
    // 		}
    // 
    // 		int index1 = buffer[ 0 ];
    // 		int index2 = buffer[ 1 ];
    // 
    //         int pos1X = getX( index1 );
    //         int pos1Z = getZ( index1 );
    // 		int pos2X = getX( index2 );
    // 		int pos2Z = getZ( index2 );
    // 
    // 		int lastDirect = W3TerrainDirection.GetDirection( pos1X , pos1Z , pos2X , pos2Z );
    // 
    // 		PathCache.Add( index1 );
    // 
    // 		for ( int i = 2 ; i < l ; i++ ) 
    // 		{
    // 			index1 = index2;
    // 
    //             pos1X = pos2X;
    //             pos1Z = pos2Z;
    // 
    // 			index2 = buffer[ i ];
    // 			pos2X = getX( index2 );
    // 			pos2Z = getZ( index2 );
    // 
    // 			int direct = W3TerrainDirection.GetDirection( pos1X , pos1Z , pos2X , pos2Z );
    // 
    // 			if ( lastDirect == direct ) 
    // 			{
    // 				continue;
    // 			}
    // 			else 
    // 			{
    // 				PathCache.Add( index1 );
    // 			}
    // 
    // 			lastDirect = direct;
    // 		}
    // 
    // 		PathCache.Add( index2 );
    // 
    // 		return true;
    // 	}
    // 
    // 
    // 	public bool uncompressePath( int[] buffer )
    // 	{
    // 		PathCache.Clear();
    // 
    // // 		W3TerrainPos pos1 = new W3TerrainPos();
    // // 		W3TerrainPos pos2 = new W3TerrainPos();
    // // 
    // // 		if ( buffer.Length < 2 )
    // // 		{
    // // 			return false;
    // // 		}
    // // 
    // // 		int index1 = buffer[ 0 ];
    // // 		pos1.posX = getX( index1 );
    // // 		pos1.posZ = getZ( index1 );
    // // 
    // // 		for ( int i = 1 ; i < buffer.Length ; i++ )
    // // 		{
    // // 			int index2 = buffer[ i ];
    // // 
    // // 			pos2.posX = getX( index2 );
    // // 			pos2.posZ = getZ( index2 );
    // // 
    // // 			int direct = W3TerrainDirection.GetDirection( pos2 , pos1 );
    // // 
    // // 			switch ( direct ) 
    // // 			{
    // // 				case (int)W3TerrainDirectionType.EAST:
    // // 					for ( int j = 0 ; j < pos2.posX - pos1.posX ; j++ ) 
    // // 					{
    // // 						int index = getIndex( pos1.posX - j , pos1.posZ );
    // // 						PathCache.Add( index );
    // // 					}
    // // 					break;
    // // 				case (int)W3TerrainDirectionType.SOUTH:
    // // 					for ( int j = 0 ; j < pos2.posZ - pos1.posZ ; j++ ) 
    // // 					{
    // // 						int index = getIndex( pos1.posX , pos1.posZ - j );
    // // 						PathCache.Add( index );
    // // 					}
    // // 					break;
    // // 				case (int)W3TerrainDirectionType.WEST:
    // // 					for ( int j = 0 ; j < pos1.posX - pos2.posX ; j++ ) 
    // // 					{
    // // 						int index = getIndex( pos1.posX + j , pos1.posZ );
    // // 						PathCache.Add( index );
    // // 					}
    // // 					break;
    // // 				case (int)W3TerrainDirectionType.NORTH:
    // // 					for ( int j = 0 ; j < pos1.posZ - pos2.posZ ; j++ ) 
    // // 					{
    // // 						int index = getIndex( pos1.posX , pos1.posZ + j );
    // // 						PathCache.Add( index );
    // // 					}                
    // // 					break;
    // // 				case (int)W3TerrainDirectionType.NORTHEAST:
    // // 					for ( int j = 0 ; j < pos2.posX - pos1.posX ; j++ ) 
    // // 					{
    // // 						int index = getIndex( pos1.posX - j , pos1.posZ + j );
    // // 						PathCache.Add( index );
    // // 					}
    // // 					break;
    // // 				case (int)W3TerrainDirectionType.NORTHWEST:
    // // 					for ( int j = 0 ; j < pos1.posX - pos2.posX ; j++ ) 
    // // 					{
    // // 						int index = getIndex( pos1.posX + j , pos1.posZ + j );
    // // 						PathCache.Add( index );
    // // 					}
    // // 					break;
    // // 				case (int)W3TerrainDirectionType.SOUTHEAST:
    // // 					for ( int j = 0 ; j < pos2.posX - pos1.posX ; j++ ) 
    // // 					{
    // // 						int index = getIndex( pos1.posX - j , pos1.posZ - j );
    // // 						PathCache.Add( index );
    // // 					}
    // // 					break;
    // // 				case (int)W3TerrainDirectionType.SOUTHWEST:
    // // 					for ( int j = 0 ; j < pos1.posX - pos2.posX ; j++ ) 
    // // 					{
    // // 						int index = getIndex( pos1.posX + j , pos1.posZ - j );
    // // 						PathCache.Add( index );
    // // 					}
    // // 					break;
    // // 				default:
    // // 					// error,
    // // 					return false;
    // // 			}
    // // 
    // // 			pos1.posX = pos2.posX;
    // // 			pos1.posZ = pos2.posZ;
    // // 			index1 = index2;
    // // 		}
    // // 
    // // 		PathCache.Add( index1 );
    // 
    // 		return true;
    // 	}
    // 
    // 
    // // 	public bool CheckPath( float sx , float sz , float ex , float ez )
    // // 	{
    // // 		if ( sx == ex && sz == ez )
    // // 		{
    // // 			return true;
    // // 		}
    // // 
    // // 		float minX = sx > ex ? ex : sx;
    // // 		float minZ = sz > ez ? ez : sz;
    // // 
    // // 		Rect rects = new Rect( minX - 0.5f , minZ - 0.5f , Math.Abs( ex - sx ) + 1.0f , Math.Abs( ez - sz ) + 1.0f );
    // // 
    // // 		Debug.Log( rects );
    // // 
    // // 		int sx1 = (int)( ( -sx - 2 ) / 4 );
    // // 		int sz1 = (int)( ( -sz - 2 ) / 4 );
    // // 
    // // 		int ex1 = (int)( ( -ex - 2 ) / 4 );
    // // 		int ez1 = (int)( ( -ez - 2 ) / 4 );
    // // 
    // // 		if ( W3MapManager.instance.isBlock( ex1 , ez1 ) )
    // // 		{
    // // 			return false;
    // // 		}
    // // 
    // // 		if ( ex1 >= sx1 )
    // // 		{
    // // 			ex1++;
    // // 			sx1--;
    // // 		}
    // // 		else
    // // 		{
    // // 			ex1--;
    // // 			sx1++;
    // // 		}
    // // 
    // // 		if ( ez1 >= sz1 )
    // // 		{
    // // 			ez1++;
    // // 			sz1--;
    // // 		}
    // // 		else
    // // 		{
    // // 			ez1--;
    // // 			sz1++;
    // // 		}
    // // 
    // // 		int minsx = ex1 > sx1 ? sx1 : ex1;
    // // 		int minsz = ez1 > sz1 ? sz1 : ez1;
    // // 
    // // //		Vector2 start = new Vector2( sx , sz );
    // // //		Vector2 end = new Vector2( sz , ez );
    // // 
    // // 		Debug.Log( sx1 + " " + ex1 + " " + sz1 + " " + ez1 );
    // // 
    // // 		for ( int i = 0 ; i < Math.Abs( sx1 - ex1 ) + 1 ; i++ )
    // // 		{
    // // 			for ( int j = 0 ; j < Math.Abs( sz1 - ez1 ) + 1 ; j++ )
    // // 			{
    // // 				Rect rect = new Rect( ( minsx + i ) * -4 - 4 , ( minsz + j ) * -4 - 4 , 4 , 4 );
    // // 
    // // 				Debug.Log( rect + " " + ( minsx + i ) + " " + ( minsz + j ) );
    // // 
    // // 				if ( rects.Overlaps( rect ) )
    // // 				{
    // // 					Debug.Log( "in " + rect + " " + ( minsx + i ) + " " + ( minsz + j ) );
    // // 
    // // 					if ( W3MapManager.instance.isBlock( ( minsx + i ) , ( minsz + j ) ) )
    // // 					{
    // // 						return false;
    // // 					}
    // // 				}
    // // 			}
    // // 		}
    // // 
    // // 		return true;
    // // 	}
    // 
    // 	private static bool CheckRectLine( Vector2 start , Vector2 end , Rect rect )
    // 	{
    // 		bool result = false;
    // 		if ( rect.Contains(start) || rect.Contains(end) )
    // 			result = true;
    // 		else
    // 		{
    // 			result |= CheckRectLineH( start , end , rect.yMin , rect.xMin , rect.xMax );
    // 			result |= CheckRectLineH( start , end , rect.yMax , rect.xMin , rect.xMax );
    // 			result |= CheckRectLineV( start , end , rect.xMin , rect.yMin , rect.yMax );
    // 			result |= CheckRectLineV( start , end , rect.xMax , rect.yMin , rect.yMax );
    // 		}
    // 		return result;
    // 	}
    // 
    // 	private static bool CheckRectLineH( Vector2 start , Vector2 end , float y0 , float x1 , float x2 )
    // 	{
    // 		if ((y0 < start.y) && (y0 < end.y))
    // 			return false;
    // 		if ((y0 > start.y) && (y0 > end.y))
    // 			return false;
    // 		if (start.y == end.y)
    // 		{
    // 			if (y0 == start.y)
    // 			{
    // 				if ((start.x < x1) && (end.x < x1))
    // 					return false;
    // 				if ((start.x > x2) && (end.x > x2))
    // 					return false;
    // 				return true;
    // 			}
    // 			else
    // 			{
    // 				return false;
    // 			}
    // 		}
    // 		float x = (end.x - start.x) * (y0 - start.y) / (end.y - start.y) + start.x;
    // 		return ((x >= x1) && (x <= x2));
    // 	}
    // 
    // 	private static bool CheckRectLineV( Vector2 start , Vector2 end , float x0 , float y1 , float y2 )
    // 	{
    // 		if ((x0 < start.x) && (x0 < end.x))
    // 			return false;
    // 		if ((x0 > start.x) && (x0 > end.x))
    // 			return false;
    // 		if (start.x == end.x)
    // 		{
    // 			if (x0 == start.x)
    // 			{
    // 				if ((start.y < y1) && (end.y < y1))
    // 					return false;
    // 				if ((start.y > y2) && (end.y > y2))
    // 					return false;
    // 				return true;
    // 			}
    // 			else
    // 			{
    // 				return false;
    // 			}
    // 		}
    // 		float y = (end.y - start.y) * (x0 - start.x) / (end.x - start.x) + start.y;
    // 		return ((y >= y1) && (y <= y2));
    // 	}


}
