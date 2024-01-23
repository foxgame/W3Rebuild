using UnityEngine;
using System;
using System.IO;
using System.Runtime;
using System.Collections;
using System.Runtime.InteropServices;


public class W3Texture
{
	public Texture2D texture2D = null;

	public void load( string path )
	{
		texture2D = (Texture2D)Resources.Load( path );
	}
}



