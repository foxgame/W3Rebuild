using UnityEngine;
using System;
using System.Collections.Generic;


public class W3Timer
{
    public int id;
    public bool pause;
    public bool periodic;
    public float timeout;
    public float time;
    public BJCode code;
}

public class W3TimerManager : SingletonMono< W3TimerManager >
{
    int timerID = 0;

    public List< W3Timer > timers = new List< W3Timer >();

    public W3Timer getTimer( int id )
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( timers[ i ].id == id )
            {
                return timers[ i ];
            }
        }

        return null;
    }

    public int createTimer()
    {
        timerID++;

        W3Timer t = new W3Timer();
        t.id = timerID;
        t.pause = true;
        t.periodic = false;

        return t.id;
    }

    public void destroyTimer( int id )
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( timers[ i ].id == id )
            {
                timers.RemoveAt( i );
                return;
            }
        }
    }

    public void timerStart( int id , double timeout , bool periodic ,BJCode handlerFunc )
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( timers[ i ].id == id )
            {
                timers[ i ].pause = false;
                timers[ i ].timeout = (float)timeout;
                timers[ i ].periodic = periodic;
                timers[ i ].code = handlerFunc; 

                return;
            }
        }
    }

    public double timerGetElapsed( int id )
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( timers[ i ].id == id )
            {   
                return (double)timers[ i ].time;
            }
        }

        return 0.0;
    }

    public double timerGetRemaining( int id )
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( timers[ i ].id == id )
            {
                return (double)( timers[ i ].time - timers[ i ].timeout );
            }
        }

        return 0.0;
    }

    public double timerGetTimeout( int id )
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( timers[ i ].id == id )
            {
                return (double)timers[ i ].timeout;
            }
        }

        return 0.0;
    }

    public void pauseTimer( int id )
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( timers[ i ].id == id )
            {
                timers[ i ].pause = true;
                return;
            }
        }
    }

    public void resumeTimer( int id )
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( timers[ i ].id == id )
            {
                timers[ i ].pause = false;
                return;
            }
        }
    }

    public int getExpiredTimer()
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( timers[ i ].time > timers[ i ].timeout )
            {
                return timers[ i ].id;
            }
        }

        return 0;
    }


    private void Update()
    {
        for ( int i = 0 ; i < timers.Count ; i++ )
        {
            if ( !timers[ i ].pause )
            {
                timers[ i ].time += Time.deltaTime;

                if ( timers[ i ].time > timers[ i ].timeout )
                {
                    if ( timers[ i ].code != null )
                    {
                        timers[ i ].code();
                    }

                    if ( timers[ i ].periodic )
                    {
                        timers[ i ].time -= timers[ i ].timeout;
                    }
                    else
                    {
                        timers[ i ].pause = true;
                    }
                }
            }
        }

    }



    public int createTimerDialog( int id )
    {
        return 0;
    }

    public void destroyTimerDialog( int id )
    {
    }

    public void timerDialogSetTitle( int id , string title )
    {
    }

    public void timerDialogSetTitleColor( int id , int red , int green , int blue , int alpha )
    {
    }

    public void timerDialogSetTimeColor( int id , int red , int green , int blue , int alpha )
    {
    }

    public void timerDialogSetSpeed( int id , double speedMultFactor )
    {
    }

    public void timerDialogDisplay( int id , bool display )
    {
    }

    public bool isTimerDialogDisplayed( int id )
    {
        return false;
    }

    public void timerDialogSetRealTimeRemaining( int id , float timeRemaining )
    {
    }


}
