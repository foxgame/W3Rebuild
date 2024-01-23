// Generated by .


	public partial class GameDefine 
	{

		public class n04x06_ai
		{
		//==================================================================================================
		//  $Id: n04x06.ai,v 1.21 2003/05/07 16:44:03 rpardo Exp $
		//==================================================================================================
			public BJPlayer  user = PlayerEx(2);
		//--------------------------------------------------------------------------------------------------
		//  main
		//--------------------------------------------------------------------------------------------------
			public void main(  )
			{
				// Original JassCode
				CampaignAI(NAGA_CORAL,null);
				SetReplacements(5,5,5);
				do_campaign_farms = false;
				SetAmphibious();
				SetBuildUnitEx( 1, 1, 1, NAGA_SLAVE );
				SetBuildUnitEx( 1, 1, 1, NAGA_TEMPLE );
				SetBuildUnitEx( 1, 1, 1, NAGA_SHRINE );
				SetBuildUnitEx( 1, 1, 1, NAGA_SPAWNING );
				SetBuildUnitEx( 5, 5, 5, NAGA_SLAVE );
				SetBuildUnitEx( 3, 3, 3, NAGA_GUARDIAN );
				SetBuildUpgrEx( 0,0,1, UPG_SIREN );
				SetBuildUpgrEx( 0,0,1, UPG_NAGA_ATTACK );
				SetBuildUpgrEx( 0,0,1, UPG_NAGA_ARMOR );
				CampaignDefenderEx( 1,1,1, NAGA_MYRMIDON );
				CampaignDefenderEx( 3,3,4, NAGA_COUATL );
				CampaignDefenderEx( 1,1,1, NAGA_REAVER );
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 6, 6, 6, NAGA_REAVER );
				CampaignAttackerEx( 1, 1, 2, NAGA_MYRMIDON );
				SuicideOnPlayer(M1,user);
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ENSNARE );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4, 4, 6, NAGA_COUATL );
				SuicideOnPlayerEx(M5,M5,M4,user);
				SetBuildUpgrEx( 0,0,2, UPG_SIREN );
				SetBuildUpgrEx( 0,0,1, UPG_NAGA_ABOLISH );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 5, 5, 6, NAGA_REAVER );
				CampaignAttackerEx( 1, 1, 1, NAGA_TURTLE );
				CampaignAttackerEx( 2, 2, 3, NAGA_SIREN );
				SuicideOnPlayerEx(M5,M5,M4,user);
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2, 2, 4, NAGA_MYRMIDON );
				CampaignAttackerEx( 1, 1, 1, NAGA_TURTLE );
				SuicideOnPlayerEx(M5,M5,M4,user);
				//*** WAVE 5+ ***
				while( true )
				{
					InitAssaultGroup();
					CampaignAttackerEx( 4, 4, 6, NAGA_COUATL );
					SuicideOnPlayerEx(M5,M5,M4,user);
					InitAssaultGroup();
					CampaignAttackerEx( 2, 2, 2, NAGA_MYRMIDON );
					CampaignAttackerEx( 1, 1, 2, NAGA_TURTLE );
					SuicideOnPlayerEx(M5,M5,M4,user);
					InitAssaultGroup();
					CampaignAttackerEx( 5, 5, 6, NAGA_REAVER );
					CampaignAttackerEx( 2, 2, 3, NAGA_SIREN );
					CampaignAttackerEx( 1, 1, 1, NAGA_TURTLE );
					SuicideOnPlayerEx(M5,M5,M4,user);
				}
			}

		} // class n04x06_ai 

	}

