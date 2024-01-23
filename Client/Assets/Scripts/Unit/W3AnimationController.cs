using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum W3AnimationType
{
	Stand = 0,
	Stand1,
	Stand2,
	Stand3,
	Stand4,
	Stand5,

	StandReady,
	StandReady2,
	StandReadyGold,
	StandReadyLumber,
	StandReadyAlternate,
	StandReadySwim,

	StandWork,
	StandWork2,
	StandWorkFifth,
	StandWorkGold,
	StandGold,

	StandAlternate,
	StandAlternate1,
	StandAlternate2,
	StandAlternate3,

	StandVictory,
	StandPortrait,
	StandLumber,
	StandCinematic,
	StandFleshFirst,
	StandFleshSecond,

	StandFirst,
	StandSecond,
	StandThird,
	StandFourth,
	StandFifth,

	StandWounded,

	StandWorkFirst,
	StandWorkSecond,
	StandWorkThird,
	StandWorkFourth,
	StandWorkLumber,
	StandWorkAlternate,

	StandWalkAlternate,
	StandWorkCompleteAlternate,

	StandDefend,

	StandHit,
	StandHitUpgradeFirst,

	StandChannel,
	StandChannel1,
	StandChannel2,
	StandChannel3,

	StandChannelLumber,

	StandItchHead,

	StandVar1,
	StandVar2,
	StandVar3,
	StandVar4,

	StandSwim,
	StandSwim2,

	StandWalk,

	XStand,
	OldStand,
	OldStand2,

	AlternateStand1,
	AlternateStand2,
	AlternateStand3,
	AlternateStand4,

	AlternateWalk,
	AlternateDeath,
	AlternateSpellThrow,
	AlternateSpellSlam,
	AlternateDissipate,

	StandTalkFirst,
	StandTalkSecond,
	StandTalkThird,
	StandTalkFourth,
	StandTalkGesture,

	StandStretch,
	StandMedium,
	StandLarge,

	StandUpgradeFirst,
	StandUpgradeSecond,

	StandWorkUpgradeFirst,
	StandWorkUpgradeSecond,
	StandUpgradeFirstSecond,
	StandAlternateUpgradeFirstSecond,
	StandBirthAlternateWorkUpgradeFirstSecond,
	StandWorkAlternateBirthUpgradeFirstSecond,

	StandAlternateSwim,


	Walk,
	Walk1,
	Walk2,
	Walk3,
	WalkFast,
	WalkDefend,
	WalkLumber,
	WalkGold,

	WalkChannel,

	WalkAlternate,
	WalkAlternate1,
	WalkAlternate2,
	WalkAlternate3,

	WalkChannelFast,

	WalkSwim,
	WalkSwim2,
	WalkSwimAlternate,

	SwimWalk,


	TempWalk,




	Attack,
	Attack1,
	Attack2,
	Attack3,
	AttackSpell,
	AttackSpell1,
	AttackSpell2,

	AttackSpellSlam,
	AttackSpellSwim,
	AttackSlam,
	AttackSlam1,
	AttackSlam2,
	AttackGold,
	AttackLumber,
	AttackUpgrade,
	AttackUpgradeSecond,

	AttackDefend,
	AttackReady,

	AttackOne,
	AttackTwo,

	AttackOneAlternate,
	AttackTwoAlternate,

	AttackMissle,
	AttackSlamOgreLordOnly,

	AttackWalkStandSpin,

	AttackAlternate,
	AttackAlternate2,
	AlternateReady,

	AlternateAttack1,
	AlternateAttack2,

	AttackSlamAlternate,
	AttackSpellAlternate,


	AttackUpgradeFirst,

	AttackUnarmed,
	AttackHVar1,
	AttackHVar2,

	Attack1HVar1,
	Attack1HVar2,
	Attack2HVar1,
	Attack2HVar2,

	AttackVar1,

	AttackSwim,
	AttackSwimSpell1,

	AttackRange,


	Death,
	Death1,
	Death2,
	Death3,
	DeathFire,
	DeathSpell,
	DeathSecond,
	DeathThird,

	DeathAlternate,
	DeathMedium,
	DeathLarge,

	DeathUpgradeFirst,
	DeathUpgradeSecond,

	DeathSwim,
	DeathAlternateUpgradeFirst,



	Portrait,
	Portrait1,
	Portrait2,
	Portrait3,
	Portrait4,
	PortraitTalk,
	PortraitTalk1,
	PortraitTalk2,
	PortraitTalk3,
	PortraitAlternate,
	PortraitAlternate1,
	PortraitAlternate2,
	PortraitAlternate3,

	PortraitTalkAlternate,
	PortraitTalkAlternate1,
	PortraitTalkAlternate2,

	PortraitUpgradeFirst,
	PortraitUpgradeSecond,

	PortraitTalkAlternateAlternateEx,
	PortraitTalkAlternateAlternateEx1,

	PortraitTalkRight,
	PortraitTalkLeft,

	PortraitAlternateAlternateEx,
	PortraitAlternateAlternateEx1,
	PortraitAlternateAlternateEx2,

	Attack2HLVar1,
	Attack2HLVar2,

	PortraitAlternateAlternateEx3,

	StandUpgrade,

	Spell,
	Spell1,
	Spell2,
	SpellSlam,
	SpellAttack,
	SpellAttack1,
	SpellAttackTow,

	SpellThrow,
	SpellThird,
	SpellDevour,
	SpellEntangle,
	SpellWail,
	SpellBlink,
	SpellPuke,
	SpellBerserk,
	SpellCallStorm,
	SpellOgreMagiOnly,
	SpellEatTree,
	SpellFix,
	SpellAlternate,
	SpellSwim,

	OldSpellBerserk,


	Birth,
	Birth1,
	Birth2,
	Birth3,

	BirthAlternate,
	BirthMedium,
	BirthLarge,
	BirthUpgrade,
	BirthUpgradeFirst,
	BirthUpgradeSecond,
	BirthSwim,

	BirthSecond,

	BirthUpgradeFirstSecondThird,
	StandUpgradeFirstReadyAttack,
	AttackStandReadyUpgradeSecond,
	StandUpgradeThirdAttackReady,
	PortraitUpgradeThird,


	Sleep,

	Dissipate,
	DissipateAlternate,
	DissipateSwim,


	Decay,
	DecayFlesh,
	DecayBone,
	DecayBones,
	DecayFleshAlternate,
	DecayAlternate,
	DecayAlternateBone,
	DecaySwim,


	Morph,
	MorphThird,
	MorphAlternate,

	MorphSwim,
	MorphAlternateSwim,

	AlternateTalk2,
	AlternateAttack,
	AttackAlternate1,



	MainMenuBirth,
	MainMenuDeath,
	MainMenuStand,

	RealmSelectionBirth,
	RealmSelectionStand,
	RealmSelectionDeath,

	SinglePlayerBirth,
	SinglePlayerStand,
	SinglePlayerDeath,
	SinglePlayerBirthAlternate,
	SinglePlayerDeathAlternate,
	SinglePlayerSkirmishBirth,
	SinglePlayerSkirmishStand,
	SinglePlayerSkirmishDeath,
	SinglePlayerSkirmishMorph,
	SinglePlayerSkirmishMorphAlternate,


	MainCancelPanelBirth,
	MainCancelPanelStand,
	MainCancelPanelDeath,


	BattlenetChatRoomBirth,
	BattlenetChatRoomStand,
	BattlenetChatRoomDeath,
	BattlenetUserlistMorph,
	BattlenetUserlistMorphAlternate,

	BattlenetWelcomeBirth,
	BattlenetWelcomeStand,
	BattlenetWelcomeDeath,

	BattlenetChannelBirth,
	BattlenetChannelStand,
	BattlenetChannelDeath,

	BattlenetProfileBirth,
	BattlenetProfileStand,
	BattlenetProfileDeath,

	BattlenetAMMBirth,
	BattlenetAMMStand,
	BattlenetAMMDeath,

	BattlenetCustomBirth,
	BattlenetCustomStand,
	BattlenetCustomDeath,

	BattlenetCustomCreateBirth,
	BattlenetCustomCreateStand,
	BattlenetCustomCreateDeath,

	BattlenetAdvancedOptionsMorph,
	BattlenetAdvancedOptionsMorphAlternate,

	BattlenetTeamChatBirth,
	BattlenetTeamChatStand,
	BattlenetTeamChatDeath,

	MultiplayerSubmenuMorph,
	MultiplayerSubmenuMorphAlternate,

	MultiplayerPreGameChatBirth,
	MultiplayerPreGameChatStand,
	MultiplayerPreGameChatDeath,

	OptionsBirth,
	OptionsStand,
	OptionsDeath,
	OptionsMorph,

	OptionsStandAlternate,
	OptionsMorphAlternate,



	Base,

	Hold,
	HoldItem,
	HoldWalkSwim,

	Unused,
	Nothing,
	Normal,
	Select,

	TargetSelect,
	InvalidTarget,

	ScrollLeft,
	ScrollRight,
	ScrollUp,
	ScrollDown,
	ScrollUpLeft,
	ScrollUpRight,
	ScrollDownLeft,
	ScrollDownRight,


	Blank,
	Blank1,
	Blank2,
	Blank3,

	Bla,
	Bla1,
	BlaSpell,

	CinStand,
	CinStandLook,

	ScoreScreen5Button,
	ScoreScreen4Button,

	TeamColor01Stand,
	TeamColor02Stand,
	TeamColor03Stand,
	TeamColor04Stand,
	TeamColor05Stand,
	TeamColor06Stand,
	TeamColor07Stand,
	TeamColor08Stand,
	TeamColor09Stand,
	TeamColor10Stand,
	TeamColor11Stand,


	HumanInterlude01,
	HumanXInterlude01,
	HumanXInterlude02,
	HumanXInterlude03,
	HumanXInterlude04,
	HumanXInterlude05,
	HumanXInterlude06,
	HumanXInterlude07,

	Undead01,
	Undead02,
	Undead03,
	Undead04,
	Undead04a,
	Undead04b,
	Undead05,
	Undead06,
	Undead07,

	UndeadX01,
	UndeadX02,
	UndeadX03,
	UndeadX04,
	UndeadX05,
	UndeadX06,
	UndeadX07,
	UndeadX07A,
	UndeadX07B,
	UndeadX07C,
	UndeadX08,

	Human01,
	Human02,
	Human03,
	Human04,
	Human05,
	Human06,
	Human07,
	Human08,
	Human09,

	HumanX01,
	HumanX02,
	HumanX03,
	HumanX04,
	HumanX05,
	HumanX06,
	HumanX07,

	Orc01,
	Orc02,
	Orc03,
	Orc04,
	Orc05,
	Orc06,
	Orc07,
	Orc08,

	OrcX01,
	OrcX02,
	OrcX03,
	OrcX04,
	OrcX05,
	OrcX06,
	OrcX07,
	OrcX08,

	OrcInterlude01,

	OrcXAct01Mission01,
	OrcXAct01Mission02,
	OrcXAct01Mission03,
	OrcXAct01Mission04,
	OrcXAct01Mission05,
	OrcXAct01Mission06,
	OrcXAct01Mission07,

	OrcXAct02Mission01,
	OrcXAct02Mission02,
	OrcXAct02Mission03,
	OrcXAct02Mission04,
	OrcXAct02Mission05,
	OrcXAct02Mission06,
	OrcXAct02Mission07,

	OrcXQuest01,
	OrcXQuest02,
	OrcXQuest03,

	NightElf01,
	NightElf02,
	NightElf03,
	NightElf04,
	NightElf05,
	NightElf06,
	NightElf07,
	NightElf08,

	NightElfX01,
	NightElfX02,
	NightElfX03,
	NightElfX04,
	NightElfX05,
	NightElfX06,
	NightElfX07,
	NightElfX08,

	UndeadXInterlude01,
	UndeadXInterlude02,

	NightElfInterlude01,

	NightElfXInterlude01,
	NightElfXInterlude02,
	NightElfXInterlude03,
	NightElfXInterlude04,
	NightElfXInterlude05,
	NightElfXInterlude06,
	NightElfXInterlude07,

	Small,
	Medium,
	Large,
	Larger,


	Friendly,
	Enemy,
	Neutral,

	FriendlyBig,
	EnemyBig,
	NeutralBig,

	Tutorial01,
	Tutorial02,

	UndeadInterlude01,
	UndeadInterlude02,

	UndeadXInterlude03,

	Found,

	AllyPingBirth,
	AllyPingStand,
	AllyPingDeath,

	StandSwimAlternate,
	MorphSwimAlternate,

	PortraitFirst,
	PortraitSecond,
	PortraitThird,
	PortraitFourth,
	PortraitFifth,

	PortraitFirstUpgrade,
	PortraitSecondUpgrade,
	PortraitThirdUpgrade,
	PortraitFourthUpgrade,
	PortraitFifthUpgrade,

	StandFirstAttackReady,
	StandSecondAttackReady,
	StandThirdAttackReady,
	StandFourthAttackReady,
	StandFifthAttackReady,

	StandFirstAttackReadyUpgrade,
	StandSecondAttackReadyUpgrade,
	StandThirdAttackReadyUpgrade,
	StandFourthAttackReadyUpgrade,
	StandFifthAttackReadyUpgrade,

	BrithSecond,
	BirthThird,

	MorphDefend,


	WalkUpgrade,
	DeathUpgrade,
	DecayUpgrade,
	SpellUpgrade,

	SpellMorph,
	SpellAttackTwo,
	SpellChannel,

	Tab1,
	Tab2,
	Tab3,
	Tab4,
	Tab5,
	Tab6,
	Tab7,

	BrokenNotes,
	Target,
	Cast,

	PortraitTalkAlternateNotUsed1,

	SpellSlamSwim,
	SpellChainLightning,

	CombatWound,
	CombatCritical,

	AttackArmedVar1,
	AttackArmedVar2,

	OnHoldStandSpellDefendAlternate,

	StandReadyAttack,
	StandWorkSwim,

	PortriatTalk,

	TeamColor00Stand,
	DeathExplode,

	ProtraitTalkLeft,
	ProtraitTalkRight,

	count,

    none
}


