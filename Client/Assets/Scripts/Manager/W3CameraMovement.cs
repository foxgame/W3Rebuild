using UnityEngine;
using System.Collections;

// Reference Unity RTS Engine.

public class W3CameraMovement : SingletonMono< W3CameraMovement >
{
    public float sensitivityX = 1f;
    public float sensitivityY = 1f;
    public float sensitivetyZ = 1f;
    public float sensitivetyMove = 1f;
    public float sensitivetyMouseWheel = 10f;

    bool firstTime = false;

    [Header( "FOV" )]
//     public int x1 = 0;
//     public int x2 = 0;
//     public int z1 = 0;
//     public int z2 = 0;
//     public int w = 0;
//     public int h = 0;
//     public int fovWidth = 30;
//     public int fovHeight = 20;



    [Header( "General Settings:" )]
    Camera mainCamera;
    public float cameraHeight = 950.0f;

    [Header( "Camera Movement:" )]
    public float mvtSpeed = 1.00f;

    public KeyCode moveUpKey = KeyCode.UpArrow;
    public KeyCode moveDownKey = KeyCode.DownArrow;
    public KeyCode moveRightKey = KeyCode.RightArrow;
    public KeyCode moveLeftKey = KeyCode.LeftArrow;

    //screen edge movement:
    public bool moveOnScreenEdge = false;
    public bool ignoreUI = false;
    public int screenEdgeSize = 25;

    Rect downRect;
    Rect upRect;
    Rect leftRect;
    Rect rightRect;

    bool canMoveOnEdge = true;

    //Camera position limits: //The Y axis here refers to the Z axis of the camera position.
    public bool screenLimit = false;
    public Vector2 minPos;
    public Vector2 maxPos;

    [Header( "Panning:" )]
    //Panning:
    public bool panning = true;
    public KeyCode panningKey = KeyCode.Space;
    Vector2 mouseAxis;
    public float panningSpeed = 15f;

    [Header( "Zoom:" )]
    //Camera zoom in/zoom out:
    public bool zoomEnabled = true;
    public bool canZoomWithKey = true;
    //Zoom keys:
    public KeyCode zoomInKey = KeyCode.PageUp;
    public KeyCode zoomOutKey = KeyCode.PageDown;

    public float zoomSmoothTime = 1.0f;
    float zoomVelocity;
    //Use mouse wheel for zooming in and out?
    public bool zoomOnMouseWheel = false;
    public float zoomScrollWheelSensitivty = 5.0f;
    public float zoomScrollWheelSpeed = 15.0f;


    [Header( "Follow Unit:" )]
    //Follow unit:
    public bool canFollowUnit = true;
    [HideInInspector]
    public W3Unit unitToFollow;
    public KeyCode followUnitKey = KeyCode.Space;

    [Header( "Minimap Camera:" )]
    //Minimap:
//    public Camera minimapCam;
    public float offsetX;
    public float offsetZ;
    //UI: 
    public Canvas minimapCanvas;
//     public Image minimapCursor;

    public float fov;
    public float minFOV = 40.0f;
    public float maxFOV = 57.0f;

    [Header( "UI Camera:" )]
    public Camera uiCam;

    bool moved = false;


    void Start()
    {
        mainCamera = GetComponent<Camera>();

        //initially not following any unit
        unitToFollow = null;

        //get the camera FOV
        fov = mainCamera.fieldOfView;

        //set the camera's position and rotation angles
        mainCamera.transform.position = new Vector3( mainCamera.transform.position.x , cameraHeight , mainCamera.transform.position.z );
        mainCamera.transform.eulerAngles = new Vector3( 75.0f , 180.0f , 0.0f );
    }

