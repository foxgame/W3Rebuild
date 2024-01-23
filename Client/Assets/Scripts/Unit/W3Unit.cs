using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public partial class W3Unit : W3Base
{
    Transform unitTrans;
    Transform shadowTrans;
    Transform selectTrans;
    Transform hpTrans;

    W3TerrainMoveableSprite shadowMoveableSprite;
    W3TerrainMoveableSprite selectionMoveableSprite;
    W3TerrainMoveableSprite uberSplatSprite;

    W3AnimationController ac;

    GameObject selectionObj;
    MeshRenderer selectionRender;

    public float upVision = GameDefine.TERRAIN_SIZE_PER * 2;

    public bool isBuilding = false;


    public W3UnitDataConfigData unitData;
    public W3UnitUIConfigData unitUI;
    public W3UnitBalanceConfigData unitBalance;
    public W3UnitWeaponsConfigData unitWeapons;
    public W3UberSplatDataConfigData splatData;


    public W3HPBar hpBar;

    public List< W3Unit > targets = new List< W3Unit >();

    List<Shader> shaderBuff = null;
    static public Dictionary<string , string> shaderDic = new Dictionary<string , string>();


    public void setActive( bool b )
    {
        gameObject.SetActive( b );
    }

    public void enable( bool b )
    {
        shadowMoveableSprite.gameObject.SetActive( b );
        selectionMoveableSprite.gameObject.SetActive( b );

        hpBar.enable( b );
    }

    public void drawLine()
    {
        if ( !isMoving ||
            targetPoints.Count == 0 )
            return;

        int c = W3MapManager.instance.playerColor[ baseData.playerID ];
        Material m = W3GameColorManager.instance.GetMaterialUnitMeshColor( c , null );

        m.SetPass( 0 );

        Vector3 pos = getPosition();

        GL.Vertex3( pos.x , pos.y + 16 , pos.z );
        pos = targetPoints[ 0 ];
        pos.y = W3TerrainManager.instance.smallNodes[ (int)-pos.z / GameDefine.TERRAIN_SIZE_PER ][ (int)-pos.x / GameDefine.TERRAIN_SIZE_PER ].y + 16;
        GL.Vertex3( pos.x , pos.y , pos.z );

        for ( int i = 0 ; i < targetPoints.Count - 1 ; i++ )
        {
            pos = targetPoints[ i ];
            pos.y = W3TerrainManager.instance.smallNodes[ (int)-pos.z / GameDefine.TERRAIN_SIZE_PER ][ (int)-pos.x / GameDefine.TERRAIN_SIZE_PER ].y + 16;
            GL.Vertex3( pos.x , pos.y , pos.z );

            pos = targetPoints[ i + 1 ];
            pos.y = W3TerrainManager.instance.smallNodes[ (int)-pos.z / GameDefine.TERRAIN_SIZE_PER ][ (int)-pos.x / GameDefine.TERRAIN_SIZE_PER ].y + 16;
            GL.Vertex3( pos.x , pos.y , pos.z );
        }
    }
    
    void Awake()
    {
        initHandler();

        unitTrans = GetComponentInChildren<Animation>().transform;

        shadowTrans = transform.Find( "Shadow" );
        selectTrans = transform.Find( "Selection" );

        selectionMoveableSprite = selectTrans.GetComponent<W3TerrainMoveableSprite>();

        if ( shadowTrans != null )
            shadowMoveableSprite = shadowTrans.GetComponent<W3TerrainMoveableSprite>();


        selectionObj = transform.Find( "Selection" ).gameObject;
        selectionRender = selectionObj.GetComponent<MeshRenderer>();

        Transform uberSplatTrans = transform.Find( "UberSplat" );

        if ( uberSplatTrans != null )
            uberSplatSprite = uberSplatTrans.GetComponent< W3TerrainMoveableSprite >();

        ac = GetComponent<W3AnimationController>();

        ac.callback = updataAnimation;

        defaultAnimationType = animationType;
    }

    void Start()
    {
        if ( startAnimation )
        {
            if ( animationType == W3AnimationType.none )
            {
                return;
            }

            if ( randomPlay )
                ac.randomPlay( animationType );
            else
                ac.play( animationType );
        }
        else
        {
            ac.noPlay( animationType );
        }
    }

    public void addHP( int hp )
    {
        baseData.hp += hp;
        hpBar.setHP( baseData.hp );

        if ( baseData.hp <= 0 )
        {
            death();
        }
    }

    public void death()
    {
        playAnimation( W3AnimationType.Death );
        W3TerrainManager.instance.removeUnit( lastPosition.x , lastPosition.z , baseID , collision );

        enable( false );
    }

    public void setHPTrans( Transform trans )
    {
        trans.transform.parent = transform;
        hpTrans = trans;
    }

    public float visionRange()
    {
        return unitBalance.sight;
    }

    public void setuberSplatPos( float x , float z )        
    {
        if ( uberSplatSprite != null )
        {
            uberSplatSprite.movePosReal( x , z );
            uberSplatSprite.updateUV1();

            setBuildingShadow( x , z );
        }
    }

    public void updateUberSplat()
    {
        string name = splatData.dir + "/Materials/" + splatData.file;
        uberSplatSprite.updateMaterial( name );
    }

    public void setBuildingShadow( float x , float z )
    {
        W3FogManager.instance.setShadow( (int)-x / GameDefine.TERRAIN_SIZE_PER - unitUI.buildingShadowX , (int)-z / GameDefine.TERRAIN_SIZE_PER - unitUI.buildingShadowZ ,
        unitUI.buildingShadowW , unitUI.buildingShadowH , unitUI.buildingShadow );
    }

    public void clearBuildingShadow( float x , float z )
    {
        W3FogManager.instance.clearShadow( (int)-x / GameDefine.TERRAIN_SIZE_PER - unitUI.buildingShadowX , (int)-z / GameDefine.TERRAIN_SIZE_PER - unitUI.buildingShadowZ ,
        unitUI.buildingShadowW , unitUI.buildingShadowH );
    }

    public void updateShader( bool b )
    {
        if ( b )
            shaderBuff = new List<Shader>();

        Renderer[] rr = GetComponentsInChildren<Renderer>();

        for ( int i = 0 ; i < rr.Length ; i++ )
        {
            if ( b )
            {
                shaderBuff.Add( rr[ i ].material.shader );

                if ( rr[ i ].material.shader != null && shaderDic.ContainsKey( rr[ i ].material.shader.name ) )
                {
                    if ( rr[ i ].gameObject.name == "UberSplat" )
                    {
                        rr[ i ].material.shader = Shader.Find( "W3/BuildBase" );
                    }
                    else
                    {
                        string str = shaderDic[ rr[ i ].material.shader.name ];
                        rr[ i ].material.shader = Shader.Find( str );
                    }
                }
            }
            else
            {
                rr[ i ].material.shader = shaderBuff[ i ];
            }
        }

    }
    
    public void updateUnit( float delay )
    {
        updateMovement( delay );
        updateAttack( delay );
        updateTrans( delay );
    }

    public bool isLocalPlayer()
    {
        return W3PlayerManager.instance.localPlayer == baseData.playerID;
    }

    void updataAnimation( W3AnimationType type , bool enabled , float time )
    {
        if ( type == W3AnimationType.Birth && enabled == false )
        {
            ac.play( defaultAnimationType );
        }

        if ( type == W3AnimationType.Death && enabled == false )
        {
            ac.play( W3AnimationType.DecayFlesh );
        }
    }

    public void playAnimation( W3AnimationType t , bool onece = false )
    {
        ac.play( t , onece );
    }

    public void pauseAnimation( bool b )
    {
        ac.pause( b );
    }

    public override void selection( bool b )
    {
        selectionObj.SetActive( b );

        if ( isBuilding )
        {
            if ( b )
            {
                setSelectionPos( unitTrans.position );

                if ( W3PlayerManager.instance.isLocalPlayer( baseData.playerID ) )
                {
                    selectionRender.material = W3MaterialConfig.instance.getMaterial( "Materials/SelectBuildingMaterial" );
                }
                else
                {
                    selectionRender.material = W3MaterialConfig.instance.getMaterial( "Materials/SelectBuildingMaterialEnemy" );
                }
            }
        }
        else
        {
            if ( b )
            {
                setSelectionPos( unitTrans.position );

                if ( W3PlayerManager.instance.isLocalPlayer( baseData.playerID ) )
                {
                    selectionRender.material = W3MaterialConfig.instance.getMaterial( "Materials/SelectUnitMaterial" );
                }
                else
                {
                    selectionRender.material = W3MaterialConfig.instance.getMaterial( "Materials/SelectUnitMaterialEnemy" );
                }
            }
            
        }


    }



    
}

