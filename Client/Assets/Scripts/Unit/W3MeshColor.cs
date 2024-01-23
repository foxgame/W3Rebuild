using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class W3MeshColor : MonoBehaviour
{
    public Texture2D texture2d;
    public int type;

//     void Start()
//     {
//         UpdateColor( type );
//     }

    public void UpdateColor( int t )
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sharedMaterial = W3GameColorManager.instance.GetMaterialMeshColor( t , texture2d );
    }

}

