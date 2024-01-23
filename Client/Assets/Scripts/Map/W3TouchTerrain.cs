using UnityEngine;
using System.Collections;

public class W3TouchTerrain : MonoBehaviour 
{
	private bool useMouse = true;
	private bool useTouch = true;
	private bool isEditor = false;

	//public GameAtlasAsset gameAtlasAsset = null;

	static public Vector2 lastTouchPosition = Vector2.zero;

	static public MouseOrTouch currentTouch = null;

	static public int currentTouchID = -1;
	static public bool isTouch = false;

	private Camera touchCamera = null;

	public class MouseOrTouch
	{
		public Vector2 pos;			// Current position of the mouse or touch event
		public Vector2 delta;		// Delta since last update
		public Vector2 totalDelta;	// Delta since the event started being tracked
	}

	static MouseOrTouch[] mMouse = new MouseOrTouch[] { new MouseOrTouch(), new MouseOrTouch(), new MouseOrTouch() };

	void Start()
	{		
		//GameAtlas gameAtlas = gameAtlasAsset.GetGameAtlas();
		//AtlasRegion region = gameAtlas.FindRegion( "1" );

		if (Application.platform == RuntimePlatform.Android ||
			Application.platform == RuntimePlatform.IPhonePlayer)
		{
			useMouse = false;
			useTouch = true;
		}
		else if (Application.platform == RuntimePlatform.WindowsEditor ||
			Application.platform == RuntimePlatform.OSXEditor )
		{
			isEditor = true;
		}

		touchCamera = GameObject.FindWithTag( "MainCamera" ).GetComponent< Camera >();
	}


	void ProcessMouse ()
	{
		bool updateRaycast = (Time.timeScale < 0.9f);

		if (!updateRaycast)
		{
			for (int i = 0; i < 3; ++i)
			{
				if (Input.GetMouseButton(i) || Input.GetMouseButtonUp(i))
				{
					updateRaycast = true;
					break;
				}
			}
		}

		// Update the position and delta
		mMouse[0].pos = Input.mousePosition;
		mMouse[0].delta = mMouse[0].pos - lastTouchPosition;

		bool posChanged = (mMouse[0].pos != lastTouchPosition);
		lastTouchPosition = mMouse[0].pos;

		// Update the object under the mouse
		//if (updateRaycast) mMouse[0].current = Raycast(Input.mousePosition, ref lastHit) ? lastHit.collider.gameObject : fallThrough;

		// Propagate the updates to the other mouse buttons
		for (int i = 1; i < 3; ++i)
		{
			mMouse[i].pos = mMouse[0].pos;
			mMouse[i].delta = mMouse[0].delta;
			//mMouse[i].current = mMouse[0].current;
		}

		// Is any button currently pressed?
		bool isPressed = false;

		for (int i = 0; i < 3; ++i)
		{
			if (Input.GetMouseButton(i))
			{
				isPressed = true;
				break;
			}
		}


		// Process all 3 mouse buttons as individual touches
		for (int i = 0; i < 1; ++i)
		{
			bool pressed = Input.GetMouseButtonDown(i);
			bool unpressed = Input.GetMouseButtonUp(i);

			currentTouch = mMouse[i];
			currentTouchID = -1 - i;

			ProcessTouch(pressed, unpressed);
		}

		currentTouch = null;
	}

	void UpdateBound()
	{

	}

	void onTouch()
	{
        Ray ray1 = touchCamera.ScreenPointToRay( lastTouchPosition );

		RaycastHit hit;

		LayerMask l = 1 << LayerMask.NameToLayer( "Terrain" );

		if ( !Physics.Raycast( ray1 , out hit , 2000 , l ) )
		{
			return;
		}

		if ( hit.collider.gameObject != null )
		{
            W3TerrainSmallNode sn = W3TerrainManager.instance.getSmallNode( (int)-hit.point.x , (int)-hit.point.z );

            Vector3 pos = new Vector3( hit.point.x , sn.ym , hit.point.z );

            GameObject obj1 = (GameObject)Resources.Load( "Prefabs/Units/Human/Footman/Footman" );
            GameObject obj = Instantiate( obj1 );
            obj.transform.position = pos;
            obj.transform.eulerAngles = new Vector3( 0.0f , Random.Range( 0.0f , 360.0f ) , 0.0f );

            //W3MapManager.instance.calculateFOV( (int)-hit.point.x , (int)-hit.point.z , 20.0f , 2.0f );
        }
	}

	void onMouseMove()
	{
// 		Ray ray1 = touchCamera.ScreenPointToRay( lastTouchPosition );
// 
// 		RaycastHit hit;
// 
// 		LayerMask l = 1 << LayerMask.NameToLayer( "Terrain" );
// 
// 		if ( !Physics.Raycast( ray1 , out hit , 100 , l ) )
// 		{
// 			return;
// 		}
// 
// 		if ( hit.collider.gameObject != null )
// 		{
// 			W3TerrainSprite spr = hit.collider.gameObject.GetComponent< W3TerrainSprite >();
// 
// //			if ( lastSprite == spr )
// //			{
// //				return;
// //			}
// 
// 			if ( spr != null )
// 			{
// 				W3TerrainGrid grid = W3MapManager.instance.getGrid( (int)-hit.point.x , (int)-hit.point.z );
// 
// 				W3BuildingSelection.instance.setPos( (int)hit.point.x , grid.height , (int)hit.point.z );
// 
// //				W3Selection.instance.highLightTerrain( spr );
// 
// //				lastSprite = spr;
// 
// 				if ( isTouch )
// 				{
// //					W3Selection.instance.setTerrain( spr );
// 
// //					W3BuildingSelection.instance.setPos(  );
// 				}
// 			}
// 		}
	}


	void ProcessTouch ( bool pressed , bool unpressed )
	{
		if ( pressed )
		{
			isTouch = true;
		}

		if ( unpressed )
		{
			isTouch = false;

			onTouch();
		}
	}


	void Update()
	{
		onMouseMove();

		if ( useMouse || ( useTouch && isEditor ) ) 
		{
			ProcessMouse();
		}
		else
		{
			if ( Input.touchCount > 0 )
			{
				isTouch = true;			

				lastTouchPosition = Input.GetTouch( 0 ).position;
			}
			else
			{
				if ( isTouch )
				{
					isTouch = false;

					onTouch();
				}
			}
		}


	}

}

