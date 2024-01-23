using UnityEngine;
using System;

public class W3Propeller : MonoBehaviour
{
	public float speed = 1.0f;

	public void FixedUpdate()
	{
		transform.localEulerAngles = new Vector3( transform.localEulerAngles.x ,
			transform.localEulerAngles.y , transform.localEulerAngles.z + Time.fixedDeltaTime * speed );
	}

}