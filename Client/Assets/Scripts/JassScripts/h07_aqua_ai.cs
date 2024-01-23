// Generated by .

	public partial class GameDefine 
	{

		public class h07_aqua_ai
		{
		//============================================================================
		//  Human 07 -- aqua player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(1);
			public void set_cheats(  )
			{
				// Original JassCode
				Cheat("warnings");
				Cheat("viewres");
				Cheat("av");
				Cheat("dg");
				Cheat("vision 3");
				Cheat("vision 10");
			}

		//============================================================================
		//  hero_levels
		//============================================================================
			public int hero_levels(  )
			{
				// Original JassCode
				int hero = GetHeroId();
				int level = GetHeroLevelAI();
				if(  hero == LICH  )
				{
					if(  level == 2 || level == 4  )
					{
						// frost nova
						return UnitId( "AUfn" );
					}
					if(  level == 3  )
					{
						// frost armor
						return UnitId( "AUfa" );
					}
					if(  level == 5  )
					{
						// death & decay
						return UnitId( "AUdd" );
					}
				}
				return 0;
			}

		//============================================================================
		//  SetDefenders
		//============================================================================
			public void SetDefenders(  )
			{
				// Original JassCode
				CampaignDefender( EASY, 1, GARGOYLE );
				CampaignDefender( EASY, 2, GHOUL );
				CampaignDefender( EASY, 1, NECRO );
			}

		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				set_cheats();
				CampaignAI(ZIGGURAT_1,hero_levels);
				SetBuildUnit( 1, ACOLYTE );
				SetBuildUnit( 1, NECROPOLIS_1 );
				SetBuildUnit( 1, CRYPT );
				SetBuildUnit( 1, GRAVEYARD );
				SetBuildUnit( 1, UNDEAD_MINE );
				SetBuildUnit( 1, NECROPOLIS_2 );
				SetBuildUnit( 1, UNDEAD_ALTAR );
				SetBuildUnit( 1, SLAUGHTERHOUSE );
				SetBuildUnit( 1, DAMNED_TEMPLE );
				SetBuildUnit( 1, NECROPOLIS_3 );
				SetBuildUnit( 1, SAC_PIT );
				SetBuildUnit( 5, ACOLYTE );
				while( true )
				{
					SetDefenders();
					if(  CommandsWaiting() != 0 )
						break;
					Sleep(10);
				}
				PopLastCommand();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 4, GHOUL );
				CampaignAttacker( EASY, 1, NECRO );
				SuicideOnPlayer(0,user);
				SetDefenders();
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 3, GARGOYLE );
				SuicideOnPlayer(5*60,user);
				SetDefenders();
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 4, GHOUL );
				CampaignAttacker( EASY, 1, ABOMINATION );
				CampaignAttacker( EASY, 1, NECRO );
				CampaignAttacker( EASY, 1, LICH );
				SuicideOnPlayer(4*60,user);
				SetDefenders();
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttacker( EASY, 2, GHOUL );
				CampaignAttacker( EASY, 2, ABOMINATION );
				CampaignAttacker( EASY, 1, NECRO );
				CampaignAttacker( EASY, 1, MEAT_WAGON );
				CampaignAttacker( EASY, 1, LICH );
				SuicideOnPlayer(5*60,user);
				while( true )
				{
					SetDefenders();
					//*** WAVE 5,7,9,... ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 6, GARGOYLE );
					SuicideOnPlayer(5*60,user);
					SetDefenders();
					//*** WAVE 6,8,10,... ***
					InitAssaultGroup();
					CampaignAttacker( EASY, 4, ABOMINATION );
					CampaignAttacker( EASY, 2, NECRO );
					CampaignAttacker( EASY, 2, MEAT_WAGON );
					CampaignAttacker( EASY, 1, LICH );
					SuicideOnPlayer(4*60,user);
				}
			}

		} // class h07_aqua_ai 

	}
