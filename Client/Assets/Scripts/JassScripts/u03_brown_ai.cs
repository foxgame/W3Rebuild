// Generated by .


	public partial class GameDefine 
	{

		public class u03_brown_ai
		{
		//============================================================================
		//  Undead 03 -- brown player -- AI Script
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
				SetBuildUnit( 3, PEASANT );
				SetBuildUnit( 1, LUMBER_MILL );
				SetBuildUnit( 1, BLACKSMITH );
				SetBuildUnit( 7, PEASANT );
				CampaignDefender( EASY, 2, HIGH_FOOTMEN);
				CampaignDefender( EASY, 3, HIGH_ARCHER );
				CampaignDefender( EASY, 1, PRIEST );
				SleepForever();
			}

		} // class u03_brown_ai 

	}
