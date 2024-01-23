using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum W3MovementMode
{
    Normal = 0,
    GiveWay,
    Build,

}

public partial class W3Unit
{
    W3MovementMode movementMode;

    // move
    Vector3 moveDirection;
    Vector3 moveDirectionPer;
    Vector3Int movePoint;
    Vector3Int moveToPoint;
    Vector3Int moveFromPoint;
    int moveCount = 0;
    int moveTimes = 0;

    // rotate
    int rotationAngleFrom = 0;
    int rotationAngleTo = 0;
    int rotationTimes = 0;
    int rotationCount = 0;
    float rotationPer = 0.0f;
    bool rotationPlus = false;

    int wonderTimes;

    public List< Vector3Int > targetPoints = new List< Vector3Int >();
    public List< Vector3Int > targetPointsBuff = new List< Vector3Int >();

    public List< Vector3Int > moveAgainPoints = new List< Vector3Int >();
    public List< Vector3Int > movePoints = new List< Vector3Int >();

    uint pathTime;

    bool isMoving = false;
    bool isRotating = false;
    bool isWonder = false;
    bool tryMoveAgainLine = false;
    bool tryMoveAgain2D = false;

    bool autoMove = false;
    bool autoFight = false;
    int autoMoveX = 0;
    int autoMoveZ = 0;

    public Vector3Int lastPosition;
    
 

    void getSmallNode( float x , float z , ref int tnsX , ref int tnsZ )
    {
        tnsX = (int)( -x / GameDefine.TERRAIN_SIZE_PER );
        tnsZ = (int)( -z / GameDefine.TERRAIN_SIZE_PER );

        if ( collision % 2 == 0 )
        {
            if ( -x % GameDefine.TERRAIN_SIZE_PER >= GameDefine.TERRAIN_SIZE_PER_HALF )
            {
                tnsX++;
            }
            if ( -z % GameDefine.TERRAIN_SIZE_PER >= GameDefine.TERRAIN_SIZE_PER_HALF )
            {
                tnsZ++;
            }
        }
    }


    public bool moveToBuild( int bx , int bz , W3UnitDataConfigData d )
    {
        int tnsX = 0;
        int tnsZ = 0;

        getSmallNode( unitTrans.position.x , unitTrans.position.z , ref tnsX , ref tnsZ );

        int n1x = ( d.pathW / 2 - d.pathX ) - d.pathMinX + 1;
        int n1z = ( d.pathH / 2 - d.pathZ ) - d.pathMinZ + 1;

        int n2x = d.pathMaxX - ( d.pathW / 2 - d.pathX ) + 1;
        int n2z = d.pathMaxZ - ( d.pathH / 2 - d.pathZ ) + 1;

        int npx = 0;
        int npz = 0;

        W3PathFinder.instance.findNearPosBuild( tnsX , tnsZ , bx , bz , n1x , n1z , n2x , n2z , collision , out npx , out npz );

        if ( tnsX == npx && tnsZ == npz )
        {
            return false;
        }

        movementMode = W3MovementMode.Build;

        bool b = moveTo( tnsX , tnsZ , npx , npz );

        if ( b )
        {
            W3PathFinder.instance.addCache( npx , npz );
        }

        return b;
    }

    public bool moveToGiveWay( int bx , int bz , W3UnitDataConfigData d )
    {
        int tnsX = 0;
        int tnsZ = 0;

        getSmallNode( unitTrans.position.x , unitTrans.position.z , ref tnsX , ref tnsZ );

        int n1x = ( d.pathW / 2 - d.pathX ) - d.pathMinX + 2;
        int n1z = ( d.pathH / 2 - d.pathZ ) - d.pathMinZ + 2;

        int n2x = d.pathMaxX - ( d.pathW / 2 - d.pathX ) + 2;
        int n2z = d.pathMaxZ - ( d.pathH / 2 - d.pathZ ) + 2;

        int npx = 0;
        int npz = 0;

        W3PathFinder.instance.findNearPosGiveWay( tnsX , tnsZ , bx , bz , n1x , n1z , n2x , n2z , collision , out npx , out npz );

        if ( tnsX == npx && tnsZ == npz )
        {
            return false;
        }

        bool b = moveTo( tnsX , tnsZ , npx , npz );

        if ( b )
        {
            W3PathFinder.instance.addCache( npx , npz );
        }

        return b;
    }

