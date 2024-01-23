using System;
using UnityEngine;
using System.Collections.Generic;



[ System.Serializable ]
public class W3AnimationVisiblityT
{
	public float time;
	public string name;
	public float v;
    public bool remove;
}

[ System.Serializable ]
public class W3AnimationVisiblityF
{
	public W3AnimationType type;
	public List< W3AnimationVisiblityT > f;
}

[ System.Serializable ]
public class W3AnimationVisiblity : MonoBehaviour
{
	public List< W3AnimationVisiblityF > frames;
}


