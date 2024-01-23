using UnityEngine;
using System.Collections.Generic;

public class W3Trigger
{
    public int id;
    public int eventID;

    public bool enabled;
    public bool waitOnSleeps;

    public int evalCount;
    public int execCount;

    public BJCode code;


    // EVENT_GAME_VARIABLE_LIMIT
    public string varName;
    public int opCode;
    public float limitVal;

    // EVENT_GAME_STATE_LIMIT
    public int state;

    // EVENT_GAME_TIMER_EXPIRED
    public W3Timer timer;

    // EVENT_DIALOG_CLICK
    public int dialogID;

    // EVENT_DIALOG_BUTTON_CLICK
    public int buttonID;


    // EVENT_GAME_VICTORY
    // EVENT_GAME_END_LEVEL
    // EVENT_GAME_SHOW_SKILL
    // EVENT_GAME_BUILD_SUBMENU
    // EVENT_GAME_SAVE
    // EVENT_GAME_LOADED 
    // EVENT_GAME_TOURNAMENT_FINISH_SOON
    // EVENT_GAME_TOURNAMENT_FINISH_NOW
    public string saveBasicFileName;
    public float finishSoonTimeRemaining;
    public int finishNowRule;
    public int finishNowPlayer;
    public int[] scores;

    // EVENT_GAME_ENTER_REGION
    // EVENT_GAME_LEAVE_REGION
    public BJRegion region;
    public BJBoolExpr filter;
    public List<int> enteringUnit;
    public List<int> leavingUnit;

    // EVENT_GAME_TRACKABLE_HIT
    // EVENT_GAME_TRACKABLE_TRACK
    public int trackableID;


    public int playerID;

    // EVENT_PLAYER_HERO_LEVEL
    // EVENT_UNIT_HERO_LEVEL
    public int levelingUnitID;

    // EVENT_PLAYER_HERO_SKILL
    // EVENT_UNIT_HERO_SKILL
    public int learningUnitID;
    public int learnedSkill;
    public int learnedSkillLevel;

    // EVENT_PLAYER_HERO_REVIVABLE
    public int revivableUnitID;

    // EVENT_PLAYER_HERO_REVIVE_START
    // EVENT_PLAYER_HERO_REVIVE_CANCEL
    // EVENT_PLAYER_HERO_REVIVE_FINISH
    // EVENT_UNIT_HERO_REVIVE_START
    // EVENT_UNIT_HERO_REVIVE_CANCEL
    // EVENT_UNIT_HERO_REVIVE_FINISH
    public int revivingUnitID;

    // EVENT_PLAYER_UNIT_ATTACKED
    public int attackerID;

    // EVENT_PLAYER_UNIT_RESCUED
    public int rescuerID;

    // EVENT_PLAYER_UNIT_DEATH
    public int dyingUnitID;

    public int killingUnitID;

    // EVENT_PLAYER_UNIT_DECAY
    public int decayingUnitID;

    // EVENT_PLAYER_UNIT_CONSTRUCT_START
    public int constructingStructureID;

    // EVENT_PLAYER_UNIT_CONSTRUCT_FINISH
    // EVENT_PLAYER_UNIT_CONSTRUCT_CANCEL
    public int cancelledStructureID;
    public int constructedStructureID;

    // EVENT_PLAYER_UNIT_RESEARCH_START
    // EVENT_PLAYER_UNIT_RESEARCH_CANCEL
    // EVENT_PLAYER_UNIT_RESEARCH_FINISH
    public int researchingUnitID;
    public int researched;

    // EVENT_PLAYER_UNIT_TRAIN_START
    // EVENT_PLAYER_UNIT_TRAIN_CANCEL
    public int trainedUnitType;

    // EVENT_PLAYER_UNIT_TRAIN_FINISH
    public int trainedUnitID;

    // EVENT_PLAYER_UNIT_DETECTED
    public int detectedUnitID;

    // EVENT_PLAYER_UNIT_SUMMONED
    public int summoningUnitID;

    public int summonedUnitID;

    // EVENT_PLAYER_UNIT_LOADED
    public int transportUnitID;
    public int loadedUnitID;

    // EVENT_PLAYER_UNIT_SELL
    public int sellingUnitID;
    public int soldUnitID;
    public int buyingUnitID;

    public int soldItemID;

    // EVENT_PLAYER_UNIT_CHANGE_OWNER
    public int changingUnitID;
    public int changingUnitPrevOwnerID;

    // EVENT_PLAYER_UNIT_DROP_ITEM
    // EVENT_PLAYER_UNIT_PICKUP_ITEM
    // EVENT_PLAYER_UNIT_USE_ITEM
    public int manipulatingUnitID;
    public int manipulatedItemID;

    // EVENT_PLAYER_UNIT_ISSUED_ORDER
    public int orderedUnitID;
    public int issuedOrderID;

    // EVENT_PLAYER_UNIT_ISSUED_POINT_ORDER
    public float orderPointX;
    public float orderPointY;

