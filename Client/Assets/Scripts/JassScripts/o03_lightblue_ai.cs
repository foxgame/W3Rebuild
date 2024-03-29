// Generated by .


	public partial class GameDefine 
	{

		public class o03_lightblue_ai
		{
		//============================================================================
		//  Orc 3 -- light blue player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(0);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(HOUSE,null);
				SetReplacements(1,1,3);
				SetBuildUnit( 8, PEASANT );
				CampaignDefenderEx( 1,1,2, FOOTMEN );
				CampaignDefenderEx( 1,1,1, PRIEST );
				CampaignDefenderEx( 0,0,1, PALADIN );
				WaitForSignal();
				// *** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 6,6,8, FOOTMAN );
				CampaignAttackerEx( 1,1,1, PALADIN );
				SuicideOnPlayer(0,user);
				SetBuildUpgrEx( 0,0,1, UPG_MELEE );
				SetBuildUpgrEx( 0,0,1, UPG_ARMOR );
				SetBuildUpgrEx( 0,0,2, UPG_MASONRY );
				SetBuildUpgrEx( 1,1,1, UPG_DEFEND );
				SetBuildUpgrEx( 0,0,1, UPG_PRAYING );
				// *** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 6,6,7, FOOTMAN );
				CampaignAttackerEx( 2,2,3, PRIEST );
				SuicideOnPlayerEx(M8,M8,M7,user);
				// *** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 6,6,8, FOOTMAN );
				CampaignAttackerEx( 1,1,1, PALADIN );
				SuicideOnPlayerEx(M8,M8,M7,user);
				SetBuildUpgrEx( 1,1,2, UPG_MELEE );
				SetBuildUpgrEx( 1,1,2, UPG_ARMOR );
				SetBuildUpgrEx( 1,1,2, UPG_PRAYING );
				while( true )
				{
					// *** WAVE 4 ***
					InitAssaultGroup();
					CampaignAttackerEx( 6,6,8, FOOTMAN );
					CampaignAttackerEx( 2,2,2, PRIEST );
					CampaignAttackerEx( 1,1,1, PALADIN );
					SuicideOnPlayerEx(M8,M8,M7,user);
					// *** WAVE 5 ***
					InitAssaultGroup();
					CampaignAttackerEx( 6,6,7, FOOTMAN );
					CampaignAttackerEx( 2,2,3, PRIEST );
					CampaignAttackerEx( 1,1,1, PALADIN );
					SuicideOnPlayerEx(M8,M8,M7,user);
				}
			}

		} // class o03_lightblue_ai 

	}