    void Update()
    {
        //If panning is enabled and the player is holding the panning key
        if ( panning == true && Input.GetKey( panningKey ) )
        {
            PanCam();
        }
        else
        { //if the player is not panning the camera
            MoveCam();
        }

        //Camera position limits if the camera has moved
        transform.position = RefinePosition( transform.position );

        if ( zoomEnabled == true )
        {
            CamZoom();
        }

        //Follow a unit:

        //if we can actually follow units:
        if ( canFollowUnit == true )
        {
            //if the player moved the camera
            if ( moved == true )
            {
                //we're not following a unit anymore
                unitToFollow = null;
            }

            FollowUnit();
        }

        //Minimap movement :

        //if the player presses one of the mouse buttons:
        if ( Input.GetMouseButtonUp( 0 ) || Input.GetMouseButtonUp( 1 ) )
        {
            //if the player's mouse is over the minmap
//             if ( minimapCam.rect.Contains( mainCamera.ScreenToViewportPoint( Input.mousePosition ) ) )
//             {
//                 //see if we touched the minimap and update accordinly:
//                 OnMinimapClick();
//             }
        }

        if ( Input.GetMouseButton( 1 ) )
        {
            if ( W3UnitManager.instance.selectUnitList.Count > 0 )
            {
                return;
            }


            float rotationX = Input.GetAxis( "Mouse X" ) * sensitivityX;
            float rotationY = Input.GetAxis( "Mouse Y" ) * sensitivityY;

//             if ( rotationX > 50 || rotationY > 50 ||
//                 rotationX < -50 || rotationY < -50 )
//             {
//                 return;
//             }

            if ( !firstTime )
            {
                firstTime = true;
                return;
            }

            transform.position = new Vector3( transform.position.x + rotationX , transform.position.y , transform.position.z + rotationY );
        }
        else
        {
            firstTime = false;
        }

        if ( moved == true )
        {
            UpdateMinimapCursor();
        }

        moved = false;
    }

    //method for camera panning:
    void PanCam()
    {
        mouseAxis = new Vector2( Input.GetAxis( "Mouse X" ) , Input.GetAxis( "Mouse Y" ) );

        if ( mouseAxis != Vector2.zero ) //if the player is moving the mouse
        {
            //calculate the target mvt vector here:
            Vector3 targetMvt = new Vector3( -mouseAxis.x , 0.0f , -mouseAxis.y );

            targetMvt *= panningSpeed * Time.deltaTime;

            // Put the movement vector into world space
            targetMvt = transform.rotation * targetMvt;
            // Zero out any vertical movement and normalize
            float origLen = targetMvt.magnitude;
            targetMvt.y = 0.0f;
            targetMvt.Normalize();
            targetMvt *= origLen;

            transform.Translate( targetMvt , Space.World );
            moved = true;
        }
    }

    //a method to move the camera:
    void MoveCam()
    {
        //check if the player can move the camera on screen edge:
        canMoveOnEdge = false;
        if ( moveOnScreenEdge )
        {
//             if ( !EventSystem.current.IsPointerOverGameObject() )
//             {
//                 canMoveOnEdge = true;
//             }
        }

        Vector3 targetMvt = Vector3.zero; //mvt direction

        //Screen edges rects:
        downRect = new Rect( 0.0f , 0.0f , Screen.width , screenEdgeSize );
        upRect = new Rect( 0.0f , Screen.height - screenEdgeSize , Screen.width , screenEdgeSize );
        leftRect = new Rect( 0.0f , 0.0f , screenEdgeSize , Screen.height );
        rightRect = new Rect( Screen.width - screenEdgeSize , 0.0f , screenEdgeSize , Screen.height );

        //move on edge: see if the mouse is on the screen edge while the mvt on edge is allowed
        bool moveUp = ( upRect.Contains( Input.mousePosition ) && canMoveOnEdge == true );
        bool moveDown = ( downRect.Contains( Input.mousePosition ) && canMoveOnEdge == true );
        bool moveRight = ( rightRect.Contains( Input.mousePosition ) && canMoveOnEdge == true );
        bool moveLeft = ( leftRect.Contains( Input.mousePosition ) && canMoveOnEdge == true );

        //determine the movement direction depending on the above bools
        targetMvt.x = moveRight ? 1 : moveLeft ? -1 : 0;
        targetMvt.z = moveUp ? 1 : moveDown ? -1 : 0;

        //if there's a direction to move to
        if ( targetMvt != Vector3.zero )
        {
            //move with the defined speed
            targetMvt *= mvtSpeed * Time.deltaTime;
            targetMvt = Quaternion.Euler( new Vector3( 0f , transform.eulerAngles.y , 0f ) ) * targetMvt;

            transform.Translate( targetMvt , Space.World );
            transform.position = new Vector3( transform.position.x , cameraHeight , transform.position.z );

            moved = true;
        }
        else if ( Mathf.Abs( Input.GetAxis( "Horizontal" ) ) > 0.1f || Mathf.Abs( Input.GetAxis( "Vertical" ) ) > 0.1f )
        { 
            //keyboard movement:
            //use the Horizontal and Vertical axis to move the camera with the above defined speed
            transform.Translate( Vector3.right.normalized * Input.GetAxis( "Horizontal" ) * mvtSpeed * Time.deltaTime );
            transform.Translate( ( Vector3.up + Vector3.forward ).normalized * Input.GetAxis( "Vertical" ) * mvtSpeed * Time.deltaTime );

            transform.position = new Vector3( transform.position.x , cameraHeight , transform.position.z );

            moved = true;
        }
    }

