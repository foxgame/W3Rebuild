// Generated by .


	public partial class GameDefine 
	{

		public class o03_gray_ai
		{
		//============================================================================
		//  Orc 3 -- gray player -- AI Script
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
				CampaignDefenderEx( 1,1,1, RIFLEMAN );
				CampaignDefenderEx( 1,1,1, MORTAR );
				WaitForSignal();
				// *** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,5, RIFLEMAN );
				CampaignAttackerEx( 1,1,2, MORTAR );
				SuicideOnPlayer(0,user);
				SetBuildUpgrEx( 0,0,1, UPG_LEATHER );
				SetBuildUpgrEx( 0,0,1, UPG_RANGED );
				SetBuildUpgrEx( 0,0,2, UPG_MASONRY );
				SetBuildUpgrEx( 1,1,1, UPG_DEFEND );
				SetBuildUpgrEx( 0,0,1, UPG_GUN_RANGE );
				// *** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,6, RIFLEMAN );
				CampaignAttackerEx( 1,1,1, MTN_KING );
				SuicideOnPlayerEx(M8,M8,M8,user);
				// *** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 5,5,6, RIFLEMAN );
				CampaignAttackerEx( 2,2,3, MORTAR );
				SuicideOnPlayerEx(M8,M8,M8,user);
				SetBuildUpgrEx( 1,1,2, UPG_LEATHER );
				SetBuildUpgrEx( 1,1,2, UPG_RANGED );
				SetBuildUpgrEx( 1,1,1, UPG_GUN_RANGE );
				while( true )
				{
					// *** WAVE 4 ***
					InitAssaultGroup();
					CampaignAttackerEx( 5,5,6, RIFLEMAN );
					CampaignAttackerEx( 1,1,2, MORTAR );
					CampaignAttackerEx( 1,1,1, MTN_KING );
					SuicideOnPlayerEx(M8,M8,M8,user);
					// *** WAVE 5 ***
					InitAssaultGroup();
					CampaignAttackerEx( 6,6,6, RIFLEMAN );
					CampaignAttackerEx( 2,2,3, MORTAR );
					SuicideOnPlayerEx(M8,M8,M8,user);
				}
			}

		} // class o03_gray_ai 

	}

