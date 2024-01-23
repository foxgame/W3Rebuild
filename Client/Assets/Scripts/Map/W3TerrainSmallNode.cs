using System;
using UnityEngine;

public enum W3PathType
{
    NOWALK = 2,
    NOFLY = 4,
    NOBUILD = 8,
    BLIGHT = 32,
    NOWATER = 64,
    UNKNOW = 128
};

[ System.Serializable ]
public class W3TerrainSmallNode
{
    public int x;
    public int z;

    public float ym;
    public float y;

    public int doodadID = 0;
 //   public int unitID = 0;

    public byte shadow = 0;
    public byte path = 0;
    public byte pathRegion = 0;
    public byte unitHeight = 0;

}
