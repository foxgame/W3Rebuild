// Generated by .


	public partial class GameDefine 
	{

		public class u04x06_ai
		{
		//============================================================================
		//  $Id: u04x06.ai,v 1.10 2003/05/07 16:44:03 rpardo Exp $
		//============================================================================
			public BJPlayer  user = PlayerEx(4);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(NAGA_CORAL,null);
				SetReplacements(2,2,3);
				SetAmphibious();
				SetBuildUnitEx( 1, 1, 1, NAGA_SLAVE );
				SetBuildUnitEx( 0, 0, 1, NAGA_TEMPLE );
				SetBuildUnitEx( 0, 0, 1, NAGA_SHRINE );
				SetBuildUnitEx( 0, 0, 1, NAGA_SPAWNING );
				SetBuildUnitEx( 0, 0, 1, NAGA_ALTAR );
				SetBuildUnitEx( 5, 5, 5, NAGA_SLAVE );
				SetBuildUnitEx( 0, 0, 3, NAGA_GUARDIAN );
				CampaignDefenderEx( 1,1,1, NAGA_MYRMIDON );
				CampaignDefenderEx( 0,0,1, NAGA_ROYAL );
				CampaignDefenderEx( 1,1,1, NAGA_SIREN );
				CampaignDefenderEx( 0,0,2, NAGA_REAVER );
				CampaignDefenderEx( 0,0,1, NAGA_VASHJ );
				SetBuildUpgrEx( 0,0,1, UPG_SIREN );
				SetBuildUpgrEx( 0,0,1, UPG_NAGA_ATTACK );
				SetBuildUpgrEx( 0,0,1, UPG_NAGA_ARMOR );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2, 2, 3, NAGA_MYRMIDON );
				SuicideOnPlayerEx(M3,M3,M2,user);
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ENSNARE );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4, 4, 7, NAGA_COUATL );
				SuicideOnPlayerEx(M6,M6,M5,user);
				SetBuildUpgrEx( 0,0,2, UPG_SIREN );
				SetBuildUpgrEx( 0,0,1, UPG_NAGA_ABOLISH );
				SetBuildUpgrEx( 0,0,2, UPG_NAGA_ATTACK );
				SetBuildUpgrEx( 0,0,2, UPG_NAGA_ARMOR );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4, 4, 6, NAGA_REAVER );
				CampaignAttackerEx( 2, 2, 3, NAGA_SNAP_DRAGON );
				CampaignAttackerEx( 1, 1, 1, NAGA_VASHJ );
				SuicideOnPlayerEx(M6,M6,M5,user);
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2, 2, 3, NAGA_MYRMIDON );
				CampaignAttackerEx( 2, 2, 3, NAGA_SIREN );
				CampaignAttackerEx( 1, 1, 2, NAGA_TURTLE );
				SuicideOnPlayerEx(M6,M6,M5,user);
				SetBuildUpgrEx( 0,0,3, UPG_NAGA_ATTACK );
				SetBuildUpgrEx( 0,0,3, UPG_NAGA_ARMOR );
				//*** WAVE 5+ ***
				while( true )
				{
					InitAssaultGroup();
					CampaignAttackerEx( 5, 5, 7, NAGA_COUATL );
					SuicideOnPlayerEx(M6,M6,M5,user);
					InitAssaultGroup();
					CampaignAttackerEx( 1, 1, 1, NAGA_VASHJ );
					CampaignAttackerEx( 1, 1, 2, NAGA_MYRMIDON );
					CampaignAttackerEx( 1, 1, 2, NAGA_SIREN );
					CampaignAttackerEx( 2, 2, 3, NAGA_SNAP_DRAGON );
					SuicideOnPlayerEx(M6,M6,M5,user);
					InitAssaultGroup();
					CampaignAttackerEx( 3, 3, 4, NAGA_MYRMIDON );
					CampaignAttackerEx( 2, 2, 3, NAGA_SNAP_DRAGON );
					CampaignAttackerEx( 1, 1, 2, NAGA_TURTLE );
					SuicideOnPlayerEx(M6,M6,M5,user);
					InitAssaultGroup();
					CampaignAttackerEx( 5, 5, 7, NAGA_COUATL );
					SuicideOnPlayerEx(M6,M6,M5,user);
					InitAssaultGroup();
					CampaignAttackerEx( 2, 2, 3, NAGA_SIREN );
					CampaignAttackerEx( 3, 3, 5, NAGA_MYRMIDON );
					CampaignAttackerEx( 1, 1, 2, NAGA_TURTLE );
					SuicideOnPlayerEx(M6,M6,M5,user);
				}
			}

		} // class u04x06_ai 

	}