public class W3AnimationController : MonoBehaviour 
{
	private Animation ani;
	private W3AnimationVisiblity av;
	private W3AnimationAlpha aa;


	public delegate void AnimationHandler();
	public AnimationHandler animationHandler;

	W3AnimationType playingAnimation = W3AnimationType.none;

	Dictionary< W3AnimationType , AnimationHandler > handlerDic = new Dictionary< W3AnimationType , AnimationHandler >();

	List< W3AnimationVisiblityT > avList = new List< W3AnimationVisiblityT >();

	public W3AnimationType[] animations;
    //	public W3AnimationType defaultType;

    Renderer[] renderer1 = null;
    SkinnedMeshRenderer[] skinRenderer = null;
	MeshFilter[] Filter = null;

	public delegate void updateCallaback( W3AnimationType type , bool enabled , float time );
	public updateCallaback callback = null;

	float speed = 1.0f;
    float lastTime1 = 0.0f;

    public bool alpha = false;

    void initHandler()
    {
        handlerDic[ W3AnimationType.Stand ] = handlerStand;
        handlerDic[ W3AnimationType.Stand1 ] = handlerStand1;
        handlerDic[ W3AnimationType.Stand2 ] = handlerStand2;
        handlerDic[ W3AnimationType.Stand3 ] = handlerNormal;
        handlerDic[ W3AnimationType.Stand4 ] = handlerNormal;
        handlerDic[ W3AnimationType.Walk ] = handlerNormal;
        handlerDic[ W3AnimationType.Death ] = handlerNormal;
        handlerDic[ W3AnimationType.AttackDefend ] = handlerNormal;
        handlerDic[ W3AnimationType.DecayFlesh ] = handlerNormal;
        handlerDic[ W3AnimationType.DecayBone ] = handlerNormal;
    }

