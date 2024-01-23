using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class GameHandlerManager< T > : Singleton< T >
{
	public Dictionary< string , GameHandler > uiDic = new Dictionary< string , GameHandler >();


	public void releaseUnusedHandler()
	{
		foreach ( KeyValuePair< string, GameHandler > a in uiDic )
		{ 
			a.Value.ReleaseUnused();
		}
	}
	
	
	public void setHandler( string name , GameHandler handler )
	{
		uiDic[ name ] = handler;
	}



}