    public bool moveToNormal( float x , float z )
    {
        int tnsX = 0;
        int tnsZ = 0;

        getSmallNode( unitTrans.position.x , unitTrans.position.z , ref tnsX , ref tnsZ );

        int fx = (int)( x / GameDefine.TERRAIN_SIZE_PER );
        int fz = (int)( z / GameDefine.TERRAIN_SIZE_PER );

        if ( tnsX == fx &&
            tnsZ == fz )
        {
            return false;
        }

        if ( moveToPoint.x == fx &&
            moveToPoint.z == fz )
        {
            return false;
        }

        clearBuild();

        movementMode = W3MovementMode.Normal;

        return moveTo( tnsX , tnsZ , fx , fz );
    }


    public bool moveTo( int sx , int sz , int ex , int ez )
    {
        unsafe
        {
            int count = W3PathFinder.instance.findPathStep( sx , sz ,
                ex , ez , baseID , collision , false );

            pathTime = W3PathFinder.instance.pathTime;

            if ( count > 0 )
            {
                moveToPoint.x = ex;
                moveToPoint.z = ez;

                targetPoints.Clear();
                targetPointsBuff.Clear();

                count /= 2;

                if ( W3PathFinder.instance.pathLine )
                {
                    tryMoveAgain2D = true;
                    tryMoveAgainLine = false;

                    for ( int i = 1 ; i < count ; i++ )
                    {
                        int xx = W3PathFinder.instance.bufferOut[ i * 2 ];
                        int zz = W3PathFinder.instance.bufferOut[ i * 2 + 1 ];

                        if ( i == count - 1 )
                        {
                            Vector3Int pos = new Vector3Int( -xx * GameDefine.TERRAIN_SIZE_PER ,
                        0 , -zz * GameDefine.TERRAIN_SIZE_PER );

                            if ( collision % 2 == 1 )
                            {
                                pos.x -= GameDefine.TERRAIN_SIZE_PER_HALF;
                                pos.z -= GameDefine.TERRAIN_SIZE_PER_HALF;
                            }

                            targetPoints.Add( pos );
                        }

                        targetPointsBuff.Add( new Vector3Int( xx , 0 , zz ) );

                        //                    Debug.Log( -xx + " " + -zz );
                    }
                }
                else
                {
                    tryMoveAgainLine = true;
                    tryMoveAgain2D = false;

                    for ( int i = count - 1 ; i >= 0 ; i-- )
                    {
                        int xx = W3PathFinder.instance.bufferOut[ i * 2 ];
                        int zz = W3PathFinder.instance.bufferOut[ i * 2 + 1 ];

                        Vector3Int pos = new Vector3Int( -xx * GameDefine.TERRAIN_SIZE_PER ,
                            0 ,
                            -zz * GameDefine.TERRAIN_SIZE_PER );

                        if ( collision % 2 == 1 )
                        {
                            pos.x -= GameDefine.TERRAIN_SIZE_PER_HALF;
                            pos.z -= GameDefine.TERRAIN_SIZE_PER_HALF;
                        }

                        targetPoints.Add( pos );

                        //Debug.Log( -xx + " " + -zz );
                    }
                }

                startMove();
                RotateTo( movePoint.x , movePoint.z );

                return true;
//                Debug.Log( "moveTo " + baseID + " " + unitTrans.position.x + " " + unitTrans.position.z );
            }
            else
            {
                return false;
            }
        }
    }


