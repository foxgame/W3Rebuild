using System;
using UnityEngine;
using System.Collections.Generic;

[ System.Serializable ]
public class W3AnimationAlphaT
{
	public float time;
	public float alpha;
}

[ System.Serializable ]
public class W3AnimationAlphaF
{
	public W3AnimationType type;
	public string material;
	public int texture;
	public float alpha;
	public List< W3AnimationAlphaT > f;
}

[ System.Serializable ]
public class W3AnimationAlpha : MonoBehaviour
{
	public List< W3AnimationAlphaF > frames;
}

