// Generated by .


	public partial class GameDefine 
	{

		public class u03_green_ai
		{
		//============================================================================
		//  Undead 03 -- green player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(3);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ELF_FARM,null);
				SetBuildUnit( 1, TOWN_HALL );
				SetBuildUnit( 2, PEASANT );
				SetBuildUnit( 1, ELF_HIGH_BARRACKS );
				SetBuildUnit( 1, LUMBER_MILL );
				SetBuildUnit( 4, PEASANT );
				SetBuildUnit( 1, BLACKSMITH );
				SetBuildUnit( 1, HUMAN_ALTAR );
				SetBuildUnit( 8, PEASANT );
				CampaignDefender( EASY, 3, HIGH_FOOTMEN);
				CampaignDefender( EASY, 4, HIGH_ARCHER );
				CampaignDefender( EASY, 1, PRIEST );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 2, HIGH_FOOTMEN);
				CampaignAttacker( EASY, 1, HIGH_ARCHER );
				SuicideOnPlayer(0,user);
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 3, HIGH_FOOTMEN);
				CampaignAttacker( EASY, 2, HIGH_ARCHER );
				CampaignAttacker( EASY, 1, SYLVANUS );
				SuicideOnPlayer(5*60,user);
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 2, HIGH_FOOTMEN);
				CampaignAttacker( EASY, 3, HIGH_ARCHER );
				CampaignAttacker( EASY, 1, SYLVANUS );
				SuicideOnPlayer(5*60,user);
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 4, HIGH_FOOTMEN);
				CampaignAttacker( EASY, 1, HIGH_ARCHER );
				CampaignAttacker( EASY, 1, PRIEST );
				CampaignAttacker( EASY, 1, SYLVANUS );
				SuicideOnPlayer(5*60,user);
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 3, HIGH_FOOTMEN);
				CampaignAttacker( EASY, 3, HIGH_ARCHER );
				CampaignAttacker( EASY, 1, PRIEST );
				CampaignAttacker( EASY, 1, SYLVANUS );
				SuicideOnPlayer(5*60,user);
				while( true )
				{
					//*** WAVE 6+ ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 2, HIGH_FOOTMEN);
					CampaignAttacker( EASY, 3, HIGH_ARCHER );
					CampaignAttacker( EASY, 1, PRIEST );
					CampaignAttacker( EASY, 1, SYLVANUS );
					SuicideOnPlayer(5*60,user);
				}
			}

		} // class u03_green_ai 

	}

