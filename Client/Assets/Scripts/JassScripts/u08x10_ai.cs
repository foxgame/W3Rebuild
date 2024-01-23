// Generated by .


	public partial class GameDefine 
	{

		public class u08x10_ai
		{
		//==================================================================================================
		//  $Id: u08x10.ai,v 1.3.2.3 2003/05/12 19:54:27 mheiberg Exp $
		//==================================================================================================
			public int SET_X = 1;
			public int SET_Y = 2;
		//--------------------------------------------------------------------------------------------------
		//  get_coords
		//--------------------------------------------------------------------------------------------------
			public void get_coords(  )
			{
				// Original JassCode
				int x = -1;
				int y = -1;
				int cmd;
				int data;
				while( true )
				{
					while( true )
					{
						if(  CommandsWaiting() > 0 )
							break;
						Sleep(0.1);
					}
					cmd = GetLastCommand();
					data = GetLastData();
					PopLastCommand();
					//------------------------------------------------------------------------------------------
					if(  cmd == SET_X  )
					{
						//------------------------------------------------------------------------------------------
						x = data;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SET_Y  )
					{
						//------------------------------------------------------------------------------------------
						y = data;
					}
					if(  x != -1 && y != -1 )
						break;
				}
				ShiftTownSpot(R2I(GetStartLocationX(GetPlayerStartLocation(ai_player))), R2I(GetStartLocationY(GetPlayerStartLocation(ai_player))));
				SetCaptainHome(BOTH_CAPTAINS,x,y);
				TeleportCaptain(x,y);
			}

		//--------------------------------------------------------------------------------------------------
		//  main
		//--------------------------------------------------------------------------------------------------
			public void main(  )
			{
				// Original JassCode
				CampaignAI(NAGA_CORAL,null);
				SetPeonsRepair(true);
				get_coords();
				SetBuildUnitEx( 1,1,1, NAGA_CORAL );
				SetBuildUnitEx( 1,1,1, NAGA_SPAWNING );
				SetBuildUnitEx( 1,1,1, NAGA_SHRINE );
				SetBuildUnitEx( 2,2,2, NAGA_CORAL );
				SetBuildUnitEx( 2,2,3, NAGA_GUARDIAN );
				AddGuardPost( NAGA_MYRMIDON, -3713, -953 );
				AddGuardPost( NAGA_COUATL, -3445, -1118 );
				AddGuardPost( NAGA_SIREN, -3458, -1252 );
				AddGuardPost( NAGA_REAVER, -3681, -1230 );
				CampaignDefenderEx( 1,1,3, NAGA_MYRMIDON );
				CampaignDefenderEx( 1,1,2, NAGA_SIREN );
				CampaignDefenderEx( 2,2,4, NAGA_REAVER );
				CampaignDefenderEx( 1,1,2, NAGA_COUATL );
				SetBuildUpgrEx( 1,1,1, UPG_SIREN );
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ENSNARE );
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ABOLISH );
				SetBuildUpgrEx( 2,2,2, UPG_SIREN );
				SleepForever();
			}

		} // class u08x10_ai 

	}