    //a method to zoom the camera:
    void CamZoom()
    {
        //Zoom in/out:

        //If the player presses the zoom in and out keys:
        if ( Input.GetKey( zoomInKey ) && canZoomWithKey == true )
        {
            if ( zoomVelocity > 0 )
            {
                zoomVelocity = 0.0f;
            }
            //Smoothly zoom in:
            fov = Mathf.SmoothDamp( fov , minFOV , ref zoomVelocity , zoomSmoothTime );
        }
        else if ( Input.GetKey( zoomOutKey ) && canZoomWithKey == true )
        {
            if ( zoomVelocity < 0 )
            {
                zoomVelocity = 0.0f;
            }
            //Smoothly zoom out:
            fov = Mathf.SmoothDamp( fov , maxFOV , ref zoomVelocity , zoomSmoothTime );
        }
        else if ( zoomOnMouseWheel == true )
        {
            fov -= Input.GetAxis( "Mouse ScrollWheel" ) * zoomScrollWheelSensitivty;
        }

        //Always keep the field of view between the max and the min values:
        fov = Mathf.Clamp( fov , minFOV , maxFOV );

        //update the UI cam's fog as well
        if ( uiCam )
        { 
            //if there's an ignore fog camera then update the FOV there too
//             uiCam.fieldOfView = fov;
        }
    }

    void FollowUnit()
    {
//         if ( SelectionMgr.SelectedUnits.Count == 1 )
//         {
//             //can only work with one unit selected:
//             if ( SelectionMgr.SelectedUnits[ 0 ] != null )
//             {
//                 if ( Input.GetKeyDown( FollowUnitKey ) )
//                 { //if the player presses the follow key
//                     UnitToFollow = SelectionMgr.SelectedUnits[ 0 ]; //make the selected unit, the unit to follow.
//                 }
//             }
//         }
    }

    //method called to check if the player clicked on the minimap and update accordinly:
    void OnMinimapClick()
    {
//         Ray rayCheck;
//         RaycastHit[] hits;
// 
//         //create a raycast using the minimap camera
//         rayCheck = minimapCam.ScreenPointToRay( Input.mousePosition );
//         hits = Physics.RaycastAll( RayCheck , 100.0f );
// 
//         if ( Hits.Length > 0 )
//         {
//             for ( int i = 0 ; i < Hits.Length ; i++ )
//             {
//                 //If we clicked on a part of the terrain:
//                 if ( Hits[ i ].transform.gameObject == TerrainMgr.FlatTerrain )
//                 {
//                     //if this is the left mouse button and the selection box is disabled
//                     if ( Input.GetMouseButtonUp( 0 ) && SelectionMgr.SelectionBoxEnabled == false )
//                     {
//                         //stop following the unit if it's enabled
//                         if ( CanFollowUnit == true )
//                             UnitToFollow = null;
//                         //make the camera look at the position we clicked in the minimap
//                         LookAt( Hits[ i ].point );
//                         //mark as moved:
//                         Moved = true;
//                     }
//                     //TO BE CHANGED
//                     //if the player presses the right mouse button
//                     else if ( Input.GetMouseButtonUp( 1 ) )
//                     {
//                         //move the selected units to the new clicked position in the minimap
//                         //MvtMgr.Move(SelectionMgr.SelectedUnits,Hits[i].point, 0.0f, null, InputTargetMode.None);
//                     }
//                 }
//             }
//         }
    }

