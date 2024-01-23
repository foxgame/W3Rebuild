// Generated by .


	public partial class GameDefine 
	{

		public class o04_darkgreen_ai
		{
		//============================================================================
		//  Orc 04 -- dark green player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(3);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(MOON_WELL,null);
				campaign_wood_peons = 100;
				SetBuildUnitEx( 5,5,5, WISP );
				CampaignDefenderEx( 1,1,3, ARCHER );
				CampaignDefenderEx( 1,1,2, HUNTRESS );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,5, ARCHER );
				SuicideOnPlayer(M4,user);
				SetBuildUpgrEx( 0,0,1, UPG_STR_MOON );
				SetBuildUpgrEx( 1,1,1, UPG_ULTRAVISION );
				//*** WAVE 2 ***                                
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,4, ARCHER );
				CampaignAttackerEx( 2,2,3, HUNTRESS );
				SuicideOnPlayerEx(M7,M7,M6,user);
				SetBuildUpgrEx( 0,0,1, UPG_MOON_ARMOR );
				SetBuildUpgrEx( 0,0,1, UPG_GLAIVE );
				//*** WAVE 3 ***                                
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,3, ARCHER );
				CampaignAttackerEx( 2,2,4, HUNTRESS );
				CampaignAttackerEx( 1,1,1, BALLISTA );
				SuicideOnPlayerEx(M7,M7,M6,user);
				SetBuildUpgrEx( 1,1,1, UPG_BOWS );
				SetBuildUpgrEx( 1,1,2, UPG_STR_MOON );
				//*** WAVE 4 ***                                
				InitAssaultGroup();
				CampaignAttackerEx( 6,6,8, ARCHER );
				CampaignAttackerEx( 0,0,2, HUNTRESS );
				CampaignAttackerEx( 1,1,2, BALLISTA );
				SuicideOnPlayerEx(M7,M7,M6,user);
				SetBuildUpgrEx( 0,0,1, UPG_MARKSMAN );
				SetBuildUpgrEx( 1,1,2, UPG_MOON_ARMOR );
				//*** WAVE 5 ***                                
				InitAssaultGroup();
				CampaignAttackerEx( 5,5,7, HUNTRESS );
				CampaignAttackerEx( 0,0,2, BALLISTA );
				SuicideOnPlayerEx(M7,M7,M6,user);
				SetBuildUpgrEx( 1,1,1, UPG_GLAIVE );
				SetBuildUpgrEx( 0,0,1, UPG_BOLT );
				while( true )
				{
					//*** WAVE 6 ***
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,5, ARCHER );
					CampaignAttackerEx( 3,3,5, HUNTRESS );
					CampaignAttackerEx( 1,1,2, BALLISTA );
					SuicideOnPlayerEx(M7,M7,M6,user);
					//*** WAVE 7 ***
					InitAssaultGroup();
					CampaignAttackerEx( 7,7,7, ARCHER );
					CampaignAttackerEx( 0,0,2, HUNTRESS );
					CampaignAttackerEx( 2,2,3, BALLISTA );
					SuicideOnPlayerEx(M7,M7,M6,user);
					//*** WAVE 8 ***
					InitAssaultGroup();
					CampaignAttackerEx( 5,5,8, HUNTRESS );
					CampaignAttackerEx( 1,1,2, BALLISTA );
					SuicideOnPlayerEx(M7,M7,M6,user);
				}
			}

		} // class o04_darkgreen_ai 

	}

