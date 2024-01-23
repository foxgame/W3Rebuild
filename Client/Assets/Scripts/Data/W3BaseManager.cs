using UnityEngine;
using System;
using System.Collections.Generic;



public class W3BaseManager : SingletonMono< W3BaseManager >
{
    int baseID = 100000;
    Dictionary<int , W3Base> data = new Dictionary<int , W3Base>();




    public override void initSingletonMono()
    {
    }


    public void queueAnimation( int id , string whichAnimation )
    {

    }

    public void setAnimation( int id , string whichAnimation )
    {

    }

    public void setAnimationSpeed( int id , float speedFactor )
    {

    }

    public void restoreLife( int id , float l , bool b )
    {

    }

    public void setInvulnerable( int id , bool b )
    {
        W3Base d = getData( id );

        d.baseData.invulnerable = b;
    }

    public void setVisible( int iid , bool b )
    {
        W3Base d = getData( iid );

        d.baseData.visible = b;
    }

    public void setLife( int id , float l )
    {
        W3Base d = getData( id );

        d.baseData.hp = (int)l;
    }

    public void setMaxLife( int id , float ml )
    {
        W3Base d = getData( id );

        d.baseData.hpMax = (int)ml;
    }

    public void setPosition( int id , float x , float y )
    {
        W3Base d = getData( id );

        d.baseData.x = x;
        d.baseData.z = y;
    }

    public void setOccluderHeight( int id , float h )
    {
        W3Base d = getData( id );

        d.baseData.occluderHeight = h;
    }

    public void kill( int id )
    {
    }

    public void remove( int id )
    {
        if ( data.ContainsKey( id ) )
        {
            W3Base d = data[ id ];

            GameObject.Destroy( d.gameObject );
            data.Remove( id );
        }
    }

    public void show( int id , bool b )
    {
        if ( data.ContainsKey( id ) )
        {
            W3Base w3base = data[ id ];
            w3base.gameObject.SetActive( b );
        }
    }

    public void addDoodad( int id , W3Doodad doodad )
    {
        data.Add( id , doodad );
    }
   

    public int addData( W3Base d )
    {
        baseID++;

        data.Add( baseID , d );

        d.baseID = baseID;

        return baseID;
    }

    public W3Base getData( int id )
    {
        if ( data.ContainsKey( id ) )
        {
            return data[ id ];
        }

        return null;
    }

    public W3Unit getUnit( int id )
    {
        if ( data.ContainsKey( id ) )
        {
            return data[ id ] as W3Unit;
        }

        return null;
    }

    public W3Doodad getDoodad( int id )
    {
        if ( data.ContainsKey( id ) )
        {
            return data[ id ] as W3Doodad;
        }

        return null;
    }

}

