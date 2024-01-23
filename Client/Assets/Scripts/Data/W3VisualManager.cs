using UnityEngine;
using System;
using System.Collections.Generic;



public class W3VisualManager : SingletonMono< W3VisualManager >
{

    public void setTerrainFog( float a , float b , float c , float d , float e )
    {
    }

    public void resetTerrainFog()
    {
    }

    public void setUnitFog( float a , float b , float c , float d , float e )
    {
    }

    public void setTerrainFogEx( int style , float zstart , float zend , float density , float red , float green , float blue )
    {
    }

    public void displayTextToPlayer( int pid , float x , float y , string message )
    {
    }

    public void displayTimedTextToPlayer( int pid , float x , float y , float duration , string message )
    {
    }

    public void displayTimedTextFromPlayer( int pid , float x , float y , float duration , string message )
    {
    }

    public void clearTextMessages()
    {
    }

    public void setDayNightModels( string terrainDNCFile , string unitDNCFile )
    {
    }

    public void setSkyModel( string skyModelFile )
    {
    }

    public void enableUserControl( bool b )
    {
    }

    public void enableUserUI( bool b )
    {
    }

    public void suspendTimeOfDay( bool b )
    {
    }

    public void setTimeOfDayScale( float r )
    {
    }

    public float getTimeOfDayScale()
    {
        return 0.0f;
    }

    public void showInterface( bool flag , float fadeDuration )
    {
    }

    public void pauseGame( bool flag )
    {
    }

    public void unitAddIndicator( int uid , int red , int green , int blue , int alpha )
    {
    }

    public void addIndicator( int wid , int red , int green , int blue , int alpha )
    {
    }

    public void pingMinimap( float x , float y , float duration )
    {
    }

    public void pingMinimapEx( float x , float y , float duration , int red , int green , int blue , bool extraEffects )
    {
    }

    public void enableOcclusion( bool flag )
    {
    }

    public void setIntroShotText( string introText )
    {
    }

    public void setIntroShotModel( string introModelPath )
    {
    }

    public void enableWorldFogBoundary( bool b )
    {
    }

    public void playModelCinematic( string modelName )
    {
    }

    public void playCinematic( string movieName )
    {
    }

    public void forceUIKey( string key )
    {
    }

    public void forceUICancel()
    {
    }

    public void displayLoadDialog()
    {
    }

    public void setAltMinimapIcon( string iconPath )
    {
    }

    public void disableRestartMission( bool flag )
    {
    }

    public int createTextTag()
    {
        return 0;
    }

    public void destroyTextTag( int id )
    {
    }

    public void setTextTagText( int id , string s , float height )
    {
    }

    public void setTextTagPos( int id , float x , float y , float heightOffset )
    {
    }

    public void setTextTagPosUnit( int id , int uid , float heightOffset )
    {
    }

    public void setTextTagColor( int id , int red , int green , int blue , int alpha )
    {
    }

    public void setTextTagVelocity( int id , float xvel , float yvel )
    {
    }

    public void setTextTagVisibility( int id , bool flag )
    {
    }

    public void setReservedLocalHeroButtons( int reserved )
    {
    }

    public int getAllyColorFilterState()
    {
        return 0;
    }

    public void setAllyColorFilterState( int state )
    {
    }


    public int createTrackable( string trackableModelPath , float x , float y , float facing )
    {
        return 0;
    }









}

