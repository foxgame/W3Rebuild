using UnityEngine;
using System;
using System.Collections.Generic;


public class W3SoundManager : SingletonMono< W3SoundManager >
{



    public void newSoundEnvironment( string environmentName )
    {
    }

    public int createSound( string fileName , bool looping , bool is3D , bool stopwhenoutofrange , int fadeInRate , int fadeOutRate , string eaxSetting )
    {
        return 0;
    }

    public int createSoundFilenameWithLabel( string fileName , bool looping , bool is3D , bool stopwhenoutofrange , int fadeInRate , int fadeOutRate , string SLKEntryName )
    {
        return 0;
    }

    public int createSoundFromLabel( string soundLabel , bool looping , bool is3D , bool stopwhenoutofrange , int fadeInRate , int fadeOutRate )
    {
        return 0;
    }

    public int createMIDISound( string soundLabel , int fadeInRate , int fadeOutRate )
    {
        return 0;
    }

    public void setSoundParamsFromLabel( int soundHandle , string soundLabel )
    {
    }

    public void setSoundDistanceCutoff( int soundHandle , float cutoff )
    {
    }

    public void setSoundChannel( int soundHandle , int channel )
    {
    }

    public void setSoundVolume( int soundHandle , int volume )
    {
    }

    public void setSoundPitch( int soundHandle , float pitch )
    {
    }

    public void setSoundPlayPosition( int soundHandle , int millisecs )
    {
    }

    public void setSoundDistances( int soundHandle , float minDist , float maxDist )
    {
    }

    public void setSoundConeAngles( int soundHandle , float inside , float outside , int outsideVolume )
    {
    }

    public void setSoundConeOrientation( int soundHandle , float x , float y , float z )
    {
    }

    public void setSoundPosition( int soundHandle , float x , float y , float z )
    {
    }

    public void setSoundVelocity( int soundHandle , float x , float y , float z )
    {
    }

    public void attachSoundToUnit( int soundHandle , int whichUnit )
    {
    }

    public void startSound( int soundHandle )
    {
    }

    public void stopSound( int soundHandle , bool killWhenDone , bool fadeOut )
    {
    }

    public void killSoundWhenDone( int soundHandle )
    {
    }

    public void setMapMusic( string musicName , bool random , int index )
    {
    }

    public void clearMapMusic()
    {
    }

    public void playMusic( string musicName )
    {
    }

    public void playMusicEx( string musicName , int frommsecs , int fadeinmsecs )
    {
    }

    public void stopMusic( bool fadeOut )
    {
    }

    public void resumeMusic()
    {
    }

    public void playThematicMusic( string musicFileName )
    {
    }

    public void playThematicMusicEx( string musicFileName , int frommsecs )
    {
    }

    public void endThematicMusic()
    {
    }

    public void setMusicVolume( int volume )
    {
    }

    public void setMusicPlayPosition( int millisecs )
    {
    }

    public void setThematicMusicPlayPosition( int millisecs )
    {
    }

    public void setSoundDuration( int soundHandle , int duration )
    {
    }

    public int getSoundDuration( int soundHandle )
    {
        return 0;
    }

    public int getSoundFileDuration( string musicFileName )
    {
        return 0;
    }

    public void volumeGroupSetVolume( int vgroup , float scale )
    {
    }

    public void volumeGroupReset()
    {
    }

    public bool getSoundIsPlaying( int soundHandle )
    {
        return false;
    }

    public bool getSoundIsLoading( int soundHandle )
    {
        return false;
    }

    public void registerStackedSound( int soundHandle , bool byPosition , float rectwidth , float rectheight )
    {
    }

    public void unregisterStackedSound( int soundHandle , bool byPosition , float rectwidth , float rectheight )
    {
    }




}



