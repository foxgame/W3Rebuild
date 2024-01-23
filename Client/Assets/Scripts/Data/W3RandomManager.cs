using UnityEngine;
using System;
using System.Collections.Generic;


public class W3RandomManager : SingletonMono<W3RandomManager>
{

    public int getRandomInt( int lowBound , int highBound )
    {
        return UnityEngine.Random.Range( lowBound , highBound );
    }

    public float getRandomReal( float lowBound , float highBound )
    {
        return UnityEngine.Random.Range( lowBound , highBound );
    }

    public int createUnitPool()
    {
        return 0;
    }

    public void destroyUnitPool( int pool )
    {

    }

    public void unitPoolAddUnitType( int pool , int unitId , double weight )
    {

    }

    public void unitPoolRemoveUnitType( int pool , int unitId )
    {

    }

    public int placeRandomUnit( int pool , int pid , float x , float y , float facing )
    {
        return 0;
    }


    public int createItemPool()
    {
        return 0;
    }

    public void destroyItemPool( int pool )
    {

    }

    public void itemPoolAddItemType( int pool , int itemId , float weight )
    {
    }

    public void itemPoolRemoveItemType( int pool , int itemId )
    {
    }

    public int placeRandomItem( int pool , float x , float y )
    {
        return 0;
    }


    public int chooseRandomCreep( int level )
    {
        return 0;
    }

    public int chooseRandomNPBuilding()
    {
        return 0;
    }

    public int chooseRandomItem( int level )
    {
        return 0;
    }

    public int chooseRandomItemEx( int type , int level )
    {
        return 0;
    }

    public void setRandomSeed( int seed )
    {
    }


}