    void Awake()
	{
		ani = GetComponentInChildren< Animation >();
		av = GetComponent< W3AnimationVisiblity >();
		aa = GetComponent< W3AnimationAlpha >();

        initHandler();

        skinRenderer = transform.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        renderer1 = transform.gameObject.GetComponentsInChildren<Renderer>();
		Filter = transform.gameObject.GetComponentsInChildren< MeshFilter >();
        
//		Play( W3AnimationType.StandDefend );
	}

    public void addHandler( W3AnimationType t , AnimationHandler h )
    {
        handlerDic[ t ] = h;
    }

    public void handlerNormal()
	{
	}

	public void handlerStand()
	{
//		if( ani[ playingAnimation.ToString() ].normalizedTime > 0.95f )
//		{
//		}
	}

	public void handlerStand1()
	{
//		if( ani[ playingAnimation.ToString() ].normalizedTime > 0.95f )
//		{
//		}
	}

	public void handlerStand2()
	{

	}

    public void enable( bool b )
    {
        ani.enabled = b;
        enabled = b;
    }

	public void pause( bool b )
	{
		if ( playingAnimation != W3AnimationType.none )
		{
			ani[ playingAnimation.ToString() ].speed = b ? 0.0f : speed;
		}
	}

	public void randomPlay( W3AnimationType t )
	{
		play( t );

		ani[ playingAnimation.ToString() ].normalizedTime = Random.Range( 0.0f , 1.0f );
	}

