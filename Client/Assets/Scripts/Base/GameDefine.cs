using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.GZip; 
using ICSharpCode.SharpZipLib.Zip; 
using System.IO;
using System;


public enum W3GameError
{
    GAME_NO_ERROR = 0,

};


public partial class GameDefine
{
    public const byte NOWALK = 2;
    public const byte NOFLY = 4;
    public const byte NOBUILD = 8;
    public const byte BLIGHT = 32;
    public const byte NOWATER = 64;
    public const byte UNKNOW = 128;


    public const int TERRAIN_SIZE_PER_HALF = 16;
    public const int TERRAIN_SIZE_PER = 32;
    public const int TERRAIN_SIZE = 128;
    public const int INVALID_ID = -1;
    public const int MAX_PLAYER = 16;

    public const float W3SCALE = 1.574912f;
    public const float W3SCALE1 = 2.54f * 32;

    public const byte SHADOW_VALUE = 150;

    public static byte[] Compress( byte[] bytesToCompress )
    {
        byte[] rebyte = null;
        MemoryStream ms = new MemoryStream();

        GZipOutputStream s = new GZipOutputStream( ms );

        try
        {
            s.Write( bytesToCompress , 0 , bytesToCompress.Length );
            s.Flush();
            s.Finish();
        }
        catch ( System.Exception ex )
        {
#if UNITY_EDITOR
            Debug.Log( ex );
#endif
        }

        ms.Seek( 0 , SeekOrigin.Begin );

        rebyte = ms.ToArray();

        s.Close();
        ms.Close();

        s.Dispose();
        ms.Dispose();

        return rebyte;
    }

    public static byte[] DeCompress( byte[] bytesToDeCompress )
    {
        byte[] rebyte = new byte[ bytesToDeCompress.Length * 20 ];

        MemoryStream ms = new MemoryStream( bytesToDeCompress );
        MemoryStream outStream = new MemoryStream();

        GZipInputStream s = new GZipInputStream( ms );

        int read = s.Read( rebyte , 0 , rebyte.Length );
        while ( read > 0 )
        {
            outStream.Write( rebyte , 0 , read );
            read = s.Read( rebyte , 0 , rebyte.Length );
        }

        byte[] rebyte1 = outStream.ToArray();

        ms.Close();
        s.Close();
        outStream.Close();

        ms.Dispose();
        s.Dispose();
        outStream.Dispose();

        bytesToDeCompress = null;
        rebyte = null;

        return rebyte1;
    }



}
