using System;
using UnityEngine;
using System.Collections.Generic;




[ System.Serializable ]
public class W3TerrainMeshVertex : MonoBehaviour
{
	public sbyte[] v;
	public sbyte[] v2;

	public List< sbyte > nBLTL;
	public List< sbyte > nTLTR;
	public List< sbyte > nTRBR;
	public List< sbyte > nBRBL;

	public List< sbyte > nM;

}

