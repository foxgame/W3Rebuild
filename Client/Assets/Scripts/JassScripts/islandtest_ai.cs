// Generated by .


	public partial class GameDefine 
	{

		public class islandtest_ai
		{
		//============================================================================
		//  islandtest -- blue player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(0);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				do_debug_cheats = true;
				CampaignAI(HOUSE,null);
				SetBuildUnitEx( 2,2,2, ZEPPLIN );
				CampaignDefenderEx( 2,2,2, FOOTMAN );
				WaitForSignal();
				while( true )
				{
					//*** WAVE 1+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 1,1,1, FOOTMAN);
					CampaignAttackerEx( 1,1,1, RIFLEMAN);
					CampaignAttackerEx( 1,1,1, PRIEST);
					CampaignAttackerEx( 1,1,1, SORCERESS);
					CampaignAttackerEx( 1,1,1, KNIGHT);
					SuicideOnPlayerEx(M2,M2,M2,user);
					//*** WAVE 2+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 5,5,5, FOOTMAN);
					CampaignAttackerEx( 5,5,5, MORTAR);
					SuicideOnPlayerEx(M2,M2,M2,user);
					//*** WAVE 3+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 4,4,4, FOOTMAN);
					CampaignAttackerEx( 2,2,2, RIFLEMAN);
					SuicideOnPlayerEx(M2,M2,M2,user);
				}
			}

		} // class islandtest_ai 

	}

