using UnityEngine;
using System.Collections;


public abstract class Singleton< T > : MonoBehaviour 
{
	static public T mInstance = default(T);
	static public T instance
	{
		get
		{
			return mInstance;
		}
	}



}

