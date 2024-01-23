using System;
using UnityEngine;

[ System.Serializable ]
public class W3TerrainNode
{
	public enum Type
	{
		bottomRight = 0,
		bottomLeft = 1,
		topRight = 2,
		topLeft = 3,

		count = 4,

		topRR = 5,
		bottomRR = 6,
		ttLeft = 7,
		ttRight = 8,

		ccount = 9
	}

	public int x;
	public int z;

    public float y;

	public short waterLevel;

    public sbyte layerHeight;

    public byte flags;
    
    public byte textureTypeSize;
    public byte[] textureType;
    public byte[] textureUV;

    public byte waterType;
    public float waterY;
    public Vector2[] waterColor;
    public sbyte waterShoreLine;

    public byte shadow;
    public byte mask;
}

