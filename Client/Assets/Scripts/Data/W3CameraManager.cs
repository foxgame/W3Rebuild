using UnityEngine;
using System;
using System.Collections.Generic;


public class W3CameraManager : SingletonMono<W3CameraManager>
{



    public void setCameraPosition( float x , float y )
    {
    }

    public void setCameraQuickPosition( float x , float y )
    {
    }

    public void setCameraBounds( float x1 , float y1 , float x2 , float y2 , float x3 , float y3 , float x4 , float y4 )
    {
    }

    public void stopCamera()
    {
    }

    public void resetToGameCamera( float duration )
    {
    }

    public void panCameraTo( float x , float y )
    {
    }

    public void panCameraToTimed( float x , float y , float duration )
    {
    }

    public void panCameraToWithZ( float x , float y , float zOffsetDest )
    {
    }

    public void panCameraToTimedWithZ( float x , float y , float zOffsetDest , float duration )
    {
    }

    public void setCinematicCamera( string cameraModelFile )
    {
    }

    public void setCameraRotateMode( float x , float y , float radiansToSweep , float duration )
    {
    }

    public void setCameraField( int whichField , float value , float duration )
    {
    }

    public void adjustCameraField( int whichField , float offset , float duration )
    {
    }

    public void setCameraTargetController( int whichUnit , float xoffset , float yoffset , bool inheritOrientation )
    {
    }

    public void setCameraOrientController( int whichUnit , float xoffset , float yoffset )
    {
    }


    public int createCameraSetup()
    {
        return 0;
    }

    public void cameraSetupSetField( int whichSetup , int whichField , float value , float duration )
    {
    }

    public float cameraSetupGetField( int whichSetup , int whichField )
    {
        return 0.0f;
    }

    public void cameraSetupSetDestPosition( int whichSetup , float x , float y , float duration )
    {
    }

    public float cameraSetupGetDestPositionX( int whichSetup )
    {
        return 0.0f;
    }

    public float cameraSetupGetDestPositionY( int whichSetup )
    {
        return 0.0f;
    }

    public void cameraSetupApply( int whichSetup , bool doPan , bool panTimed )
    {
    }

    public void cameraSetupApplyWithZ( int whichSetup , float zDestOffset )
    {
    }

    public void cameraSetupApplyForceDuration( int whichSetup , bool doPan , float forceDuration )
    {
    }

    public void cameraSetupApplyForceDurationWithZ( int whichSetup , float zDestOffset , float forceDuration )
    {
    }

    public void cameraSetTargetNoise( float mag , float velocity )
    {
    }

    public void cameraSetSourceNoise( float mag , float velocity )
    {
    }

    public void cameraSetTargetNoiseEx( float mag , float velocity , bool vertOnly )
    {
    }

    public void cameraSetSourceNoiseEx( float mag , float velocity , bool vertOnly )
    {
    }

    public void cameraSetSmoothingFactor( float factor )
    {
    }

    public void setCineFilterTexture( string filename )
    {
    }

    public void setCineFilterBlendMode( int whichMode )
    {
    }

    public void setCineFilterTexMapFlags( int whichFlags )
    {
    }

    public void setCineFilterStartUV( float minu , float minv , float maxu , float maxv )
    {
    }

    public void setCineFilterEndUV( float minu , float minv , float maxu , float maxv )
    {
    }

    public void setCineFilterStartColor( int red , int green , int blue , int alpha )
    {
    }

    public void setCineFilterEndColor( int red , int green , int blue , int alpha )
    {
    }

    public void setCineFilterDuration( float duration )
    {
    }

    public void displayCineFilter( bool flag )
    {
    }

    public bool isCineFilterDisplayed()
    {
        return false;
    }

    public void setCinematicScene( int portraitUnitId , int color , string speakerTitle , string text , float sceneDuration , float voiceoverDuration )
    {
    }

    public void endCinematicScene()
    {
    }

    public void forceCinematicSubtitles( bool flag )
    {
    }

    public float getCameraMargin( int whichMargin )
    {
        return 0.0f;
    }

    public float getCameraBoundMinX()
    {
        return 0.0f;
    }

    public float getCameraBoundMinY()
    {
        return 0.0f;
    }

    public float getCameraBoundMaxX()
    {
        return 0.0f;
    }

    public float getCameraBoundMaxY()
    {
        return 0.0f;
    }

    public float getCameraField( int whichField )
    {
        return 0.0f;
    }

    public float getCameraTargetPositionX()
    {
        return 0.0f;
    }

    public float getCameraTargetPositionY()
    {
        return 0.0f;
    }

    public float getCameraTargetPositionZ()
    {
        return 0.0f;
    }

    public float getCameraEyePositionX()
    {
        return 0.0f;
    }

    public float getCameraEyePositionY()
    {
        return 0.0f;
    }

    public float getCameraEyePositionZ()
    {
        return 0.0f;
    }




}