    public void stop()
    {
        ani.Stop();
    }

    public void noPlay( W3AnimationType t )
    {
        if ( ani == null )
        {
            return;
        }

        avList.Clear();

        for ( int i = 0 ; i < renderer1.Length ; i++ )
        {
            renderer1[ i ].enabled = true;
        }

        if ( av != null )
        {
            for ( int j = 0 ; j < av.frames.Count ; j++ )
            {
                if ( av.frames[ j ].type == t )
                {
                    for ( int i = 0 ; i < av.frames[ j ].f.Count ; i++ )
                    {
                        bool bin = false;

                        for ( int k = i + 1 ; k < av.frames[ j ].f.Count ; k++ )
                        {
                            if ( av.frames[ j ].f[ k ].name == av.frames[ j ].f[ i ].name )
                            {
                                bin = true;
                            }
                        }

                        if ( !bin )
                        {
                            av.frames[ j ].f[ i ].remove = true;
                        }

                        avList.Add( av.frames[ j ].f[ i ] );
                    }

                    checkVisible( 0.0f );

                    return;
                }
            }
        }

    }

    public void play( W3AnimationType t , bool onece = false )
	{
        if ( ani == null )
        {
            return;
        }

        if ( playingAnimation == t &&
            !onece )
        {
            return;
        }

		playingAnimation = t;

		ani.Play( t.ToString() );

		avList.Clear();

		for ( int i = 0 ; i < renderer1.Length ; i++ )
		{
            renderer1[ i ].enabled = true;
		}

		if ( handlerDic.ContainsKey( t ) )
		{
			animationHandler = handlerDic[ t ];
		}

		if ( av != null )
		{
			for ( int j = 0 ; j < av.frames.Count ; j++ )
			{
				if ( av.frames[ j ].type == t )
				{
					for ( int i = 0 ; i < av.frames[ j ].f.Count ; i++ )
					{
                        bool bin = false;

                        for ( int k = i + 1 ; k < av.frames[ j ].f.Count ; k++ )
                        {
                            if ( av.frames[ j ].f[ k ].name == av.frames[ j ].f[ i ].name )
                            {
                                bin = true;
                            }
                        }

                        if ( !bin )
                        {
                            av.frames[ j ].f[ i ].remove = true;
                        }

						avList.Add( av.frames[ j ].f[ i ] );
					}

					checkVisible( 0.0f );

					return;
				}
			}
		}

	}

