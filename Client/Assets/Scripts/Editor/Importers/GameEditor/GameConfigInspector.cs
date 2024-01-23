using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;



[ CustomEditor( typeof( W3GameConfigManager ) ) ]
public class GameConfigManagerInspector : Editor
{
	static bool bload = false;

	public override void OnInspectorGUI()
	{
        base.OnInspectorGUI();

        if ( GUILayout.Button( "reload" ) || !bload ) 
		{
            string pathCustom = "";

            W3GameConfigManager config = FindObjectOfType<W3GameConfigManager>();

            if ( config.custom == W3Custom.ReignofChaos )
            {
                pathCustom = "Custom_V0";
            }
            if ( config.custom == W3Custom.FrozenThrone )
            {
                pathCustom = "Custom_V1";
            }


            bload = true;

			W3CliffTypesConfig config0 = FindObjectOfType< W3CliffTypesConfig >();
			config0.clearConfig();

			string path = Application.dataPath + "/Objects/Data/CliffTypes.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config0.initConfig();
				config0.load( bytes );
			});


			W3TerrainConfig config1 = FindObjectOfType< W3TerrainConfig >();
			config1.clearConfig();

			path = Application.dataPath + "/Objects/Data/Terrain.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config1.initConfig();
				config1.load( bytes );
			});

			W3DoodadsConfig config2 = FindObjectOfType< W3DoodadsConfig >();
			config2.clearConfig();
			path = Application.dataPath + "/Objects/Data/Doodads.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config2.initConfig();
				config2.load( bytes );
			});


			W3DestructableDataConfig config3 = FindObjectOfType< W3DestructableDataConfig >();
			config3.clearConfig();
			path = Application.dataPath + "/Objects/Data/DestructableData.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config3.initConfig();
				config3.load( bytes );
			});


			W3UnitBalanceConfig config4 = FindObjectOfType< W3UnitBalanceConfig >();
			config4.clearConfig();
			path = Application.dataPath + "/Objects/" + pathCustom + "/Units/UnitBalance.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config4.initConfig();
				config4.load( bytes );
			});


			W3UnitDataConfig config5 = FindObjectOfType< W3UnitDataConfig >();
			config5.clearConfig();
			path = Application.dataPath + "/Objects/Data/UnitData.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config5.initConfig();
				config5.load( bytes );
			});


			W3UnitUIConfig config6 = FindObjectOfType< W3UnitUIConfig >();
			config6.clearConfig();
			path = Application.dataPath + "/Objects/Data/UnitUI.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config6.initConfig();
				config6.load( bytes );
			});


			W3WaterConfig config7 = FindObjectOfType< W3WaterConfig >();
			config7.clearConfig();

			path = Application.dataPath + "/Objects/Data/Water.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config7.initConfig();
				config7.load( bytes );
			});

            W3UberSplatDataConfig config8 = FindObjectOfType<W3UberSplatDataConfig>();
            config8.clearConfig();

            path = Application.dataPath + "/Objects/Data/UberSplatData.txt";
            GameSetting.instance.loadRes(path, (byte[] bytes, bool err) =>
            {
                config8.initConfig();
                config8.load(bytes);
            });

            W3UnitWeaponsConfig config9 = FindObjectOfType<W3UnitWeaponsConfig>();
            config9.clearConfig();

            path = Application.dataPath + "/Objects/" + pathCustom + "/Units/UnitWeapons.txt";
            GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) =>
            {
                config9.initConfig();
                config9.load( bytes );
            } );


            W3ItemDataConfig config10 = FindObjectOfType<W3ItemDataConfig>();
            config10.clearConfig();

            path = Application.dataPath + "/Objects/" + pathCustom + "/Units/ItemData.txt";
            GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) =>
            {
                config10.initConfig();
                config10.load( bytes );
            } );




            //             Texture2D fogTex = new Texture2D( 32 , 32 , TextureFormat.Alpha8 , false , true );
            //             Texture2D shadowTex = new Texture2D( 32 , 32 , TextureFormat.Alpha8 , false , true );
            // 
            //             byte[] buffer = new byte[ 32 * 32 ];
            // 
            //             for ( int i = 0 ; i < 32 * 32 ; i++ )
            //             {
            //                 buffer[ i ] = 255;
            //             }
            // 
            //             fogTex.LoadRawTextureData( buffer );
            //             fogTex.Apply();
            //             shadowTex.LoadRawTextureData( buffer );
            //             shadowTex.Apply();

            //             Shader.SetGlobalTexture( "W3FogTex0" , fogTex );
            //             Shader.SetGlobalTexture( "W3ShadowTex0" , shadowTex );


            
            W3UnitFuncConfig config51 = FindObjectOfType< W3UnitFuncConfig >();
			config51.clearConfig();
			path = Application.dataPath + "/Objects/" + pathCustom + "/Units/HumanUnitFunc.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config51.initConfig();
				config51.load( bytes );
			});

			path = Application.dataPath + "/Objects/" + pathCustom + "/Units/NightElfUnitFunc.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config51.load( bytes );
			});

			path = Application.dataPath + "/Objects/" + pathCustom + "/Units/OrcUnitFunc.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config51.load( bytes );
			});

			path = Application.dataPath + "/Objects/" + pathCustom + "/Units/UndeadUnitFunc.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config51.load( bytes );
			});

			path = Application.dataPath + "/Objects/" + pathCustom + "/Units/NeutralUnitFunc.txt";
			GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) => {
				config51.load( bytes );
			});

//             path = Application.dataPath + "/Objects/" + pathCustom + "/Units/CampaignUnitFunc.txt";
//             GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) =>
//             {
//                 config51.load( bytes );
//             } );

            W3SkinsConfig config52 = FindObjectOfType< W3SkinsConfig >();
            config52.clearConfig();
            path = Application.dataPath + "/Objects/UI/war3skins.txt";
            GameSetting.instance.loadRes( path , ( byte[] bytes , bool err ) =>
            {
                config52.initConfig();
                config52.load( bytes );
            } );



        }
	}
}



