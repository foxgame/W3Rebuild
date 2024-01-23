
using System.Collections.Generic;

    partial class GameDefine
	{

		public static bool debug_fast_attacks = false;

        public static BJCodeGetString GetGlobalString = null;

    }

    public class BJManager
    {

    }

	public class BJLocation
	{
        public float x;
        public float y;
        public float z;
	}

	public class BJRect
	{
        public float minX;
        public float minY;

        public float maxX;
        public float maxY;
    }

	public class BJItemType
	{
        public int type;
	}

	public class BJTerrainDeformation
	{
        public int id;
	}

	public class BJTextTag
	{
        public int id;
	}

	public class BJMultiBoard
	{
        public int id;
	}

	public class BJMultiBoardItem
	{
        public int id;
	}

	public class BJAbility
	{
        public int id;
	}

	public class BJAIdifficulty
	{
        public int difficulty;
    }

	public class BJVersion
	{
        public int version;
    }

	public class BJTrigger : BJEvent
	{

	}

	public class BJCameraSetup
	{
        public int id;
    }

	public class BJCameraField
	{
        public int field;
    }

	public class BJPlayer
	{
        public int id;
	}

	public class BJUnit : BJWidget
	{
    }

	public class BJItem : BJWidget
	{

	}

	public class BJButton
	{
        public int id;
	}

	public class BJDialog
	{
        public int id;
	}

	public class BJAllianceType
	{
        public int type;
	}

	public class BJPlayerGameResult
	{
        public int result;
    }

	public class BJQuest
	{
        public int id;
	}

	public class BJQuestItem
	{
        public int id;
    }

	public class BJForce
	{
        public int force;
	}

	public class BJEvent
	{
        public int id;
	}

	public delegate bool BJBoolExpr();

	public class BJLimitOP
	{
        public int op;
	}

	public class BJRegion
	{
        public List< BJLocation > cellList;
        public List< BJRect > rectList;

    }

    public class BJWeatherEffect
	{
        public int effect;
	}

	public class BJSound
	{
        public int id;
	}

	public class BJVolumeGroup
	{
        public int id;
	}

	public class BJFogModifier
	{
        public int id;
	}

	public class BJfogState
	{
        public int state;
    }

	public class BJEffect
	{
        public int effect;
	}

	public class BJWidget
	{
        public int id;
	}

	public class BJGroup
	{
        public int id;
	}

	public class BJUnitState
	{
        public int state;
	}

	public class BJDestructAble : BJWidget
	{

	}

	public delegate void BJCode();
	public delegate int BJCodeInt();
    public delegate System.Object BJCodeGetString( string str );

    public class BJMapControl
	{
        public int control;
    }

	public class BJDefeatCondition
	{
        public int id;
	}

	public class BJTimer
	{
        public int id;
	}

	public class BJTimerDialog
	{
        public int id;
	}

	public class BJLeaderBoard
	{
        public int id;
	}

	public class BJPlayerColor
	{
        public int color;
	}

	public class BJBlendMode
	{
        public int mode;
    }

	public class BJGameCache
	{
        public int cache;
	}

	public class BJPlayerSlotState
	{
        public int state;
    }

	public class BJPlayerState
	{
        public int state;
	}


	public class BJRace
	{
        public int race;
	}

	public class BJRacePreference
	{
        public int racePreference;
    }

	public class BJIgameState
	{
        public int state;
    }

    public class BJFGameState : BJGameState
	{
	}

	public class BJGameEvent : BJEvent
	{
	}

	public class BJPlayerEvent : BJEvent
    {
    }

	public class BJWidgetEvent : BJEvent
    {
    }

	public class BJDialogEvent : BJEvent
    {
    }

    public class BJUnitEvent : BJEvent
    {
    }

    public class BJPlayerUnitEvent : BJEvent
    {
    }

    public class BJUnitType
	{
        public int type;
    }

	public class BJGameSpeed
	{
        public int speed;
    }

	public class BJPlacement
	{
        public int placement;
    }

	public class BJStartLocPrio
	{
        public int prio;
	}

	public class BJGameDifficulty
	{
        public int difficulty;
    }

	public class BJGameType
	{
        public int type;
	}

	public class BJMapFlag
	{
        public int flag;
    }

	public class BJMapVisibility
	{
        public int visibility;
    }

	public class BJMapSetting
	{
        public int setting;
    }

	public class BJMapDensity
	{
        public int density;
    }

	public class BJRarityControl
	{
        public int control;
    }

	public class BJTexMapFlags
	{
        public int flags;
    }

	public class BJEffectType
	{
        public int type;
    }

	public class BJEventID
	{
        public int id;
	}

	public class BJConditionFunc
	{

	}

	public class BJFilterFunc
	{

	}

	public class BJGameState
	{
        public int state;
    }

	public class BJTrackAble
	{
        public int id;
	}

	public class BJTriggerCondition
	{
        public BJBoolExpr condition;
    }

	public class BJUnitPool
	{
        public int pool;
	}

	public class BJItemPool
	{
        public int pool;
	}

	public class BJTriggerAction
	{
        public BJCode action;
    }