    public void moveAgain()
    {
        int tnsX = 0;
        int tnsZ = 0;

        getSmallNode( unitTrans.position.x , unitTrans.position.z , ref tnsX , ref tnsZ );

        if ( moveAgainPoints.Count > 0 )
            moveAgainPoints.RemoveAt( 0 );

        targetPoints.Clear();

        unsafe
        {
            int count = 0;

            if ( moveAgainPoints.Count > 0 )
            {
                float x = moveAgainPoints[ 0 ].x;
                float z = moveAgainPoints[ 0 ].z;

                if ( collision % 2 == 1 )
                {
                    x += GameDefine.TERRAIN_SIZE_PER_HALF;
                    z += GameDefine.TERRAIN_SIZE_PER_HALF;
                }

                int xx = (int)( -x / GameDefine.TERRAIN_SIZE_PER );
                int zz = (int)( -z / GameDefine.TERRAIN_SIZE_PER );

                count = W3PathFinder.instance.findPathStep( tnsX , tnsZ ,
                xx , zz , baseID , collision , false );
            }
            else
            {
                count = W3PathFinder.instance.findPathStep( tnsX , tnsZ ,
                moveToPoint.x , moveToPoint.z , baseID , collision , false );
            }

            //            Debug.Log( "id " + unit.baseID + " " + finalPosition.x + " " + finalPosition.z );

            if ( count > 0 )
            {
                pathTime = W3PathFinder.instance.pathTime;

                count /= 2;

                if ( W3PathFinder.instance.pathLine )
                {
                    for ( int i = 1 ; i < count ; i++ )
                    {
                        int xx = W3PathFinder.instance.bufferOut[ i * 2 ];
                        int zz = W3PathFinder.instance.bufferOut[ i * 2 + 1 ];

                        Vector3Int pos = new Vector3Int( -xx * GameDefine.TERRAIN_SIZE_PER ,
                         0 , -zz * GameDefine.TERRAIN_SIZE_PER );

                        if ( collision % 2 == 1 )
                        {
                            pos.x -= GameDefine.TERRAIN_SIZE_PER_HALF;
                            pos.z -= GameDefine.TERRAIN_SIZE_PER_HALF;
                        }

                        targetPoints.Add( pos );

                        //                    Debug.Log( -xx + " " + -zz );
                    }
                }
                else
                {
                    for ( int i = count - 1 ; i >= 0 ; i-- )
                    {
                        int xx = W3PathFinder.instance.bufferOut[ i * 2 ];
                        int zz = W3PathFinder.instance.bufferOut[ i * 2 + 1 ];

                        Vector3Int pos = new Vector3Int( -xx * GameDefine.TERRAIN_SIZE_PER ,
                            0 ,
                            -zz * GameDefine.TERRAIN_SIZE_PER );

                        if ( collision % 2 == 1 )
                        {
                            pos.x -= GameDefine.TERRAIN_SIZE_PER_HALF;
                            pos.z -= GameDefine.TERRAIN_SIZE_PER_HALF;
                        }

                        targetPoints.Add( pos );

                        //Debug.Log( -xx + " " + -zz );
                    }
                }


                for ( int i = 1 ; i < moveAgainPoints.Count ; i++ )
                {
                    targetPoints.Add( moveAgainPoints[ i ] );

                    //                    Debug.Log( "moveAgain2 " + unit.baseID + " " + moveAgainPoints[ i ].x + " " + moveAgainPoints[ i ].z );
                }

                isWonder = false;

                startMove();

                Debug.Log( "moveAgainT " + baseID + " " + unitTrans.position.x + " " + unitTrans.position.z );
            }
            else
            {
                playAnimation( defaultAnimationType );
            }
        }
    }


    public void moveAgain2D()
    {
//         tryMoveAgain2D = true;
// 
//         int tnsX = 0;
//         int tnsZ = 0;
//         getSmallNode( unitTrans.position.x , unitTrans.position.z , ref tnsX , ref tnsZ );
// 
//         int x = movePoint.x - tnsX;
//         int z = movePoint.z - tnsZ;
// 
//         if ( x != 0 &&
//             z != 0 )
//         {
//             Debug.Log( "moveAgain2D " + baseID + " " + unitTrans.position.x + " " + unitTrans.position.z );
// 
//             startMove();
// 
//             tryMoveAgain2D = false;
// 
//             return;
//         }
// 
//         wonder( 0 );
    }


