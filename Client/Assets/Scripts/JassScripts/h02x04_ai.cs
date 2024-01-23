// Generated by .

	public partial class GameDefine 
	{

		public class h02x04_ai
		{
		//==================================================================================================
		//  $Id: h02x04.ai,v 1.18 2003/05/07 23:04:17 rpardo Exp $
		//==================================================================================================
			public BJPlayer  user = PlayerEx(1);
		//--------------------------------------------------------------------------------------------------
		//  after_expand
		//--------------------------------------------------------------------------------------------------
			public void after_expand(  )
			{
				// Original JassCode
				// grab all the upgrades we're ever going to get post-quest, then start a post-quest loop
				//
				SetBuildUpgrEx( 1,1,1, UPG_BLK_SPHINX );
				SetBuildUpgrEx( 2,2,3, UPG_UNHOLY_STR );
				SetBuildUpgrEx( 2,2,3, UPG_CR_ATTACK );
				SetBuildUpgrEx( 2,2,3, UPG_UNHOLY_ARMOR );
				SetBuildUpgrEx( 2,2,3, UPG_CR_ARMOR );
				SetBuildUpgrEx( 1,1,1, UPG_FIEND_WEB );
				SetBuildUpgrEx( 0,0,1, UPG_PLAGUE );
				SetBuildUpgrEx( 1,1,1, UPG_GHOUL_FRENZY );
				SetBuildUpgrEx( 1,1,1, UPG_EXHUME );
				SetBuildUpgrEx( 1,1,1, UPG_WYRM_BREATH );
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,2, ABOMINATION );
				CampaignAttackerEx( 4,4,6, GHOUL );
				SuicideOnPlayerEx(M2,M2,M2,user);
				//*** WAVE 2 ***
				InitAssaultGroup();
				CampaignAttackerEx( 2,2,7, GARGOYLE );
				CampaignAttackerEx( 1,1,2, FROST_WYRM );
				CampaignAttackerEx( 1,1,3, BLK_SPHINX );
				SuicideOnPlayerEx(M4,M4,M3,user);
				//*** WAVE 3 ***
				InitAssaultGroup();
				CampaignAttackerEx( 1,1,1, DREAD_LORD );
				CampaignAttackerEx( 4,4,6, GHOUL );
				CampaignAttackerEx( 2,2,3, CRYPT_FIEND );
				CampaignAttackerEx( 1,1,2, MEAT_WAGON );
				SuicideOnPlayerEx(M5,M5,M4,user);
				while( true )
				{
					//*** WAVE 4+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,8, GARGOYLE );
					CampaignAttackerEx( 2,2,4, BLK_SPHINX );
					SuicideOnPlayerEx(M4,M4,M3,user);
					InitAssaultGroup();
					CampaignAttackerEx( 1,1,1, DREAD_LORD );
					CampaignAttackerEx( 3,3,5, ABOMINATION );
					CampaignAttackerEx( 1,1,2, BLK_SPHINX );
					SuicideOnPlayerEx(M5,M5,M4,user);
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,8, GARGOYLE );
					CampaignAttackerEx( 1,1,2, FROST_WYRM );
					CampaignAttackerEx( 1,1,3, BLK_SPHINX );
					SuicideOnPlayerEx(M4,M4,M3,user);
					InitAssaultGroup();
					CampaignAttackerEx( 4,4,6, GHOUL );
					CampaignAttackerEx( 2,2,3, ABOMINATION );
					CampaignAttackerEx( 2,2,3, CRYPT_FIEND );
					CampaignAttackerEx( 1,1,1, MEAT_WAGON );
					CampaignAttackerEx( 0,0,1, DREAD_LORD );
					SuicideOnPlayerEx(M5,M5,M4,user);
				}
			}

		//--------------------------------------------------------------------------------------------------
		//  test_expand
		//--------------------------------------------------------------------------------------------------
			public void test_expand(  )
			{
				// Original JassCode
				if(  CommandsWaiting() > 0  )
				{
					after_expand();
				}
			}

		//--------------------------------------------------------------------------------------------------
		//  main
		//--------------------------------------------------------------------------------------------------
			public void main(  )
			{
				// Original JassCode
				CampaignAI(ZIGGURAT_1,null);
				DoCampaignFarms(false);
				SetRandomPaths(false);
				SetReplacements(2,2,3);
				SetWoodPeons(2);
				SetBuildUnitEx( 2,2,2, UNDEAD_BARGE );
				SetBuildUnitEx( 5,5,5, ACOLYTE );
				SetBuildUpgrEx( 1,1,1, UPG_BLK_SPHINX );
				CampaignDefenderEx( 0,0,1, DREAD_LORD );
				CampaignDefenderEx( 1,1,2, ABOMINATION );
				CampaignDefenderEx( 1,1,1, BLK_SPHINX );
				CampaignDefenderEx( 0,0,1, GARGOYLE );
				CampaignDefenderEx( 0,0,1, CRYPT_FIEND );
				CampaignDefenderEx( 1,1,1, OBS_STATUE );
				WaitForSignal();
				//*** WAVE 1 ***
				InitAssaultGroup();
				CampaignAttackerEx( 3,3,5, GARGOYLE );
				SuicideOnPlayerEx(20,20,20,user);
				test_expand();
				while( true )
				{
					//*** WAVE 2+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 3,3,7, GARGOYLE );
					CampaignAttackerEx( 1,1,2, FROST_WYRM );
					SuicideOnPlayerEx(M2,M2,M2,user);
					test_expand();
					//*** WAVE 3+ ***
					InitAssaultGroup();
					CampaignAttackerEx( 4,4,7, GARGOYLE );
					CampaignAttackerEx( 0,0,1, BLK_SPHINX );
					SuicideOnPlayerEx(M2,M2,M2,user);
					test_expand();
				}
			}

		} // class h02x04_ai 

	}

