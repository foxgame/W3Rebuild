// Generated by .


	public partial class GameDefine 
	{

		public class n07x06_ai
		{
		//==================================================================================================
		//  $Id: n07x06.ai,v 1.24 2003/05/03 14:01:08 rpardo Exp $
		//==================================================================================================
			public BJPlayer  user = PlayerEx(1);
		//--------------------------------------------------------------------------------------------------
		//  main
		//--------------------------------------------------------------------------------------------------
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ZIGGURAT_1,null);
				SetReplacements(2,2,3);
				SetRandomPaths(false);
				campaign_wood_peons = 2;
				SetCaptainHome(ATTACK_CAPTAIN,-225,330);
				SetBuildUnitEx( 1,1,1, NECROPOLIS_1 );
				SetBuildUnitEx( 1,1,1, ACOLYTE );
				SetBuildUnitEx( 1,1,1, UNDEAD_ALTAR );
				SetBuildUnitEx( 1,1,1, CRYPT );
				SetBuildUnitEx( 1,1,1, TOMB_OF_RELICS );
				SetBuildUnitEx( 1,1,1, GRAVEYARD );
				SetBuildUnitEx( 9,9,9, ZIGGURAT_1 );
				SetBuildUnitEx( 1,1,1, NECROPOLIS_2 );
				SetBuildUnitEx( 1,1,1, SLAUGHTERHOUSE );
				SetBuildUnitEx( 1,1,1, DAMNED_TEMPLE );
				SetBuildUnitEx( 1,1,1, NECROPOLIS_3 );
				SetBuildUnitEx( 5,5,5, ACOLYTE );
				SetBuildUnitEx( 6,6,6, ZIGGURAT_2 );
				SetBuildUnitEx( 3,3,3, ZIGGURAT_FROST );
				CampaignDefenderEx( 1,1,1, LICH );
				CampaignDefenderEx( 1,1,1, DREAD_LORD );
				CampaignDefenderEx( 1,1,1, ABOMINATION );
				CampaignDefenderEx( 1,1,1, NECRO );
				CampaignDefenderEx( 1,1,1, OBS_STATUE );
				CampaignDefenderEx( 1,1,1, GARGOYLE );
				CampaignDefenderEx( 1,1,1, FROST_WYRM );
				CampaignDefenderEx( 1,1,1, CRYPT_FIEND );
				SetBuildUpgrEx( 1,1,1, UPG_BLK_SPHINX );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,6, GHOUL );
				CampaignAttackerEx( 1,1,3, ABOMINATION );
				CampaignAttackerEx( 1,1,2, NECRO );
				SuicideOnPlayerEx(M2,M2,M1,user);
				SetBuildUpgr( 1, UPG_FIEND_WEB );
				SetBuildUpgrEx( 1,1,1, UPG_UNHOLY_STR );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,4, GHOUL );
				CampaignAttackerEx( 2,2,4, CRYPT_FIEND );
				CampaignAttackerEx( 1,1,1, DREAD_LORD );
				SuicideOnPlayerEx(M4,M4,M3,user);
				SetBuildUpgrEx( 1,1,1, UPG_CR_ATTACK );
				SetBuildUpgrEx( 1,1,1, UPG_SKEL_LIFE );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 6,6,12, GHOUL );
				CampaignAttackerEx( 3,3,4, NECRO );
				CampaignAttackerEx( 1,1,1, OBS_STATUE );
				CampaignAttackerEx( 1,1,1, LICH );
				SuicideOnPlayerEx(M4,M4,M3,user);
				SetBuildUpgrEx( 0,0,1, UPG_PLAGUE );
				SetBuildUpgrEx( 1,1,1, UPG_UNHOLY_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_CR_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_NECROS );
				SetBuildUpgrEx( 1,1,1, UPG_BANSHEE );
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 0,0,2, NECRO );
				CampaignAttackerEx( 3,3,4, ABOMINATION );
				CampaignAttackerEx( 1,1,1, MEAT_WAGON );
				CampaignAttackerEx( 1,1,1, DREAD_LORD );
				SuicideOnPlayerEx(M4,M4,M3,user);
				SetBuildUpgrEx( 2,2,2, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 2,2,2, UPG_CR_ATTACK );
				SetBuildUpgrEx( 1,1,1, UPG_SKEL_MASTERY );
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,3, ABOMINATION );
				CampaignAttackerEx( 1,1,1, OBS_STATUE );
				CampaignAttackerEx( 4,4,6, GARGOYLE );
				CampaignAttackerEx( 1,1,1, LICH );
				SuicideOnPlayerEx(M3,M3,M3,user);
				SetBuildUpgrEx( 1,1,1, UPG_GHOUL_FRENZY );
				SetBuildUpgrEx( 1,1,1, UPG_STONE_FORM );
				//*** WAVE 6 ***
				InitAssaultGroup();
				CampaignAttackerEx( 6,6,8, GARGOYLE );
				CampaignAttackerEx( 2,2,2, BLK_SPHINX );
				SuicideOnPlayerEx(M3,M3,M3,user);
				SetBuildUpgrEx( 2,2,2, UPG_UNHOLY_ARMOR );
				SetBuildUpgrEx( 2,2,2, UPG_CR_ARMOR );
				SetBuildUpgrEx( 2,2,2, UPG_NECROS );
				SetBuildUpgrEx( 1,2,2, UPG_BANSHEE );
				while( true )
				{
					//*** WAVE 7+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,4, ABOMINATION );
					CampaignAttackerEx( 2,2,3, NECRO );
					CampaignAttackerEx( 2,2,3, BANSHEE );
					CampaignAttackerEx( 1,1,1, LICH );
					CampaignAttackerEx( 1,1,1, OBS_STATUE );
					CampaignAttackerEx( 1,1,2, MEAT_WAGON );
					SuicideOnPlayerEx(M3,M3,M3,user);
					//*** WAVE 8+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 6,6,8, GHOUL );
					CampaignAttackerEx( 3,3,4, GARGOYLE );
					CampaignAttackerEx( 2,2,2, BLK_SPHINX );
					CampaignAttackerEx( 1,1,2, MEAT_WAGON );
					CampaignAttackerEx( 1,1,1, DREAD_LORD );
					SuicideOnPlayerEx(M3,M3,M3,user);
				}
			}

		} // class n07x06_ai 

	}