    public void moveAgainLine()
    {
        targetPoints.Clear();

        tryMoveAgainLine = true;

        int tnsX = 0;
        int tnsZ = 0;
        getSmallNode( unitTrans.position.x , unitTrans.position.z , ref tnsX , ref tnsZ );

        for ( int i = 0 ; i < targetPointsBuff.Count ; i++ )
        {
            if ( tnsX == targetPointsBuff[ i ].x &&
                tnsZ == targetPointsBuff[ i ].z )
            {
                for ( int j = i + 1 ; j < targetPointsBuff.Count ; j++ )
                {
                    Vector3Int pos = new Vector3Int( -targetPointsBuff[ j ].x * GameDefine.TERRAIN_SIZE_PER ,
                        0 , -targetPointsBuff[ j ].z * GameDefine.TERRAIN_SIZE_PER );

                    if ( collision % 2 == 1 )
                    {
                        pos.x -= GameDefine.TERRAIN_SIZE_PER_HALF;
                        pos.z -= GameDefine.TERRAIN_SIZE_PER_HALF;
                    }

                    targetPoints.Add( pos );
                    //Debug.Log( "pos " + pos.x + " " + pos.z );
                }

                startMove();

                return;
            }
        }

        wonder( 0 , true );
    }


    public void setAngles( Vector3 a )
    {
        unitTrans.localEulerAngles = a;
    }

    public void setPos( Vector3 pos )
    {
        if ( lastPosition.x > 0 || lastPosition.z > 0 )
        {
            if ( isBuilding )
                W3TerrainManager.instance.removeUnitBuilding( lastPosition.x , lastPosition.z , baseID , unitData );
            else
                W3TerrainManager.instance.removeUnit( lastPosition.x , lastPosition.z , baseID , collision );
        }

        lastPosition.x = (int)-pos.x / GameDefine.TERRAIN_SIZE_PER;
        lastPosition.z = (int)-pos.z / GameDefine.TERRAIN_SIZE_PER;

        if ( isBuilding )
            W3TerrainManager.instance.setUnitBuilding( lastPosition.x , lastPosition.z , baseID , unitData );
        else
            W3TerrainManager.instance.setUnit( lastPosition.x , lastPosition.z , baseID , collision );

        unitTrans.position = pos;

        if ( shadowMoveableSprite != null )
            shadowMoveableSprite.movePosReal( unitTrans.position.x , unitTrans.position.z );

        if ( hpTrans != null )
            hpTrans.transform.localPosition = new Vector3( unitTrans.position.x , pos.y + 100.0f , unitTrans.position.z );
    }

    public void setSelectionPos( Vector3 pos )
    {
        if ( selectionMoveableSprite != null && selectionMoveableSprite.gameObject.activeSelf )
            selectionMoveableSprite.movePosReal( unitTrans.position.x , unitTrans.position.z );
    }

    public Vector3 getPosition()
	{
        return unitTrans.position;
	}

    public void RotateTo( int x , int z )
    {
        rotationAngleTo = (short)W3TerrainDirection.getAngle( moveFromPoint.x , moveFromPoint.z , 
           x , z );

        rotationTimes = 0;   
        rotationPer = unitData.turnRate * Time.fixedDeltaTime * 360;
        rotationPlus = false;
        rotationCount = 0;
        rotationAngleFrom = (int)unitTrans.eulerAngles.y;

        if ( rotationAngleFrom > rotationAngleTo )
        {
            int dis0 = rotationAngleFrom - rotationAngleTo;
            int dis1 = 360 + rotationAngleTo - rotationAngleFrom;

            if ( dis0 > dis1 )
            {
                rotationCount = (int)( dis1 / rotationPer );
                if ( dis1 % rotationPer > 0 )
                    rotationCount++;

                rotationPlus = true;
            }
            else
            {
                rotationCount = (int)( dis0 / rotationPer );
                if ( dis0 % rotationPer > 0 )
                    rotationCount++;
            }
        }
        else
        {
            int dis0 = rotationAngleTo - rotationAngleFrom;
            int dis1 = 360 + rotationAngleFrom - rotationAngleTo;

            if ( dis0 > dis1 )
            {
                rotationCount = (int)( dis1 / rotationPer );
                if ( dis1 % rotationPer > 0 )
                    rotationCount++;
            }
            else
            {
                rotationCount = (int)( dis0 / rotationPer );
                if ( dis0 % rotationPer > 0 )
                    rotationCount++;

                rotationPlus = true;
            }
        }

        if ( rotationCount > 0 )
        {
            isRotating = true;
        }
    }

