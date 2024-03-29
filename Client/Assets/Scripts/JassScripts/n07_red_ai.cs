// Generated by .


	public partial class GameDefine 
	{

		public class n07_red_ai
		{
		//============================================================================
		//  Night Elf 07 -- red player -- AI Script
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(BURROW,null);
				SetReplacements(1,1,0);
				SetGroupsFlee(true);
				SetHeroesFlee(true);
				SetUnitsFlee(true);
				SetPeonsRepair(true);
				SetSlowChopping(false);
				SetBuildUnit( 1, PEON );
				SetBuildUnit( 1, ORC_BARRACKS );
				SetBuildUnit( 2, PEON );
				SetBuildUnit( 1, FORGE );
				SetBuildUnit( 3, PEON );
				SetBuildUnit( 1, ORC_ALTAR );
				SetBuildUnit( 1, BESTIARY );
				SetBuildUnit( 1, LODGE );
				SetBuildUnit( 1, TOTEM );
				SetBuildUnit( 8, PEON );
				CampaignDefenderEx( 1,1,1, THRALL );
				CampaignDefenderEx( 0,0,1, GRUNT );
				CampaignDefenderEx( 1,1,0, TAUREN );
				CampaignDefenderEx( 1,1,1, HEAD_HUNTER );
				CampaignDefenderEx( 1,1,1, WITCH_DOCTOR );
				CampaignDefenderEx( 1,1,1, SHAMAN );
				while( true )
				{
					if(  GetUnitCount(FORTRESS)==0 )
						break;
					Sleep(5);
				}
				InitBuildArray();
				do_campaign_farms = false;
				campaign_gold_peons = 0;
				campaign_wood_peons = 0;
				SleepForever();
			}

		} // class n07_red_ai 

	}

