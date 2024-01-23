using UnityEngine;
using System;
using System.Collections.Generic;



public class W3Group
{
    public int id;
    public List< int > unitID;
}

public class W3GroupManager : SingletonMono< W3GroupManager >
{
    int groupID = 0;
    public List< W3Group > groups = new List< W3Group >();

    public int createGroup()
    {
        groupID++;

        W3Group t = new W3Group();
        t.id = groupID;

        return t.id;
    }

    public void destroyGroup( int id )
    {
        for ( int i = 0 ; i < groups.Count ; i++ )
        {
            if ( groups[ i ].id == id )
            {
                groups.RemoveAt( i );
                return;
            }
        }
    }

    public void groupAddUnit( int id , int uid )
    {
        for ( int i = 0 ; i < groups.Count ; i++ )
        {
            if ( groups[ i ].id == id )
            {
                groups[ i ].unitID.Add( uid );

                return;
            }
        }
    }

    public void GroupRemoveUnit( int id , int uid )
    {
        for ( int i = 0 ; i < groups.Count ; i++ )
        {
            if ( groups[ i ].id == id )
            {
                for ( int j = 0 ; j < groups[ i ].unitID.Count ; j++ )
                {
                    if ( groups[ i ].unitID[ j ] == uid )
                    {
                        groups[ i ].unitID.RemoveAt( j );

                        return;
                    }
                }

                return;
            }
        }
    }

    public void groupClear( int id )
    {
        for ( int i = 0 ; i < groups.Count ; i++ )
        {
            if ( groups[ i ].id == id )
            {
                groups[ i ].unitID.Clear();

                return;
            }
        }
    }

    public void groupEnumUnitsOfType( int id , string name , int countLimit )
    {

    }

    public void groupEnumUnitsOfPlayer( int id , int pid , int countLimit )
    {

    }

    public void groupEnumUnitsInRect( int id ,BJRect rect , int countLimit )
    {

    }
    
    public void groupEnumUnitsInRange( int id , float x , float y , float radius , int countLimit )
    {

    }

    public void groupEnumUnitsSelected( int id , int pid , int countLimit )
    {

    }

    public bool groupImmediateOrder( int id , W3OrderType order )
    {
        return false;
    }

    public bool groupPointOrder( int id , W3OrderType order , float x , float y )
    {
        return false;
    }

    public bool groupTargetOrder( int id , W3OrderType order , int widgetID )
    {
        return false;
    }

    public void forGroup( int id ,BJCode code )
    {

    }

    public int firstOfGroup( int id )
    {
        return 0;
    }

    

}
