// Generated by .

	public partial class GameDefine 
	{

		public class u03_gray_ai
		{
		//============================================================================
		//  Undead 03 -- gray player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(3);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(HOUSE,null);
				SetReplacements(0,1,3);
				SetBuildUnitEx( 0,0,0, TOWN_HALL );
				SetBuildUnitEx( 0,0,0, PEASANT );
				SetBuildUnitEx( 0,0,1, BARRACKS );
				SetBuildUnitEx( 0,0,8, HOUSE );
				SetBuildUnitEx( 0,0,1, WORKSHOP );
				SetBuildUnitEx( 0,0,1, BLACKSMITH );
				CampaignDefenderEx( 1,1,3, RIFLEMEN );
				CampaignDefenderEx( 0,1,2, MORTAR );
				SleepForever();
			}

		} // class u03_gray_ai 

	}

