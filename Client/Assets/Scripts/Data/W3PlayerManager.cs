using UnityEngine;
using System;
using System.Collections.Generic;

public class W3PlayerData
{
    public int id;

	public int gold;
	public int wood;

	public int food;
	public int maxFood;

    public bool observer;
    public int race;

    public int unitCount;

    public float handicap;
    public float handicapXP;

	public List< W3Unit > units = new List<W3Unit>();
}

public class W3FogModifier
{
    public int id;

    public int playerID;

    public int state;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    public float centerX;
    public float centerY;
    public float radius;

    public bool useSharedVision;
    public bool afterUnits;
}

public class W3PlayerManager : SingletonMono<W3PlayerManager>
{
    public bool fogMask;
    public bool fogEnable;

    public W3PlayerData[] players = new W3PlayerData[ GameDefine.MAX_PLAYER_SLOTS ];
    public int localPlayer = 0;

    int modifierID = 0;
    List< W3FogModifier > modifiers = new List< W3FogModifier >();

    public override void initSingletonMono()
    {
        for ( int i = 0 ; i < GameDefine.MAX_PLAYER_SLOTS ; i++ )
        {
            players[ i ] = new W3PlayerData();
        }
    }

    public int createFogModifierRect( int id , int state , float minX , float minY , float maxX , float maxY , bool useSharedVision , bool afterUnits )
    {
        modifierID++;

        W3FogModifier m = new W3FogModifier();
        m.id = modifierID;
        m.playerID = id;
        m.state = state;
        m.minX = minX;
        m.minY = minY;
        m.maxX = maxX;
        m.maxY = maxY;
        m.useSharedVision = useSharedVision;
        m.afterUnits = afterUnits;

        return modifierID;
    }

    public int createFogModifierRadius( int id , int state , float centerX , float centerY , float radius , bool useSharedVision , bool afterUnits )
    {
        modifierID++;

        W3FogModifier m = new W3FogModifier();
        m.id = modifierID;
        m.playerID = id;
        m.state = state;
        m.centerX = centerX;
        m.centerY = centerY;
        m.radius = radius;
        m.useSharedVision = useSharedVision;
        m.afterUnits = afterUnits;

        return modifierID;
    }
    
    public void destroyFogModifier( int id )
    {
        for ( int i = 0 ; i < modifiers.Count ; i++ )
        {
            if ( modifiers[ i ].id == id )
            {
                modifiers.RemoveAt( i );
                return;
            }
        }
    }

    public void fogModifierStart( int id )
    {

    }

    public void fogModifierStop( int id )
    {

    }

    public void addUnit( int pid , W3Unit unit )
    {
        unit.baseData.playerID = pid;
        players[ pid ].units.Add( unit );
    }

    public W3PlayerData getLocalPlayer()
    {
        return players[ localPlayer ];
    }

    public W3PlayerData getPlayer( int id )
    {
        return players[ id ];
    }

    public bool isLocalPlayer( int id )
    {
        return localPlayer == id;
    }

    public bool isPlayerAlly( int id , int pid )
    {
        Dictionary<int , bool> alliance = W3MapManager.instance.PlayerAlliance[ id ].alliance[ pid ];

        foreach ( KeyValuePair<int , bool> kvp in alliance )
        {
            if ( kvp.Value )
            {
                return true;
            }
        }

        return false;
    }

    public void updateUnits( float delay )
    {
        for ( int i = 0 ; i < players.Length ; i++ )
        {
            for ( int j = 0 ; j < players[ i ].units.Count ; j++ )
            {
                players[ i ].units[ j ].updateUnit( delay );
            }
        }
    }


    public bool isPlayerEnemy( int id , int pid )
    {
        return false;
    }

    public bool isPlayerInForce( int id , int force )
    {
        return false;
    }

    public bool isVisibleToPlayer( int id , float x , float y )
    {
        return false;
    }

    public bool isFoggedToPlayer( int id , float x , float y )
    {
        return false;
    }

    public bool isMaskedToPlayer( int id , float x , float y )
    {
        return false;
    }

    public int getPlayerUnitCount( int id )
    {
        return 0;
    }

    public int getPlayerTypedUnitCount( int id , string unitName , bool includeIncomplete , bool includeUpgrades )
    {
        return 0;
    }

    public int getPlayerStructureCount( int id , bool includeIncomplete )
    {
        return 0;
    }

    public int getPlayerState( int id , int state )
    {
        return 0;
    }

    public bool getPlayerAlliance( int id , int pid , int type )
    {
        return false;
    }

    public float getPlayerHandicap( int id )
    {
        return players[ id ].handicap;
    }

    public float getPlayerHandicapXP( int id )
    {
        return players[ id ].handicapXP;
    }

    public void setPlayerHandicap( int id , float handicap )
    {
        players[ id ].handicap = handicap;
    }

    public void setPlayerHandicapXP( int id , float handicap )
    {
        players[ id ].handicapXP = handicap;
    }

    public void setPlayerTechMaxAllowed( int id , int techid , int maximum )
    {

    }

    public int getPlayerTechMaxAllowed( int id , int techid )
    {
        return 0;
    }

    public void addPlayerTechResearched( int id , int techid , int levels )
    {

    }

    public void setPlayerTechResearched( int id , int techid , int levels )
    {

    }

    public bool getPlayerTechResearched( int id , int techid , bool specificonly )
    {
        return false;
    }

    public int getPlayerTechCount( int id , int techid , bool specificonly )
    {
        return 0;
    }

    public void setPlayerUnitsOwner( int id , int newOwner )
    {

    }

    public void cripplePlayer( int id , int force , bool flag )
    {

    }

    public void setPlayerAbilityAvailable( int id , int abilid , bool avail )
    {

    }

    public void setPlayerState( int id , int state , int value )
    {

    }

    public void removePlayer( int id , int result )
    {

    }
    
    public void cachePlayerHeroData( int id )
    {
    }

    public void setFogStateRect( int id , int state , float minX , float minY , float maxX , float maxY , bool useSharedVision )
    {

    }

    public void setFogStateRadius( int id , int state , float centerX , float centerY , float radius , bool useSharedVision )
    {

    }


}
