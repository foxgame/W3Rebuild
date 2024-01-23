using UnityEngine;
using System.Collections;



public class GameCache : SingletonMono< GameCache >
{
	public override void initSingletonMono()
	{

	}

	public void addCache( GameObject obj )
	{
		obj.transform.parent = transform;
		obj.SetActive( false );
	}



	
}
