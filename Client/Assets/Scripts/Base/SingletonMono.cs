using UnityEngine;
using System.Collections;


public abstract class SingletonMono< T > : MonoBehaviour where T : SingletonMono< T >
{
	static private T mInstance = null;
	static public T instance
	{
		get
		{
			return mInstance;
		}
	}

	private void Awake()
	{
		if( mInstance == null )
		{
			mInstance = this as T;
			mInstance.initSingletonMono();
		}
	}
	
	public virtual void initSingletonMono()
	{

	}

}

