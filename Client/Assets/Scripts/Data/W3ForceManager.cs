using UnityEngine;
using System;
using System.Collections.Generic;



public class W3Force
{
    public int id;
    public List<int> players;
}

public class W3ForceManager : SingletonMono< W3ForceManager >
{
    public int forceID = 0;
    public List< W3Force > forces = new List< W3Force >();

    public int createForce()
    {
        forceID++;

        W3Force t = new W3Force();
        t.id = forceID;

        return t.id;
    }

    public void destroyForce( int id )
    {
        for ( int i = 0 ; i < forces.Count ; i++ )
        {
            if ( forces[ i ].id == id )
            {
                forces.RemoveAt( i );
                return;
            }
        }
    }

    public void forceAddPlayer( int id , int pid )
    {
        for ( int i = 0 ; i < forces.Count ; i++ )
        {
            if ( forces[ i ].id == id )
            {
                forces[ i ].players.Add( pid );

                return;
            }
        }
    }

    public void forceRemovePlayer( int id , int pid )
    {
        for ( int i = 0 ; i < forces.Count ; i++ )
        {
            if ( forces[ i ].id == id )
            {
                for ( int j = 0 ; j < forces[ i ].players.Count ; j++ )
                {
                    if ( forces[ i ].players[ j ] == pid )
                    {
                        forces[ i ].players.RemoveAt( j );

                        return;
                    }
                }

                return;
            }
        }
    }

    public void forceClear( int id )
    {
        for ( int i = 0 ; i < forces.Count ; i++ )
        {
            if ( forces[ i ].id == id )
            {
                forces[ i ].players.Clear();

                return;
            }
        }
    }

    public void forceEnumPlayers( int id , int countLimit )
    {

    }

    public void forceEnumAllies( int id , int pid , int countLimit )
    {

    }

    public void forceEnumEnemies( int id , int pid , int countLimit )
    {

    }

    public void forForce( int id ,BJCode code )
    {

    }

 

}
