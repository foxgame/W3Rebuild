// Generated by .


	public partial class GameDefine 
	{

		public class o04_purple_ai
		{
		//============================================================================
		//  Orc 04 -- purple player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(0);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(MOON_WELL,null);
				campaign_wood_peons = 100;
				SetBuildUnit( 1, TREE_LIFE );
				SetBuildUnit( 1, WISP );
				SetBuildUnit( 1, ANCIENT_WAR );
				SetBuildUnit( 2, WISP );
				SetBuildUnit( 1, HUNTERS_HALL );
				SetBuildUnit( 1, TREE_AGES );
				SetBuildUnit( 3, WISP );
				SetBuildUnit( 1, ELF_ALTAR );
				SetBuildUnit( 1, ANCIENT_WIND );
				SetBuildUnit( 1, ANCIENT_LORE );
				SetBuildUnit( 1, TREE_ETERNITY );
				SetBuildUnit( 5, WISP );
				CampaignDefender( EASY, 3, ARCHER );
				CampaignDefender( EASY, 2, HUNTRESS );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 2, ARCHER );
				SuicideOnPlayer(0,user);
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 2, ARCHER );
				CampaignAttacker( EASY, 1, HUNTRESS );
				SuicideOnPlayer(3*60,user);
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 2, ARCHER );
				CampaignAttacker( EASY, 2, HUNTRESS );
				CampaignAttacker( EASY, 1, BALLISTA );
				SuicideOnPlayer(4*60,user);
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 5, ARCHER );
				CampaignAttacker( EASY, 2, HUNTRESS );
				CampaignAttacker( EASY, 5, MOON_CHICK );
				SuicideOnPlayer(6*60,user);
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 3, ARCHER );
				CampaignAttacker( EASY, 1, HUNTRESS );
				SuicideOnPlayer(2*60,user);
				//*** WAVE 6 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 4, HUNTRESS );
				CampaignAttacker( EASY, 2, BALLISTA );
				CampaignAttacker( EASY, 1, WISP );
				CampaignAttacker( EASY, 1, MOON_CHICK );
				SuicideOnPlayer(6*60,user);
				//*** WAVE 7 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 2, HUNTRESS );
				CampaignAttacker( EASY, 1, ANCIENT_PROTECT );
				SuicideOnPlayer(3*60,user);
				//*** WAVE 8 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 6, ARCHER );
				SuicideOnPlayer(4*60,user);
				while( true )
				{
					//*** WAVE 9 ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 3, HUNTRESS );
					CampaignAttacker( EASY, 2, ANCIENT_PROTECT );
					CampaignAttacker( EASY, 1, MOON_CHICK );
					SuicideOnPlayer(4*6,user);
					//*** WAVE 10 ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 4, ARCHER );
					CampaignAttacker( EASY, 2, HUNTRESS );
					CampaignAttacker( EASY, 1, BALLISTA );
					CampaignAttacker( EASY, 2, WISP );
					SuicideOnPlayer(6*60,user);
					//*** WAVE 11 ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 2, ARCHER );
					CampaignAttacker( EASY, 1, HUNTRESS );
					CampaignAttacker( EASY, 1, ANCIENT_PROTECT );
					CampaignAttacker( EASY, 1, WISP );
					SuicideOnPlayer(2*60,user);
					//*** WAVE 12 ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 6, ARCHER );
					CampaignAttacker( EASY, 1, BALLISTA );
					CampaignAttacker( EASY, 1, MOON_CHICK );
					SuicideOnPlayer(4*60,user);
				}
			}

		} // class o04_purple_ai 

	}

