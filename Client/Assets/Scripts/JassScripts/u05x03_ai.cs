// Generated by .


	public partial class GameDefine 
	{

		public class u05x03_ai
		{
		//==================================================================================================
		//  $Id: u05x03.ai,v 1.15.2.1 2003/05/09 09:17:05 abond Exp $
		//==================================================================================================
			public BJPlayer  user = PlayerEx(4);
			// normal & easy delay
			public int  delay = M5;
			// hard delay
			public int  hard_delay = M4;
			// hard delay after final signal
			public int  fast_delay = M3;
		//--------------------------------------------------------------------------------------------------
		//  check_base_deaths
		//--------------------------------------------------------------------------------------------------
			public void check_base_deaths(  )
			{
				// Original JassCode
				if(  difficulty == HARD  )
				{
					return;
				}
				while( true )
				{
					if(  CommandsWaiting() != 0 )
						break;
					Trace("check_base_deaths waiting for signal...\n");
					Sleep(10);
				}
				PopLastCommand();
				delay = fast_delay;
			}

		//--------------------------------------------------------------------------------------------------
		//  upgrade_towers
		//--------------------------------------------------------------------------------------------------
			public void upgrade_towers(  )
			{
				// Original JassCode
				int count;
				while( true )
				{
					count = TownCountDone(WATCH_TOWER);
					if(  count > 0  )
					{
						SetProduce(count,GUARD_TOWER,-1);
					}
					Sleep(1);
				}
			}

		//--------------------------------------------------------------------------------------------------
		//  main
		//--------------------------------------------------------------------------------------------------
			public void main(  )
			{
				// Original JassCode
				CampaignAI(HOUSE,null);
				SetReplacements(2,2,4);
				if(  difficulty == HARD  )
				{
					delay = hard_delay;
				}
				SetBuildUnitEx( 0,0,1, TOWN_HALL );
				SetBuildUnitEx( 3,3,3, PEASANT );
				SetBuildUnitEx( 0,0,2, BARRACKS );
				SetBuildUnitEx( 0,0,1, LUMBER_MILL );
				SetBuildUnitEx( 0,0,1, HUMAN_ALTAR );
				SetBuildUnitEx( 0,0,1, BLACKSMITH );
				SetBuildUnitEx( 7,7,7, PEASANT );
				SetBuildUnitEx( 0,0,1, KEEP );
				SetBuildUnitEx( 0,0,1, WORKSHOP );
				SetBuildUnitEx( 0,0,1, CASTLE );
				WaitForSignal();
				// SUICIDE WAVES - Occur after timer runs out
				SuicideOnce( 4, 4, 4, FOOTMAN );
				SuicideOnce( 2, 2, 2, KNIGHT );
				SuicideOnce( 1, 1, 2, MORTAR );
				SuicideOnce( 2, 2, 2, RIFLEMAN );
				WaitForSignal();
				// ENABLE DEFENDERS - Occurs after Suicide is over
				CampaignDefenderEx( 1,1,1, KNIGHT );
				CampaignDefenderEx( 1,1,1, RIFLEMAN );
				CampaignDefenderEx( 0,0,1, COPTER );
				StartThread( upgrade_towers);
				WaitForSignal();
				StartThread( check_base_deaths);
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,6, FOOTMAN );
				CampaignAttackerEx( 2,2,4, RIFLEMAN );
				SuicideOnPlayerEx(M1,M1,M1,user);
				SetBuildUpgrEx( 1,1,1, UPG_MELEE );
				SetBuildUpgrEx( 1,1,1, UPG_RANGED );
				SetBuildUpgrEx( 1,1,1, UPG_MASONRY );
				SetBuildUpgrEx( 1,1,1, UPG_DEFEND );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,2, STEAM_TANK );
				CampaignAttackerEx( 4,4,4, RIFLEMAN );
				SuicideOnPlayer(delay,user);
				SetBuildUpgrEx( 1,1,1, UPG_GUN_RANGE );
				SetBuildUpgrEx( 1,1,1, UPG_LEATHER );
				SetBuildUpgrEx( 1,1,1, UPG_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_BREEDING );
				SetBuildUpgrEx( 1,1,1, UPG_BOMBS );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 6,6,8, FOOTMAN );
				CampaignAttackerEx( 3,3,6, RIFLEMAN );
				CampaignAttackerEx( 2,2,3, MORTAR );
				SuicideOnPlayer(delay,user);
				SetBuildUpgrEx( 0,0,2, UPG_MELEE );
				SetBuildUpgrEx( 0,0,2, UPG_RANGED );
				SetBuildUpgrEx( 1,1,2, UPG_MASONRY );
				SetBuildUpgrEx( 1,1,1, UPG_TANK );
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,3, ROCKET_TANK );
				CampaignAttackerEx( 2,2,4, FOOTMAN );
				CampaignAttackerEx( 3,3,6, RIFLEMAN );
				SuicideOnPlayer(delay,user);
				SetBuildUpgrEx( 0,0,2, UPG_LEATHER );
				SetBuildUpgrEx( 0,0,2, UPG_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_BREEDING );
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,6, KNIGHT );
				CampaignAttackerEx( 2,2,4, RIFLEMAN );
				CampaignAttackerEx( 1,1,1, MORTAR );
				SuicideOnPlayer(delay,user);
				while( true )
				{
					//*** WAVE 6 ***
					InitAssaultGroup();
					CampaignAttackerEx( 2,2,3, ROCKET_TANK );
					CampaignAttackerEx( 2,2,5, KNIGHT );
					CampaignAttackerEx( 3,3,5, RIFLEMAN );
					SuicideOnPlayer(delay,user);
					//*** WAVE 7 ***
					InitAssaultGroup();
					CampaignAttackerEx( 5,5,7, KNIGHT );
					CampaignAttackerEx( 2,2,4, RIFLEMAN );
					CampaignAttackerEx( 1,1,2, MORTAR );
					SuicideOnPlayer(delay,user);
				}
			}

		} // class u05x03_ai 

	}

