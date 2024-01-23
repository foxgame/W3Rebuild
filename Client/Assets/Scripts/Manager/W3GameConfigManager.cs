using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum W3Custom
{
    ReignofChaos = 0,
    FrozenThrone = 1,

    Count = 2        
}

public class W3GameConfigManager : SingletonMonoManager<W3GameConfigManager>
{
	[ HideInInspector ]
	public bool IsLoaded = false;

    public W3Custom custom = W3Custom.ReignofChaos;

    public override void initSingletonMono()
	{
		loadAll();
	}

	public void loadAll()
	{
		IsLoaded = true;

		// load complete
//		LoginScene.instance.loadScene();
	}

}

