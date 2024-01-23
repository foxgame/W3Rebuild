// Generated by .


	public partial class GameDefine 
	{

		public class u04_lightblue_ai
		{
		//============================================================================
		//  Undead 04 -- light blue player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(3);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ELF_FARM,null);
				SetReplacements(1,1,3);
				do_campaign_farms = false;
				SetBuildUnitEx( 0,0,1, TOWN_HALL );
				SetBuildUnitEx( 2,2,2, PEASANT );
				SetBuildUnitEx( 0,0,1, ELF_HIGH_BARRACKS );
				SetBuildUnitEx( 2,2,2, ZEPPLIN );
				SetBuildUnitEx( 0,0,1, LUMBER_MILL );
				SetBuildUnitEx( 5,5,5, PEASANT );
				SetBuildUnitEx( 0,0,1, KEEP );
				SetBuildUnitEx( 0,0,1, SANCTUM );
				SetBuildUnitEx( 0,0,1, CASTLE );
				CampaignDefenderEx( 0,0,1, HIGH_SWORDMAN );
				CampaignDefenderEx( 1,1,1, HIGH_ARCHER );
				CampaignDefenderEx( 0,0,1, SORCERESS );
				CampaignDefenderEx( 0,0,1, DRAGON_HAWK );
				CampaignDefenderEx( 1,1,1, SYLVANUS );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,2, HIGH_SWORDMAN );
				CampaignAttackerEx( 2,2,4, HIGH_ARCHER );
				SuicideOnPlayerEx(M4,M4,M1,user);
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,2, HIGH_SWORDMAN );
				CampaignAttackerEx( 2,2,2, HIGH_ARCHER );
				CampaignAttackerEx( 1,1,1, PRIEST );
				CampaignAttackerEx( 0,0,1, SYLVANUS );
				SuicideOnPlayerEx(M8,M8,M5,user);
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,6, DRAGON_HAWK );
				SuicideOnPlayerEx(M8,M8,M5,user);
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,2, HIGH_SWORDMAN );
				CampaignAttackerEx( 3,3,4, HIGH_ARCHER );
				CampaignAttackerEx( 1,1,2, SORCERESS );
				CampaignAttackerEx( 0,0,2, BALLISTA );
				CampaignAttackerEx( 1,1,1, SYLVANUS );
				SuicideOnPlayerEx(M8,M8,M5,user);
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,6, DRAGON_HAWK );
				CampaignAttackerEx( 3,3,6, HIGH_ARCHER );
				SuicideOnPlayerEx(M8,M8,M5,user);
				while( true )
				{
					//*** WAVE 6+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 2,2,4, HIGH_SWORDMAN );
					CampaignAttackerEx( 2,2,4, HIGH_ARCHER );
					CampaignAttackerEx( 1,1,2, PRIEST );
					CampaignAttackerEx( 1,1,2, SORCERESS );
					CampaignAttackerEx( 1,1,2, BALLISTA );
					CampaignAttackerEx( 1,1,1, SYLVANUS );
					SuicideOnPlayerEx(M8,M8,M5,user);
					//*** WAVE 7+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 5,5,9, DRAGON_HAWK );
					SuicideOnPlayerEx(M8,M8,M5,user);
					//*** WAVE 8+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 2,2,5, DRAGON_HAWK );
					CampaignAttackerEx( 2,2,4, HIGH_ARCHER );
					CampaignAttackerEx( 2,2,2, PRIEST );
					CampaignAttackerEx( 1,1,2, SORCERESS );
					CampaignAttackerEx( 0,0,1, SYLVANUS );
					SuicideOnPlayerEx(M8,M8,M5,user);
				}
			}

		} // class u04_lightblue_ai 

	}
