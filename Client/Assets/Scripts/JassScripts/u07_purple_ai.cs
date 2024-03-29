// Generated by .


	public partial class GameDefine 
	{

		public class u07_purple_ai
		{
		//============================================================================
		//  Undead 07 -- Purple player (Light Blue) -- AI Script
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
				SetCaptainHome(ATTACK_CAPTAIN,-2924,-3633);
				SetCaptainHome(DEFENSE_CAPTAIN,-2184,-3257);
				SetBuildUnitEx( 0,0,1, TOWN_HALL );
				SetBuildUnitEx( 3,3,3, PEASANT );
				SetBuildUnitEx( 0,0,1, BARRACKS );
				SetBuildUnitEx( 0,0,1, LUMBER_MILL );
				SetBuildUnitEx( 0,0,1, HUMAN_ALTAR );
				SetBuildUnitEx( 0,0,1, BLACKSMITH );
				SetBuildUnitEx( 7,7,7, PEASANT );
				SetBuildUnitEx( 0,0,1, KEEP );
				SetBuildUnitEx( 0,0,1, WORKSHOP );
				SetBuildUnitEx( 0,0,1, SANCTUM );
				CampaignDefenderEx( 1,1,1, FOOTMAN );
				CampaignDefenderEx( 0,0,1, MORTAR );
				CampaignDefenderEx( 1,1,1, RIFLEMAN );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,6, FOOTMEN );
				CampaignAttackerEx( 1,1,2, RIFLEMAN );
				CampaignAttackerEx( 0,0,2, PRIEST );
				SuicideOnPlayerEx(M4,M4,M2,user);
				SetBuildUpgrEx( 0,0,1, UPG_MELEE );
				SetBuildUpgrEx( 0,0,1, UPG_RANGED );
				SetBuildUpgrEx( 0,0,1, UPG_PRAYING );
				SetBuildUpgrEx( 0,0,1, UPG_MASONRY );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,5, FOOTMEN );
				CampaignAttackerEx( 1,1,3, RIFLEMAN );
				CampaignAttackerEx( 0,0,2, MORTAR );
				CampaignAttackerEx( 1,1,2, PRIEST );
				SuicideOnPlayerEx(M8,M8,M7,user);
				SetBuildUpgrEx( 0,0,1, UPG_GUN_RANGE );
				SetBuildUpgrEx( 0,0,1, UPG_LEATHER );
				SetBuildUpgrEx( 0,0,1, UPG_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_PRAYING );
				SetBuildUpgrEx( 1,1,1, UPG_DEFEND );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 0,0,4, FOOTMEN );
				CampaignAttackerEx( 5,5,8, RIFLEMAN );
				CampaignAttackerEx( 1,1,2, MORTAR );
				CampaignAttackerEx( 1,1,2, PRIEST );
				SuicideOnPlayerEx(M6,M6,M6,user);
				SetBuildUpgrEx( 1,1,2, UPG_MELEE );
				SetBuildUpgrEx( 1,1,2, UPG_RANGED );
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 5,5,10, FOOTMEN );
				CampaignAttackerEx( 2,2,4, MORTAR );
				CampaignAttackerEx( 1,1,2, PRIEST );
				SuicideOnPlayerEx(M8,M8,M7,user);
				SetBuildUpgrEx( 1,1,2, UPG_LEATHER );
				SetBuildUpgrEx( 1,1,2, UPG_ARMOR );
				while( true )
				{
					//*** WAVE 5+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 1,1,3, FOOTMEN );
					CampaignAttackerEx( 5,5,7, RIFLEMAN );
					CampaignAttackerEx( 1,1,2, MORTAR );
					CampaignAttackerEx( 0,0,2, PRIEST );
					SuicideOnPlayerEx(M6,M6,M6,user);
					//*** WAVE 6+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 4,4,7, FOOTMEN );
					CampaignAttackerEx( 2,2,3, RIFLEMAN );
					CampaignAttackerEx( 2,2,3, PRIEST );
					SuicideOnPlayerEx(M8,M8,M7,user);
				}
			}

		} // class u07_purple_ai 

	}

