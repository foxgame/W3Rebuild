// Generated by .


	public partial class GameDefine 
	{

		public class n01_red_ai
		{
		//============================================================================
		//  Night Elf 1 -- red player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(1);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(BURROW,null);
				SetReplacements(1,1,3);
				campaign_wood_peons = 100;
				campaign_gold_peons = 0;
				SetBuildUnit ( 2, PEON );
				CampaignDefenderEx( 1,1,0, HEAD_HUNTER );
				CampaignDefenderEx( 0,0,1, GRUNT );
				SetBuildUpgrEx( 0,0,1, UPG_ORC_SPIKES );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,3, GRUNT );
				SuicideOnPlayerEx(M6,M6,M5,user);
				SetBuildUpgrEx( 0,0,1, UPG_ORC_ARMOR );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,3, HEAD_HUNTER );
				SuicideOnPlayerEx(M6,M6,M5,user);
				SetBuildUpgrEx( 0,0,1, UPG_ORC_RANGED );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,3, GRUNT );
				SuicideOnPlayerEx(M6,M6,M5,user);
				SetBuildUpgrEx( 0,0,1, UPG_ORC_MELEE );
				while( true )
				{
					//*** WAVE 5+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 2,2,3, GRUNT );
					CampaignAttackerEx( 0,0,1, HEAD_HUNTER );
					SuicideOnPlayerEx(M6,M6,M5,user);
					//*** WAVE 6+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 4,4,6, GRUNT );
					SuicideOnPlayerEx(M6,M6,M5,user);
				}
			}

		} // class n01_red_ai 

	}

