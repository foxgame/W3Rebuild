using UnityEngine;


public class W3WaterManager : SingletonMono< W3WaterManager >
{
    int index = 0;
    float time = 1.0f;

    public Material materialObj = null;

    Texture2D[] textures = new Texture2D[ 45 ];

    public void initWaterTextures()
    {
        for ( int i = 0 ; i < 45 ; i++ )
        {
            string str = i < 10 ? ( "0" + i ) : i.ToString();
            textures[ i ] = (Texture2D)Resources.Load( "ReplaceableTextures/Water/Water" + str );
        }
    }

    void FixedUpdate()
    {
        if ( materialObj != null )
        {
            WaterUpdate();
        }
    }

    void WaterUpdate()
    {
        time += Time.deltaTime;

        if ( time > 0.1f )
        {
            materialObj.mainTexture = textures[ index ];

            index++;

            if ( index >= 45 )
            {
                index = 0;
            }

            time = 0.0f;
        }

    }
}
