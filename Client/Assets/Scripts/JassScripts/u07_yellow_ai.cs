// Generated by .


	public partial class GameDefine 
	{

		public class u07_yellow_ai
		{
		//============================================================================
		//  Undead 07 -- Yellow player (Blue) -- AI Script
		//============================================================================
			public BJPlayer  user = Player(6);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				do_debug_cheats = true;
				CampaignAI(HOUSE,null);
				SetReplacements(2,2,4);
				do_campaign_farms = false;
				SetCaptainHome(ATTACK_CAPTAIN,2531,744);
				SetBuildUnitEx( 0,0,1, TOWN_HALL );
				SetBuildUnitEx( 3,3,3, PEASANT );
				SetBuildUnitEx( 0,0,2, BARRACKS );
				SetBuildUnitEx( 0,0,1, LUMBER_MILL );
				SetBuildUnitEx( 0,0,1, HUMAN_ALTAR );
				SetBuildUnitEx( 3,3,3, PEASANT );
				SetBuildUnitEx( 0,0,1, BLACKSMITH );
				SetBuildUnitEx( 7,7,7, PEASANT );
				SetBuildUnitEx( 0,0,1, KEEP );
				SetBuildUnitEx( 0,0,1, WORKSHOP );
				SetBuildUnitEx( 0,0,1, SANCTUM );
				SetBuildUnitEx( 0,0,1, CASTLE );
				CampaignDefenderEx( 1,1,1, KNIGHT );
				CampaignDefenderEx( 1,1,1, SORCERESS );
				CampaignDefenderEx( 0,0,1, RIFLEMAN );
				CampaignDefenderEx( 0,0,1, ARCHMAGE );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,4, KNIGHT );
				CampaignAttackerEx( 1,1,2, SORCERESS );
				CampaignAttackerEx( 1,1,4, RIFLEMAN );
				SuicideOnPlayerEx(M4,M4,M2,user);
				SetBuildUpgrEx( 1,1,1, UPG_MELEE );
				SetBuildUpgrEx( 1,1,1, UPG_RANGED );
				SetBuildUpgrEx( 0,0,1, UPG_PRAYING );
				SetBuildUpgrEx( 0,0,1, UPG_SORCERY );
				SetBuildUpgrEx( 0,0,1, UPG_MASONRY );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,4, KNIGHT );
				CampaignAttackerEx( 1,1,2, PRIEST );
				CampaignAttackerEx( 4,4,4, RIFLEMAN );
				CampaignAttackerEx( 0,0,2, MORTAR );
				SuicideOnPlayerEx(M9,M9,M6,user);
				SetBuildUpgrEx( 1,1,1, UPG_GUN_RANGE );
				SetBuildUpgrEx( 1,1,1, UPG_LEATHER );
				SetBuildUpgrEx( 1,1,1, UPG_ARMOR );
				SetBuildUpgrEx( 1,1,2, UPG_PRAYING );
				SetBuildUpgrEx( 0,0,1, UPG_BREEDING );
				SetBuildUpgrEx( 0,0,1, UPG_SENTINEL );
				SetBuildUpgrEx( 1,1,1, UPG_BOMBS );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,6, KNIGHT );
				CampaignAttackerEx( 2,2,3, SORCERESS );
				CampaignAttackerEx( 0,0,2, RIFLEMAN );
				SuicideOnPlayerEx(M9,M9,M7,user);
				SetBuildUpgrEx( 0,0,2, UPG_MELEE );
				SetBuildUpgrEx( 0,0,2, UPG_RANGED );
				SetBuildUpgrEx( 1,1,2, UPG_SORCERY );
				SetBuildUpgrEx( 1,1,2, UPG_MASONRY );
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 0,0,2, KNIGHT );
				CampaignAttackerEx( 2,2,1, PRIEST );
				CampaignAttackerEx( 2,2,2, SORCERESS );
				CampaignAttackerEx( 4,4,7, RIFLEMAN );
				CampaignAttackerEx( 1,1,2, MORTAR );
				SuicideOnPlayerEx(M9,M9,M6,user);
				SetBuildUpgrEx( 0,0,2, UPG_LEATHER );
				SetBuildUpgrEx( 0,0,2, UPG_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_BREEDING );
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,6, KNIGHT );
				CampaignAttackerEx( 1,1,2, PRIEST );
				CampaignAttackerEx( 1,1,2, SORCERESS );
				CampaignAttackerEx( 0,0,2, RIFLEMAN );
				CampaignAttackerEx( 1,1,1, MORTAR );
				SuicideOnPlayerEx(M9,M9,M7,user);
				SetBuildUpgrEx( 2,2,2, UPG_SORCERY );
				while( true )
				{
					//*** WAVE 6 ***
					InitAssaultGroup();
					CampaignAttackerEx( 1,1,4, KNIGHT );
					CampaignAttackerEx( 1,1,2, PRIEST );
					CampaignAttackerEx( 1,1,2, SORCERESS );
					CampaignAttackerEx( 4,4,6, RIFLEMAN );
					CampaignAttackerEx( 1,1,2, MORTAR );
					SuicideOnPlayerEx(M9,M9,M6,user);
					//*** WAVE 7 ***
					InitAssaultGroup();
					CampaignAttackerEx( 5,5,7, KNIGHT );
					CampaignAttackerEx( 1,1,2, PRIEST );
					CampaignAttackerEx( 1,1,2, SORCERESS );
					CampaignAttackerEx( 0,0,2, RIFLEMAN );
					CampaignAttackerEx( 1,1,2, MORTAR );
					SuicideOnPlayerEx(M9,M9,M7,user);
				}
			}

		} // class u07_yellow_ai 

	}