    // EVENT_PLAYER_UNIT_ISSUED_TARGET_ORDER
    public int orderTargetID;
    public int orderTargetDestructableID;
    public int orderTargetItemID;
    public int orderTargetUnitID;

    // EVENT_UNIT_SPELL_CHANNEL
    // EVENT_UNIT_SPELL_CAST
    // EVENT_UNIT_SPELL_EFFECT
    // EVENT_UNIT_SPELL_FINISH
    // EVENT_UNIT_SPELL_ENDCAST
    // EVENT_PLAYER_UNIT_SPELL_CHANNEL
    // EVENT_PLAYER_UNIT_SPELL_CAST
    // EVENT_PLAYER_UNIT_SPELL_EFFECT
    // EVENT_PLAYER_UNIT_SPELL_FINISH
    // EVENT_PLAYER_UNIT_SPELL_ENDCAST
    public int spellAbilityUnitID;
    public int spellAbilityID;
    public int spellAbility;

    public int allianceType;


    public int playerState;

    public string chatMessageToDetect;
    public bool exactMatchOnly;

    // EVENT_WIDGET_DEATH
    public int widgetID;


    public int triggerUnitID;

    // EVENT_UNIT_STATE_LIMIT
    public int unitID;
    public int unitState;

    // EVENT_UNIT_DAMAGED
    public int damage;

    // EVENT_UNIT_DETECTED 
    public int detectingPlayer;

    // EVENT_UNIT_ACQUIRED_TARGET
    // EVENT_UNIT_TARGET_IN_RANGE
    public int targetUnitID;


    public float range;

    public List< BJBoolExpr > conditions;
    public List< BJCode > actions;

    public float timeOut;

    public int sound;
    public float soundOffset;

    public bool evaluate;

}


public class W3TriggerManager : SingletonMono<W3TriggerManager>
{
    public int eventID;


    public int filterUnit;
    public int enumUnit;

    public int filterDestructable;
    public int enumDestructable;

    public int filterItem;
    public int enumItem;

    public int filterPlayer;
    public int enumPlayer;

    public int lastTriggerID;
    public W3Trigger lastTrigger;

    public int triggerID = 0;
    public List<W3Trigger> triggers = new List<W3Trigger>();


    public int createTrigger()
    {
        triggerID++;

        W3Trigger t = new W3Trigger();
        t.id = triggerID;

        return t.id;
    }

