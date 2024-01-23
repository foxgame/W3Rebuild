using UnityEngine;
using System;
using System.Collections.Generic;

public class W3GameManager : SingletonMono< W3GameManager >
{
    public int gameState;
    public int eventGameState;

    public int winningPlayer;

    public int version;

    public override void initSingletonMono()
    {
        Application.targetFrameRate = -1;

        GameSetting.instance.init();
    }


    void FixedUpdate()
    {
        W3PlayerManager.instance.updateUnits( Time.fixedDeltaTime );
    }

    public void releaseUnused()
    {
        Resources.UnloadUnusedAssets();
    }

    public bool versionCompatible( int version )
    {
        return false;
    }

    public bool versionSupported( int version )
    {
        return false;
    }
    
    public void endGame( bool doScoreScreen )
    {

    }

    public void changeLevel( string newLevel , bool doScoreScreen )
    {

    }

    public void restartGame( bool doScoreScreen )
    {

    }

    public void reloadGame( )
    {

    }

    public void setCampaignMenuRace( int race )
    {

    }

    public void setCampaignMenuRaceEx( int campaignIndex )
    {

    }

    public void forceCampaignSelectScreen()
    {

    }

    public void loadGame( string saveFileName , bool doScoreScreen )
    {

    }

    public void saveGame( string saveFileName )
    {

    }

    public bool renameSaveDirectory( string sourceDirName , string destDirName )
    {
        return false;
    }

    public bool removeSaveDirectory( string sourceDirName  )
    {
        return false;
    }

    public bool copySaveGame( string sourceSaveName , string destSaveName )
    {
        return false;
    }

    public bool saveGameExists( string saveName )
    {
        return false;
    }

    public void syncSelections()
    {

    }

    public void setFloatGameState( int state , float value )
    {

    }

    public float getFloatGameState( int state )
    {
        return 0.0f;
    }

    public void setIntegerGameState( int state , int value )
    {

    }

    public int getIntegerGameState( int state )
    {
        return 0;
    }



}



