// Generated by .


	public partial class GameDefine 
	{

		public class u07_orange_ai
		{
		//============================================================================
		//  Undead 07 -- Orange player (Grey)  -- AI Script
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
				SetBuildUnitEx( 0,0,1, TOWN_HALL );
				SetBuildUnitEx( 3,3,3, PEASANT );
				SetBuildUnitEx( 0,0,2, BARRACKS );
				SetBuildUnitEx( 0,0,1, LUMBER_MILL );
				SetBuildUnitEx( 0,0,1, HUMAN_ALTAR );
				SetBuildUnitEx( 3,3,3, PEASANT );
				SetBuildUnitEx( 0,0,1, BLACKSMITH );
				SetBuildUnitEx( 4,4,4, PEASANT );
				SetBuildUnitEx( 0,0,1, KEEP );
				SetBuildUnitEx( 0,0,1, WORKSHOP );
				SetBuildUnitEx( 0,0,1, SANCTUM );
				SetBuildUnitEx( 0,0,1, CASTLE );
				SetBuildUnitEx( 0,0,3, AVIARY );
				CampaignDefenderEx( 1,1,2, GRYPHON );
				CampaignDefenderEx( 1,1,1, PRIEST );
				SetBuildUpgrEx( 1,1,1, UPG_MELEE );
				SetBuildUpgrEx( 1,1,1, UPG_RANGED );
				SetBuildUpgrEx( 0,0,1, UPG_PRAYING );
				SetBuildUpgrEx( 0,0,1, UPG_SORCERY );
				SetBuildUpgrEx( 0,0,1, UPG_MASONRY );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,6, GRYPHON );
				SuicideOnPlayerEx(M5,M5,0,user);
				SetBuildUpgrEx( 1,1,1, UPG_LEATHER );
				SetBuildUpgrEx( 1,1,1, UPG_ARMOR );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,4, KNIGHT );
				CampaignAttackerEx( 3,3,4, GRYPHON );
				CampaignAttackerEx( 0,0,2, MORTAR );
				SuicideOnPlayerEx(M10,M10,M7,user);
				SetBuildUpgrEx( 1,1,1, UPG_HAMMERS );
				SetBuildUpgrEx( 1,1,2, UPG_PRAYING );
				SetBuildUpgrEx( 0,0,1, UPG_BREEDING );
				SetBuildUpgrEx( 0,0,1, UPG_SENTINEL );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 0,0,2, KNIGHT );
				CampaignAttackerEx( 2,2,3, SORCERESS );
				CampaignAttackerEx( 4,4,6, GRYPHON );
				SuicideOnPlayerEx(M10,M10,M8,user);
				SetBuildUpgrEx( 0,0,2, UPG_MELEE );
				SetBuildUpgrEx( 0,0,2, UPG_RANGED );
				SetBuildUpgrEx( 1,1,2, UPG_SORCERY );
				SetBuildUpgrEx( 1,1,2, UPG_MASONRY );
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,3, KNIGHT );
				CampaignAttackerEx( 1,1,2, PRIEST );
				CampaignAttackerEx( 1,1,1, SORCERESS );
				CampaignAttackerEx( 2,2,5, GRYPHON );
				CampaignAttackerEx( 0,0,2, MORTAR );
				SuicideOnPlayerEx(M10,M10,M7,user);
				SetBuildUpgrEx( 0,0,2, UPG_LEATHER );
				SetBuildUpgrEx( 0,0,2, UPG_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_BREEDING );
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,3, KNIGHT );
				CampaignAttackerEx( 0,0,1, PRIEST );
				CampaignAttackerEx( 2,2,2, SORCERESS );
				CampaignAttackerEx( 5,5,7, GRYPHON );
				CampaignAttackerEx( 0,0,1, MORTAR );
				SuicideOnPlayerEx(M10,M10,M8,user);
				SetBuildUpgrEx( 2,2,2, UPG_SORCERY );
				SetBuildUpgrEx( 1,1,1, UPG_HAMMERS );
				while( true )
				{
					//*** WAVE 6 ***
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,6, KNIGHT );
					CampaignAttackerEx( 1,1,2, PRIEST );
					CampaignAttackerEx( 1,1,2, SORCERESS );
					CampaignAttackerEx( 2,2,3, GRYPHON );
					CampaignAttackerEx( 0,0,2, MORTAR );
					SuicideOnPlayerEx(M10,M10,M7,user);
					//*** WAVE 7 ***
					InitAssaultGroup();
					CampaignAttackerEx( 1,1,2, KNIGHT );
					CampaignAttackerEx( 1,1,2, PRIEST );
					CampaignAttackerEx( 1,1,2, SORCERESS );
					CampaignAttackerEx( 6,6,8, GRYPHON );
					CampaignAttackerEx( 0,0,2, MORTAR );
					SuicideOnPlayerEx(M10,M10,M8,user);
				}
			}

		} // class u07_orange_ai 

	}

