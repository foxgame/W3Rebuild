using System;
using System.Collections.Generic;
using UnityEngine;


public struct W3BaseData
{
    public int typeID;
    public int playerID;

    public int level;
    public int type;

	public int hp;
	public int hpMax;

	public int mp;
	public int mpMax;

    public float x;
    public float y;
    public float z;

    public float scaleX;
    public float scaleY;
    public float scaleZ;
    
    public bool invulnerable;
    public float occluderHeight;

    public bool visible;

    // item
    public int dropID;
    public bool dropOnDeath;
    public bool droppable;
    public bool pawnable;
    public bool sellable;
    public bool powerup;


    // unit
    public string name;
    public int race;

    public int unitState;
    public bool creepGuard;
    public float unitStateValue;
    public float defaultPropWindowAngle;
    public float propWindowAngle;
    public float defaultAcquireRange;
    public float acquireRange;
    public float facing;
    public float facingTime;

    public float moveSpeed;
    public float defaultMoveSpeed;

    public float defaultFlyHeight;
    public float flyHeight;

    public float flyHeightRate;
    public float defaultTurnSpeed;
    public float turnSpeed;

    public float timeScale;
    public float blendTime;

    public float red;
    public float green;
    public float blue;
    public float alpha;

    public float destinationX;
    public float destinationZ;

    public int heroStr;
    public int heroAgi;
    public int heroInt;
    public int heroStrBonuses;
    public int heroAgiBonuses;
    public int heroIntBonuses;

    public int heroLevel;

    public int heroXP;
    public int heroSkillPoints;

    public bool heroXPSuspend;
    public int heroSkillSelect;

    public bool exploded;

    public bool pause;
    public bool pathing;

    public int points;

    public int foodUsed;
    public int foodMade;

    public int resourceAmount;

    public int userData;

    public List< string > unitAnimations;


}

public class W3Base : MonoBehaviour
{
//	[ HideInInspector ]
	public int baseID;

	[ HideInInspector ]
	public int color;

	[ HideInInspector ]
	public int race;

	public bool startAnimation = false;
	public bool randomPlay = false;

    public W3BaseData baseData;

    public W3AnimationType animationType;
    public W3AnimationType defaultAnimationType;

    public byte collision = 1;

    public void clearFogUV()
    {
        MeshFilter[] filter = GetComponentsInChildren<MeshFilter>();

        for ( int j = 0 ; j < filter.Length ; j++ )
        {
            Mesh mesh = filter[ j ].mesh;

            Vector2[] uv2 = new Vector2[ mesh.vertexCount ];

            for ( int i = 0 ; i < mesh.vertexCount ; i++ )
            {
                uv2[ i ].x = 0.0f;
                uv2[ i ].y = 0.0f;
            }

            mesh.uv2 = uv2;
        }
    }

    public void updateFogUV()
    {
        MeshFilter[] filter = GetComponentsInChildren<MeshFilter>();

        for ( int j = 0 ; j < filter.Length ; j++ )
        {
            Mesh mesh = filter[ j ].mesh;

            if ( filter[ j ].gameObject.GetComponent< W3StaicUVMesh >() == null )
            {
                continue;
            }
            
            Bounds b = mesh.bounds;

            Vector3[] v = mesh.vertices;
            Vector2[] uv2 = new Vector2[ mesh.vertexCount ];

            for ( int i = 0 ; i < mesh.vertexCount ; i++ )
            {
                Vector3 worldPt = filter[ j ].transform.TransformPoint( v[ i ] );

                uv2[ i ].x = -worldPt.x / W3FogManager.instance.fogWidth / GameDefine.TERRAIN_SIZE_PER;
                uv2[ i ].y = -worldPt.z / W3FogManager.instance.fogHeight / GameDefine.TERRAIN_SIZE_PER;
            }

            mesh.uv2 = uv2;
        }
    }

    public bool islocalPlayer()
    {
        return baseData.playerID == W3PlayerManager.instance.localPlayer;
    }

    public bool isPlayer( int pid )
    {
        return baseData.playerID == pid;
    }

    public bool isAlliance( int pid )
    {
        return W3MapManager.instance.isAlliance( pid , baseData.playerID );
    }

    public bool isEnemy( int pid )
    {
        if ( isPlayer( pid ) )
            return false;

        if ( isAlliance( pid ) )
            return false;

        return true;
    }



    public virtual void selection( bool b )
    {
    }



}



