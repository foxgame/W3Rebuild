// Generated by .


	public partial class GameDefine 
	{

		public class u03x04_ai
		{
		//============================================================================
		//  $Id: u03x04.ai,v 1.4.2.1 2003/05/09 09:17:04 abond Exp $
		//============================================================================
			public BJPlayer  user = PlayerEx(2);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ZIGGURAT_1,null);
				SetReplacements(1,1,3);
				campaign_wood_peons = 2;
				SetBuildUnitEx( 1, 1, 1, ACOLYTE );
				SetBuildUnitEx( 0, 0, 1, NECROPOLIS_1 );
				SetBuildUnitEx( 0, 0, 1, CRYPT );
				SetBuildUnitEx( 0, 0, 5, ZIGGURAT_1 );
				SetBuildUnitEx( 0, 0, 1, GRAVEYARD );
				SetBuildUnitEx( 0, 0, 1, UNDEAD_ALTAR );
				SetBuildUnitEx( 0, 0, 1, NECROPOLIS_2 );
				SetBuildUnitEx( 0, 0, 1, SLAUGHTERHOUSE );
				SetBuildUnitEx( 0, 0, 1, DAMNED_TEMPLE );
				SetBuildUnitEx( 0, 0, 4, ZIGGURAT_2 );
				SetBuildUnitEx( 0, 0, 1, ZIGGURAT_FROST );
				SetBuildUnitEx( 0, 0, 1, NECROPOLIS_3 );
				SetBuildUnitEx( 5, 5, 5, ACOLYTE );
				CampaignDefenderEx( 1, 1, 1, ABOMINATION );
				CampaignDefenderEx( 1, 1, 1, CRYPT_FIEND );
				SetBuildUpgrEx( 0,0,1, UPG_GHOUL_FRENZY);
				SetBuildUpgrEx( 1,1,1, UPG_FIEND_WEB );
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,4, ABOMINATION );
				SuicideOnPlayerEx(M4,M4,M3,user);
				SetBuildUpgrEx( 0,0,1, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 0,1,1, UPG_CR_ATTACK );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,2, GHOUL );
				CampaignAttackerEx( 3,3,4, CRYPT_FIEND );
				SuicideOnPlayerEx(M5,M5,M3,user);
				SetBuildUpgrEx( 0,0,1, UPG_UNHOLY_ARMOR);
				SetBuildUpgrEx( 0,0,1, UPG_CR_ARMOR );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,4, ABOMINATION );
				CampaignAttackerEx( 3,3,4, CRYPT_FIEND );
				SuicideOnPlayerEx(M5,M5,M3,user);
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,2, GHOUL );
				CampaignAttackerEx( 2,2,4, ABOMINATION );
				CampaignAttackerEx( 1,1,2, MEAT_WAGON );
				SuicideOnPlayerEx(M5,M5,M3,user);
				SetBuildUpgrEx( 1,1,2, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 1,1,2, UPG_CR_ATTACK );
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,2, GHOUL );
				CampaignAttackerEx( 5,5,7, CRYPT_FIEND );
				CampaignAttackerEx( 1,1,2, MEAT_WAGON );
				SuicideOnPlayerEx(M5,M5,M3,user);
				SetBuildUpgrEx( 1,1,2, UPG_UNHOLY_ARMOR);
				SetBuildUpgrEx( 1,1,2, UPG_CR_ARMOR );
				//*** WAVE 6 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,5, ABOMINATION );
				CampaignAttackerEx( 3,3,4, CRYPT_FIEND );
				SuicideOnPlayerEx(M5,M5,M3,user);
				SetBuildUpgrEx( 2,2,3, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 2,2,3, UPG_CR_ATTACK );
				SetBuildUpgrEx( 1,1,1, UPG_GHOUL_FRENZY);
				//*** WAVE 7+ ***
				while( true )
				{
					InitAssaultGroup();
					CampaignAttackerEx( 2,2,2, GHOUL );
					CampaignAttackerEx( 5,5,7, ABOMINATION );
					SuicideOnPlayerEx(M5,M5,M3,user);
					InitAssaultGroup();
					CampaignAttackerEx( 2,2,2, GHOUL );
					CampaignAttackerEx( 5,5,7, CRYPT_FIEND );
					CampaignAttackerEx( 1,1,2, MEAT_WAGON );
					SuicideOnPlayerEx(M5,M5,M3,user);
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,5, ABOMINATION );
					CampaignAttackerEx( 3,3,4, CRYPT_FIEND );
					SuicideOnPlayerEx(M5,M5,M3,user);
				}
			}

		} // class u03x04_ai 

	}