    public void startMove()
	{
        moveTimes = 0;
        moveCount = 0;

        moveFromPoint.x = (int)unitTrans.position.x;
        moveFromPoint.z = (int)unitTrans.position.z;

        movePoint = targetPoints[ 0 ];
        moveDirection = movePoint - moveFromPoint;
        moveDirection.x = (int)moveDirection.x;
        moveDirection.y = 0;
        moveDirection.z = (int)moveDirection.z;

        moveDirectionPer = moveDirection.normalized * Time.fixedDeltaTime * unitUI.run;

        moveCount = moveDirection.x != 0 ? (int)( moveDirection.x / moveDirectionPer.x ) 
            : (int)( moveDirection.z / moveDirectionPer.z );

        if ( moveDirection.x != 0 ? moveDirection.x % moveDirectionPer.x != 0 : 
            moveDirection.z % moveDirectionPer.z != 0 )
            moveCount++;

        if ( moveCount < 0 )
        {
            Debug.LogError( "moveCount < 0" );
            moveCount = 0;
            isMoving = false;
            playAnimation( defaultAnimationType );
            return;
        }

        isMoving = true;

        stopAttack();
        playAnimation( W3AnimationType.Walk );

 
//         movePoints.Clear();
// 
//         for ( int i = 1 ; i < moveCount ; i++ )
//         {
//             Vector3Int v3 = new Vector3Int( (int)( moveFromPoint.x + moveDirectionPer.x * i ) , 0 ,
//                 (int)( moveFromPoint.z + moveDirectionPer.z * i ) );
// 
//             movePoints.Add( v3 );
//         }
// 
//         movePoints.Add( new Vector3Int( (int)( movePoint.x ) , 0 ,
//                 (int)( movePoint.z ) ) );
    }

	public void updateMovement( float delay )
	{
//		if ( GameManager.instance.pause )
//		{
//			return;
//		}

		if ( isMoving )
		{
			moving();
        }

        if ( isRotating )
        {
            rotating();

            if ( isRotating &&
                !isMoving )
                rotating();
        }
	}

    void rotating()
    {
        if ( isRotating )
        {
            rotationTimes++;

            int a = 0;

            if ( rotationPlus )
            {
                a = (short)( rotationAngleFrom + rotationPer * rotationTimes );
                if ( a > 360 )
                    a -= 360;
            }
            else
            {
                a = (short)( rotationAngleFrom - rotationPer * rotationTimes );
                if ( a < 0 )
                    a += 360;
            }

//             Debug.Log( "a " + a );
            Vector3 v = new Vector3( unitTrans.localEulerAngles.x ,
                a ,
                unitTrans.localEulerAngles.z );

            if ( rotationCount == rotationTimes )
            {
                v = new Vector3( unitTrans.localEulerAngles.x ,
                rotationAngleTo ,
                unitTrans.localEulerAngles.z );

                isRotating = false;
            }

            unitTrans.localEulerAngles = v;
        }
    }

    void wonder( int n , bool m )
    {
        isWonder = true;
        wonderTimes = n;
        moveAgainPoints.Clear();

        if ( m )
        {
            for ( int i = 1 ; i < targetPoints.Count ; i++ )
                moveAgainPoints.Add( targetPoints[ i ] );
        }
    }

    bool checkAttack()
    {
        for ( int i = 0 ; i < targets.Count ; i++ )
        {
            W3Unit u = targets[ i ];

            Vector3 v3 = u.getPosition();
            int dx = (int)( v3.x - unitTrans.position.x );
            int dz = (int)( v3.z - unitTrans.position.z );

            float d = GameMath.sqrt( dx , dz );

            if ( d < unitWeapons.rangeN1 * 1.0f + GameDefine.TERRAIN_SIZE_PER_HALF )
            {
                // attack
                startAttack();
            }
            else
            {
                stopAttack();
            }
        }

        return false;
    }

