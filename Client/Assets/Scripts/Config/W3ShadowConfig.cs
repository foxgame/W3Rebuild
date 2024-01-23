using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class W3ShadowConfig : SingletonMono< W3ShadowConfig >
{
    public float[] data = new float[ 32 ];

    public enum W3ShadowType
    {
        Null = 0,
        Whole = 1 ,
        Partial = 2,
        Bound = 4,
        
        Count
    }

    public byte getData( byte b )
    {
        if ( b == (byte)W3ShadowType.Null )
        {
            return 255;
        }

        return 150;
    }

    void Start()
    {
        data[ 0 ] = 1.0f;
        data[ 1 ] = 0.6f;
        data[ 2 ] = 0.6f;
        data[ 4 ] = 0.6f;

    }

}


