using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class W3TeamGlow : MonoBehaviour 
{
    public int type;

//     void Start()
//     {
//         UpdateColor( type );
//     }

    public void UpdateColor( int t )
	{
		Renderer renderer = GetComponent< Renderer >();
		renderer.sharedMaterial = W3GameColorManager.instance.GetMaterialTeamGlow( t );
	}

}

