using UnityEngine;
using System;
using System.Collections.Generic;



public class W3AIManager : SingletonMono<W3AIManager>
{




    public void debugS( string str )
    {

    }

    public void debugFI( string str , int val )
    {

    }

    public void debugUnitID( string str , int val )
    {

    }

    public void displayText( int p , string str )
    {

    }

    public void displayTextI( int p , string str , int val )
    {

    }

    public void displayTextII( int p , string str , int v1 , int v2 )
    {

    }

    public void displayTextIII( int p , string str , int v1 , int v2 , int v3 )
    {

    }

    public bool doAiScriptDebug()
    {

        return false;
    }

    public int getAiPlayer()
    {

        return 0;
    }

    public int getHeroId()
    {

        return 0;
    }

    public int getHeroLevelAI()
    {

        return 0;
    }

    public int getUnitCount( int unitid )
    {

        return 0;
    }

    public int getPlayerUnitTypeCount( int p , int unitid )
    {

        return 0;
    }

    public int getUnitCountDone( int unitid )
    {

        return 0;
    }

    public int getTownUnitCount( int id , int tn , bool dn )
    {

        return 0;
    }

    public int getUnitGoldCost( int unitid )
    {

        return 0;
    }

    public int getUnitWoodCost( int unitid )
    {

        return 0;
    }

    public int getUnitBuildTime( int unitid )
    {

        return 0;
    }

    public int getMinesOwned()
    {

        return 0;
    }

    public int getGoldOwned()
    {

        return 0;
    }

    public int townWithMine()
    {

        return 0;
    }

    public bool townHasMine( int townid )
    {

        return false;
    }

    public bool townHasHall( int townid )
    {

        return false;
    }

    public int getUpgradeLevel( int id )
    {

        return 0;
    }

    public int getUpgradeGoldCost( int id )
    {

        return 0;
    }

    public int getUpgradeWoodCost( int id )
    {

        return 0;
    }

    public int getNextExpansion()
    {

        return 0;
    }

    public int getMegaTarget()
    {

        return 0;
    }

    public int getBuilding( int p )
    {

        return 0;
    }

    public int getEnemyPower()
    {

        return 0;
    }

    public void setAllianceTarget( int id )
    {

    }

    public int getAllianceTarget()
    {

        return 0;
    }

    public bool setProduce( int qty , int id , int town )
    {

        return false;
    }

    public void unsummon( int unitid )
    {

    }

    public bool setExpansion( int peon , int id )
    {

        return false;
    }

    public bool setUpgrade( int id )
    {

        return false;
    }

    public void setHeroLevels(BJCodeInt func )
    {

    }

    public void setNewHeroes( bool state )
    {

    }

    public void purchaseZeppelin()
    {

    }

    public bool mergeUnits( int qty , int a , int b , int make )
    {

        return false;
    }

    public bool convertUnits( int qty , int id )
    {

        return false;
    }

    public void setCampaignAI()
    {

    }

    public void setMeleeAI()
    {

    }

    public void setTargetHeroes( bool state )
    {

    }

    public void setPeonsRepair( bool state )
    {

    }

    public void setRandomPaths( bool state )
    {

    }

    public void setDefendPlayer( bool state )
    {

    }

    public void setHeroesFlee( bool state )
    {

    }

    public void setHeroesBuyItems( bool state )
    {

    }

    public void setWatchMegaTargets( bool state )
    {

    }

    public void setIgnoreInjured( bool state )
    {

    }

    public void setHeroesTakeItems( bool state )
    {

    }

    public void setUnitsFlee( bool state )
    {

    }

    public void setGroupsFlee( bool state )
    {

    }

    public void setSlowChopping( bool state )
    {

    }

    public void setCaptainChanges( bool allow )
    {

    }

    public void setSmartArtillery( bool state )
    {

    }

    public void setReplacementCount( int qty )
    {

    }

    public void groupTimedLife( bool allow )
    {

    }

    public void removeInjuries()
    {

    }

    public void removeSiege()
    {

    }

    public void initAssault()
    {

    }

    public bool addAssault( int qty , int id )
    {

        return false;
    }

    public bool addDefenders( int qty , int id )
    {

        return false;
    }

    public int getCreepCamp( int min , int max , bool flyers_ok )
    {

        return 0;
    }

    public void startGetEnemyBase()
    {

    }

    public bool waitGetEnemyBase()
    {

        return false;
    }

    public int getEnemyBase()
    {

        return 0;
    }

    public int getExpansionFoe()
    {

        return 0;
    }

    public int getEnemyExpansion()
    {

        return 0;
    }

    public int getExpansionX()
    {

        return 0;
    }

    public int getExpansionY()
    {

        return 0;
    }

    public void setStagePoint( float x , float y )
    {

    }

    public void attackMoveKill( int target )
    {

    }

    public void attackMoveXY( int x , int y )
    {

    }

    public void loadZepWave( int x , int y )
    {

    }

    public bool suicidePlayer( int id , bool check_full )
    {

        return false;
    }

    public bool suicidePlayerUnits( int id , bool check_full )
    {

        return false;
    }

    public bool captainInCombat( bool attack_captain )
    {

        return false;
    }

    public bool isTowered( int target )
    {

        return false;
    }

    public void clearHarvestAI()
    {

    }

    public void harvestGold( int town , int peons )
    {

    }

    public void harvestWood( int town , int peons )
    {

    }

    public int getExpansionPeon()
    {

        return 0;
    }

    public void stopGathering()
    {

    }

    public void addGuardPost( int id , float x , float y )
    {

    }

    public void fillGuardPosts()
    {

    }

    public void returnGuardPosts()
    {

    }

    public void createCaptains()
    {

    }

    public void setCaptainHome( int which , float x , float y )
    {

    }

    public void resetCaptainLocs()
    {

    }

    public void shiftTownSpot( float x , float y )
    {

    }

    public void teleportCaptain( float x , float y )
    {

    }

    public void clearCaptainTargets()
    {

    }

    public void captainAttack( float x , float y )
    {

    }

    public void captainVsUnits( int id )
    {

    }

    public void captainVsPlayer( int id )
    {

    }

    public void captainGoHome()
    {

    }

    public bool captainIsHome()
    {

        return false;
    }

    public bool captainIsFull()
    {

        return false;
    }

    public bool captainIsEmpty()
    {

        return false;
    }

    public int captainGroupSize()
    {

        return 0;
    }

    public int captainReadiness()
    {

        return 0;
    }

    public bool captainRetreating()
    {

        return false;
    }

    public int captainReadinessHP()
    {

        return 0;
    }

    public int captainReadinessMa()
    {

        return 0;
    }

    public bool captainAtGoal()
    {

        return false;
    }

    public bool creepsOnMap()
    {

        return false;
    }

    public void suicideUnit( int count , int unitid )
    {

    }

    public void suicideUnitEx( int ct , int uid , int pid )
    {

    }

    public void startThread(BJCode func )
    {

    }

    public void sleep( float seconds )
    {

    }

    public bool unitAlive( int id )
    {

        return false;
    }

    public bool unitInvis( int id )
    {

        return false;
    }

    public int ignoredUnits( int unitid )
    {

        return 0;
    }

    public bool townThreatened()
    {

        return false;
    }

    public void disablePathing()
    {

    }

    public void setAmphibious()
    {

    }

    public int commandsWaiting()
    {

        return 0;
    }

    public int getLastCommand()
    {

        return 0;
    }

    public int getLastData()
    {

        return 0;
    }

    public void popLastCommand()
    {

    }

    public int meleeDifficulty()
    {

        return 0;
    }

}