    public void destroyTrigger( int id )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers.RemoveAt( i );
                return;
            }
        }
    }

    public void resetTrigger( int id )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].evalCount = 0;
                triggers[ i ].execCount = 0;
                return;
            }
        }
    }

    public void enableTrigger( int id , bool b )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].enabled = b;
                return;
            }
        }
    }

    public bool isTriggerEnabled( int id )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                return triggers[ i ].enabled;
            }
        }

        return false;
    }

    public void triggerWaitOnSleeps( int id , bool b )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].waitOnSleeps = b;
                return;
            }
        }
    }

    public bool isTriggerWaitOnSleeps( int id )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                return triggers[ i ].waitOnSleeps;
            }
        }

        return false;
    }

    public int getTriggerEvalCount( int id )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                return triggers[ i ].evalCount;
            }
        }

        return 0;
    }

    public int getTriggerExecCount( int id )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                return triggers[ i ].execCount;
            }
        }

        return 0;
    }


    public int triggerRegisterVariableEvent( int id , string varName , int opCode , float limitVal )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].varName = varName;
                triggers[ i ].opCode = opCode;
                triggers[ i ].limitVal = limitVal;
                triggers[ i ].eventID = GameDefine.EVENT_GAME_VARIABLE_LIMIT.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterTimerEvent( int id , float timeOut , bool periodic )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].timer = new W3Timer();
                triggers[ i ].timer.timeout = timeOut;
                triggers[ i ].timer.periodic = periodic;
                triggers[ i ].eventID = GameDefine.EVENT_GAME_TIMER_EXPIRED.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterTimerExpireEvent( int id , int tid )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].timer = W3TimerManager.instance.getTimer( tid );
                triggers[ i ].eventID = GameDefine.EVENT_GAME_TIMER_EXPIRED.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterGameStateEvent( int id , int state , int opCode , float limitVal )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].state = state;
                triggers[ i ].opCode = opCode;
                triggers[ i ].limitVal = limitVal;
                triggers[ i ].eventID = GameDefine.EVENT_GAME_STATE_LIMIT.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterDialogEvent( int id , int did )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].dialogID = did;
                triggers[ i ].eventID = GameDefine.EVENT_DIALOG_CLICK.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterDialogButtonEvent( int id , int bid )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].buttonID = bid;
                triggers[ i ].eventID = GameDefine.EVENT_DIALOG_BUTTON_CLICK.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterGameEvent( int id , int eid )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].eventID = eid;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterEnterRegion( int id , BJRegion region , BJBoolExpr filter )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].region = region;
                triggers[ i ].filter = filter;
                triggers[ i ].eventID = GameDefine.EVENT_GAME_ENTER_REGION.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterLeaveRegion( int id , BJRegion region , BJBoolExpr filter )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].region = region;
                triggers[ i ].filter = filter;
                triggers[ i ].eventID = GameDefine.EVENT_GAME_LEAVE_REGION.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterTrackableHitEvent( int id , int tid )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].trackableID = tid;
                triggers[ i ].eventID = GameDefine.EVENT_GAME_TRACKABLE_HIT.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }


    public int triggerRegisterTrackableTrackEvent( int id , int tid )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].trackableID = tid;
                triggers[ i ].eventID = GameDefine.EVENT_GAME_TRACKABLE_TRACK.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }


    public int triggerRegisterPlayerEvent( int id , int pid , int eid )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].playerID = pid;
                triggers[ i ].eventID = eid;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterPlayerUnitEvent( int id , int pid , int uid , BJBoolExpr filter )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].playerID = pid;
                triggers[ i ].eventID = uid;
                triggers[ i ].filter = filter;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterPlayerAllianceChange( int id , int pid , int at )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].playerID = pid;
                triggers[ i ].allianceType = at;
                triggers[ i ].eventID = GameDefine.EVENT_PLAYER_ALLIANCE_CHANGED.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterPlayerStateEvent( int id , int pid , int state , int opCode , float limitVal )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].playerState = state;
                triggers[ i ].opCode = opCode;
                triggers[ i ].limitVal = limitVal;
                triggers[ i ].eventID = GameDefine.EVENT_PLAYER_STATE_LIMIT.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterPlayerChatEvent( int id , int pid , string chatMessageToDetect , bool exactMatchOnly )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].playerID = pid;
                triggers[ i ].chatMessageToDetect = chatMessageToDetect;
                triggers[ i ].exactMatchOnly = exactMatchOnly;
                triggers[ i ].eventID = GameDefine.EVENT_PLAYER_CHAT.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterDeathEvent( int id , int wid )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].widgetID = wid;
                triggers[ i ].eventID = GameDefine.EVENT_WIDGET_DEATH.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterUnitStateEvent( int id , int uid , int state , int opCode , float limitVal )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].unitID = uid;
                triggers[ i ].unitState = state;
                triggers[ i ].opCode = opCode;
                triggers[ i ].limitVal = limitVal;
                triggers[ i ].eventID = GameDefine.EVENT_UNIT_STATE_LIMIT.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterUnitEvent( int id , int uid , int eid )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].unitID = uid;
                triggers[ i ].eventID = GameDefine.EVENT_UNIT_STATE_LIMIT.id;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterFilterUnitEvent( int id , int uid , int eid , BJBoolExpr filter )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].unitID = uid;
                triggers[ i ].eventID = GameDefine.EVENT_UNIT_STATE_LIMIT.id;
                triggers[ i ].filter = filter;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public int triggerRegisterUnitInRange( int id , int uid , float range , BJBoolExpr filter )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].unitID = uid;
                triggers[ i ].range = range;
                triggers[ i ].eventID = GameDefine.EVENT_UNIT_TARGET_IN_RANGE.id;
                triggers[ i ].filter = filter;

                return triggers[ i ].eventID;
            }
        }

        return GameDefine.INVALID_ID;
    }

    public void triggerAddCondition( int id , BJBoolExpr condition )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].conditions.Add( condition );

                return;
            }
        }
    }

    public void triggerRemoveCondition( int id , BJBoolExpr condition )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                for ( int j = 0 ; j < triggers[ i ].conditions.Count ; j++ )
                {
                    if ( triggers[ i ].conditions[ j ] == condition )
                    {
                        triggers[ i ].conditions.RemoveAt( j );

                        return;
                    }
                }
            }
        }
    }

    public void triggerClearConditions( int id )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                for ( int j = 0 ; j < triggers[ i ].conditions.Count ; j++ )
                {
                    triggers[ i ].conditions.Clear();

                    return;
                }
            }
        }
    }

    public void triggerAddAction( int id , BJCode action )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                triggers[ i ].actions.Add( action );

                return;
            }
        }
    }

    public void triggerRemoveAction( int id , BJCode action )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                for ( int j = 0 ; j < triggers[ i ].actions.Count ; j++ )
                {
                    if ( triggers[ i ].actions[ j ] == action )
                    {
                        triggers[ i ].actions.RemoveAt( j );

                        return;
                    }
                }
            }
        }
    }

    public void triggerClearActions( int id )
    {
        for ( int i = 0 ; i < triggers.Count ; i++ )
        {
            if ( triggers[ i ].id == id )
            {
                for ( int j = 0 ; j < triggers[ i ].actions.Count ; j++ )
                {
                    triggers[ i ].actions.Clear();

                    return;
                }
            }
        }
    }

    






}



