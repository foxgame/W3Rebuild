// Generated by .


	public partial class GameDefine 
	{

		public class u05a_brown_ai
		{
		//============================================================================
		//  Undead 05a -- brown player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(3);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ELF_FARM,null);
				SetReplacements(0,1,3);
				SetBuildUnitEx( 0,0,1, TOWN_HALL );
				SetBuildUnitEx( 2,2,2, PEASANT );
				SetBuildUnitEx( 0,0,1, ELF_HIGH_BARRACKS );
				SetBuildUnitEx( 0,0,14, ELF_FARM );
				SetBuildUnitEx( 0,0,1, LUMBER_MILL );
				SetBuildUnitEx( 0,0,1, BLACKSMITH );
				SetBuildUnitEx( 0,0,1, HUMAN_ALTAR );
				SetBuildUnitEx( 0,0,1, KEEP );
				SetBuildUnitEx( 0,0,1, SANCTUM );
				SetBuildUnitEx( 0,0,1, CASTLE );
				CampaignDefenderEx( 1,1,3, HIGH_SWORDMAN );
				CampaignDefenderEx( 1,1,4, HIGH_ARCHER );
				CampaignDefenderEx( 0,1,1, PRIEST );
				CampaignDefenderEx( 0,1,1, SORCERESS );
				CampaignDefenderEx( 0,2,4, DRAGON_HAWK );
				SetBuildUpgrEx( 0,0,2, UPG_SORCERY );
				SetBuildUpgrEx( 0,0,2, UPG_PRAYING );
				SleepForever();
			}

		} // class u05a_brown_ai 

	}

