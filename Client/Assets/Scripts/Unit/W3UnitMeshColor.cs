using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class W3UnitMeshColor : MonoBehaviour 
{
	public Texture2D texture2d;
	public int type;

    public bool noColor = false;

// 	void Awake()
// 	{
// //         if ( !noColor )
// //             UpdateColor( type );
//     }

    public void UpdateColor( int t )
	{
		Renderer renderer = GetComponent< Renderer >();
		renderer.sharedMaterial = W3GameColorManager.instance.GetMaterialUnitMeshColor( t , texture2d );
	}

}

