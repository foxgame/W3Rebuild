using UnityEngine;
using System.Collections;


public abstract class SingletonNew< T > where T : new()
{
	static protected T mInstance = default(T);
	static public T instance
	{
		get
		{
			if ( mInstance == null )
			{
				mInstance = new T();
			}
			return mInstance;
		}
	}
	
}
