// Generated by .


	public partial class GameDefine 
	{

		public class u05b_teal_ai
		{
		//============================================================================
		//  Undead 05b -- teal player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(3);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ELF_FARM,null);
				SetReplacements(0,1,3);
				SetBuildUnitEx( 0,0,1, TOWN_HALL );
				SetBuildUnitEx( 2,2,2, PEASANT );
				SetBuildUnitEx( 0,0,2, ELF_HIGH_BARRACKS );
				SetBuildUnitEx( 0,0,13, ELF_FARM );
				SetBuildUnitEx( 0,0,1, LUMBER_MILL );
				SetBuildUnitEx( 5,5,5, PEASANT );
				SetBuildUnitEx( 0,0,1, BLACKSMITH );
				SetBuildUnitEx( 0,0,1, HUMAN_ALTAR );
				SetBuildUnitEx( 0,0,1, KEEP );
				SetBuildUnitEx( 0,0,1, SANCTUM );
				SetBuildUnitEx( 0,0,1, CASTLE );
				CampaignDefenderEx( 1,1,3, HIGH_SWORDMAN );
				CampaignDefenderEx( 1,1,4, HIGH_ARCHER );
				CampaignDefenderEx( 0,0,1, PRIEST );
				CampaignDefenderEx( 0,0,1, SORCERESS );
				CampaignDefenderEx( 0,2,4, DRAGON_HAWK );
				CampaignDefenderEx( 0,0,1, ARCHMAGE );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,2, HIGH_SWORDMAN );
				CampaignAttackerEx( 1,2,4, HIGH_ARCHER );
				SuicideOnPlayerEx(M10,M8,M5,user);
				SetBuildUpgrEx( 0,1,1, UPG_SORCERY );
				SetBuildUpgrEx( 0,1,1, UPG_PRAYING );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,3, HIGH_SWORDMAN );
				CampaignAttackerEx( 2,3,3, HIGH_ARCHER );
				CampaignDefenderEx( 0,2,4, DRAGON_HAWK );
				SuicideOnPlayerEx(M10,M8,M4,user);
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 0,1,4, HIGH_SWORDMAN );
				CampaignAttackerEx( 2,3,4, HIGH_ARCHER );
				CampaignAttackerEx( 1,1,1, PRIEST );
				CampaignAttackerEx( 0,0,1, SORCERESS );
				CampaignAttackerEx( 0,1,2, BALLISTA );
				SuicideOnPlayerEx(M10,M8,M4,user);
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,4,4, HIGH_SWORDMAN );
				CampaignAttackerEx( 2,2,4, HIGH_ARCHER );
				CampaignAttackerEx( 0,1,1, PRIEST );
				CampaignDefenderEx( 0,2,4, DRAGON_HAWK );
				SuicideOnPlayerEx(M10,M8,M4,user);
				SetBuildUpgrEx( 0,0,2, UPG_SORCERY );
				SetBuildUpgrEx( 0,0,2, UPG_PRAYING );
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,3,4, HIGH_SWORDMAN );
				CampaignAttackerEx( 1,3,4, HIGH_ARCHER );
				CampaignAttackerEx( 1,1,2, PRIEST );
				CampaignAttackerEx( 0,0,1, SORCERESS );
				CampaignDefenderEx( 0,2,4, DRAGON_HAWK );
				CampaignAttackerEx( 0,1,2, BALLISTA );
				SuicideOnPlayerEx(M10,M8,M4,user);
				while( true )
				{
					//*** WAVE 6+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 1,2,2, HIGH_SWORDMAN );
					CampaignAttackerEx( 3,5,7, HIGH_ARCHER );
					CampaignAttackerEx( 1,1,2, PRIEST );
					CampaignAttackerEx( 0,1,1, SORCERESS );
					CampaignDefenderEx( 0,2,4, DRAGON_HAWK );
					SuicideOnPlayerEx(M10,M8,M4,user);
					//*** WAVE 7+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 3,4,8, HIGH_SWORDMAN );
					CampaignAttackerEx( 0,2,2, HIGH_ARCHER );
					CampaignAttackerEx( 1,3,4, PRIEST );
					CampaignAttackerEx( 0,0,1, SORCERESS );
					CampaignAttackerEx( 0,1,2, BALLISTA );
					SuicideOnPlayerEx(M10,M8,M4,user);
					//*** WAVE 8+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 2,2,5, HIGH_SWORDMAN );
					CampaignAttackerEx( 1,3,3, HIGH_ARCHER );
					CampaignAttackerEx( 1,1,2, PRIEST );
					CampaignAttackerEx( 1,2,4, SORCERESS );
					CampaignDefenderEx( 0,2,4, DRAGON_HAWK );
					SuicideOnPlayerEx(M10,M8,M4,user);
				}
			}

		} // class u05b_teal_ai 

	}

