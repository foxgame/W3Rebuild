// Generated by .


	public partial class GameDefine 
	{

		public class u08x05_ai
		{
		//==================================================================================================
		//  $Id: u08x05.ai,v 1.5 2003/04/29 22:18:47 bfitch Exp $
		//==================================================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(NAGA_CORAL,null);
				SetAmphibious();
				DoCampaignFarms(false);
				SetPeonsRepair(true);
				SetReplacements(9,9,9);
				SetCaptainHome(BOTH_CAPTAINS,3300,5700);
				campaign_wood_peons = 3;
				SetBuildUnitEx( 1,1,1, NAGA_TEMPLE );
				SetBuildUnitEx( 1,1,1, NAGA_SLAVE );
				SetBuildUnitEx( 1,1,1, NAGA_SPAWNING );
				SetBuildUnitEx( 1,1,1, NAGA_SHRINE );
				SetBuildUnitEx( 3,3,3, NAGA_SLAVE );
				SetBuildUnitEx( 3,3,3, NAGA_GUARDIAN );
				// this town is not to be attacked, so it has uber defense
				//
				CampaignDefenderEx( 2,2,2, NAGA_ROYAL );
				CampaignDefenderEx( 8,8,8, NAGA_SNAP_DRAGON );
				CampaignDefenderEx( 3,3,3, NAGA_SIREN );
				CampaignDefenderEx( 8,8,8, NAGA_COUATL );
				SleepForever();
			}

		} // class u08x05_ai 

	}