    void checkEnemy()
    {
        int range = (int)( visionRange() - 2 );
        int dis = 1;

        int n1 = collision / 2;
        int n2 = collision % 2 + n1;

        int x1 = lastPosition.x - n1;
        int z1 = lastPosition.z - n1;
        int x2 = lastPosition.x + n2;
        int z2 = lastPosition.z + n2;

        int unitID = 0;
        int d = 9999;

        while ( range > dis )
        {
            int z = z1 - dis;
            int x = x1 - dis;
            int fx = x2 + dis;
            int fz = z2 + dis;

            for ( int i = 0 ; i < fx - x ; i++ )
            {
                unitID = W3PathFinder.instance.getUnitID( x + i , z );

                if ( unitID > 0 )
                {
                    int dd = Mathf.Abs( x + i - lastPosition.x ) + Mathf.Abs( z - lastPosition.z );
                    if ( dd < d )
                    {
                        d = dd;
                        autoMoveX = x + i;
                        autoMoveZ = z;
                    }

                    break;
                }

                
            }


            for ( int i = 0 ; i < fx - x ; i++ )
            {
                unitID = W3PathFinder.instance.getUnitID( x + i , fz - 1 );

                if ( unitID > 0 )
                {
                    int dd = Mathf.Abs( x + i - lastPosition.x ) + Mathf.Abs( fz - 1 - lastPosition.z );
                    if ( dd < d )
                    {
                        d = dd;
                        autoMoveX = x + i;
                        autoMoveZ = fz - 1;
                    }

                    break;
                }
            }


            for ( int i = 1 ; i < fz - z - 1 ; i++ )
            {
                unitID = W3PathFinder.instance.getUnitID( x , z + i );

                if ( unitID > 0 )
                {
                    int dd = Mathf.Abs( x - lastPosition.x ) + Mathf.Abs( z + i - lastPosition.z );
                    if ( dd < d )
                    {
                        d = dd;
                        autoMoveX = x;
                        autoMoveZ = z + i;
                    }
                    
                    break;
                }
            }


            for ( int i = 1 ; i < fz - z - 1 ; i++ )
            {
                unitID = W3PathFinder.instance.getUnitID( fx - 1 , z + i );

                if ( unitID > 0 )
                {
                    int dd = Mathf.Abs( fx - 1 - lastPosition.x ) + Mathf.Abs( z + i - lastPosition.z );
                    if ( dd < d )
                    {
                        d = dd;
                        autoMoveX = fx - 1;
                        autoMoveZ = z + i;
                    }

                    break;
                }
            }

            if ( unitID > 0 )
                break;

            dis++;
        }

        if ( dis == 1 )
        {
            // auto attack
            autoFight = true;
        }
        else
        {
            // auto move

            autoMove = true;
        }

    }


