using UnityEngine;
using System;
using System.Collections.Generic;




public class W3UnitManager : SingletonMono<W3UnitManager>
{
    public List<W3Unit> selectUnitList = new List<W3Unit>();

    Transform unitObjTrans = null;

    public override void initSingletonMono()
    {
        unitObjTrans = GameObject.Find( "Map" ).transform.Find( "Units" ).transform;

    }

    public void setUnitState( int uid , int s , float v )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.unitState = s;
        unit.baseData.unitStateValue = v;
    }

    public void setUnitFacing( int uid , float a )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.facing = a;
        unit.baseData.facingTime = 0.0f;
    }

    public void setUnitFacingTimed( int uid , float a , float t )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.facing = a;
        unit.baseData.facingTime = t;
    }

    public void setUnitMoveSpeed( int uid , float s )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.moveSpeed = s;
    }

    public void setUnitFlyHeight( int uid , float h , float r )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.flyHeight = h;
        unit.baseData.flyHeightRate = r;
    }

    public void setUnitTurnSpeed( int uid , float s )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.turnSpeed = s;
    }

    public void setUnitPropWindow( int uid , float a )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.propWindowAngle = a;
    }

    public void setUnitAcquireRange( int uid , float a )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.acquireRange = a;
    }

    public void setUnitCreepGuard( int uid , bool b )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.creepGuard = b;
    }

    public void setUnitOwner( int uid , int pid , bool cc )
    {
        W3Base item = W3BaseManager.instance.getData( uid );

        item.baseData.playerID = pid;

        if ( cc )
        {

        }
    }

    public void setUnitColor( int uid , int c )
    {

    }

    public void setUnitScale( int uid , float x , float y , float z )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.scaleX = x;
        unit.baseData.scaleY = y;
        unit.baseData.scaleZ = z;
    }

    public void setUnitTimeScale( int uid , float s )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.timeScale = s;
    }

    public void setUnitBlendTime( int uid , float s )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.blendTime = s;
    }

    public void setUnitVertexColor( int uid , int red , int green , int blue , int alpha )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.red = red / 255.0f;
        unit.baseData.green = green / 255.0f;
        unit.baseData.blue = blue / 255.0f;
        unit.baseData.alpha = alpha / 255.0f;
    }

    public void queueUnitAnimation( int uid , string a )
    {

    }

    public void setUnitAnimation( int uid , string a )
    {

    }

    public void setUnitAnimationByIndex( int uid , int a )
    {

    }

    public void setUnitAnimationWithRarity( int uid , string a , int c )
    {

    }

    public void addUnitAnimationProperties( int uid , string a , bool add )
    {

    }

    public void setUnitLookAt( int uid , string bone , int lookAtTarget , float offsetX , float offsetY , float offsetZ )
    {

    }

    public void resetUnitLookAt( int uid )
    {

    }

    public void setUnitRescuable( int uid , int pid , bool b )
    {

    }

    public void setUnitRescueRange( int uid , float range )
    {

    }

    public void setHeroStr( int uid , int newStr , bool permanent )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        if ( permanent )
        {
            unit.baseData.heroStr = newStr;
        }
        else
        {
            unit.baseData.heroStrBonuses = newStr;
        }
    }

    public void setHeroAgi( int uid , int newAgi , bool permanent )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        if ( permanent )
        {
            unit.baseData.heroAgi = newAgi;
        }
        else
        {
            unit.baseData.heroAgiBonuses = newAgi;
        }
    }

    public void setHeroInt( int uid , int newInt , bool permanent )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        if ( permanent )
        {
            unit.baseData.heroInt = newInt;
        }
        else
        {
            unit.baseData.heroIntBonuses = newInt;
        }
    }

    public int GetHeroStr( int uid , bool includeBonuses )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        if ( includeBonuses )
        {
            return unit.baseData.heroStr + unit.baseData.heroStrBonuses;
        }
        else
        {
            return unit.baseData.heroStr;
        }
    }

    public int GetHeroAgi( int uid , bool includeBonuses )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        if ( includeBonuses )
        {
            return unit.baseData.heroAgi + unit.baseData.heroAgiBonuses;
        }
        else
        {
            return unit.baseData.heroAgi;
        }
    }

    public int GetHeroInt( int uid , bool includeBonuses )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        if ( includeBonuses )
        {
            return unit.baseData.heroInt + unit.baseData.heroIntBonuses;
        }
        else
        {
            return unit.baseData.heroInt;
        }
    }

    public bool unitStripHeroLevel( int uid , int howManyLevels )
    {
        return false;
    }

    public void setHeroXP( int uid , int newXpVal , bool showEyeCandy )
    {

    }

    public bool unitModifySkillPoints( int uid , int skillPointDelta )
    {
        return false;
    }

    public void addHeroXP( int uid , int xpToAdd , bool showEyeCandy )
    {

    }

    public void setHeroLevel( int uid , int level , bool showEyeCandy )
    {

    }

    public void suspendHeroXP( int uid , bool flag )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.heroXPSuspend = flag;
    }

    public void selectHeroSkill( int uid , int abilcode )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.heroSkillSelect = abilcode;
    }

    public bool reviveHero( int uid , float x , float y , bool doEyecandy )
    {
        return false;
    }

    public void setUnitExploded( int uid , bool exploded )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.exploded = exploded;
    }

    public void setUnitInvulnerable( int uid , bool flag )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.invulnerable = flag;
    }

    public void pauseUnit( int uid , bool flag )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.pause = flag;
    }

    public void setUnitPathing( int uid , bool flag )
    {
        W3Base unit = W3BaseManager.instance.getData( uid );

        unit.baseData.pathing = flag;
    }

    public void clearSelection()
    {
        for ( int i = 0 ; i < selectUnitList.Count ; i++ )
        {
            selectUnitList[ i ].selection( false );
        }

        selectUnitList.Clear();

//         W3UnitUI.instance.clear();
    }

    public void selectUnit( int uid , bool flag )
    {

    }

    public bool unitAddItem( int id , int iid )
    {
        return false;
    }

    public int unitAddItemById( int id , int iid )
    {
        return GameDefine.INVALID_ID;
    }

    public bool unitAddItemToSlotById( int id , int iid , int slot )
    {
        return false;
    }

    public void unitRemoveItem( int id , int iid )
    {

    }

    public int unitRemoveItemFromSlot( int id , int slot )
    {
        return GameDefine.INVALID_ID;
    }

    public bool unitHasItem( int id , int iid )
    {
        return false;
    }

    public int unitItemInSlot( int id , int slot )
    {
        return GameDefine.INVALID_ID;
    }

    public bool unitUseItem( int id , int iid )
    {
        return false;
    }

    public bool unitUseItemPoint( int id , int iid , float x , float y )
    {
        return false;
    }

    public bool unitUseItemTarget( int id , int iid , int target )
    {
        return false;
    }

    public void setUnitUseFood( int id , bool useFood )
    {

    }

    public bool isUnitInGroup( int id , int gid )
    {

        return false;
    }

    public bool isUnitInForce( int id , int fid )
    {

        return false;
    }

    public bool isUnitOwnedByPlayer( int id , int pid )
    {

        return false;
    }

    public bool isUnitAlly( int id , int pid )
    {

        return false;
    }

    public bool isUnitEnemy( int id , int pid )
    {

        return false;
    }

    public bool isUnitVisible( int id , int pid )
    {

        return false;
    }

    public bool isUnitDetected( int id , int pid )
    {

        return false;
    }

    public bool isUnitInvisible( int id , int pid )
    {

        return false;
    }

    public bool isUnitFogged( int id , int pid )
    {

        return false;
    }

    public bool isUnitMasked( int id , int pid )
    {

        return false;
    }

    public bool isUnitSelected( int id , int pid )
    {

        return false;
    }

    public bool isUnitRace( int id , int rid )
    {

        return false;
    }

    public bool isUnitType( int id , int type )
    {

        return false;
    }

    public bool isUnit( int id , int uid )
    {

        return false;
    }

    public bool isUnitInRange( int id , int uid , float distance )
    {

        return false;
    }

    public bool isUnitInRangeXY( int id , float x , float y , float distance )
    {

        return false;
    }

    public bool isUnitHidden( int id )
    {

        return false;
    }

    public bool isUnitIllusion( int id )
    {

        return false;
    }

    public bool isUnitInTransport( int id , int uid )
    {

        return false;
    }

    public bool isUnitLoaded( int id )
    {

        return false;
    }

    public bool isUnitIdType( int id , int type )
    {

        return false;
    }

    public void unitShareVision( int id , int pid , bool share )
    {

    }

    public void unitSuspendDecay( int id , bool suspend )
    {

    }

    public bool unitAddAbility( int id , int abilityId )
    {

        return false;
    }

    public bool unitRemoveAbility( int id , int abilityId )
    {

        return false;
    }

    public bool unitRemoveBuffs( int id , bool removePositive , bool removeNegative )
    {

        return false;
    }

    public bool unitRemoveBuffsEx( int id , bool removePositive , bool removeNegative , bool magic , bool physical , bool timedLife , bool aura , bool autoDispel )
    {

        return false;
    }

    public bool unitHasBuffsEx( int id , bool removePositive , bool removeNegative , bool magic , bool physical , bool timedLife , bool aura , bool autoDispel )
    {
        return false;
    }

    public int unitCountBuffsEx( int id , bool removePositive , bool removeNegative , bool magic , bool physical , bool timedLife , bool aura , bool autoDispel )
    {
        return 0;
    }

    public void unitAddSleep( int id , bool add )
    {

    }

    public bool unitCanSleep( int id )
    {
        return false;
    }

    public void unitAddSleepPerm( int id , bool add )
    {

    }

    public bool unitCanSleepPerm( int id )
    {
        return false;
    }

    public bool unitIsSleeping( int id )
    {
        return false;
    }
    
    public void unitWakeUp()
    {

    }

    public void unitApplyTimedLife( int id , int buffId , float duration )
    {

    }

    public bool unitIgnoreAlarm( int id , bool flag )
    {
        return false;
    }

    public bool unitIgnoreAlarmToggled( int id )
    {
        return false;
    }

    public void unitResetCooldown( int id )
    {

    }

    public void unitSetConstructionProgress( int id , int constructionPercentage )
    {

    }

    public void unitSetUpgradeProgress( int id , int upgradePercentage )
    {

    }

    public void unitPauseTimedLife( int id , bool flag )
    {

    }

    public void unitSetUsesAltIcon( int id , bool flag )
    {

    }

    public bool issueImmediateOrder( int id , int order )
    {
        return false;
    }
    
    public bool issuePointOrder( int id , int order , float x , float y )
    {
        return false;
    }

    public bool issueTargetOrder( int id , int order , int tid )
    {
        return false;
    }

    public bool issueInstantTargetOrder( int id , int order , int tid , int itid )
    {
        return false;
    }

    public bool issueBuildOrder( int id , int unitId , float x , float y )
    {
        return false;
    }

    public bool issueNeutralImmediateOrder( int pid , int id , int unitId )
    {
        return false;
    }

    public bool issueNeutralPointOrder( int pid , int id , int unitId , float x , float y )
    {
        return false;
    }

    public bool issueNeutralTargetOrder( int pid , int id , int unitId , int tid )
    {
        return false;
    }

    public void setResourceAmount( int id , int amount )
    {
        W3Base unit = W3BaseManager.instance.getData( id );

        unit.baseData.resourceAmount = amount;
    }

    public void addResourceAmount( int id , int amount )
    {
        W3Base unit = W3BaseManager.instance.getData( id );

        unit.baseData.resourceAmount += amount;
    }

    public void waygateSetDestination( int id , float x , float y )
    {
        W3Base unit = W3BaseManager.instance.getData( id );

        unit.baseData.destinationX = x;
        unit.baseData.destinationZ = y;
    }

    public void waygateActivate( int id , bool activate )
    {
    }

    public bool waygateIsActive( int id )
    {
        return false;
    }

    public void addItemToAllStock( int itemId , int currentStock , int stockMax )
    {

    }

    public void addItemToStock( int id , int itemId , int currentStock , int stockMax )
    {

    }

    public void addUnitToAllStock( int unitId , int currentStock , int stockMax )
    {

    }

    public void addUnitToStock( int id , int unitId , int currentStock , int stockMax )
    {

    }

    public void removeItemFromAllStock( int itemId )
    {

    }

    public void removeItemFromStock( int id , int itemId )
    {

    }

    public void removeUnitFromAllStock( int unitId )
    {

    }

    public void removeUnitFromStock( int id , int unitId )
    {

    }

    public void setAllItemTypeSlots( int slots )
    {

    }

    public void setAllUnitTypeSlots( int slots )
    {

    }

    public void setItemTypeSlots( int id , int slots )
    {

    }

    public void setUnitTypeSlots( int id , int slots )
    {

    }

    public void setUnitUserData( int id , int data )
    {
        W3Base unit = W3BaseManager.instance.getData( id );

        unit.baseData.userData = data;
    }


   

    public int createUnit( int pid , string unitid , float x , float z , float face , bool corpse )
    {
        return createUnit( pid , GameDefine.UnitId( unitid ) , x , z , face , corpse );
    }

    public int createUnit( int pid , int unitid , float x , float z , float face , bool corpse )
    {
        W3UnitUIConfigData d = W3UnitUIConfig.instance.getData( unitid );
        W3UnitDataConfigData d1 = W3UnitDataConfig.instance.getData( unitid );
        W3UnitBalanceConfigData d2 = W3UnitBalanceConfig.instance.getData( unitid );
        W3UnitWeaponsConfigData d3 = W3UnitWeaponsConfig.instance.getData( unitid );
        W3UberSplatDataConfigData d4 = W3UberSplatDataConfig.instance.getData( d.uberSplat );

        float a = (float)face * Mathf.Rad2Deg;

        float gx = ( W3TerrainManager.instance.offsetX - (float)x );
        float gz = ( W3TerrainManager.instance.offsetY - (float)z );

        float sx = -gx / GameDefine.TERRAIN_SIZE_PER;
        float sz = -gz / GameDefine.TERRAIN_SIZE_PER;

        if ( d1.isBuilding )
        {
            if ( !W3TerrainManager.instance.canBuild( (int)sx , (int)sz , d1 ) )
            {
                return GameDefine.INVALID_ID;
            }

            W3TerrainManager.instance.setBuildingPath( (int)sx , (int)sz , d1 );
        }

        float gy = W3TerrainManager.instance.getSmallNode( (int)sx , (int)sz ).ym;

        string name1 = "Prefabs\\" + d.file;

        GameObject obj = (GameObject)GameObject.Instantiate( (GameObject)Resources.Load( name1 ) , unitObjTrans );

        GameObject hpObj = W3HPBar.Create();
        W3HPBar hpbar = hpObj.GetComponent<W3HPBar>();
        hpbar.init( 100 , 100 );

        W3Unit unit = obj.GetComponent<W3Unit>();
        unit.unitData = d1;
        unit.unitUI = d;
        unit.unitBalance = d2;
        unit.unitWeapons = d3;
        unit.splatData = d4;
        unit.startAnimation = true;
        unit.baseData.typeID = unitid;

        unit.baseData.hp = 100;
        unit.baseData.hpMax = 100;

        unit.collision = (byte)( d2.collision / 16 );
        unit.setHPTrans( hpObj.transform );
        unit.hpBar = hpbar;

        hpbar.init( unit.baseData.hp , unit.baseData.hpMax );

        if ( d2.collision % 16 != 0 )
        {
            unit.collision++;
        }
        if ( d2.collision % 32 == 0 )
        {
            unit.collision++;
        }
        if ( unit.collision == 1 )
        {
            unit.collision = 2;
        }

        int baseID = W3BaseManager.instance.addData( unit );

        unit.setPos( new Vector3( gx , gy , gz ) );
        unit.setAngles( new Vector3( 0.0f , a , 0.0f ) );
        unit.setuberSplatPos( gx , gz );

        unit.updateFogUV();

        if ( d1.isBuilding )
        {
            unit.updateUberSplat();
        }

        W3UnitMeshColor[] mc = unit.GetComponentsInChildren<W3UnitMeshColor>();
        for ( int i = 0 ; i < mc.Length ; i++ )
        {
            mc[ i ].type = pid;
            mc[ i ].UpdateColor( mc[ i ].type );
        }

        W3PlayerManager.instance.addUnit( pid , unit );

        return baseID;
    }


    public int createUnitA( int pid , int unitid , float x , float z , float a , bool corpse )
    {
        W3UnitUIConfigData d = W3UnitUIConfig.instance.getData( unitid );
        W3UnitDataConfigData d1 = W3UnitDataConfig.instance.getData( unitid );
        W3UnitBalanceConfigData d2 = W3UnitBalanceConfig.instance.getData( unitid );
        W3UnitWeaponsConfigData d3 = W3UnitWeaponsConfig.instance.getData( unitid );
        W3UberSplatDataConfigData d4 = W3UberSplatDataConfig.instance.getData( d.uberSplat );

        if ( d1.isBuilding )
        {
            if ( !W3TerrainManager.instance.canBuild( (int)x , (int)z , d1 ) )
            {
                return GameDefine.INVALID_ID;
            }

            W3TerrainManager.instance.setBuildingPath( (int)x , (int)z , d1 );
        }

        float gy = W3TerrainManager.instance.getSmallNode( (int)x , (int)z ).ym;

        string name1 = "Prefabs\\" + d.file;

        GameObject obj = (GameObject)GameObject.Instantiate( (GameObject)Resources.Load( name1 ) , unitObjTrans );

        GameObject hpObj = W3HPBar.Create();
        W3HPBar hpbar = hpObj.GetComponent<W3HPBar>();
        hpbar.init( 100 , 100 );

        W3Unit unit = obj.GetComponent<W3Unit>();
        unit.unitData = d1;
        unit.unitUI = d;
        unit.unitBalance = d2;
        unit.unitWeapons = d3;
        unit.splatData = d4;
        unit.startAnimation = true;

        unit.baseData.typeID = unitid;

        unit.baseData.hp = 100;
        unit.baseData.hpMax = 100;

        unit.collision = (byte)( d2.collision / 16 );
        unit.setHPTrans( hpObj.transform );
        unit.hpBar = hpbar;

        hpbar.init( unit.baseData.hp , unit.baseData.hpMax );

        if ( d2.collision % 16 != 0 )
        {
            unit.collision++;
        }
        if ( d2.collision % 32 == 0 )
        {
            unit.collision++;
        }
        if ( unit.collision == 1 )
        {
            unit.collision = 2;
        }

        int baseID = W3BaseManager.instance.addData( unit );

        unit.setPos( new Vector3( -x * GameDefine.TERRAIN_SIZE_PER , gy , -z * GameDefine.TERRAIN_SIZE_PER ) );
        unit.setAngles( new Vector3( 0.0f , a , 0.0f ) );
        unit.setuberSplatPos( -x , -z );

        unit.updateFogUV();

        if ( d1.isBuilding )
        {
            unit.updateUberSplat();
        }

        W3UnitMeshColor[] mc = unit.GetComponentsInChildren<W3UnitMeshColor>();
        for ( int i = 0 ; i < mc.Length ; i++ )
        {
            mc[ i ].type = pid;
            mc[ i ].UpdateColor( mc[ i ].type );
        }

        W3PlayerManager.instance.addUnit( pid , unit );

        return baseID;
    }


    public int createUnitA( int pid , string unitid , float x , float z , float a , bool corpse )
    {
        return createUnitA( pid , GameDefine.UnitId( unitid ) , x , z , a , corpse );
    }

    public void selectUnits( int minX , int minZ , int maxX , int maxZ )
    {
        clearSelection();

        for ( int i = minX ; i < maxX ; i++ )
        {
            for ( int j = minZ ; j < maxZ ; j++ )
            {
                int unitID = W3PathFinder.instance.getUnitID( i , j );

                if ( unitID >  0 )
                {
                    W3Unit unit = W3BaseManager.instance.getUnit( unitID );

                    if ( unit )
                    {
                        if ( unit.isLocalPlayer() )
                        {
                            if ( selectUnitList.Count > 0 )
                            {
                                if ( unit.isBuilding )
                                {
                                    if ( unit.isBuilding && selectUnitList[ 0 ].isBuilding )
                                    {
                                        unit.selection( true );
                                        selectUnitList.Add( unit );
                                    }
                                }
                                else
                                {
                                    if ( !selectUnitList[ 0 ].isBuilding )
                                    {
                                        unit.selection( true );
                                        selectUnitList.Add( unit );
                                    }
                                }
                            }
                            else
                            {
                                unit.selection( true );
                                selectUnitList.Add( unit );

//                                W3UnitUI.instance.updateUI( unit.baseData.typeID );
                            }
                        }
                        else
                        {
                            if ( selectUnitList.Count == 0 )
                            {
                                unit.selection( true );
                                selectUnitList.Add( unit );
                                return;
                            }
                        }
                        //Debug.Log( unit.baseID );
                    }
                }
            }
        }
    }


}

