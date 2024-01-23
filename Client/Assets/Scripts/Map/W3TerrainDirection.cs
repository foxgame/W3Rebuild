using UnityEngine;



//     N(-Z)
//W(X)     E(-X)
//	   S(Z)

public enum W3TerrainDirectionType
{
	NORTH = 3,
	SOUTH = 7,
	WEST = 1,
	EAST = 5,
	NORTHWEST = 2,
	NORTHEAST = 4,
	SOUTHWEST = 0,
	SOUTHEAST = 6,

	COUNT = 8
}


public class W3TerrainDirection
{

    public static float getAngle( int fromX , int fromZ , int toX , int toZ )
    {
        int lenZ = toZ - fromZ;
        int lenX = toX - fromX;

        if ( lenX == 0 && fromZ < toZ )
        {
            return 0;
        }
        else if ( lenX == 0 && fromZ > toZ )
        {
            return 180;
        }
        else if ( lenZ == 0 && fromX > toX )
        {
            return 270;
        }
        else if ( lenZ == 0 && fromX < toX )
        {
            return 90;
        }

        float tanZX = Mathf.Abs( lenZ ) / (float)Mathf.Abs( lenX );
        float angle = 0;


        if ( lenZ > 0 && lenX < 0 )
        {
            angle = Mathf.Atan( tanZX ) * 180 / 3.1416f + 270;
        }
        else if ( lenZ > 0 && lenX > 0 )
        {
            angle = 90 - Mathf.Atan( tanZX ) * 180 / 3.1416f;
        }
        else if ( lenZ < 0 && lenX < 0 )
        {
            angle = 270 - Mathf.Atan( tanZX ) * 180 / 3.1416f;
        }
        else if ( lenZ < 0 && lenX > 0 )
        {
            angle = Mathf.Atan( tanZX ) * 180 / 3.1416f + 90;
        }

        return angle;
    }

    public static int getReverseDirection( int direct )
	{
		switch ( direct ) 
		{
			case (int)W3TerrainDirectionType.SOUTHEAST:
				return (int)W3TerrainDirectionType.NORTHWEST;
			case (int)W3TerrainDirectionType.SOUTHWEST:
				return (int)W3TerrainDirectionType.NORTHEAST;
			case (int)W3TerrainDirectionType.NORTHWEST:
				return (int)W3TerrainDirectionType.SOUTHEAST;
			case (int)W3TerrainDirectionType.NORTHEAST:
				return (int)W3TerrainDirectionType.SOUTHWEST;
			case (int)W3TerrainDirectionType.SOUTH:
				return (int)W3TerrainDirectionType.NORTH;
			case (int)W3TerrainDirectionType.NORTH:
				return (int)W3TerrainDirectionType.SOUTH;
			case (int)W3TerrainDirectionType.EAST:
				return (int)W3TerrainDirectionType.WEST;
			case (int)W3TerrainDirectionType.WEST:
				return (int)W3TerrainDirectionType.EAST;

		}

		return (int)W3TerrainDirectionType.COUNT;
	}

	public static int getDirection( int x0 , int z0 , int x1 , int z1 )
	{
		int x = x0 - x1;
		int z = z0 - z1;

		int direction = (int)W3TerrainDirectionType.COUNT;

		if ( x > 0 && z > 0 ) 
			direction = (int)W3TerrainDirectionType.SOUTHWEST;
		else if ( x > 0 && z == 0 ) 
			direction = (int)W3TerrainDirectionType.WEST;
		else if ( x > 0 && z < 0 ) 
			direction = (int)W3TerrainDirectionType.NORTHWEST;
		else if ( x == 0 && z < 0 ) 
			direction = (int)W3TerrainDirectionType.NORTH;
		else if ( x < 0 && z < 0 ) 
			direction = (int)W3TerrainDirectionType.NORTHEAST;
		else if ( x < 0 && z == 0 ) 
			direction = (int)W3TerrainDirectionType.EAST;
		else if ( x < 0 && z > 0 ) 
			direction = (int)W3TerrainDirectionType.SOUTHWEST;
		else if ( x == 0 && z > 0 ) 
			direction = (int)W3TerrainDirectionType.SOUTH;

		return direction;
	}


    public static int getDirection4( int x0 , int z0 , int x1 , int z1 )
    {
        int x = x0 - x1;
        int z = z0 - z1;

        int direction = (int)W3TerrainDirectionType.COUNT;

        if ( x > 0 && z > 0 )
        {
            if ( x > z )
            {
                direction = (int)W3TerrainDirectionType.WEST;
            }
            else
            {
                direction = (int)W3TerrainDirectionType.SOUTH;
            }
        }
        else if ( x > 0 && z == 0 )
            direction = (int)W3TerrainDirectionType.WEST;
        else if ( x > 0 && z < 0 )
        {
            if ( x > -z )
            {
                direction = (int)W3TerrainDirectionType.WEST;
            }
            else
            {
                direction = (int)W3TerrainDirectionType.NORTH;
            }
        }
        else if ( x == 0 && z < 0 )
            direction = (int)W3TerrainDirectionType.NORTH;
        else if ( x < 0 && z < 0 )
        {
            if ( -x > -z )
            {
                direction = (int)W3TerrainDirectionType.EAST;
            }
            else
            {
                direction = (int)W3TerrainDirectionType.NORTH;
            }
        }
        else if ( x < 0 && z == 0 )
            direction = (int)W3TerrainDirectionType.EAST;
        else if ( x < 0 && z > 0 )
        {
            if ( -x > z )
            {
                direction = (int)W3TerrainDirectionType.EAST;
            }
            else
            {
                direction = (int)W3TerrainDirectionType.SOUTH;
            }
        }
        else if ( x == 0 && z > 0 )
            direction = (int)W3TerrainDirectionType.SOUTH;

        return direction;
    }

}