	void checkVisible( float time )
	{
		if ( avList.Count == 0 )
		{
			return;
		}

		for ( int i = 0 ; i < renderer1.Length ; i++ )
		{
			for (int j = 0 ; j < avList.Count ; j++ ) 
			{
				W3AnimationVisiblityT v1 = avList[ j ];

				if ( renderer1[ i ].gameObject.name == v1.name )
				{
					if ( time >= v1.time )
					{
                        renderer1[ i ].enabled = ( v1.v == 0.0f ? false : true );

                        if ( avList[ j ].remove )
    						avList.RemoveAt( j );
					}

					break;
				}
			}
		}
	}

	void checkAlpha( float time )
	{
		if ( aa == null )
		{
			return;
		}

		if ( time - lastTime1 < 0.05f )
		{
			return;
		}

		lastTime1 = time;

		time = time - (float)System.Math.Truncate( time );

		for ( int i = 0 ; i < aa.frames.Count ; i++ )
		{
			float firstAlhpa = 0.0f;
			float lastAlhpa = 0.0f;
			float firstTime = 0;
			float lastTime = 0;

			for ( int j = 0; j < aa.frames[ i ].f.Count ; j++ )
			{
				if ( time <= aa.frames[ i ].f[ j ].time )
				{
					lastAlhpa = aa.frames[ i ].f[ j ].alpha;
					lastTime = aa.frames[ i ].f[ j ].time;
					break;
				}
				else
				{
					firstAlhpa = aa.frames[ i ].f[ j ].alpha;
					firstTime = aa.frames[ i ].f[ j ].time;
				}
			}

			if ( lastTime - firstTime == 0 )
			{
				return;
			}

			float alpha = ( lastAlhpa - firstAlhpa ) / ( lastTime - firstTime ) * ( time - firstTime ) + firstAlhpa;

//			Debug.Log( "alpha" + lastAlhpa + " " + firstAlhpa + " " + lastTime + " " + firstTime + " " + time + " " + alpha );

            for ( int k = 0 ; k < renderer1.Length ; k++ )
            {
                if ( renderer1[ k ].gameObject.name == aa.frames[ i ].material )
                {
                    //renderer1[ k ].sharedMaterial.color = new Color( 1.0f , 1.0f , 1.0f , alpha );
                }
            }

            // 			for ( int k = 0 ; k < Filter.Length ; k++ )
            // 			{
            //                 if ( Filter[ k ].gameObject.name == aa.frames[ i ].material )
            //                 {
            //                     Color[] colors = new Color[ Filter[ k ].sharedMesh.vertices.Length ];
            //                     for ( int l = 0 ; l < colors.Length ; l++ )
            //                     {
            //                         colors[ l ] = new Color( 1.0f , 1.0f , 1.0f , alpha );
            //                     }
            // 
            //                     Filter[ k ].sharedMesh.r.colors = colors;
            //                 }
            // 			}
        }





	}

	void FixedUpdate()
	{
		#if UNITY_EDITOR
//		AnimationState s = ani[ playingAnimation.ToString() ];
//		Debug.Log( playingAnimation + " " + ani[ playingAnimation.ToString() ].normalizedTime );
		#endif

		if ( playingAnimation != W3AnimationType.none )
		{
			if ( callback != null )
			{
				callback( playingAnimation , ani[ playingAnimation.ToString() ].enabled , ani[ playingAnimation.ToString() ].normalizedTime );
			}

            if ( alpha )
            {
                checkAlpha( ani[ playingAnimation.ToString() ].normalizedTime );
            }

            checkVisible( ani[ playingAnimation.ToString() ].normalizedTime );

			if ( animationHandler != null )
			{
				animationHandler();
			}
		}
	}

}

