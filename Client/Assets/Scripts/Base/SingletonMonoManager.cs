using UnityEngine;
using System.Collections;


public abstract class SingletonMonoManager< T > : MonoBehaviour where T : SingletonMonoManager< T >
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
		if ( mInstance == null )
		{
			mInstance = this as T;
			DontDestroyOnLoad( gameObject );
			mInstance.initSingletonMono();
		}
		else
		{
			Destroy( gameObject );
		}
	}
	
	public virtual void initSingletonMono()
	{
		
	}
	
}

