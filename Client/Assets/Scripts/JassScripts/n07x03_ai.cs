// Generated by .


	public partial class GameDefine 
	{

		public class n07x03_ai
		{
		//==================================================================================================
		//  $Id: n07x03.ai,v 1.13 2003/05/03 19:03:16 bfitch Exp $
		//==================================================================================================
			public BJPlayer  targ = PlayerEx(1);
		//--------------------------------------------------------------------------------------------------
		//  main
		//--------------------------------------------------------------------------------------------------
			public void main(  )
			{
				// Original JassCode
				CampaignAI(NAGA_CORAL,null);
				SetAmphibious();
				SetReplacements(1,1,3);
				SetRandomPaths(false);
				SetCaptainHome(DEFENSE_CAPTAIN,-7872,7753);
				SetCaptainHome(ATTACK_CAPTAIN,-3413,8287);
				CampaignDefenderEx( 1, 1, 2, NAGA_MYRMIDON );
				CampaignDefenderEx( 0, 0, 1, NAGA_SIREN );
				CampaignDefenderEx( 0, 0, 2, NAGA_COUATL );
				SetBuildUpgrEx( 1,1,2, UPG_SIREN );
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ENSNARE );
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ABOLISH );
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ATTACK );
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ARMOR );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 5, 5, 6, NAGA_REAVER );
				SuicideOnPlayer(M5,targ);
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3, 3, 4, NAGA_COUATL );
				SuicideOnPlayer(M5,targ);
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1, 1, 2, NAGA_MYRMIDON );
				CampaignAttackerEx( 2, 2, 3, NAGA_SIREN );
				SuicideOnPlayer(M5,targ);
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3, 3, 4, NAGA_REAVER );
				CampaignAttackerEx( 3, 3, 4, NAGA_DRAGON );
				CampaignAttackerEx( 0, 0, 5, NAGA_COUATL );
				SuicideOnPlayer(M5,targ);
				while( true )
				{
					//*** WAVE 5+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 1, 1, 3, NAGA_MYRMIDON );
					CampaignAttackerEx( 2, 2, 4, NAGA_SIREN );
					CampaignAttackerEx( 0, 0, 5, NAGA_COUATL );
					SuicideOnPlayer(M5,targ);
				}
			}

		} // class n07x03_ai 

	}

