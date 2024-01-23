// Generated by .


	public partial class GameDefine 
	{

		public class n07_lightblue_ai
		{
		//============================================================================
		//  Night Elf 07 -- light blue player -- AI Script
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(HOUSE,null);
				SetReplacements(1,1,0);
				SetGroupsFlee(true);
				SetHeroesFlee(true);
				SetUnitsFlee(true);
				SetPeonsRepair(true);
				SetSlowChopping(false);
				SetBuildUnit( 1, PEASANT );
				SetBuildUnit( 1, BARRACKS );
				SetBuildUnit( 2, PEASANT );
				SetBuildUnit( 1, LUMBER_MILL );
				SetBuildUnit( 3, PEASANT );
				SetBuildUnit( 1, BLACKSMITH );
				SetBuildUnit( 1, HUMAN_ALTAR );
				SetBuildUnit( 1, WORKSHOP );
				SetBuildUnit( 1, SANCTUM );
				SetBuildUnit( 8, PEASANT );
				CampaignDefenderEx( 1,1,1, JAINA );
				CampaignDefenderEx( 0,0,1, FOOTMEN );
				CampaignDefenderEx( 1,1,0, KNIGHT );
				CampaignDefenderEx( 1,1,1, RIFLEMEN );
				CampaignDefenderEx( 1,1,1, PRIEST );
				CampaignDefenderEx( 1,1,1, SORCERESS );
				while( true )
				{
					if(  GetUnitCount(CASTLE)==0 )
						break;
					Sleep(5);
				}
				InitBuildArray();
				do_campaign_farms = false;
				campaign_gold_peons = 0;
				campaign_wood_peons = 0;
				SleepForever();
			}

		} // class n07_lightblue_ai 

	}