    void moving()
    {
        if ( isWonder )
        {
            wonderTimes++;

            if ( wonderTimes > 5 )
            {
                moveAgain();
                wonderTimes = 0;
            }

            return;
        }

        if ( autoMove )
        {
            autoMove = false;
            //            moveTo( autoMoveX * GameDefine.TERRAIN_SIZE_PER , autoMoveZ * GameDefine.TERRAIN_SIZE_PER );
            return;
        }

        moveTimes++;
        Vector3Int posNext = moveFromPoint;
        posNext.x += (int)( moveDirectionPer.x * moveTimes );
        posNext.z += (int)( moveDirectionPer.z * moveTimes );

        int px = -posNext.x % GameDefine.TERRAIN_SIZE_PER;
        int pz = -posNext.z % GameDefine.TERRAIN_SIZE_PER;

        if ( collision % 2 == 0 )
        {
            // no 16 16
            if ( moveDirectionPer.x > 0
             && moveDirectionPer.z < 0
             && px == GameDefine.TERRAIN_SIZE_PER_HALF
             && pz == GameDefine.TERRAIN_SIZE_PER_HALF )
            {
                posNext.x += 1;
                posNext.z -= 1;
            }

            if ( moveDirectionPer.x < 0
                && moveDirectionPer.z > 0
                && px == GameDefine.TERRAIN_SIZE_PER_HALF
                && pz == GameDefine.TERRAIN_SIZE_PER_HALF )
            {
                posNext.x -= 1;
                posNext.z += 1;
            }

        }

        int tnsX = 0;
        int tnsZ = 0;
        getSmallNode( posNext.x , posNext.z , ref tnsX , ref tnsZ );

        //        Debug.Log( "posNext " + posNext.x + " " + posNext.z + " " + tnsX + " " + tnsZ + "  d " + moveDirection.x + " " + moveDirection.z + " t" + targetDirection.x + " " + targetDirection.z );

        if ( lastPosition.x != moveToPoint.x ||
            lastPosition.z != moveToPoint.z )
        {
            if ( tnsX != lastPosition.x ||
                tnsZ != lastPosition.z )
            {
                bool b = W3PathFinder.instance.isUnitPathTime( tnsX , tnsZ , baseID , collision , pathTime );

                // next 
                if ( W3PathFinder.instance.isUnitSize( tnsX , tnsZ , baseID , collision ) )
                {
                    Debug.Log( "isUnitSize " + posNext.x + " " + posNext.z + "  " + tnsX + " " + tnsZ + " l " + lastPosition.x + " " + lastPosition.z );

                    if ( !b &&
                        moveDirection.x != 0 &&
                        moveDirection.z != 0 )
                    {
                        // try move with 4 directions

                        int mx = Mathf.Abs( tnsX * GameDefine.TERRAIN_SIZE_PER + movePoint.x );
                        int mz = Mathf.Abs( tnsZ * GameDefine.TERRAIN_SIZE_PER + movePoint.z );

                        if ( mx == 0 && mz == 0 )
                        {
                            //wonder( 5 );
                            //return;
                        }
                        else if ( mx <= GameDefine.TERRAIN_SIZE_PER
                        && mz <= GameDefine.TERRAIN_SIZE_PER )
                        {
                            int mdx = moveDirection.x > 0 ? -1 : 1;
                            int mdz = moveDirection.z > 0 ? -1 : 1;

                            if ( !W3PathFinder.instance.isUnitSize( lastPosition.x + mdx , lastPosition.z , baseID , collision ) )
                            {
                                Vector3Int pos = new Vector3Int( -( lastPosition.x + mdx ) * GameDefine.TERRAIN_SIZE_PER ,
                            0 , (int)unitTrans.position.z );

                                if ( collision % 2 == 1 )
                                {
                                    pos.x -= GameDefine.TERRAIN_SIZE_PER_HALF;
                                }

                                targetPoints.Insert( 0 , pos );
                                startMove();
                                return;
                            }
                            else if ( !W3PathFinder.instance.isUnitSize( lastPosition.x , lastPosition.z + mdz , baseID , collision ) )
                            {
                                Vector3Int pos = new Vector3Int( (int)unitTrans.position.x ,
                            0 , -( lastPosition.z + mdz ) * GameDefine.TERRAIN_SIZE_PER );

                                if ( collision % 2 == 1 )
                                {
                                    pos.z -= GameDefine.TERRAIN_SIZE_PER_HALF;
                                }

                                targetPoints.Insert( 0 , pos );
                                startMove();
                                return;
                            }
                        }
                    }


                    if ( ( b || ( tryMoveAgainLine && tryMoveAgain2D ) ) )
                    {
                        if ( Mathf.Abs( tnsX - moveToPoint.x ) < 5 &&
                        Mathf.Abs( tnsZ - moveToPoint.z ) < 5 )
                        {
                            isMoving = false;
                            playAnimation( defaultAnimationType );
                            return;
                        }

                        wonder( 5 , true );
                        return;
                    }

                    if ( !tryMoveAgainLine )
                    {
                        moveAgainLine();
                        return;
                    }

                    if ( !tryMoveAgain2D )
                    {
                        tryMoveAgain2D = true;
                        //                        moveAgain2D();
                        return;
                    }
                }

            }
        }

        if ( moveCount == moveTimes )
        {
            posNext.x = movePoint.x;
            posNext.z = movePoint.z;
        }

        float tnXf = (float)posNext.x / GameDefine.TERRAIN_SIZE;
        float tnZf = (float)posNext.z / GameDefine.TERRAIN_SIZE;
        int tnX = (int)-tnXf;
        int tnZ = (int)-tnZf;

        if ( tnsX != lastPosition.x ||
            tnsZ != lastPosition.z )
        {
            if ( W3PathFinder.instance.isUnitSize( tnsX , tnsZ , baseID , collision ) )
            {
                Debug.LogError( "move error" );
                isMoving = false;
                playAnimation( defaultAnimationType );
                return;
            }

            W3TerrainManager.instance.removeUnit( lastPosition.x , lastPosition.z , baseID , collision );

            lastPosition.x = tnsX;
            lastPosition.z = tnsZ;
            W3TerrainManager.instance.setUnit( lastPosition.x , lastPosition.z , baseID , collision );
            W3FogManager.instance.calculateFog();

            //            checkEnemy();
        }

        // update y
        W3TerrainSmallNode tsn = W3TerrainManager.instance.getSmallNode( tnsX , tnsZ );
        W3TerrainNode tn = W3TerrainManager.instance.getNode( tnX , tnZ );
        W3TerrainNode tna = W3TerrainManager.instance.getNode( tnX , tnZ + 1 );
        W3TerrainNode tnb = W3TerrainManager.instance.getNode( tnX + 1 , tnZ + 1 );
        W3TerrainNode tnc = W3TerrainManager.instance.getNode( tnX + 1 , tnZ );

        if ( -tnXf - tna.x + tna.z + tnZf > 1.0f )
        {
            posNext.y = (int)( ( -tnZf - tnc.z ) * ( tnb.y - tnc.y ) +
            ( tnc.x + tnXf ) * ( tn.y - tnc.y ) + tnc.y );
        }
        else
        {
            posNext.y = (int)( ( -tnXf - tna.x ) * ( tnb.y - tna.y ) +
            ( tna.z + tnZf ) * ( tn.y - tna.y ) + tna.y );
        }


        if ( shadowMoveableSprite != null )
            shadowMoveableSprite.movePosReal( posNext.x , posNext.z );

        if ( selectionMoveableSprite != null && selectionMoveableSprite.gameObject.activeSelf )
            selectionMoveableSprite.movePosReal( posNext.x , posNext.z );

        if ( hpTrans != null )
            hpTrans.transform.localPosition = new Vector3( posNext.x , posNext.y + 100.0f , posNext.z );

        unitTrans.position = posNext;


        if ( moveCount == moveTimes )
        {
            W3FogManager.instance.calculateFog();

            targetPoints.RemoveAt( 0 );

            if ( targetPoints.Count > 0 )
            {
                startMove();
                RotateTo( movePoint.x , movePoint.z );

                if ( targetPoints.Count > 1 )
                {
                    getSmallNode( targetPoints[ 1 ].x , targetPoints[ 1 ].z , ref tnsX , ref tnsZ );

                    bool b = W3PathFinder.instance.isUnitPathTime( tnsX , tnsZ , baseID , collision , pathTime );

                    // forecast next 
                    if ( b && W3PathFinder.instance.isUnitSize( tnsX , tnsZ , baseID , collision ) )
                    {
                        Debug.Log( "isUnitSizeN " + targetPoints[ 1 ].x + " " + targetPoints[ 1 ].z + "  " + tnsX + " " + tnsZ + " l " + lastPosition.x + " " + lastPosition.z );

                        if ( Mathf.Abs( tnsX - moveToPoint.x ) < 5 &&
                            Mathf.Abs( tnsZ - moveToPoint.z ) < 5 )
                        {
                            isMoving = false;
                            playAnimation( defaultAnimationType );
                            return;
                        }

                        targetPoints.RemoveAt( 0 );
                        targetPoints.RemoveAt( 0 );

                        wonder( 5 , true );
                    }
                }
                

//                 Debug.Log( "dir " + (int)movePosition.x + " " + (int)movePosition.z + " " + (int)targetPoint.x + " " + (int)targetPoint.z + " " + 
//                     W3Dll.W3MapGetAngle( (int)movePosition.x , (int)movePosition.z , (int)targetPoint.x , (int)targetPoint.z ) );

                return;
            }

            isMoving = false;

            playAnimation( defaultAnimationType );

            //            checkAttack();

            handler();

            return;
        }

    }



}

