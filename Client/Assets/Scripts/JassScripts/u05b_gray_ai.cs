// Generated by .


	public partial class GameDefine 
	{

		public class u05b_gray_ai
		{
		//============================================================================
		//  Undead 05b -- Gray player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(3);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				do_debug_cheats = true;
				CampaignAI(HOUSE,null);
				SetReplacements(0,1,3);
				SetBuildUnitEx( 0,0,1, TOWN_HALL );
				SetBuildUnitEx( 3,3,3, PEASANT );
				SetBuildUnitEx( 0,0,1, BARRACKS );
				SetBuildUnitEx( 0,0,1, LUMBER_MILL );
				SetBuildUnitEx( 0,0,13, HOUSE );
				SetBuildUnitEx( 0,0,1, HUMAN_ALTAR );
				SetBuildUnitEx( 3,3,3, PEASANT );
				SetBuildUnitEx( 0,0,1, BLACKSMITH );
				SetBuildUnitEx( 0,0,1, KEEP );
				SetBuildUnitEx( 0,0,1, WORKSHOP );
				SetBuildUnitEx( 4,4,4, PEASANT );
				SetBuildUnitEx( 0,0,1, WATCH_TOWER );
				SetBuildUnitEx( 0,0,3, GUARD_TOWER );
				SetBuildUnitEx( 0,0,1, CASTLE );
				CampaignDefenderEx( 1,2,4, FOOTMAN );
				CampaignDefenderEx( 0,1,1, MORTAR );
				CampaignDefenderEx( 1,1,3, RIFLEMAN );
				CampaignDefenderEx( 0,0,1, MURADIN );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,2,3, RIFLEMAN );
				SuicideOnPlayerEx(M5,M4,0,user);
				SetBuildUpgrEx( 0,0,1, UPG_MELEE );
				SetBuildUpgrEx( 0,0,1, UPG_RANGED );
				SetBuildUpgrEx( 0,0,1, UPG_MASONRY );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 0,1,1, MURADIN );
				CampaignAttackerEx( 2,2,3, RIFLEMAN );
				CampaignAttackerEx( 0,1,2, MORTAR );
				SuicideOnPlayerEx(M10,M8,M4,user);
				SetBuildUpgrEx( 0,0,1, UPG_GUN_RANGE );
				SetBuildUpgrEx( 0,0,1, UPG_LEATHER );
				SetBuildUpgrEx( 0,0,1, UPG_ARMOR );
				SetBuildUpgrEx( 0,1,1, UPG_DEFEND );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,4, FOOTMEN );
				CampaignAttackerEx( 3,5,8, RIFLEMAN );
				SuicideOnPlayerEx(M10,M8,M4,user);
				SetBuildUpgrEx( 0,1,2, UPG_MELEE );
				SetBuildUpgrEx( 0,1,2, UPG_RANGED );
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,1, MURADIN );
				CampaignAttackerEx( 0,2,4, MORTAR );
				SuicideOnPlayerEx(M10,M8,M4,user);
				SetBuildUpgrEx( 0,1,2, UPG_LEATHER );
				SetBuildUpgrEx( 0,1,2, UPG_ARMOR );
				while( true )
				{
					//*** WAVE 5+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 1,1,1, MURADIN );
					CampaignAttackerEx( 3,5,7, RIFLEMAN );
					CampaignAttackerEx( 1,1,2, MORTAR );
					SuicideOnPlayerEx(M10,M8,M4,user);
					//*** WAVE 6+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 1,2,3, RIFLEMAN );
					CampaignAttackerEx( 1,2,3, MORTAR );
					SuicideOnPlayerEx(M10,M8,M4,user);
				}
			}

		} // class u05b_gray_ai 

	}

