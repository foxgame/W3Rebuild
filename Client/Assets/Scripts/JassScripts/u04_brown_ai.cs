// Generated by .


	public partial class GameDefine 
	{

		public class u04_brown_ai
		{
		//============================================================================
		//  Undead 04 -- brown player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(3);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ELF_FARM,null);
				SetReplacements(1,1,3);
				do_campaign_farms = false;
				CampaignDefenderEx( 0,0,1, HIGH_SWORDMAN );
				CampaignDefenderEx( 1,1,2, HIGH_ARCHER );
				CampaignDefenderEx( 0,0,1, PRIEST );
				SleepForever();
			}

		} // class u04_brown_ai 

	}

