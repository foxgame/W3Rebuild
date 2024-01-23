// Generated by .

	public partial class GameDefine 
	{

		public class n08_purple_ai
		{
		//============================================================================
		//  Night Elf 8 -- purple player -- AI Script
		//============================================================================
			public BJPlayer  user = Player(1);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ZIGGURAT_1,null);
				SetReplacements(2,2,4);
				SetCaptainHome(ATTACK_CAPTAIN,-2265,-4200 );
				SetCaptainHome(DEFENSE_CAPTAIN,-2453,-5875 );
				campaign_wood_peons = 2;
				SetBuildUnit( 1, NECROPOLIS_1 );
				SetBuildUnit( 1, ACOLYTE );
				SetBuildUnit( 1, UNDEAD_MINE );
				SetBuildUnit( 1, GRAVEYARD );
				SetBuildUnit( 8, ZIGGURAT_1 );
				SetBuildUnit( 3, CRYPT );
				SetBuildUnit( 1, UNDEAD_ALTAR );
				SetBuildUnit( 1, NECROPOLIS_2 );
				SetBuildUnit( 3, SLAUGHTERHOUSE );
				SetBuildUnit( 2, DAMNED_TEMPLE );
				SetBuildUnit( 1, NECROPOLIS_3 );
				SetBuildUnit( 1, BONEYARD );
				SetBuildUnit( 5, ACOLYTE );
				SetBuildUnit( 8, ZIGGURAT_2 );
				CampaignDefenderEx( 1,1,2, ABOMINATION );
				CampaignDefenderEx( 1,1,1, NECRO );
				CampaignDefenderEx( 2,2,2, GHOUL );
				CampaignDefenderEx( 1,1,1, CRYPT_FIEND );
				CampaignDefenderEx( 1,1,1, LICH );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,6, GHOUL );
				CampaignAttackerEx( 2,2,3, ABOMINATION );
				CampaignAttackerEx( 2,2,3, NECRO );
				SuicideOnPlayerEx(M2,M2,M1,user);
				SetBuildUpgrEx( 1,1,1, UPG_FIEND_WEB );
				SetBuildUpgrEx( 1,1,1, UPG_CANNIBALIZE );
				SetBuildUpgrEx( 1,1,1, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 1,1,1, UPG_CR_ATTACK );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,4, GHOUL );
				CampaignAttackerEx( 1,1,2, ABOMINATION );
				CampaignAttackerEx( 2,2,4, CRYPT_FIEND );
				CampaignAttackerEx( 1,1,2, MEAT_WAGON );
				SuicideOnPlayerEx(M6,M6,M4,user);
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,5, GHOUL );
				CampaignAttackerEx( 2,2,3, ABOMINATION );
				CampaignAttackerEx( 1,1,2, NECRO );
				CampaignAttackerEx( 1,1,2, BANSHEE );
				CampaignAttackerEx( 1,1,1, LICH );
				SuicideOnPlayerEx(M6,M6,M4,user);
				SetBuildUpgrEx( 2,2,2, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 2,2,2, UPG_CR_ATTACK );
				SetBuildUpgrEx( 1,1,1, UPG_SKEL_LIFE );
				SetBuildUpgrEx( 1,1,1, UPG_WYRM_BREATH );
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,4, CRYPT_FIEND );
				CampaignAttackerEx( 4,4,6, ABOMINATION );
				CampaignAttackerEx( 2,2,4, MEAT_WAGON );
				SuicideOnPlayerEx(M6,M6,M4,user);
				SetBuildUpgrEx( 1,1,1, UPG_PLAGUE );
				SetBuildUpgrEx( 2,2,2, UPG_UNHOLY_ARMOR);
				SetBuildUpgrEx( 2,2,2, UPG_CR_ARMOR );
				SetBuildUpgrEx( 2,2,2, UPG_NECROS );
				SetBuildUpgrEx( 2,2,2, UPG_BANSHEE );
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,5, ABOMINATION );
				CampaignAttackerEx( 4,4,6, NECRO );
				CampaignAttackerEx( 1,1,1, LICH );
				CampaignAttackerEx( 1,1,2, MEAT_WAGON );
				SuicideOnPlayerEx(M6,M6,M4,user);
				SetBuildUpgrEx( 2,2,3, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 2,2,3, UPG_CR_ATTACK );
				SetBuildUpgrEx( 1,1,1, UPG_GHOUL_FRENZY);
				//*** WAVE 6 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,5, ABOMINATION );
				CampaignAttackerEx( 4,4,6, BANSHEE );
				CampaignAttackerEx( 1,1,1, LICH );
				CampaignAttackerEx( 1,1,2, MEAT_WAGON );
				SuicideOnPlayerEx(M6,M6,M4,user);
				SetBuildUpgrEx( 2,2,3, UPG_UNHOLY_ARMOR);
				SetBuildUpgrEx( 2,2,3, UPG_CR_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_MEAT_WAGON );
				while( true )
				{
					//*** WAVE 7+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 10,10,12, GHOUL );
					CampaignAttackerEx( 2,2,3, ABOMINATION );
					CampaignAttackerEx( 2,2,3, NECRO );
					CampaignAttackerEx( 1,1,1, LICH );
					CampaignAttackerEx( 1,1,2, MEAT_WAGON );
					SuicideOnPlayerEx(M6,M6,M4,user);
					//*** WAVE 8+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,5, ABOMINATION );
					CampaignAttackerEx( 3,3,4, CRYPT_FIEND );
					CampaignAttackerEx( 4,4,6, BANSHEE );
					CampaignAttackerEx( 1,1,1, LICH );
					CampaignAttackerEx( 2,2,3, MEAT_WAGON );
					SuicideOnPlayerEx(M6,M6,M4,user);
				}
			}

		} // class n08_purple_ai 

	}
