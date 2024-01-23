// Generated by .


	public partial class GameDefine 
	{

		public class n04_purple_ai
		{
		//============================================================================
		//  Night Elf 04 -- purple player -- AI Script
		//============================================================================
			public BJPlayer orcs = Player(0);
			public BJPlayer humans = Player(9);
		//============================================================================
		//  after_orcs
		//============================================================================
			public void after_orcs(  )
			{
				// Original JassCode
				SetStagePoint(237,-4826);
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 3, GARGOYLE );
				SuicideOnPlayer(M1,humans);
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 1, FROST_WYRM );
				SuicideOnPlayer(M3,humans);
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 4, GARGOYLE );
				CampaignAttacker( EASY, 1, FROST_WYRM );
				SuicideOnPlayer(M3,humans);
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 4, GARGOYLE );
				SuicideOnPlayer(M3,humans);
				while( true )
				{
					//*** WAVE 5 ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 4, GARGOYLE );
					SuicideOnPlayer(M3,humans);
					//*** WAVE 6+ ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 4, GARGOYLE );
					CampaignAttacker( EASY, 1, FROST_WYRM );
					SuicideOnPlayer(M3,humans);
				}
			}

		//============================================================================
		//  test_orcs
		//============================================================================
			public void test_orcs(  )
			{
				// Original JassCode
				if(  CommandsWaiting() > 0  )
				{
					after_orcs();
				}
			}

		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				Cheat("control 3");
				Cheat("vision 3");
				CampaignAI(BURROW,null);
				do_campaign_farms = false;
				SetStagePoint(-4350,-103);
				SetBuildUnit( 2, ACOLYTE );
				CampaignDefender( EASY, 2, GARGOYLE );
				CampaignDefender( EASY, 1, FROST_WYRM );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 3, GARGOYLE );
				SuicideOnPlayer(M2,orcs);
				test_orcs();
				while( true )
				{
					//*** WAVE 2 ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 7, GARGOYLE );
					SuicideOnPlayer(M2,orcs);
					test_orcs();
					//*** WAVE 3 ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 5, GARGOYLE );
					CampaignAttacker( EASY, 1, FROST_WYRM );
					SuicideOnPlayer(M2,orcs);
					test_orcs();
					//*** WAVE 4 ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 8, GARGOYLE );
					SuicideOnPlayer(M2,orcs);
					test_orcs();
				}
			}

		} // class n04_purple_ai 

	}

