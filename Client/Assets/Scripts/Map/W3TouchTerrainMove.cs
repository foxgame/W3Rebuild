using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class W3TouchTerrainMove : MonoBehaviour 
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

    int time = 0;

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
		mMouse[1].pos = Input.mousePosition;
		mMouse[1].delta = mMouse[1].pos - lastTouchPosition;

		bool posChanged = (mMouse[1].pos != lastTouchPosition);
		lastTouchPosition = mMouse[1].pos;

		// Update the object under the mouse
		//if (updateRaycast) mMouse[0].current = Raycast(Input.mousePosition, ref lastHit) ? lastHit.collider.gameObject : fallThrough;

		// Propagate the updates to the other mouse buttons
// 		for (int i = 1; i < 3; ++i)
// 		{
// 			mMouse[i].pos = mMouse[1].pos;
// 			mMouse[i].delta = mMouse[1].delta;
// 			//mMouse[i].current = mMouse[1].current;
// 		}

		// Is any button currently pressed?
		bool isPressed = false;

        if ( Input.GetMouseButton( 1 ) )
        {
            isPressed = true;
        }

        // Process all 3 mouse buttons as individual touches

        bool pressed0 = Input.GetMouseButtonDown( 0 );
        bool unpressed0 = Input.GetMouseButtonUp( 0 );

        bool pressed1 = Input.GetMouseButtonDown( 1 );
        bool unpressed1 = Input.GetMouseButtonUp( 1 );



        currentTouch = mMouse[ 1 ];
        //        currentTouchID = -1 - i;

        ProcessTouch0( pressed0 , unpressed0 );
        ProcessTouch1( pressed1 , unpressed1 );

        currentTouch = null;
	}


    void onTouch0()
    {
        if ( UnityEngine.EventSystems.EventSystem.current == null )
        {
            return;
        }

        GameObject currentSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if ( currentSelected != null )
        {
            return;
        }

        Ray ray1 = touchCamera.ScreenPointToRay( lastTouchPosition );

        RaycastHit hit;

        LayerMask l = 1 << LayerMask.NameToLayer( "Terrain" );

        if ( !Physics.Raycast( ray1 , out hit , 2000 , l ) )
        {
            return;
        }

        //		RaycastHit[] hits = Physics.RaycastAll( ray1 , 100 );


        if ( hit.collider.gameObject != null )
        {
            W3BuildManager.instance.build( (int)hit.point.x ,
                (int)hit.point.z );
        }
    }

    void onTouch1()
	{
        if ( UnityEngine.EventSystems.EventSystem.current == null )
        {
            return;
        }

        GameObject currentSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if ( currentSelected != null )
        {
            return;
        }

        time++;

        if ( time < 3 )
        {
            return;
        }

        Ray ray1 = touchCamera.ScreenPointToRay( lastTouchPosition );

        RaycastHit hit;

        LayerMask l = 1 << LayerMask.NameToLayer( "Terrain" );

        if ( !Physics.Raycast( ray1 , out hit , 2000 , l ) )
        {
            return;
        }

        //		RaycastHit[] hits = Physics.RaycastAll( ray1 , 100 );


        if ( hit.collider.gameObject != null )
		{
            if ( W3UnitManager.instance.selectUnitList.Count > 0 )
            {
                W3OrderManager.instance.addMoveQueue( W3UnitManager.instance.selectUnitList
                , (int)hit.point.x , (int)hit.point.z );
            }
		}
	}


	void onMouseMove()
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
            W3BuildManager.instance.setPos( (int)hit.point.x ,
                (int)hit.point.z );
        }
    }


    void ProcessTouch0( bool pressed , bool unpressed )
    {
        if ( pressed )
        {
            isTouch = true;
        }

        if ( unpressed )
        {
            isTouch = false;

            onTouch0();
        }
    }

    void ProcessTouch1 ( bool pressed , bool unpressed )
	{
		if ( pressed )
		{
			isTouch = true;
		}

		if ( unpressed )
		{
			isTouch = false;

			onTouch1();
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

					onTouch1();
				}
			}
		}


	}

}



