using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public enum GameSceneType
{
//	GST_LOGIN = 0,
    GST_MAIN = 0,
    GST_BATTLE,
	
	GST_COUNT
};


public class W3GameSceneManager : SingletonMonoManager< W3GameSceneManager >
{
    GameDefine.p2_BootyBay_J jass = new GameDefine.p2_BootyBay_J();

    public override void initSingletonMono()
    {
    }


    public bool isLoading = false;

	public void loadScene( GameSceneType l )
	{
		isLoading = true;

		int level = SceneManager.GetActiveScene().buildIndex; 

		// release scene
		switch ( level )
		{
// 			case (int)GameSceneType.GST_LOGIN:
// 			{
// 				LoginScene.instance.unloadScene();
//             }
//             break;
			case (int)GameSceneType.GST_MAIN:
			{
                MainScene.instance.unloadScene();

                W3MapManager.instance.mapFile = "p2_BootyBay";

                GameDefine.initJass();
                jass.config();
            }
                break;
			case (int)GameSceneType.GST_BATTLE:
			{
				BattleScene.instance.unloadScene();
			}
			break;
		}

        loadSceneAsync( (int)l );
	}

	void sceneLoaded()
	{
		int level = SceneManager.GetActiveScene().buildIndex;

        // release scene
        switch ( level )
        {
//             case (int)GameSceneType.GST_LOGIN:
//                 {
//                     LoginScene.instance.loadScene();
// 
//                 }
//                 break;
            case (int)GameSceneType.GST_MAIN:
                {
                    MainScene.instance.loadScene();
//                    jass.main();
                }
                break;
            case (int)GameSceneType.GST_BATTLE:
                {
                    BattleScene.instance.loadScene();
                }
                break;
        }

        isLoading = false;

	}
	
	AsyncOperation async = null;

	public void loadSceneAsync( int s )
	{
        StartCoroutine( loadSceneCoroutine( s ) );
	}

	IEnumerator loadSceneCoroutine( int s )
	{
		yield return new WaitForSeconds( 0.1f );

		async =	SceneManager.LoadSceneAsync( s );
		async.allowSceneActivation = false;
		async.allowSceneActivation = true;

        while ( async.progress != 1 )
        {
            yield return new WaitForEndOfFrame();
        }

        async = null;
        sceneLoaded();
    }


}
