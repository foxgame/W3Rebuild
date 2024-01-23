using UnityEngine;
using System;
using System.Collections.Generic;


public class W3EffectsManager : SingletonMono< W3EffectsManager >
{




    public int addWeatherEffect( float minX , float minY , float maxX , float maxY , int effectID )
    {
        return 0;
    }

    public void removeWeatherEffect( int whichEffect )
    {

    }

    public void enableWeatherEffect( int whichEffect , bool enable )
    {
    }

    public int terrainDeformCrater( float x , float y , float radius , float depth , int duration , bool permanent )
    {
        return 0;
    }

    public int terrainDeformRipple( float x , float y , float radius , float depth , int duration , int count , float spaceWaves , float timeWaves , float radiusStartPct , bool limitNeg )
    {
        return 0;
    }

    public int terrainDeformWave( float x , float y , float dirX , float dirY , float distance , float speed , float radius , float depth , int trailTime , int count )
    {
        return 0;
    }

    public int terrainDeformRandom( float x , float y , float radius , float minDelta , float maxDelta , int duration , int updateInterval )
    {
        return 0;
    }

    public void terrainDeformStop( int deformation , int duration )
    {
    }

    public void terrainDeformStopAll()
    {
    }

    public int addSpecialEffect( string modelName , float x , float y )
    {
        return 0;
    }

    public int addSpecialEffectTarget( string modelName , int targetWidget , string attachPointName )
    {
        return 0;
    }

    public void destroyEffect( int whichEffect )
    {
    }

    public int addSpellEffect( string abilityString , int t , float x , float y )
    {
        return 0;
    }

    public int addSpellEffectById( int abilityId , int t , float x , float y )
    {
        return 0;
    }

    public int addSpellEffectTarget( string modelName , int t , int targetWidget , string attachPoint )
    {
        return 0;
    }

    public int addSpellEffectTargetById( int abilityId , int t , int targetWidget , string attachPoint )
    {
        return 0;
    }







}

