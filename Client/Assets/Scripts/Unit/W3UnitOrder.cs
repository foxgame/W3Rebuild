using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class W3Unit
{
    public W3Unit building = null;
    public int buildingX = 0;
    public int buildingZ = 0;
    public int shadowBuildingX = 0;
    public int shadowBuildingZ = 0;

    public W3Order order = null;

    public delegate void OrderHandler();
    OrderHandler handler;

    Dictionary< W3OrderType , OrderHandler > handlerDic = new Dictionary< W3OrderType , OrderHandler >();

    public void readyBuild( W3Unit u , int x , int z , int sx , int sz )
    {
        clearBuild();

        W3PathFinder.instance.clearCache();

        int wh = u.unitData.pathW / 2;
        int hh = u.unitData.pathH / 2;

        List< int > list = new List< int >();

        moveToBuild( x , z , u.unitData );

        int c = 0;
        for ( int i = z - hh ; i < z + u.unitData.pathH - hh ; i++ )
        {
            for ( int j = x - wh ; j < x + u.unitData.pathW - wh ; j++ )
            {
                int uid = W3PathFinder.instance.getUnitID( j , i );

                if ( uid > 0 &&
                    !list.Contains( uid ) )
                {
                    W3Unit unit = W3BaseManager.instance.getUnit( uid );

                    unit.moveToGiveWay( x , z , u.unitData );

                    list.Add( uid );
                }
            }
        }

        buildingX = x;
        buildingZ = z;

        shadowBuildingX = sx;
        shadowBuildingZ = sz;

        building = u;
    }

    public void clearBuild()
    {
        if ( building != null )
        {
            W3TerrainManager.instance.removeBuildingPath( 
                buildingX , 
                buildingZ , 
                building.unitData );

            building.clearBuildingShadow( shadowBuildingX , shadowBuildingZ );

            Destroy( building.gameObject );
            building = null;
        }
    }

    public void setOrder( W3Order o )
    {
        order = o;

        if ( handlerDic.ContainsKey( o.type ) )
        {
            handler = handlerDic[ o.type ];
        }
        else
        {
            handler = handlerNormal;
        }

        handler();
    }

    public void onBuild()
    {

    }

    void initHandler()
    {
        handlerDic.Add( W3OrderType.build , handlerBuild );
        handler = handlerNormal;
    }

    public void handlerNormal()
    {
        checkAttack();
    }

    public void handlerBuild()
    {

    }


}

