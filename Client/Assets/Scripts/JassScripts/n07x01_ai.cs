// Generated by .


	public partial class GameDefine 
	{

		public class n07x01_ai
		{
		//==================================================================================================
		//  $Id: n07x01.ai,v 1.27 2003/05/05 11:28:09 bfitch Exp $
		//==================================================================================================
			public BJPlayer  targ = PlayerEx(6);
			public void main(  )
			{
				// Original JassCode
				CampaignAI(HOUSE,null);
				SetPeonsRepair(true);
				SetDefendPlayer(true);
				SetReplacements(6,6,3);
				SetBuildUnitEx( 1,1,1, BLOOD_PEASANT );
				SetBuildUnitEx( 1,1,1, TOWN_HALL );
				SetBuildUnitEx( 1,1,1, HUMAN_ALTAR );
				SetBuildUnitEx( 4,4,4, WATCH_TOWER );
				SetBuildUnitEx( 1,1,1, LUMBER_MILL );
				SetBuildUnitEx( 4,4,4, GUARD_TOWER );
				SetBuildUnitEx( 1,1,0, ARCANE_VAULT );
				SetBuildUnitEx( 1,1,0, BLACKSMITH );
				SetBuildUnitEx( 1,1,0, KEEP );
				SetBuildUnitEx( 1,1,0, ARCANE_SANCTUM );
				SetBuildUnitEx( 1,1,0, CASTLE );
				SetBuildUnitEx( 1,1,0, AVIARY );
				SetBuildUnitEx( 8,8,8, BLOOD_PEASANT );
				CampaignDefenderEx( 1,1,1, KAEL );
				CampaignDefenderEx( 1,1,1, SPELL_BREAKER );
				CampaignDefenderEx( 2,2,2, HIGH_SWORDMAN );
				CampaignDefenderEx( 1,1,1, SORCERESS );
				CampaignDefenderEx( 1,1,1, PRIEST );
				CampaignDefenderEx( 2,2,2, HIGH_ARCHER );
				SetBuildUpgrEx( 1,1,1, UPG_SORCERY );
				SetBuildUpgrEx( 1,1,1, UPG_PRAYING );
				SetBuildUpgrEx( 1,1,1, UPG_MASONRY );
				SetBuildUpgrEx( 1,1,1, UPG_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_LEATHER );
				SetBuildUpgrEx( 1,1,1, UPG_RANGED );
				SetBuildUpgrEx( 1,1,1, UPG_MELEE );
				SetBuildUpgrEx( 2,2,1, UPG_PRAYING );
				SetBuildUpgrEx( 2,2,1, UPG_MASONRY );
				SetBuildUpgrEx( 2,2,2, UPG_ARMOR );
				SetBuildUpgrEx( 2,2,2, UPG_LEATHER );
				SetBuildUpgrEx( 2,2,2, UPG_RANGED );
				SetBuildUpgrEx( 2,2,2, UPG_MELEE );
				SetBuildUpgrEx( 3,3,2, UPG_MASONRY );
				SetBuildUpgrEx( 3,3,2, UPG_ARMOR );
				SetBuildUpgrEx( 3,3,2, UPG_LEATHER );
				SetBuildUpgrEx( 3,3,2, UPG_RANGED );
				SetBuildUpgrEx( 3,3,2, UPG_MELEE );
				SleepForever();
			}

		} // class n07x01_ai 

	}

