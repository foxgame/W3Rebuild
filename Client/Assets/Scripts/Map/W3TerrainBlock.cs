using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W3TerrainBlock : MonoBehaviour
{

    public void setBlock( byte b )
    {
        MeshRenderer render = GetComponent<MeshRenderer>();
        render.material = Instantiate( (Material)Resources.Load( "Materials/TerrainBlock" ) );

        Color color = new Color( 0.0f , 0.0f , 0.0f , 0.2f );

        if ( ( b & GameDefine.NOWALK ) == GameDefine.NOWALK )
        {
            color.r = 1.0f;
        }
        if ( ( b & GameDefine.NOFLY ) == GameDefine.NOFLY )
        {
            color.g = 1.0f;
        }
        if ( ( b & GameDefine.NOBUILD ) == GameDefine.NOBUILD )
        {
            color.b = 1.0f;
        }

        render.material.color = color;
    }

}