    void FixedUpdate()
    {
        if ( !W3TerrainManager.instance.isLoaded )
        {
            return;
        }

        float tnXf = (float)transform.position.x / GameDefine.TERRAIN_SIZE;
        float tnZf = (float)transform.position.z / GameDefine.TERRAIN_SIZE - offsetZ;
        int tnX = (int)-tnXf;
        int tnZ = (int)-tnZf;

        W3TerrainNode tn = W3TerrainManager.instance.getNode( tnX , tnZ );
        W3TerrainNode tna = W3TerrainManager.instance.getNode( tnX , tnZ + 1 );
        W3TerrainNode tnb = W3TerrainManager.instance.getNode( tnX + 1 , tnZ + 1 );
        W3TerrainNode tnc = W3TerrainManager.instance.getNode( tnX + 1 , tnZ );

        float y;
        if ( -tnXf - tna.x + tna.z + tnZf > 1.0f )
        {
            y = ( ( -tnZf - tnc.z ) * ( tnb.y - tnc.y ) +
            ( tnc.x + tnXf ) * ( tn.y - tnc.y ) + tnc.y );
        }
        else
        {
            y = ( ( -tnXf - tna.x ) * ( tnb.y - tna.y ) +
            ( tna.z + tnZf ) * ( tn.y - tna.y ) + tna.y );
        }

        if ( y > 256 )
        {
            cameraHeight = 950 + ( y - 256 );
        }
        else
            cameraHeight = 950;

//        Debug.Log( y );

        //         x1 = (int)-transform.position.x / GameDefine.TERRAIN_SIZE_PER - fovWidth;
        //         x2 = (int)-transform.position.x / GameDefine.TERRAIN_SIZE_PER + fovWidth;
        // 
        //         z1 = (int)-( transform.position.z / GameDefine.TERRAIN_SIZE_PER - offsetZ ) - fovHeight;
        //         z2 = (int)-( transform.position.z / GameDefine.TERRAIN_SIZE_PER - offsetZ ) + fovHeight;
        // 
        //         w = x2 - x1;
        //         h = z2 - z1;

        //update the field of view here:
        mainCamera.fieldOfView = Mathf.Lerp( mainCamera.fieldOfView , fov , Time.deltaTime * zoomScrollWheelSpeed );

        //if we can actually follow units:
        if ( canFollowUnit == true )
        {
            if ( unitToFollow != null )
            { 
                //if the camera is following a unit:
                LookAt( unitToFollow.transform.position );
            }
        }
    }

    //looks at the selected unit:
    public void LookAtSelectedUnit()
    {
//         if ( SelectionMgr.SelectedUnits.Count == 1 )
//         {
//             LookAt( SelectionMgr.SelectedUnits[ 0 ].transform.position );
//         }
    }

    //look at a position in the map
    public void LookAt( Vector3 lookAtPos )
    {
        //look at the new position 
        transform.position = RefinePosition( new Vector3( lookAtPos.x + offsetX , transform.position.y , lookAtPos.z + offsetZ ) );

    }

    public void UpdateMinimapCursor()
    {
        Ray rayCheck;
        RaycastHit[] hits;

        //raycast using the main camera
        rayCheck = mainCamera.ScreenPointToRay( new Vector3( Screen.width / 2 , Screen.height / 2 , 0.0f ) );
        hits = Physics.RaycastAll( rayCheck , 100.0f );

        if ( hits.Length > 0 )
        {
            for ( int i = 0 ; i < hits.Length ; i++ )
            {
                //as soon as we hit the main terrain 
//                 if ( hits[ i ].transform.gameObject == TerrainMgr.FlatTerrain )
//                 {
//                     //change the mini map cursor position to suit the new camera position
//                     SetMiniMapCursorPos( hits[ i ].point );
//                 }
            }
        }
    }

    //set the minimap cursor position here
    public void SetMiniMapCursorPos( Vector3 newPos )
    {
//         Vector2 canvasPos = Vector2.zero;
//         RectTransformUtility.ScreenPointToLocalPointInRectangle( MinimapCanvas.GetComponent<RectTransform>() , MinimapCam.WorldToScreenPoint( RefinePosition( NewPos ) ) , MinimapCam , out CanvasPos );
//         minimapCursor.GetComponent<RectTransform>().localPosition = new Vector3( CanvasPos.x , CanvasPos.y , MinimapCursor.GetComponent<RectTransform>().localPosition.z );
    }

    //refine the position to suit the camera's settings
    Vector3 RefinePosition( Vector3 position )
    {
        //if we're using screen limit
        if ( screenLimit == true )
        {
            //clamp the position
            position = new Vector3( Mathf.Clamp( position.x , minPos.x , maxPos.x ) , position.y , Mathf.Clamp( position.z , minPos.y , maxPos.y ) );
        }

        return position;
    }

}


