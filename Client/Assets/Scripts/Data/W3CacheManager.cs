using UnityEngine;
using System;
using System.Collections.Generic;



public class W3Cache
{
    public int id;
}

public class W3CacheManager : SingletonMono<W3CacheManager>
{
    public int cacheID;



    public bool reloadGameCachesFromDisk()
    {
        return false;
    }

    public int initGameCache( string campaignFile )
    {
        return 0;
    }

    public bool saveGameCache( int cache )
    {
        return false;
    }

    public void storeInteger( int cache , string missionKey , string key , int value )
    {
    }

    public void storeReal( int cache , string missionKey , string key , float value )
    {
    }

    public void storeBoolean( int cache , string missionKey , string key , bool value )
    {
    }

    public bool storeUnit( int cache , string missionKey , string key , int whichUnit )
    {
        return false;
    }

    public bool storeString( int cache , string missionKey , string key , string value )
    {
        return false;
    }

    public void syncStoredInteger( int cache , string missionKey , string key )
    {

    }

    public void SyncStoredReal( int cache , string missionKey , string key )
    {
    }

    public void SyncStoredBoolean( int cache , string missionKey , string key )
    {
    }

    public void SyncStoredUnit( int cache , string missionKey , string key )
    {
    }

    public void SyncStoredString( int cache , string missionKey , string key )
    {
    }


    public int getStoredInteger( int cache , string missionKey , string key )
    {
        return 0;
    }

    public float getStoredReal( int cache , string missionKey , string key )
    {
        return 0.0f;
    }

    public bool getStoredBoolean( int cache , string missionKey , string key )
    {
        return false;
    }

    public string getStoredString( int cache , string missionKey , string key )
    {
        return "";
    }

    public int restoreUnit( int cache , string missionKey , string key , int pid , float x , float y , float facing )
    {
        return 0;
    }


    public bool haveStoredInteger( int cache , string missionKey , string key )
    {
        return false;
    }
    public bool haveStoredReal( int cache , string missionKey , string key )
    {
        return false;
    }
    public bool haveStoredBoolean( int cache , string missionKey , string key )
    {
        return false;
    }
    public bool haveStoredUnit( int cache , string missionKey , string key )
    {
        return false;
    }
    public bool haveStoredString( int cache , string missionKey , string key )
    {
        return false;
    }


    public void flushGameCache( int cache )
    {

    }

    public void flushStoredMission( int cache , string missionKey )
    {

    }
    public void flushStoredInteger( int cache , string missionKey , string key )
    {

    }
    public void flushStoredReal( int cache , string missionKey , string key )
    {

    }
    public void flushStoredBoolean( int cache , string missionKey , string key )
    {

    }
    public void flushStoredUnit( int cache , string missionKey , string key )
    {

    }
    public void flushStoredString( int cache , string missionKey , string key )
    {

    }


}


