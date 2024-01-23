using UnityEngine;
using System;
using System.Collections.Generic;

public class W3DestructableManager : SingletonMono< W3DestructableManager >
{
    Transform destructableObjTrans = null;

    public override void initSingletonMono()
    {
        destructableObjTrans = GameObject.Find( "Map" ).transform.Find( "DestructableObjects" ).transform;

    }

    public void enumDestructablesInRect( BJRect rect , BJBoolExpr filter , BJCode actionFunc )
    {

    }
    
    public int createDestructable( int objectid , float x , float y , float z , float face , float scale , int variation , bool dead )
    {
        W3DestructableDataConfigData d1 = W3DestructableDataConfig.instance.getData( objectid );

        GameObject obj = null;

        if ( d1 == null )
        {
            return GameDefine.INVALID_ID;
        }

        string name1 = "Prefabs\\" + d1.file + ( d1.numVar > 1 ? variation.ToString() : "" );

        float a = -90.0f - face * Mathf.Rad2Deg;
        float gx = ( W3TerrainManager.instance.offsetX - x ) / 128 * GameDefine.TERRAIN_SIZE;
        float gz = ( W3TerrainManager.instance.offsetY - y ) / 128 * GameDefine.TERRAIN_SIZE;

        W3TerrainSmallNode sn = W3TerrainManager.instance.getSmallNode( (int)-gx , (int)-gz );

        float gy = z > 0 ? ( z / 128 * GameDefine.TERRAIN_SIZE ) : sn.ym;

        obj = (GameObject)Instantiate( (GameObject)Resources.Load( name1 ) , destructableObjTrans );
        obj.transform.position = new Vector3( gx , gy , gz );
        obj.transform.localEulerAngles = new Vector3( 0.0f , a , 0.0f );
        obj.transform.localScale = new Vector3( scale , scale , scale );

        W3ReplaceableTexture[] rts = obj.GetComponentsInChildren<W3ReplaceableTexture>();

        if ( rts != null )
        {
            for ( int i0 = 0 ; i0 < rts.Length ; i0++ )
            {
                Renderer r = rts[ i0 ].GetComponent<Renderer>();
                r.sharedMaterial = W3MaterialConfig.instance.getMaterial( d1.texFile , d1.texFile , "W3/MeshBaseAlphaCutoff" );
            }
        }

        W3Base doo = obj.GetComponent<W3Base>();
        return W3BaseManager.instance.addData( doo );
    }

}

