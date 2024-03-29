// Generated by .


	public partial class GameDefine 
	{

		public class n03_purple_ai
		{
		//============================================================================
		//  Night Elf 03 -- purple player -- AI Script
		//============================================================================
			public double MAIN_TOWN_X = -4279;
			public double MAIN_TOWN_Y = 3057;
			public double DEFENSE_X = -3919;
			public double DEFENSE_Y = 1198;
			public double FAILSAFE_X = -2700;
			public double FAILSAFE_Y = 80;
			public double GRAVEYARD_X = -1979;
			public double GRAVEYARD_Y = -1037;
			public int MIN_GHOULS_NORMAL = 3;
			public int MIN_GHOULS_HARD = 8;
			public int MAX_GHOULS_NORMAL = 10;
			public int MAX_GHOULS_HARD = 15;
			public bool  on_alert = false;
			public int  defcon = 5;
			public int  trees_alive = 100;
			public int  best_ghouls = 0;
		//============================================================================
		//  set_defcon
		//============================================================================
			public void lower_defcon(  )
			{
				// Original JassCode
				on_alert = true;
				defcon = defcon - 1;
				TraceI("DEFCON {0}\n",defcon);
				//--------------------------------------------------------------------
				if(  defcon == 4  )
				{
					//--------------------------------------------------------------------
					AddGuardPost( NECRO, -1800, 230 );
					CampaignDefenderEx( 1,1,2, NECRO );
					//--------------------------------------------------------------------
				}
				else if(  defcon == 3  )
				{
					//--------------------------------------------------------------------
					AddGuardPost( GARGOYLE, -2320, 725 );
					AddGuardPost( NECRO, -2340, 325 );
					CampaignDefenderEx( 1,1,1, CRYPT_FIEND );
					//--------------------------------------------------------------------
				}
				else if(  defcon == 2  )
				{
					//--------------------------------------------------------------------
					AddGuardPost( ABOMINATION, -2110, 70 );
					AddGuardPost( NECRO, -2620, 480 );
					CampaignDefenderEx( 1,1,2, ABOMINATION );
					//--------------------------------------------------------------------
					// defcon 1
				}
				else
				{
					//--------------------------------------------------------------------
					AddGuardPost( ABOMINATION, -2025, 570 );
					AddGuardPost( GARGOYLE, -2450, 520 );
					AddGuardPost( CRYPT_FIEND, -2120, 290 );
					CampaignDefender( EASY, 1, TICHONDRIUS );
					SetCaptainHome(DEFENSE_CAPTAIN,FAILSAFE_X,FAILSAFE_Y);
				}
			}

		//============================================================================
		//  set_wood_ghouls
		//============================================================================
			public void set_wood_ghouls(  )
			{
				// Original JassCode
				int ghouls = GetUnitCount(GHOUL);
				// detect lost ghouls to reinforce them with defenders
				//
				if(  ! on_alert && defcon > 1 && ghouls < best_ghouls  )
				{
					lower_defcon();
				}
				// detect when to stand down alert status
				//
				if(  on_alert && ghouls >= best_ghouls && ! CaptainInCombat(false)  )
				{
					on_alert = false;
				}
				// save ghoul watermark
				//
				if(  ghouls > best_ghouls  )
				{
					best_ghouls = ghouls;
				}
				while( true )
				{
					if(  CommandsWaiting() == 0 )
						break;
					trees_alive = GetLastData();
					PopLastCommand();
				}
				if(  difficulty==HARD  )
				{
					campaign_wood_peons = MAX_GHOULS_HARD - ((MAX_GHOULS_HARD-MIN_GHOULS_HARD+1)*trees_alive)/100;
				}
				else
				{
					campaign_wood_peons = MAX_GHOULS_NORMAL - ((MAX_GHOULS_NORMAL-MIN_GHOULS_NORMAL+1)*trees_alive)/100;
				}
			}

		//============================================================================
		//  rebuild_town
		//============================================================================
			public void rebuild_town(  )
			{
				// Original JassCode
				InitBuildArray();
				// revive Tichondrius as first priority
				//
				if(  GetUnitCountDone(UNDEAD_ALTAR)>0  )
				{
					SetBuildUnit( 1, TICHONDRIUS );
				}
				SetBuildUnit( 1, ACOLYTE );
				// meet all basic needs before allowing the graveyard
				//
				if(  TownCount(NECROPOLIS_1)==0 || GetUnitCount(UNDEAD_MINE)==0 || GetUnitCount(UNDEAD_ALTAR)==0 || GetUnitCount(CRYPT)==0  )
				{
					ShiftTownSpot(MAIN_TOWN_X,MAIN_TOWN_Y);
					SetBuildUnit( 1, NECROPOLIS_1 );
					SetBuildUnit( 1, UNDEAD_MINE );
					SetBuildUnit( 1, CRYPT );
					SetBuildUnit( 1, UNDEAD_ALTAR );
					return;
				}
				// allow only the graveyard when the town spot is shifted to the trees
				//
				if(  GetUnitCount(GRAVEYARD)==0  )
				{
					ShiftTownSpot(GRAVEYARD_X,GRAVEYARD_Y);
					SetBuildUnit( 1, GRAVEYARD );
					return;
				}
				SetBuildUnit( campaign_wood_peons, GHOUL );
				// build the rest of the town on the hill
				//
				ShiftTownSpot(MAIN_TOWN_X,MAIN_TOWN_Y);
				SetBuildUnit( 8, ZIGGURAT_1 );
				SetBuildUnit( 2, CRYPT_FIEND );
				SetBuildUnit( 1, NECROPOLIS_2 );
				SetBuildUnit( 1, DAMNED_TEMPLE );
				SetBuildUnit( 2, NECRO );
				SetBuildUnit( 1, SLAUGHTERHOUSE );
				SetBuildUnit( 2, ABOMINATION );
				SetBuildUnit( 8, ZIGGURAT_2 );
				SetBuildUnit( 1, NECROPOLIS_3 );
				SetBuildUnit( 5, ACOLYTE );
			}

		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ZIGGURAT_1,null);
				SetSlowChopping(false);
				SetPeonsRepair(true);
				SetReplacementCount(99);
				SetCaptainHome(DEFENSE_CAPTAIN,DEFENSE_X,DEFENSE_Y);
				CampaignDefenderEx( 1,1,1, CRYPT_FIEND );
				while( true )
				{
					rebuild_town();
					set_wood_ghouls();
					Sleep(2);
				}
			}

		} // class n03_purple_ai 

	}

