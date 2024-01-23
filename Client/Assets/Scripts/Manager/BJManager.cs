using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class GameDefine
{
    public static void initJass()
    {
        PI = 3.14159;
        E = 2.71828;
        CELLWIDTH = 128.0;
        CLIFFHEIGHT = 128.0;
        UNIT_FACING = 270.0;
        RADTODEG = 180.0 / PI;
        DEGTORAD = PI / 180.0;
        TEXT_DELAY_QUEST = 20.00;
        TEXT_DELAY_QUESTUPDATE = 20.00;
        TEXT_DELAY_QUESTDONE = 20.00;
        TEXT_DELAY_QUESTFAILED = 20.00;
        TEXT_DELAY_QUESTREQUIREMENT = 20.00;
        TEXT_DELAY_MISSIONFAILED = 20.00;
        TEXT_DELAY_ALWAYSHINT = 12.00;
        TEXT_DELAY_HINT = 12.00;
        TEXT_DELAY_SECRET = 10.00;
        TEXT_DELAY_UNITACQUIRED = 15.00;
        TEXT_DELAY_UNITAVAILABLE = 10.00;
        TEXT_DELAY_ITEMACQUIRED = 10.00;
        TEXT_DELAY_WARNING = 12.00;
        QUEUE_DELAY_QUEST = 5.00;
        QUEUE_DELAY_HINT = 5.00;
        QUEUE_DELAY_SECRET = 3.00;
        HANDICAP_EASY = 60.00;
        GAME_STARTED_THRESHOLD = 0.01;
        WAIT_FOR_COND_MIN_INTERVAL = 0.10;
        POLLED_WAIT_INTERVAL = 0.10;
        POLLED_WAIT_SKIP_THRESHOLD = 2.00;
        // Game constants
        MAX_INVENTORY = 6;
        MAX_PLAYERS = 12;
        PLAYER_NEUTRAL_VICTIM = 13;
        PLAYER_NEUTRAL_EXTRA = 14;
        MAX_PLAYER_SLOTS = 16;
        MAX_SKELETONS = 25;
        MAX_STOCK_ITEM_SLOTS = 11;
        MAX_STOCK_UNIT_SLOTS = 11;
        MAX_ITEM_LEVEL = 10;
        // Ideally these would be looked up from Units/MiscData.txt,
        // but there is currently no script functionality exposed to do that
        TOD_DAWN = 6.00;
        TOD_DUSK = 18.00;
        // Melee game settings:
        //   - Starting Time of Day (TOD)
        //   - Starting Gold
        //   - Starting Lumber
        //   - Starting Hero Tokens (free heroes)
        //   - Max heroes allowed per player
        //   - Max heroes allowed per hero type
        //   - Distance from start loc to search for nearby mines
        //
        MELEE_STARTING_TOD = 8.00;
        MELEE_STARTING_GOLD_V0 = 750;
        MELEE_STARTING_GOLD_V1 = 500;
        MELEE_STARTING_LUMBER_V0 = 200;
        MELEE_STARTING_LUMBER_V1 = 150;
        MELEE_STARTING_HERO_TOKENS = 1;
        MELEE_HERO_LIMIT = 3;
        MELEE_HERO_TYPE_LIMIT = 1;
        MELEE_MINE_SEARCH_RADIUS = 2000;
        MELEE_CLEAR_UNITS_RADIUS = 1500;
        MELEE_CRIPPLE_TIMEOUT = 120.00;
        MELEE_CRIPPLE_MSG_DURATION = 20.00;
        MELEE_MAX_TWINKED_HEROES_V0 = 3;
        MELEE_MAX_TWINKED_HEROES_V1 = 1;
        // Delay between a creep's death and the time it may drop an item.
        CREEP_ITEM_DELAY = 0.50;
        // Timing settings for Marketplace inventories.
        STOCK_RESTOCK_INITIAL_DELAY = 120;
        STOCK_RESTOCK_INTERVAL = 30;
        STOCK_MAX_ITERATIONS = 20;
        // Max events registered by a single "dest dies in region" event.
        MAX_DEST_IN_REGION_EVENTS = 64;
        // Camera settings
        CAMERA_MIN_FARZ = 100;
        CAMERA_DEFAULT_DISTANCE = 1650;
        CAMERA_DEFAULT_FARZ = 5000;
        CAMERA_DEFAULT_AOA = 304;
        CAMERA_DEFAULT_FOV = 70;
        CAMERA_DEFAULT_ROLL = 0;
        CAMERA_DEFAULT_ROTATION = 90;
        // Rescue
        RESCUE_PING_TIME = 2.00;
        // Transmission behavior settings
        NOTHING_SOUND_DURATION = 5.00;
        TRANSMISSION_PING_TIME = 1.00;
        TRANSMISSION_IND_RED = 255;
        TRANSMISSION_IND_BLUE = 255;
        TRANSMISSION_IND_GREEN = 255;
        TRANSMISSION_IND_ALPHA = 255;
        TRANSMISSION_PORT_HANGTIME = 1.50;
        // Cinematic mode settings
        CINEMODE_INTERFACEFADE = 0.50;
        CINEMODE_GAMESPEED = MAP_SPEED_NORMAL;
        // Cinematic mode volume levels
        CINEMODE_VOLUME_UNITMOVEMENT = 0.40;
        CINEMODE_VOLUME_UNITSOUNDS = 0.00;
        CINEMODE_VOLUME_COMBAT = 0.40;
        CINEMODE_VOLUME_SPELLS = 0.40;
        CINEMODE_VOLUME_UI = 0.00;
        CINEMODE_VOLUME_MUSIC = 0.55;
        CINEMODE_VOLUME_AMBIENTSOUNDS = 1.00;
        CINEMODE_VOLUME_FIRE = 0.60;
        // Speech mode volume levels
        SPEECH_VOLUME_UNITMOVEMENT = 0.25;
        SPEECH_VOLUME_UNITSOUNDS = 0.00;
        SPEECH_VOLUME_COMBAT = 0.25;
        SPEECH_VOLUME_SPELLS = 0.25;
        SPEECH_VOLUME_UI = 0.00;
        SPEECH_VOLUME_MUSIC = 0.55;
        SPEECH_VOLUME_AMBIENTSOUNDS = 1.00;
        SPEECH_VOLUME_FIRE = 0.60;
        // Smart pan settings
        SMARTPAN_TRESHOLD_PAN = 500;
        SMARTPAN_TRESHOLD_SNAP = 3500;
        // QueuedTriggerExecute settings
        MAX_QUEUED_TRIGGERS = 100;
        QUEUED_TRIGGER_TIMEOUT = 180.00;
        // Campaign indexing constants
        CAMPAIGN_INDEX_T = 0;
        CAMPAIGN_INDEX_H = 1;
        CAMPAIGN_INDEX_U = 2;
        CAMPAIGN_INDEX_O = 3;
        CAMPAIGN_INDEX_N = 4;
        CAMPAIGN_INDEX_XN = 5;
        CAMPAIGN_INDEX_XH = 6;
        CAMPAIGN_INDEX_XU = 7;
        CAMPAIGN_INDEX_XO = 8;
        // Campaign offset constants (for mission indexing)
        CAMPAIGN_OFFSET_T = 0;
        CAMPAIGN_OFFSET_H = 1;
        CAMPAIGN_OFFSET_U = 2;
        CAMPAIGN_OFFSET_O = 3;
        CAMPAIGN_OFFSET_N = 4;
        CAMPAIGN_OFFSET_XN = 0;
        CAMPAIGN_OFFSET_XH = 1;
        CAMPAIGN_OFFSET_XU = 2;
        CAMPAIGN_OFFSET_XO = 3;
        // Mission indexing constants
        // Tutorial
        MISSION_INDEX_T00 = CAMPAIGN_OFFSET_T * 1000 + 0;
        MISSION_INDEX_T01 = CAMPAIGN_OFFSET_T * 1000 + 1;
        // Human
        MISSION_INDEX_H00 = CAMPAIGN_OFFSET_H * 1000 + 0;
        MISSION_INDEX_H01 = CAMPAIGN_OFFSET_H * 1000 + 1;
        MISSION_INDEX_H02 = CAMPAIGN_OFFSET_H * 1000 + 2;
        MISSION_INDEX_H03 = CAMPAIGN_OFFSET_H * 1000 + 3;
        MISSION_INDEX_H04 = CAMPAIGN_OFFSET_H * 1000 + 4;
        MISSION_INDEX_H05 = CAMPAIGN_OFFSET_H * 1000 + 5;
        MISSION_INDEX_H06 = CAMPAIGN_OFFSET_H * 1000 + 6;
        MISSION_INDEX_H07 = CAMPAIGN_OFFSET_H * 1000 + 7;
        MISSION_INDEX_H08 = CAMPAIGN_OFFSET_H * 1000 + 8;
        MISSION_INDEX_H09 = CAMPAIGN_OFFSET_H * 1000 + 9;
        MISSION_INDEX_H10 = CAMPAIGN_OFFSET_H * 1000 + 10;
        MISSION_INDEX_H11 = CAMPAIGN_OFFSET_H * 1000 + 11;
        // Undead
        MISSION_INDEX_U00 = CAMPAIGN_OFFSET_U * 1000 + 0;
        MISSION_INDEX_U01 = CAMPAIGN_OFFSET_U * 1000 + 1;
        MISSION_INDEX_U02 = CAMPAIGN_OFFSET_U * 1000 + 2;
        MISSION_INDEX_U03 = CAMPAIGN_OFFSET_U * 1000 + 3;
        MISSION_INDEX_U05 = CAMPAIGN_OFFSET_U * 1000 + 4;
        MISSION_INDEX_U07 = CAMPAIGN_OFFSET_U * 1000 + 5;
        MISSION_INDEX_U08 = CAMPAIGN_OFFSET_U * 1000 + 6;
        MISSION_INDEX_U09 = CAMPAIGN_OFFSET_U * 1000 + 7;
        MISSION_INDEX_U10 = CAMPAIGN_OFFSET_U * 1000 + 8;
        MISSION_INDEX_U11 = CAMPAIGN_OFFSET_U * 1000 + 9;
        // Orc
        MISSION_INDEX_O00 = CAMPAIGN_OFFSET_O * 1000 + 0;
        MISSION_INDEX_O01 = CAMPAIGN_OFFSET_O * 1000 + 1;
        MISSION_INDEX_O02 = CAMPAIGN_OFFSET_O * 1000 + 2;
        MISSION_INDEX_O03 = CAMPAIGN_OFFSET_O * 1000 + 3;
        MISSION_INDEX_O04 = CAMPAIGN_OFFSET_O * 1000 + 4;
        MISSION_INDEX_O05 = CAMPAIGN_OFFSET_O * 1000 + 5;
        MISSION_INDEX_O06 = CAMPAIGN_OFFSET_O * 1000 + 6;
        MISSION_INDEX_O07 = CAMPAIGN_OFFSET_O * 1000 + 7;
        MISSION_INDEX_O08 = CAMPAIGN_OFFSET_O * 1000 + 8;
        MISSION_INDEX_O09 = CAMPAIGN_OFFSET_O * 1000 + 9;
        MISSION_INDEX_O10 = CAMPAIGN_OFFSET_O * 1000 + 10;
        // Night Elf
        MISSION_INDEX_N00 = CAMPAIGN_OFFSET_N * 1000 + 0;
        MISSION_INDEX_N01 = CAMPAIGN_OFFSET_N * 1000 + 1;
        MISSION_INDEX_N02 = CAMPAIGN_OFFSET_N * 1000 + 2;
        MISSION_INDEX_N03 = CAMPAIGN_OFFSET_N * 1000 + 3;
        MISSION_INDEX_N04 = CAMPAIGN_OFFSET_N * 1000 + 4;
        MISSION_INDEX_N05 = CAMPAIGN_OFFSET_N * 1000 + 5;
        MISSION_INDEX_N06 = CAMPAIGN_OFFSET_N * 1000 + 6;
        MISSION_INDEX_N07 = CAMPAIGN_OFFSET_N * 1000 + 7;
        MISSION_INDEX_N08 = CAMPAIGN_OFFSET_N * 1000 + 8;
        MISSION_INDEX_N09 = CAMPAIGN_OFFSET_N * 1000 + 9;
        // Expansion Night Elf
        MISSION_INDEX_XN00 = CAMPAIGN_OFFSET_XN * 1000 + 0;
        MISSION_INDEX_XN01 = CAMPAIGN_OFFSET_XN * 1000 + 1;
        MISSION_INDEX_XN02 = CAMPAIGN_OFFSET_XN * 1000 + 2;
        MISSION_INDEX_XN03 = CAMPAIGN_OFFSET_XN * 1000 + 3;
        MISSION_INDEX_XN04 = CAMPAIGN_OFFSET_XN * 1000 + 4;
        MISSION_INDEX_XN05 = CAMPAIGN_OFFSET_XN * 1000 + 5;
        MISSION_INDEX_XN06 = CAMPAIGN_OFFSET_XN * 1000 + 6;
        MISSION_INDEX_XN07 = CAMPAIGN_OFFSET_XN * 1000 + 7;
        MISSION_INDEX_XN08 = CAMPAIGN_OFFSET_XN * 1000 + 8;
        MISSION_INDEX_XN09 = CAMPAIGN_OFFSET_XN * 1000 + 9;
        MISSION_INDEX_XN10 = CAMPAIGN_OFFSET_XN * 1000 + 10;
        // Expansion Human
        MISSION_INDEX_XH00 = CAMPAIGN_OFFSET_XH * 1000 + 0;
        MISSION_INDEX_XH01 = CAMPAIGN_OFFSET_XH * 1000 + 1;
        MISSION_INDEX_XH02 = CAMPAIGN_OFFSET_XH * 1000 + 2;
        MISSION_INDEX_XH03 = CAMPAIGN_OFFSET_XH * 1000 + 3;
        MISSION_INDEX_XH04 = CAMPAIGN_OFFSET_XH * 1000 + 4;
        MISSION_INDEX_XH05 = CAMPAIGN_OFFSET_XH * 1000 + 5;
        MISSION_INDEX_XH06 = CAMPAIGN_OFFSET_XH * 1000 + 6;
        MISSION_INDEX_XH07 = CAMPAIGN_OFFSET_XH * 1000 + 7;
        MISSION_INDEX_XH08 = CAMPAIGN_OFFSET_XH * 1000 + 8;
        MISSION_INDEX_XH09 = CAMPAIGN_OFFSET_XH * 1000 + 9;
        // Expansion Undead
        MISSION_INDEX_XU00 = CAMPAIGN_OFFSET_XU * 1000 + 0;
        MISSION_INDEX_XU01 = CAMPAIGN_OFFSET_XU * 1000 + 1;
        MISSION_INDEX_XU02 = CAMPAIGN_OFFSET_XU * 1000 + 2;
        MISSION_INDEX_XU03 = CAMPAIGN_OFFSET_XU * 1000 + 3;
        MISSION_INDEX_XU04 = CAMPAIGN_OFFSET_XU * 1000 + 4;
        MISSION_INDEX_XU05 = CAMPAIGN_OFFSET_XU * 1000 + 5;
        MISSION_INDEX_XU06 = CAMPAIGN_OFFSET_XU * 1000 + 6;
        MISSION_INDEX_XU07 = CAMPAIGN_OFFSET_XU * 1000 + 7;
        MISSION_INDEX_XU08 = CAMPAIGN_OFFSET_XU * 1000 + 8;
        MISSION_INDEX_XU09 = CAMPAIGN_OFFSET_XU * 1000 + 9;
        MISSION_INDEX_XU10 = CAMPAIGN_OFFSET_XU * 1000 + 10;
        MISSION_INDEX_XU11 = CAMPAIGN_OFFSET_XU * 1000 + 11;
        MISSION_INDEX_XU12 = CAMPAIGN_OFFSET_XU * 1000 + 12;
        MISSION_INDEX_XU13 = CAMPAIGN_OFFSET_XU * 1000 + 13;
        // Expansion Orc
        MISSION_INDEX_XO00 = CAMPAIGN_OFFSET_XO * 1000 + 0;
        // Cinematic indexing constants
        CINEMATICINDEX_TOP = 0;
        CINEMATICINDEX_HOP = 1;
        CINEMATICINDEX_HED = 2;
        CINEMATICINDEX_OOP = 3;
        CINEMATICINDEX_OED = 4;
        CINEMATICINDEX_UOP = 5;
        CINEMATICINDEX_UED = 6;
        CINEMATICINDEX_NOP = 7;
        CINEMATICINDEX_NED = 8;
        CINEMATICINDEX_XOP = 9;
        CINEMATICINDEX_XED = 10;
        // Alliance settings
        ALLIANCE_UNALLIED = 0;
        ALLIANCE_UNALLIED_VISION = 1;
        ALLIANCE_ALLIED = 2;
        ALLIANCE_ALLIED_VISION = 3;
        ALLIANCE_ALLIED_UNITS = 4;
        ALLIANCE_ALLIED_ADVUNITS = 5;
        ALLIANCE_NEUTRAL = 6;
        ALLIANCE_NEUTRAL_VISION = 7;
        // Keyboard Event Types
        KEYEVENTTYPE_DEPRESS = 0;
        KEYEVENTTYPE_RELEASE = 1;
        // Keyboard Event Keys
        KEYEVENTKEY_LEFT = 0;
        KEYEVENTKEY_RIGHT = 1;
        KEYEVENTKEY_DOWN = 2;
        KEYEVENTKEY_UP = 3;
        // Transmission timing methods
        TIMETYPE_ADD = 0;
        TIMETYPE_SET = 1;
        TIMETYPE_SUB = 2;
        // Camera bounds adjustment methods
        CAMERABOUNDS_ADJUST_ADD = 0;
        CAMERABOUNDS_ADJUST_SUB = 1;
        // Quest creation states
        QUESTTYPE_REQ_DISCOVERED = 0;
        QUESTTYPE_REQ_UNDISCOVERED = 1;
        QUESTTYPE_OPT_DISCOVERED = 2;
        QUESTTYPE_OPT_UNDISCOVERED = 3;
        // Quest message types
        QUESTMESSAGE_DISCOVERED = 0;
        QUESTMESSAGE_UPDATED = 1;
        QUESTMESSAGE_COMPLETED = 2;
        QUESTMESSAGE_FAILED = 3;
        QUESTMESSAGE_REQUIREMENT = 4;
        QUESTMESSAGE_MISSIONFAILED = 5;
        QUESTMESSAGE_ALWAYSHINT = 6;
        QUESTMESSAGE_HINT = 7;
        QUESTMESSAGE_SECRET = 8;
        QUESTMESSAGE_UNITACQUIRED = 9;
        QUESTMESSAGE_UNITAVAILABLE = 10;
        QUESTMESSAGE_ITEMACQUIRED = 11;
        QUESTMESSAGE_WARNING = 12;
        // Leaderboard sorting methods
        SORTTYPE_SORTBYVALUE = 0;
        SORTTYPE_SORTBYPLAYER = 1;
        SORTTYPE_SORTBYLABEL = 2;
        // Cinematic fade filter methods
        CINEFADETYPE_FADEIN = 0;
        CINEFADETYPE_FADEOUT = 1;
        CINEFADETYPE_FADEOUTIN = 2;
        // Buff removal methods
        REMOVEBUFFS_POSITIVE = 0;
        REMOVEBUFFS_NEGATIVE = 1;
        REMOVEBUFFS_ALL = 2;
        REMOVEBUFFS_NONTLIFE = 3;
        // Buff properties - polarity
        BUFF_POLARITY_POSITIVE = 0;
        BUFF_POLARITY_NEGATIVE = 1;
        BUFF_POLARITY_EITHER = 2;
        // Buff properties - resist type
        BUFF_RESIST_MAGIC = 0;
        BUFF_RESIST_PHYSICAL = 1;
        BUFF_RESIST_EITHER = 2;
        BUFF_RESIST_BOTH = 3;
        // Hero stats
        HEROSTAT_STR = 0;
        HEROSTAT_AGI = 1;
        HEROSTAT_INT = 2;
        // Hero skill point modification methods
        MODIFYMETHOD_ADD = 0;
        MODIFYMETHOD_SUB = 1;
        MODIFYMETHOD_SET = 2;
        // Unit state adjustment methods (for replaced units)
        UNIT_STATE_METHOD_ABSOLUTE = 0;
        UNIT_STATE_METHOD_RELATIVE = 1;
        UNIT_STATE_METHOD_DEFAULTS = 2;
        UNIT_STATE_METHOD_MAXIMUM = 3;
        // Gate operations
        GATEOPERATION_CLOSE = 0;
        GATEOPERATION_OPEN = 1;
        GATEOPERATION_DESTROY = 2;
        // Game cache value types
        GAMECACHE_BOOLEAN = 0;
        GAMECACHE_INTEGER = 1;
        GAMECACHE_REAL = 2;
        GAMECACHE_UNIT = 3;
        GAMECACHE_STRING = 4;
        // Item status types
        ITEM_STATUS_HIDDEN = 0;
        ITEM_STATUS_OWNED = 1;
        ITEM_STATUS_INVULNERABLE = 2;
        ITEM_STATUS_POWERUP = 3;
        ITEM_STATUS_SELLABLE = 4;
        ITEM_STATUS_PAWNABLE = 5;
        // Itemcode status types
        ITEMCODE_STATUS_POWERUP = 0;
        ITEMCODE_STATUS_SELLABLE = 1;
        ITEMCODE_STATUS_PAWNABLE = 2;
        // Minimap ping styles
        MINIMAPPINGSTYLE_SIMPLE = 0;
        MINIMAPPINGSTYLE_FLASHY = 1;
        MINIMAPPINGSTYLE_ATTACK = 2;
        // Corpse creation settings
        CORPSE_MAX_DEATH_TIME = 8.00;
        // Corpse creation styles
        CORPSETYPE_FLESH = 0;
        CORPSETYPE_BONE = 1;
        // Elevator pathing-blocker destructable code
        ELEVATOR_BLOCKER_CODE = UnitId( "DTep" );
        ELEVATOR_CODE01 = UnitId( "DTrf" );
        ELEVATOR_CODE02 = UnitId( "DTrx" );
        // Elevator wall codes
        ELEVATOR_WALL_TYPE_ALL = 0;
        ELEVATOR_WALL_TYPE_EAST = 1;
        ELEVATOR_WALL_TYPE_NORTH = 2;
        ELEVATOR_WALL_TYPE_SOUTH = 3;
        ELEVATOR_WALL_TYPE_WEST = 4;
        //-----------------------------------------------------------------------
        // Variables
        //
        // Force predefs
        FORCE_ALL_PLAYERS = new BJForce();
        FORCE_PLAYER = new BJForce[ MAX_PLAYER_SLOTS ];
        MELEE_MAX_TWINKED_HEROES = 0;
        // Map area rects
        mapInitialPlayableArea = null;
        mapInitialCameraBounds = null;
        // Utility function vars
        forLoopAIndex = 0;
        forLoopBIndex = 0;
        forLoopAIndexEnd = 0;
        forLoopBIndexEnd = 0;
        slotControlReady = false;
        slotControlUsed = new bool[ MAX_PLAYERS ];
        slotControl = new BJMapControl[ MAX_PLAYERS ];
        // Game started detection vars
        gameStartedTimer = null;
        gameStarted = false;
        volumeGroupsTimer = CreateTimer();
        // Singleplayer check
        isSinglePlayer = false;
        // Day/Night Cycle vars
        dncSoundsDay = null;
        dncSoundsNight = null;
        dayAmbientSound = null;
        nightAmbientSound = null;
        dncSoundsDawn = null;
        dncSoundsDusk = null;
        dawnSound = null;
        duskSound = null;
        useDawnDuskSounds = true;
        dncIsDaytime = false;
        // Triggered sounds
        //sound              pingMinimapSound         = null
        rescueSound = null;
        questDiscoveredSound = null;
        questUpdatedSound = null;
        questCompletedSound = null;
        questFailedSound = null;
        questHintSound = null;
        questSecretSound = null;
        questItemAcquiredSound = null;
        questWarningSound = null;
        victoryDialogSound = null;
        defeatDialogSound = null;
        // Marketplace vars
        stockItemPurchased = null;
        stockUpdateTimer = null;
        stockAllowedPermanent = new bool[ MAX_ITEM_LEVEL + 1 ];
        stockAllowedCharged = new bool[ MAX_ITEM_LEVEL + 1 ];
        stockAllowedArtifact = new bool[ MAX_ITEM_LEVEL + 1 ];
        stockPickedItemLevel = 0;
        stockPickedItemType = null;
        // Melee vars
        meleeVisibilityTrained = null;
        meleeVisibilityIsDay = true;
        meleeGrantHeroItems = false;
        meleeNearestMineToLoc = null;
        meleeNearestMine = null;
        meleeNearestMineDist = 0.00;
        meleeGameOver = false;
        meleeDefeated = null;
        meleeVictoried = null;
        ghoul = null;
        crippledTimer = null;
        crippledTimerWindows = null;
        playerIsCrippled = null;
        playerIsExposed = null;
        finishSoonAllExposed = false;
        finishSoonTimerDialog = null;
        meleeTwinkedHeroes = null;
        // Rescue behavior vars
        rescueUnitBehavior = null;
        rescueChangeColorUnit = true;
        rescueChangeColorBldg = true;
        // Transmission vars
        cineSceneEndingTimer = null;
        cineSceneLastSound = null;
        cineSceneBeingSkipped = null;
        // Cinematic mode vars
        cineModePriorSpeed = MAP_SPEED_NORMAL;
        cineModePriorFogSetting = false;
        cineModePriorMaskSetting = false;
        cineModeAlreadyIn = false;
        cineModePriorDawnDusk = false;
        cineModeSavedSeed = 0;
        // Cinematic fade vars
        cineFadeFinishTimer = null;
        cineFadeContinueTimer = null;
        cineFadeContinueRed = 0;
        cineFadeContinueGreen = 0;
        cineFadeContinueBlue = 0;
        cineFadeContinueTrans = 0;
        cineFadeContinueDuration = 0;
        cineFadeContinueTex = "";
        // QueuedTriggerExecute vars
        queuedExecTotal = 0;
        queuedExecTriggers = new BJTrigger[ MAX_QUEUED_TRIGGERS ];
        queuedExecUseConds = new bool[ MAX_QUEUED_TRIGGERS ];
        queuedExecTimeoutTimer = CreateTimer();
        queuedExecTimeout = null;
        // Helper vars (for Filter and Enum funcs)
        destInRegionDiesCount = 0;
        destInRegionDiesTrig = null;
        groupCountUnits = 0;
        forceCountPlayers = 0;
        groupEnumTypeId = 0;
        groupEnumOwningPlayer = null;
        groupAddGroupDest = null;
        groupRemoveGroupDest = null;
        groupRandomConsidered = 0;
        groupRandomCurrentPick = null;
        groupLastCreatedDest = null;
        randomSubGroupGroup = null;
        randomSubGroupWant = 0;
        randomSubGroupTotal = 0;
        randomSubGroupChance = 0;
        destRandomConsidered = 0;
        destRandomCurrentPick = null;
        elevatorWallBlocker = null;
        elevatorNeighbor = null;
        itemRandomConsidered = 0;
        itemRandomCurrentPick = null;
        forceRandomConsidered = 0;
        forceRandomCurrentPick = null;
        makeUnitRescuableUnit = null;
        makeUnitRescuableFlag = true;
        pauseAllUnitsFlag = true;
        enumDestructableCenter = null;
        enumDestructableRadius = 0;
        setPlayerTargetColor = null;
        isUnitGroupDeadResult = true;
        isUnitGroupEmptyResult = true;
        isUnitGroupInRectResult = true;
        isUnitGroupInRectRect = null;
        changeLevelShowScores = false;
        changeLevelMapName = null;
        suspendDecayFleshGroup = CreateGroup();
        suspendDecayBoneGroup = CreateGroup();
        delayedSuspendDecayTimer = CreateTimer();
        delayedSuspendDecayTrig = null;
        livingPlayerUnitsTypeId = 0;
        lastDyingWidget = null;
        // Random distribution vars
        randDistCount = 0;
        randDistID = null;
        randDistChance = null;
        // Last X'd vars
        lastCreatedUnit = null;
        lastCreatedItem = null;
        lastRemovedItem = null;
        lastHauntedGoldMine = null;
        lastCreatedDestructable = null;
        lastCreatedGroup = CreateGroup();
        lastCreatedFogModifier = null;
        lastCreatedEffect = null;
        lastCreatedWeatherEffect = null;
        lastCreatedTerrainDeformation = null;
        lastCreatedQuest = null;
        lastCreatedQuestItem = null;
        lastCreatedDefeatCondition = null;
        lastStartedTimer = CreateTimer();
        lastCreatedTimerDialog = null;
        lastCreatedLeaderboard = null;
        lastPlayedSound = null;
        lastPlayedMusic = "";
        lastTransmissionDuration = 0;
        lastCreatedGameCache = null;
        lastLoadedUnit = null;
        lastCreatedButton = null;
        lastReplacedUnit = null;
        lastCreatedTextTag = null;
        // Filter function vars
        filterIssueHauntOrderAtLocBJ = null;
        filterEnumDestructablesInCircleBJ = null;
        filterGetUnitsInRectOfPlayer = null;
        filterGetUnitsOfTypeIdAll = null;
        filterGetUnitsOfPlayerAndTypeId = null;
        filterMeleeTrainedUnitIsHeroBJ = null;
        filterLivingPlayerUnitsOfTypeId = null;
        // Memory cleanup vars
        wantDestroyGroup = false;



    }

}
