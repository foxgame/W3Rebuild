// Generated by .


	public partial class GameDefine 
	{

		public class n08x06_ai
		{
		//============================================================================
		//  $Id: n08x06.ai,v 1.25.2.1 2003/05/09 09:17:04 abond Exp $
		//============================================================================
			public BJPlayer  user = PlayerEx(4);
		//============================================================================
		//  main
		//============================================================================
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ZIGGURAT_1,null);
				SetReplacements(1,1,3);
				DoCampaignFarms(false);
				SetWoodPeons(2);
				SetBuildUnitEx( 1, 1, 1, ACOLYTE );
				SetBuildUnitEx( 0, 0, 1, NECROPOLIS_1 );
				SetBuildUnitEx( 0, 0, 1, UNDEAD_MINE );
				SetBuildUnitEx( 0, 0, 2, CRYPT );
				SetBuildUnitEx( 0, 0, 3, ZIGGURAT_1 );
				SetBuildUnitEx( 0, 0, 1, GRAVEYARD );
				SetBuildUnitEx( 0, 0, 1, UNDEAD_ALTAR );
				SetBuildUnitEx( 0, 0, 1, NECROPOLIS_2 );
				SetBuildUnitEx( 0, 0, 3, ZIGGURAT_2 );
				SetBuildUnitEx( 0, 0, 2, SLAUGHTERHOUSE );
				SetBuildUnitEx( 0, 0, 1, SAC_PIT );
				SetBuildUnitEx( 0, 0, 1, NECROPOLIS_3 );
				SetBuildUnitEx( 0, 0, 1, BONEYARD );
				SetBuildUnitEx( 5, 5, 5, ACOLYTE );
				SetBuildUnitEx( 2, 2, 2, UNDEAD_BARGE );
				SetBuildUpgrEx( 1, 1, 1, UPG_BLK_SPHINX );
				CampaignDefenderEx( 1, 1, 1, DEATH_KNIGHT );
				CampaignDefenderEx( 1, 1, 1, ABOMINATION );
				CampaignDefenderEx( 1, 1, 1, OBS_STATUE );
				CampaignDefenderEx( 1, 1, 1, FROST_WYRM );
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 5,5,6, GARGOYLE );
				SuicideOnPlayerEx(M6,M6,M4,user);
				SetBuildUpgrEx( 0,0,1, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 0,1,1, UPG_CR_ATTACK );
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,1, MEAT_WAGON );
				CampaignAttackerEx( 6,6,8, GHOUL );
				CampaignAttackerEx( 0,0,1, DEATH_KNIGHT);
				SuicideOnPlayerEx(M7,M7,M5,user);
				SetBuildUpgrEx( 0,0,1, UPG_UNHOLY_ARMOR);
				SetBuildUpgrEx( 0,0,1, UPG_CR_ARMOR );
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 5,5,6, GARGOYLE );
				CampaignAttackerEx( 1,1,1, BLK_SPHINX );
				SuicideOnPlayerEx(M7,M7,M5,user);
				SetBuildUpgrEx( 0,0,1, UPG_PLAGUE );
				SetBuildUpgrEx( 0,0,1, UPG_GHOUL_FRENZY);
				SetBuildUpgrEx( 1,1,1, UPG_WYRM_BREATH );
				//*** WAVE 4 ***
				InitAssaultGroup();
				CampaignAttackerEx( 0,0,1, MEAT_WAGON );
				CampaignAttackerEx( 4,4,4, ABOMINATION );
				CampaignAttackerEx( 1,1,1, DEATH_KNIGHT);
				SuicideOnPlayerEx(M7,M7,M5,user);
				SetBuildUpgrEx( 1,1,2, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 1,1,2, UPG_CR_ATTACK );
				//*** WAVE 5 ***
				InitAssaultGroup();
				CampaignAttackerEx( 4,4,5, GARGOYLE );
				CampaignAttackerEx( 2,2,3, ABOMINATION );
				CampaignAttackerEx( 1,1,1, MEAT_WAGON );
				SuicideOnPlayerEx(M7,M7,M5,user);
				SetBuildUpgrEx( 1,1,2, UPG_UNHOLY_ARMOR);
				SetBuildUpgrEx( 1,1,2, UPG_CR_ARMOR );
				//*** WAVE 6 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,1, DEATH_KNIGHT);
				CampaignAttackerEx( 4,4,6, GHOUL );
				CampaignAttackerEx( 2,2,4, ABOMINATION );
				CampaignAttackerEx( 0,0,1, MEAT_WAGON );
				SuicideOnPlayerEx(M7,M7,M5,user);
				SetBuildUpgrEx( 2,2,3, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 2,2,3, UPG_CR_ATTACK );
				SetBuildUpgrEx( 1,1,1, UPG_GHOUL_FRENZY);
				SetBuildUpgrEx( 1,1,1, UPG_PLAGUE );
				//*** WAVE 7+ ***
				while( true )
				{
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,4, GARGOYLE );
					CampaignAttackerEx( 1,1,2, FROST_WYRM );
					CampaignAttackerEx( 1,1,1, BLK_SPHINX );
					SuicideOnPlayerEx(M7,M7,M5,user);
					InitAssaultGroup();
					CampaignAttackerEx( 1,1,1, DEATH_KNIGHT);
					CampaignAttackerEx( 4,4,6, ABOMINATION );
					CampaignAttackerEx( 1,1,2, MEAT_WAGON );
					SuicideOnPlayerEx(M7,M7,M5,user);
					InitAssaultGroup();
					InitAssaultGroup();
					CampaignAttackerEx( 5,5,7, GARGOYLE );
					CampaignAttackerEx( 3,3,4, BLK_SPHINX );
					SuicideOnPlayerEx(M7,M7,M5,user);
					InitAssaultGroup();
					CampaignAttackerEx( 1,1,1, DEATH_KNIGHT);
					CampaignAttackerEx( 8,8,12, GHOUL );
					CampaignAttackerEx( 1,1,2, MEAT_WAGON );
					SuicideOnPlayerEx(M7,M7,M5,user);
				}
			}

		} // class n08x06_ai 

	}

