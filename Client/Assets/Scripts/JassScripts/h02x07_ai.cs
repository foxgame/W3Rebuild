// Generated by .


	public partial class GameDefine 
	{

		public class h02x07_ai
		{
		//==================================================================================================
		//  $Id: h02x07.ai,v 1.13.2.1 2003/05/09 09:17:04 abond Exp $
		//==================================================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ZIGGURAT_1,null);
				SetHarvestLumber(false);
				DoCampaignFarms(false);
				SetBuildUnitEx( 1,1,1, UNDEAD_MINE );
				while( true )
				{
					if(  TownCountDone(NECROPOLIS_1) > 0 )
						break;
					Sleep(1);
				}
				InitBuildArray();
				ResetCaptainLocs();
				SetHarvestLumber(true);
				SetWoodPeons(2);
				CampaignDefenderEx( 1,1,2, GHOUL );
				CampaignDefenderEx( 1,1,2, CRYPT_FIEND );
				SleepForever();
			}

		} // class h02x07_ai 

	}

