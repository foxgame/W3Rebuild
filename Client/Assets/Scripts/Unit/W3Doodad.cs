using System;
using UnityEngine;

public class W3Doodad : W3Base
{
    W3AnimationController ac;

    public int width;
    public int height;




    void updataAnimation( W3AnimationType type , bool enabled , float time )
    {
        if ( type == W3AnimationType.Birth && enabled == false )
        {
            ac.play( W3AnimationType.Stand );
        }
    }

    void Awake()
    {
        ac = GetComponent<W3AnimationController>();
        ac.callback = updataAnimation;
    }

    void Start()
    {
        if ( startAnimation )
        {
            if ( animationType == W3AnimationType.none )
            {
                return;
            }

            if ( randomPlay )
                ac.randomPlay( animationType );
            else
                ac.play( animationType );
        }
        else
        {
            ac.noPlay( animationType );
            ac.enable( false );
        }
    }

    public void playAnimation( W3AnimationType t )
    {
        ac.play( t );
    }

    public void pauseAnimation( bool b )
    {
        ac.pause( b );
    }

    public void selection()
    {
        SkinnedMeshRenderer[] r = GetComponentsInChildren<SkinnedMeshRenderer>();

        for ( int i = 0 ; i < r.Length ; i++ )
        {
            string shaderName = "Sprites/Diffuse";

            Shader shader = Shader.Find( shaderName );

            r[ i ].sharedMaterial.shader = shader;
        }
    }

}

