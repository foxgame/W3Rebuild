using UnityEngine;
using System;
using System.Collections.Generic;



public class W3ItemManager : SingletonMono< W3ItemManager >
{
    Transform itemObjTrans = null;

    public override void initSingletonMono()
    {
        itemObjTrans = GameObject.Find( "Map" ).transform.Find( "Items" ).transform;

    }

    public void enumItemsInRect(BJRect r ,BJBoolExpr filter ,BJCode actionFunc )
    {

    }

    public void setItemDropOnDeath( int iid , bool b )
    {
        W3Base item = W3BaseManager.instance.getData( iid );

        item.baseData.dropOnDeath = b;
    }

    public void setItemDroppable( int iid , bool b )
    {
        W3Base item = W3BaseManager.instance.getData( iid );

        item.baseData.droppable = b;
    }

    public void setItemPawnable( int iid , bool b )
    {
        W3Base item = W3BaseManager.instance.getData( iid );

        item.baseData.pawnable = b;
    }

    public void setItemPlayer( int iid , int pid , bool cc )
    {
        W3Base item = W3BaseManager.instance.getData( iid );

        item.baseData.playerID = pid;

        if ( cc )
        {

        }
    }
    
    public void SetItemDropID( int iid , int uid )
    {
        W3Base item = W3BaseManager.instance.getData( iid );

        item.baseData.dropID = uid;
    }


    public int createItem( int itemid , float x , float y )
    {
        W3ItemDataConfigData d1 = W3ItemDataConfig.instance.getData( itemid );

        GameObject obj = null;

        if ( d1 == null )
        {
            return GameDefine.INVALID_ID;
        }

        string name1 = "Prefabs\\" + d1.file;

        float gx = ( W3TerrainManager.instance.offsetX - x ) / 128 * GameDefine.TERRAIN_SIZE;
        float gz = ( W3TerrainManager.instance.offsetY - y ) / 128 * GameDefine.TERRAIN_SIZE;

        W3TerrainSmallNode sn = W3TerrainManager.instance.getSmallNode( (int)-gx , (int)-gz );

        obj = (GameObject)Instantiate( (GameObject)Resources.Load( name1 ) , itemObjTrans );
        obj.transform.position = new Vector3( gx , sn.ym , gz );
        obj.transform.localEulerAngles = new Vector3( 0.0f , 0.0f , 0.0f );

        W3Base doo = obj.GetComponent<W3Base>();
        return W3BaseManager.instance.addData( doo );
    }

}

