using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class W3UnitUI : SingletonMono< W3UnitUI >
{
    Image[][] basicImage = null;
    Image[][] buildingImage = null;

    Transform BasicPanel = null;
    Transform BuildingPanel = null;

    W3Order[][] order = null;
    W3Order[][] buildingOrder = null;


    public override void initSingletonMono()
    {
        BasicPanel = transform.Find( "BasicPanel" );
        BuildingPanel = transform.Find( "BuildingPanel" );

        basicImage = new Image[ 3 ][];
        buildingImage = new Image[ 3 ][];

        order = new W3Order[ 3 ][];
        buildingOrder = new W3Order[ 3 ][];

        for ( int i = 0 ; i < 3 ; i++ )
        {
            basicImage[ i ] = new Image[ 4 ];
            buildingImage[ i ] = new Image[ 4 ];
            order[ i ] = new W3Order[ 4 ];
            buildingOrder[ i ] = new W3Order[ 4 ];

            for ( int j = 0 ; j < 4 ; j++ )
            {
                basicImage[ i ][ j ] = BasicPanel.Find( "Button" + i + j ).GetComponent<Image>();
                buildingImage[ i ][ j ] = BuildingPanel.Find( "Button" + i + j ).GetComponent<Image>();

                order[ i ][ j ] = new W3Order();
                buildingOrder[ i ][ j ] = new W3Order();

                order[ i ][ j ].unitsID = new List<int>();
                buildingOrder[ i ][ j ].unitsID = new List<int>();
            }
        }
    }

//     void Start()
//     {
//         updateUI( GameDefine.UnitId( "hmil" ) );
//     }

    public void onClick( int i )
    {
        W3SelectionManager.instance.UIEvent = true;

        int x = i % 4;
        int y = i / 4;

        switch ( order[ y ][ x ].type )
        {
            case W3OrderType.builds:
                {
                    BuildingPanel.gameObject.SetActive( true );
                    BasicPanel.gameObject.SetActive( false );
                }
                break;
            case W3OrderType.cancel:
                {
                    W3UnitManager.instance.clearSelection();

                    BuildingPanel.gameObject.SetActive( false );
                    BasicPanel.gameObject.SetActive( false );
                }
                break;
            case W3OrderType.trans:
                {
                    W3UnitManager.instance.selectUnitList[ 0 ].trans( order[ y ][ x ].unitsID[ 0 ] );
                }
                break;
            default:
                break;
        }
    }

    public void onBuildingClick( int i )
    {
        W3SelectionManager.instance.UIEvent = true;

        int x = i % 4;
        int y = i / 4;

        switch ( buildingOrder[ y ][ x ].type )
        {
            case W3OrderType.build:
                {
                    W3BuildManager.instance.readyToBuild( W3UnitDataConfig.instance.getData( buildingOrder[ y ][ x ].unitsID[ 0 ] ) ,
                            W3UnitUIConfig.instance.getData( buildingOrder[ y ][ x ].unitsID[ 0 ] ) );
                }
                break;
            case W3OrderType.buildsCancel:
                {
                    BuildingPanel.gameObject.SetActive( false );
                    BasicPanel.gameObject.SetActive( true );

                    W3BuildManager.instance.clear();
                }
                break;
        }
    }

    public void clear()
    {
        BuildingPanel.gameObject.SetActive( false );
        BasicPanel.gameObject.SetActive( false );

        W3BuildManager.instance.clear();
    }

    public void updateUI( int unitID )
    {
        W3UnitFuncConfigData funData = W3UnitFuncConfig.instance.getData( unitID );
        W3UnitDataConfigData unitData = W3UnitDataConfig.instance.getData( unitID );

        BuildingPanel.gameObject.SetActive( false );
        BasicPanel.gameObject.SetActive( true );

        if ( unitData.isBuilding )
        {
            if ( funData.trains.Length > 0 )
            {
                basicImage[ 1 ][ 3 ].sprite = W3TextureConfig.instance.getSprite( W3SkinsConfig.instance.getData( "CommandRally" ) );
                order[ 1 ][ 3 ].type = W3OrderType.setrally;

                for ( int i = 0 ; i < funData.trains.Length ; i++ )
                {
                    W3UnitFuncConfigData funData1 = W3UnitFuncConfig.instance.getData( funData.trains[ i ] );

                    if ( funData1 != null )
                    {
                        basicImage[ funData1.buttonPos[ 1 ] ][ funData1.buttonPos[ 0 ] ].sprite = 
                            W3TextureConfig.instance.getSprite( funData1.art );

                        order[ funData1.buttonPos[ 1 ] ][ funData1.buttonPos[ 0 ] ].type = W3OrderType.trans;
                        order[ funData1.buttonPos[ 1 ] ][ funData1.buttonPos[ 0 ] ].unitsID.Clear();
                        order[ funData1.buttonPos[ 1 ] ][ funData1.buttonPos[ 0 ] ].unitsID.Add( GameDefine.UnitId( funData.trains[ i ] ) );
                    }
                }
            }
        }
        else
        {
            int race = W3PlayerManager.instance.getLocalPlayer().race;

            string buildCmd = "";

            if ( race == GameDefine.RACE_HUMAN.race )
            {
                buildCmd = W3SkinsConfig.instance.getData( "CommandBasicStructHuman" );
            }
            else if ( race == GameDefine.RACE_ORC.race )
            {
                buildCmd = W3SkinsConfig.instance.getData( "CommandBasicStructOrc" );
            }
            else if ( race == GameDefine.RACE_NIGHTELF.race )
            {
                buildCmd = W3SkinsConfig.instance.getData( "CommandBasicStructNightElf" );
            }
            else if ( race == GameDefine.RACE_UNDEAD.race )
            {
                buildCmd = W3SkinsConfig.instance.getData( "CommandBasicStructUndead" );
            }
            else
            {
                buildCmd = W3SkinsConfig.instance.getData( "CommandBasicStruct" );
            }

            basicImage[ 0 ][ 0 ].sprite = W3TextureConfig.instance.getSprite( W3SkinsConfig.instance.getData( "CommandMove" ) );
            basicImage[ 0 ][ 1 ].sprite = W3TextureConfig.instance.getSprite( W3SkinsConfig.instance.getData( "CommandStop" ) );
            basicImage[ 0 ][ 2 ].sprite = W3TextureConfig.instance.getSprite( W3SkinsConfig.instance.getData( "CommandHoldPosition" ) );
            basicImage[ 0 ][ 3 ].sprite = W3TextureConfig.instance.getSprite( W3SkinsConfig.instance.getData( "CommandAttack" ) );

            order[ 0 ][ 0 ].type = W3OrderType.move;
            order[ 0 ][ 1 ].type = W3OrderType.stop;
            order[ 0 ][ 2 ].type = W3OrderType.holdposition;
            order[ 0 ][ 3 ].type = W3OrderType.attack;

            basicImage[ 1 ][ 0 ].sprite = W3TextureConfig.instance.getSprite( W3SkinsConfig.instance.getData( "CommandPatrol" ) );
//             basicImage[ 1 ][ 3 ].sprite = W3TextureConfig.instance.getSprite( W3SkinsConfig.instance.getData( "CommandAttackGround" ) );

            order[ 1 ][ 0 ].type = W3OrderType.patrol;

            if ( funData.builds.Length > 0 )
            {
                basicImage[ 2 ][ 0 ].sprite = W3TextureConfig.instance.getSprite( buildCmd );
                order[ 2 ][ 0 ].type = W3OrderType.builds;

                for ( int i = 0 ; i < funData.builds.Length ; i++ )
                {
                    W3UnitFuncConfigData funData1 = W3UnitFuncConfig.instance.getData( funData.builds[ i ] );

                    if ( funData1 != null )
                    {
                        buildingImage[ funData1.buttonPos[ 1 ] ][ funData1.buttonPos[ 0 ] ].sprite =
                            W3TextureConfig.instance.getSprite( funData1.art );

                        buildingOrder[ funData1.buttonPos[ 1 ] ][ funData1.buttonPos[ 0 ] ].type = W3OrderType.build;
                        buildingOrder[ funData1.buttonPos[ 1 ] ][ funData1.buttonPos[ 0 ] ].unitsID.Clear();
                        buildingOrder[ funData1.buttonPos[ 1 ] ][ funData1.buttonPos[ 0 ] ].unitsID.Add( GameDefine.UnitId( funData.builds[ i ] ) );
                    }
                }

                buildingImage[ 2 ][ 3 ].sprite = W3TextureConfig.instance.getSprite( W3SkinsConfig.instance.getData( "CommandCancel" ) );
                buildingOrder[ 2 ][ 3 ].type = W3OrderType.buildsCancel;
            }
        }

        //        basicImage[ 1 ][ 3 ].sprite = Resources.Load< Sprite >( W3SkinsConfig.instance.getData( "CommandRally" ) );

        basicImage[ 2 ][ 3 ].sprite = W3TextureConfig.instance.getSprite( W3SkinsConfig.instance.getData( "CommandCancel" ) );
        order[ 2 ][ 3 ].type = W3OrderType.cancel;

    }

}
