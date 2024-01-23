using System;
using UnityEngine;

public class W3UVAnimation : MonoBehaviour
{
	public int x = 0;
	public int y = 0;

	public float delay = 0.0f;

	bool start = false;
	int index = 0;
	float delayTime = 0.0f;

	float uvx = 0.0f;
	float uvy = 0.0f;

	Mesh mesh = null;

	public void startAnimation()
	{
		index = 0;
		delayTime = 0.0f;
		start = true;

		uvx = 1.0f / x;
		uvy = 1.0f / y;

		SkinnedMeshRenderer r = transform.GetComponent< SkinnedMeshRenderer >();
		mesh = r.sharedMesh;

		updateUV();
	}

	void updateUV()
	{
		int max = x * y;

		int ux = index % x;
		int uy = y - index / x - 1;

		float uv0 = ux * uvx;
		float uv1 = uy * uvy;

		Debug.Log( ux + " " + uy + " " + uv0 + " " + uv1 );


		Vector2[] uv = new Vector2[ mesh.uv.Length ];

		for ( int i = 0 ; i < mesh.uv.Length / 4 ; i++ )
		{
			uv[ i * 4 + 0 ].x = uv0;
			uv[ i * 4 + 0 ].y = uv1 + uvy ;
			uv[ i * 4 + 1 ].x = uv0 + uvx;
			uv[ i * 4 + 1 ].y = uv1 + uvy;

			uv[ i * 4 + 2 ].x = uv0;
			uv[ i * 4 + 2 ].y = uv1;
			uv[ i * 4 + 3 ].x = uv0 + uvx;
			uv[ i * 4 + 3 ].y = uv1;
		}

		mesh.uv = uv;
	}

	void Start()
	{
		startAnimation();
	}

	void Update()
	{
		if ( !start )
		{
			return;
		}

		delayTime += Time.fixedDeltaTime;

		if ( delayTime > delay )
		{
			delayTime -= delay;

			index++;

			if ( index > x * y )
			{
				index = 0;
			}

			updateUV();
		}
	}
}
