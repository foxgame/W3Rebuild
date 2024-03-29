// Generated by .


	public partial class GameDefine 
	{

		public class n08_red_ai
		{
		//============================================================================
		//  Night Elf 08 -- orange player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(1);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(BURROW,null);
				SetReplacements(6,6,9);
				CampaignDefenderEx( 2,2,3, INFERNAL );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,4, INFERNAL );
				SuicideOnPlayerEx(M7,M7,M6,user);
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 6,6,8, FELLHOUND );
				CampaignAttackerEx( 1,1,2, INFERNAL );
				SuicideOnPlayerEx(M7,M7,M6,user);
				while( true )
				{
					//*** WAVE 3+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,4, INFERNAL );
					CampaignAttackerEx( 2,2,3, FELLHOUND );
					SuicideOnPlayerEx(M7,M7,M6,user);
					//*** WAVE 4+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 8,8,9, FELLHOUND );
					CampaignAttackerEx( 1,1,2, INFERNAL );
					SuicideOnPlayerEx(M7,M7,M6,user);
				}
			}

		} // class n08_red_ai 

	}

