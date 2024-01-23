using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class GameToolsMenu
{
	public class AnimationFrameVisible
	{
		public ushort f;
		public byte v;
	}

	public class AnimationFrame
	{
		public string mesh;
		public List< AnimationFrameVisible > fv;
	}

	public class AnimationVisiblity : MonoBehaviour
	{
		public List< AnimationFrame > frames;
	}

	static private void CreateAsset <T> (String name) where T : ScriptableObject 
	{
		var dir = "Assets/";
		var selected = Selection.activeObject;
		if (selected != null) {
			var assetDir = AssetDatabase.GetAssetPath(selected.GetInstanceID());
			if (assetDir.Length > 0 && Directory.Exists(assetDir)) dir = assetDir + "/";
		}
		ScriptableObject asset = ScriptableObject.CreateInstance<T>();
		AssetDatabase.CreateAsset(asset, dir + name + ".asset");
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}

//	[ MenuItem("Assets/Create/W3 World Atlas") ]
//	static public void CreateGameAtlas() {
//	}

//	[ MenuItem( "W3 Tools( 1.0 ) /Movie Editor" ) ]
//	static void OpenMovieEditor()
//	{
//		GameMovieEditor window = ( GameMovieEditor )EditorWindow.GetWindow (typeof ( GameMovieEditor ), false ," Movie Editor ");
//		window.Show();
//	}
//
//	[ MenuItem( "W3 Tools( 1.0 ) /Action Viewer" ) ]
//	static void OpenActionViewer()
//	{
//		GameActionViewer window = ( GameActionViewer )EditorWindow.GetWindow ( typeof ( GameActionViewer )  , false , " Action Viewer " );
//		window.Show();
//		window.loadData();
//	}


	[ MenuItem( "W3 Tools/Jass/Jass2CSharpBJ" ) ]
	static void Jass2CSharpBJEditor()
	{
		UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );  

		for ( int i = 0 ; i < arr.Length ; i++ )
		{
			string path = Path.ChangeExtension( AssetDatabase.GetAssetPath( arr[i] ) , "" );
			path += Path.GetExtension( AssetDatabase.GetAssetPath( arr[i] ) ).Substring( 1 , Path.GetExtension( AssetDatabase.GetAssetPath( arr[i] ) ).Length - 1 );
			path += ".cs";

			FileStream fs = File.OpenRead( AssetDatabase.GetAssetPath( arr[i] ) );
			byte[] bytes1 = new byte[ fs.Length ];
			fs.Read( bytes1 , 0 , (int)fs.Length );
			fs.Close();

			JassCompiler jc = new JassCompiler( System.Text.Encoding.UTF8.GetString( bytes1 ) , "" );
			string str = jc.GetCSharpCode();

			fs = File.Create( path );
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes( str );
			fs.Write( bytes , 0 , bytes.Length );
			fs.Close();
		}
	}

	[ MenuItem( "W3 Tools/Jass/Jass2CSharpNormal" ) ]
	static void Jass2CSharpNormalEditor()
	{
		UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );  

		for ( int i = 0 ; i < arr.Length ; i++ )
		{
			string path = Path.ChangeExtension( AssetDatabase.GetAssetPath( arr[i] ) , "" );
			path += Path.GetExtension( AssetDatabase.GetAssetPath( arr[i] ) ).Substring( 1 , Path.GetExtension( AssetDatabase.GetAssetPath( arr[i] ) ).Length - 1 );
			path = path.Replace( "." , "_" );
			path += ".cs";

			FileStream fs = File.OpenRead( AssetDatabase.GetAssetPath( arr[i] ) );
			byte[] bytes1 = new byte[ fs.Length ];
			fs.Read( bytes1 , 0 , (int)fs.Length );
			fs.Close();

			string name = Path.GetFileNameWithoutExtension( AssetDatabase.GetAssetPath( arr[ i ] ) ) + Path.GetExtension( AssetDatabase.GetAssetPath( arr[ i ] ) );
			name = name.Replace( "." , "_" );

			JassCompiler jc = new JassCompiler( System.Text.Encoding.UTF8.GetString( bytes1 ) , name );
			string str = jc.GetCSharpCode();

			fs = File.Create( path );
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes( str );
			fs.Write( bytes , 0 , bytes.Length );
			fs.Close();
		}
	}


	[ MenuItem( "W3 Tools/File/Create" ) ]
	static void FileCreateMapEditor()
	{
		GameFileCreateEditor window = ( GameFileCreateEditor )EditorWindow.GetWindow ( typeof ( GameFileCreateEditor )  , false , "Create" );
		window.Show();
	}
	[ MenuItem( "W3 Tools/File/Open" ) ]
	static void FileOpenMapEditor()
	{
		GameFileCreateEditor window = ( GameFileCreateEditor )EditorWindow.GetWindow ( typeof ( GameFileCreateEditor )  , false , "Open Map" );
		window.Show();
	}
	[ MenuItem( "W3 Tools/File/Close" ) ]
	static void FileCloseMapEditor()
	{
		GameFileCreateEditor window = ( GameFileCreateEditor )EditorWindow.GetWindow ( typeof ( GameFileCreateEditor )  , false , "Close Map" );
		window.Show();
	}


// 	[ MenuItem( "W3 Tools/FBX/XMLConvert" ) ]
// 	static void FBXCreatePrefabXMLEditor()
// 	{
// 		GameFBXXml x = new GameFBXXml();
// 
// 		UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );  
// 
// 		for ( int i = 0 ; i < arr.Length ; i++ )
// 		{
// 			x.load( AssetDatabase.GetAssetPath( arr[ i ] ) );
// 
// 			CreateMateral( x , Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) ) );
// 		}
// 	}
    
	[ MenuItem( "W3 Tools/FBX/CreatePrefabUnit" ) ]
	static void FBXCreatePrefabUnit()
	{
        if ( GameObject.Find( "testUnits" ) != null )
            GameObject.DestroyImmediate( GameObject.Find( "testUnits" ) );

        GameObject objTest = new GameObject( "testUnits" );
        List<string> objTest2 = new List<string>();

        UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );

        if ( arr.Length == 0 )
        {
            return;
        }

        GameObject[] objs = new GameObject[ arr.Length ];

        for ( int i = 0 ; i < arr.Length ; i++ )
        {
            string pathDir = Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) );
            pathDir = pathDir.Replace( '\\' , '/' );

            int n1 = pathDir.IndexOf( "Units/" );
            int n2 = pathDir.Substring( n1 + 6 , pathDir.Length - n1 - 7 ).IndexOf( '/' );

            string pathDir2 = pathDir.Substring( n1 + 6 , n2 );

            if ( objTest2.Find( o => o == pathDir2 ) == null )
            {
                GameObject obj2 = new GameObject( pathDir2 );
                obj2.transform.parent = objTest.transform;
                obj2.transform.localPosition = new Vector3( 0.0f , 0.0f , 600 + objTest2.Count * 15 );
                objTest2.Add( pathDir2 );
            }

            GameFBXXml x = new GameFBXXml();
            x.load( AssetDatabase.GetAssetPath( arr[ i ] ) );

            Material[] mater = new Material[ x.materials.Count ];

            Texture2D[] t2d = CreateMateralUnit( mater , x , Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) ) );

            string path = Path.ChangeExtension( AssetDatabase.GetAssetPath( arr[ i ] ) , ".prefab" );
            path = path.Replace( '\\' , '/' );
            path = path.Replace( "Objects" , "Resources/Prefabs" );

            if ( !Directory.Exists( Path.GetDirectoryName( path ) ) )
            {
                Directory.CreateDirectory( Path.GetDirectoryName( path ) );
            }

            GameObject obj1 = new GameObject();
            GameObject objUnit = GameObject.Instantiate( (GameObject)arr[ i ] );
            objUnit.name = "Unit";
            objUnit.transform.parent = obj1.transform;



            W3UnitBalanceConfig config7 = UnityEngine.Object.FindObjectOfType<W3UnitBalanceConfig>();
            W3UnitBalanceConfigData unitData1 = new W3UnitBalanceConfigData();

            W3UnitUIConfig config6 = UnityEngine.Object.FindObjectOfType<W3UnitUIConfig>();
            W3UnitUIConfigData unitData = new W3UnitUIConfigData();

            for (int j = 0; j < config6.list.Count; j++)
            {
                if (path.ToLower().Contains(config6.list[j].file.Replace('\\', '/').ToLower()))
                {
                    unitData = config6.list[j];
                    break;
                }
            }

            for (int j = 0; j < config7.list.Count; j++)
            {
                if (config7.list[j].unitBalanceID == unitData.unitID)
                {
                    unitData1 = config7.list[j];
                    break;
                }
            }


            if ( !path.Contains( "Portrait" ) &&
                !path.Contains("portrait") )
            {
                GameObject objSelection = null;

                if ( unitData.selY > 0 )
                {
                    objSelection = (GameObject)GameObject.Instantiate(Resources.Load("W3SelectUnitFly"));
                }
                else
                {
                    objSelection = (GameObject)GameObject.Instantiate(Resources.Load("W3SelectUnit"));
                }

                objSelection.name = "Selection";
                objSelection.transform.parent = obj1.transform;
                objSelection.SetActive(false);

                GameObject objShadow = (GameObject)GameObject.Instantiate(Resources.Load("W3Shadow"));
                objShadow.name = "Shadow";
                objShadow.transform.parent = obj1.transform;
                MeshRenderer shadowmr = objShadow.GetComponent<MeshRenderer>();

                
//                Debug.Log(path);

                
                objSelection.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

                W3TerrainMoveableSprite selectionms = objSelection.GetComponent<W3TerrainMoveableSprite>();

                if ( selectionms != null )
                {
                    selectionms.initSprite( unitData.scale , unitData.scale , unitData.scale * 0.5f , unitData.scale * 0.5f );
                }
                else
                {
                    objSelection.transform.localScale = new Vector3( unitData.scale , unitData.scale , 1.0f );
                }


                if (unitData.unitShadow != null &&
                    unitData.unitShadow.Length > 0 )
                {
                    Texture2D texture3232 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/ReplaceableTextures/Shadows/" + unitData.unitShadow + ".TGA", typeof(Texture2D)) as Texture2D;
                    string path3232 = "Assets/Resources/ReplaceableTextures/Shadows/Materials/" + unitData.unitShadow + ".mat";
                    Material mater3232;

                    if (File.Exists(path3232))
                        mater3232 = AssetDatabase.LoadAssetAtPath<Material>(path3232);
                    else
                    {
                        mater3232 = new Material(Shader.Find("W3/UnitSelection"));
                        AssetDatabase.CreateAsset(mater3232, path3232);
                    }

                    mater3232.mainTexture = texture3232;
                    shadowmr.material = mater3232;

                    objShadow.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

                    W3TerrainMoveableSprite shadowms = objShadow.GetComponent<W3TerrainMoveableSprite>();
                    shadowms.initSprite( unitData.shadowW - 16 , unitData.shadowH - 16 , unitData.shadowX , unitData.shadowY );
                  
                }
                else
                {
                    objShadow.SetActive(false);
                }
            }
            



            GameObject obj = (GameObject)PrefabUtility.CreatePrefab( path , obj1 );

            GameObject.DestroyImmediate( obj1 );

            //          MeshFilter filter = obj.GetComponentInChildren<MeshFilter>();
            //          MeshCollider collider = filter.gameObject.AddComponent<MeshCollider>();
            //          collider.sharedMesh = filter.sharedMesh;

            W3Unit w3u = obj.AddComponent<W3Unit>();
            w3u.startAnimation = false;
//             w3u.sight = unitData1.sight;
//             w3u.nsight = unitData1.nsight;

//            W3MoveController w3mc = obj.AddComponent<W3MoveController>();
//            w3mc.moveSpeed = unitData.run;

            Animation ani = obj.GetComponentInChildren<Animation>();
            W3AnimationType type = W3AnimationType.none;
            
            if ( ani != null )
            {
                ani.playAutomatically = false;
                ani.cullingType = AnimationCullingType.BasedOnRenderers;

                foreach ( AnimationState state in ani )
                {
                    if ( state.name.Contains( "Stand" ) )
                    {
                        type = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , state.name );
                        ani.clip = state.clip;
                        break;
                    }
                }
                if ( type == W3AnimationType.none )
                {
                    foreach ( AnimationState state in ani )
                    {
                        if ( state.name.Contains( "Portrait" ) )
                        {
                            type = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , state.name );
                            ani.clip = state.clip;
                            break;
                        }
                    }
                }
                if ( type == W3AnimationType.none )
                {
                    foreach ( AnimationState state in ani )
                    {
                        if ( state.name.Contains( "Birth" ) )
                        {
                            type = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , state.name );
                            ani.clip = state.clip;
                            break;
                        }
                    }
                }
            }

            w3u.animationType = type;

            for ( int j = 0 ; j < x.geoset.Count ; j++ )
            {
                string n = x.name + ( j + 1 );
                Transform t = GetTransform( obj.transform , n );

                if ( t != null )
                {
                    Renderer r = t.GetComponent<Renderer>();
                    r.receiveShadows = false;

                    if ( r != null )
                    {
                        r.enabled = true;

                        int tID = x.materials[ x.geoset[ j ].materialID ].layers[ 0 ].textureID;

                        if ( x.textures[ tID ].replaceable == 1 )
                        {
                            W3UnitMeshColor c = t.gameObject.AddComponent<W3UnitMeshColor>();
                            if ( x.materials[ x.geoset[ j ].materialID ].layers.Count > 1 )
                            {
                                c.texture2d = t2d[ x.materials[ x.geoset[ j ].materialID ].layers[ 1 ].textureID ];
                            }
                            r.sharedMaterial = null;
                        }
                        else if ( x.textures[ tID ].replaceable == 2 )
                        {
                            W3TeamGlow c = t.gameObject.AddComponent<W3TeamGlow>();
                            r.sharedMaterial = null;
                        }
                        else
                        {
                            if ( x.textures[ tID ].replaceable > 2 )
                            {
                                W3ReplaceableTexture c = t.gameObject.AddComponent<W3ReplaceableTexture>();
                                c.replaceable = x.textures[ tID ].replaceable;

                                if ( x.materials[ x.geoset[ j ].materialID ].layers.Count > 1 )
                                {
                                    //c.texture2d = t2d[ x.materials[ x.geoset[ j ].materialID ].layers[ 1 ].textureID ];
                                }
                            }

                            r.sharedMaterial = mater[ x.geoset[ j ].materialID ];

                        }

                    }
                }
            }

            FBXCreateAnimations( x , obj );

            objs[ i ] = (GameObject)PrefabUtility.InstantiatePrefab( obj );

            AssetDatabase.SaveAssets();
        }

        AssetDatabase.Refresh();

        for ( int i = 0 ; i < objs.Length ; i++ )
        {
            for ( int j = 0 ; j < objTest2.Count ; j++ )
            {
                if ( AssetDatabase.GetAssetPath( arr[ i ] ).Contains( objTest2[ j ] ) )
                {
                    objs[ i ].transform.parent = GameObject.Find( "testUnits/" + objTest2[ j ] ).transform;
                    objs[ i ].transform.localPosition = new Vector3( GameObject.Find( "testUnits/" + objTest2[ j ] ).transform.childCount * 15.0f , 0 , 0 );
                    break;
                }
            }
        }

    }


	static Texture2D[] CreateMateral( Material[] mater , GameFBXXml xml , string path )
	{
        Texture2D[] t2d = new Texture2D[ xml.textures.Count ];

        for ( int i = 0 ; i < xml.materials.Count ; i++ )
		{
			string pathT1 = path + "/" + xml.name + i + ".mat";
            
            for ( int j = 0 ; j < xml.materials[ i ].layers.Count ; j++ ) 
			{
				string pt = "Assets/Resources/" + xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name;
				pt = pt.Replace( ".blp" , ".TGA" );
				pt = pt.Replace( ".BLP" , ".TGA" );
				pt = pt.Replace( "\\" , "/" );

                if ( !File.Exists( pt ) )
                {
                    pt = "Assets/Objects/" + xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name;
                    pt = pt.Replace( ".blp" , ".TGA" );
                    pt = pt.Replace( ".BLP" , ".TGA" );
                    pt = pt.Replace( "\\" , "/" );

                    if ( xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name.Contains( "TeamGlow" ) )
                    {
                        xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name = "";
                        xml.textures[ xml.materials[ i ].layers[ j ].textureID ].replaceable = 2;
                    }
                }

                Texture2D texture = null;

                if ( xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name.Length > 3 )
				{
					texture = (Texture2D)AssetDatabase.LoadAssetAtPath( pt , typeof( Texture2D ) ) as Texture2D;
                    t2d[ xml.materials[ i ].layers[ j ].textureID ] = texture; 
                }

				if ( xml.materials[ i ].layers.Count > 1 )
				{
					if ( xml.textures[ xml.materials[ i ].layers[ 0 ].textureID ].replaceable == 1 )
					{
                        if ( mater[ i ] == null )
						{
                            mater[ i ] = new Material( Shader.Find( "W3/MeshColor" ) );
                        }

						if ( mater[ i ].mainTexture == null &&
							xml.textures[ xml.materials[ i ].layers[ j ].textureID ].replaceable != 1 )
						{
							mater[ i ].mainTexture = texture;
						}
					}
					else
					{
						if ( mater[ i ] == null )
						{
                            mater[ i ] = new Material( Shader.Find( "W3/MeshAlphaColor3" ) );
						}

						if ( j == 0 )
						{
							mater[ i ].mainTexture = texture;
						}
						else
						{
							mater[ i ].SetTexture( "_SubTex" + j , texture );
						}
					}
				}
				else
				{
					pathT1 = Path.ChangeExtension( pt ,  "mat" );
					pathT1 = pathT1.Insert( pathT1.Length - 4 , "_" + xml.materials[ i ].layers[ j ].filterMode );

                    if ( File.Exists( pathT1 ) )
                    {
                        mater[ i ] = AssetDatabase.LoadAssetAtPath<Material>( pathT1 );
                    }
                    else
                    {
                        switch ( xml.materials[ i ].layers[ j ].filterMode )
                        {
                            case 0:
                                mater[ i ] = new Material( Shader.Find( "W3/MeshBase" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 1:
                                mater[ i ] = new Material( Shader.Find( "W3/MeshBaseAlphaCutoff" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 2:
                                mater[ i ] = new Material( Shader.Find( "W3/MeshBaseAlpha" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 3:
                                mater[ i ] = new Material( Shader.Find( "W3/MeshAdditive" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 4:
                                mater[ i ] = new Material( Shader.Find( "W3/MeshBaseAlphaCutoff" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            default:
                                mater[ i ] = new Material( Shader.Find( "W3/MeshBase" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                        }
                    }		
				}

				//mater[ i ].color = new Color( 1.0f , 1.0f , 1.0f , xml.materials[ i ].layers[ j ].alpha );
			}

			if ( !File.Exists( pathT1 ) && mater[ i ].mainTexture != null && 
                xml.textures[ xml.materials[ i ].layers[ 0 ].textureID ].replaceable != 1 &&
                xml.textures[ xml.materials[ i ].layers[ 0 ].textureID ].replaceable != 2 )
				AssetDatabase.CreateAsset( mater[ i ] , pathT1 );

            if ( File.Exists( pathT1 ) )
                mater[ i ] = AssetDatabase.LoadAssetAtPath<Material>( pathT1 );
        }

        return t2d;
	}

    static Texture2D[] CreateMateralUnit( Material[] mater , GameFBXXml xml , string path )
    {
        Texture2D[] t2d = new Texture2D[ xml.textures.Count ];

        for ( int i = 0 ; i < xml.materials.Count ; i++ )
        {
            string pathT1 = path + "/" + xml.name + i + "Unit.mat";

            for ( int j = 0 ; j < xml.materials[ i ].layers.Count ; j++ )
            {
                string pt = "Assets/Resources/" + xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name;
                pt = pt.Replace( ".blp" , ".TGA" );
                pt = pt.Replace( ".BLP" , ".TGA" );
                pt = pt.Replace( "\\" , "/" );

                if ( !File.Exists( pt ) )
                {
                    pt = "Assets/Objects/" + xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name;
                    pt = pt.Replace( ".blp" , ".TGA" );
                    pt = pt.Replace( ".BLP" , ".TGA" );
                    pt = pt.Replace( "\\" , "/" );

                    if ( xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name.Contains( "TeamGlow" ) )
                    {
                        xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name = "";
                        xml.textures[ xml.materials[ i ].layers[ j ].textureID ].replaceable = 2;
                    }
                }

                Texture2D texture = null;

                if ( xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name.Length > 3 )
                {
                    texture = (Texture2D)AssetDatabase.LoadAssetAtPath( pt , typeof( Texture2D ) ) as Texture2D;
                    t2d[ xml.materials[ i ].layers[ j ].textureID ] = texture;
                }

                if ( xml.materials[ i ].layers.Count > 1 )
                {
                    if ( xml.textures[ xml.materials[ i ].layers[ 0 ].textureID ].replaceable == 1 )
                    {
                        if ( mater[ i ] == null )
                        {
                            mater[ i ] = new Material( Shader.Find( "W3/UnitMeshColor" ) );
                        }

                        if ( mater[ i ].mainTexture == null &&
                            xml.textures[ xml.materials[ i ].layers[ j ].textureID ].replaceable != 1 )
                        {
                            mater[ i ].mainTexture = texture;
                        }
                    }
                    else
                    {
                        if ( mater[ i ] == null )
                        {
                            mater[ i ] = new Material( Shader.Find( "W3/UnitMeshAlphaColor3" ) );
                        }

                        if ( j == 0 )
                        {
                            mater[ i ].mainTexture = texture;
                        }
                        else
                        {
                            mater[ i ].SetTexture( "_SubTex" + j , texture );
                        }
                    }
                }
                else
                {
                    pathT1 = Path.ChangeExtension( pt , "mat" );
                    pathT1 = pathT1.Insert( pathT1.Length - 4 , "_" + xml.materials[ i ].layers[ j ].filterMode + "_Unit" );

                    if ( File.Exists( pathT1 ) )
                    {
                        mater[ i ] = AssetDatabase.LoadAssetAtPath<Material>( pathT1 );
                    }
                    else
                    {
                        switch ( xml.materials[ i ].layers[ j ].filterMode )
                        {
                            case 0:
                                mater[ i ] = new Material( Shader.Find( "W3/UnitMeshBase" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 1:
                                mater[ i ] = new Material( Shader.Find( "W3/UnitMeshBaseAlphaCutoff" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 2:
                                mater[ i ] = new Material( Shader.Find( "W3/UnitMeshBaseAlpha" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 3:
                                mater[ i ] = new Material( Shader.Find( "W3/UnitMeshAdditive" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 4:
                                mater[ i ] = new Material( Shader.Find( "W3/UnitMeshBaseAlphaCutoff" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            default:
                                mater[ i ] = new Material( Shader.Find( "W3/UnitMeshBase" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                        }
                    }
                }

                //mater[ i ].color = new Color( 1.0f , 1.0f , 1.0f , xml.materials[ i ].layers[ j ].alpha );
            }

            if ( !File.Exists( pathT1 ) && mater[ i ].mainTexture != null &&
                xml.textures[ xml.materials[ i ].layers[ 0 ].textureID ].replaceable != 1 &&
                xml.textures[ xml.materials[ i ].layers[ 0 ].textureID ].replaceable != 2 )
                AssetDatabase.CreateAsset( mater[ i ] , pathT1 );

            if ( File.Exists( pathT1 ) )
                mater[ i ] = AssetDatabase.LoadAssetAtPath<Material>( pathT1 );
        }

        return t2d;
    }

    static void CreateMateralSkinned( Material[] mater , GameFBXXml xml , string path , bool unit = false )
    {
        for ( int i = 0 ; i < xml.materials.Count ; i++ )
        {
            string pathT1 = path + "/" + xml.name + i + "Skinned.mat";

            for ( int j = 0 ; j < xml.materials[ i ].layers.Count ; j++ )
            {
                string pt = "Assets/Resources/" + xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name;
                pt = pt.Replace( ".blp" , ".TGA" );
                pt = pt.Replace( ".BLP" , ".TGA" );
                pt = pt.Replace( "\\" , "/" );

                if ( !File.Exists( pt ) )
                {
                    pt = "Assets/Objects/" + xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name;
                    pt = pt.Replace( ".blp" , ".TGA" );
                    pt = pt.Replace( ".BLP" , ".TGA" );
                    pt = pt.Replace( "\\" , "/" );

                    if ( xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name.Contains( "TeamGlow" ) )
                    {
                        xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name = "";
                        xml.textures[ xml.materials[ i ].layers[ j ].textureID ].replaceable = 2;
                    }
                }

                Texture2D texture = null;

                if ( xml.textures[ xml.materials[ i ].layers[ j ].textureID ].name.Length > 3 )
                {
                    texture = (Texture2D)AssetDatabase.LoadAssetAtPath( pt , typeof( Texture2D ) ) as Texture2D;
                }

                if ( xml.materials[ i ].layers.Count > 1 )
                {
                    if ( xml.textures[ xml.materials[ i ].layers[ 0 ].textureID ].replaceable == 1 )
                    {
                        if ( mater[ i ] == null )
                        {
                            mater[ i ] = new Material( Shader.Find( "W3/SkinnedMeshColor" ) );
                        }

                        if ( mater[ i ].mainTexture == null &&
                            xml.textures[ xml.materials[ i ].layers[ j ].textureID ].replaceable != 1 )
                        {
                            mater[ i ].mainTexture = texture;
                        }
                    }
                    else
                    {
                        if ( mater[ i ] == null )
                        {
                            mater[ i ] = new Material( Shader.Find( "W3/SkinnedMeshAlphaColor3" ) );
                        }

                        if ( j == 0 )
                        {
                            mater[ i ].mainTexture = texture;
                        }
                        else
                        {
                            mater[ i ].SetTexture( "_SubTex" + j , texture );
                        }
                    }
                }
                else
                {
                    pathT1 = Path.ChangeExtension( pt , "mat" );
                    pathT1 = pathT1.Insert( pathT1.Length - 4 , "_" + xml.materials[ i ].layers[ j ].filterMode + "_" + "Skinned" );

                    if ( File.Exists( pathT1 ) )
                    {
                        mater[ i ] = AssetDatabase.LoadAssetAtPath<Material>( pathT1 );
                    }
                    else
                    {
                        switch ( xml.materials[ i ].layers[ j ].filterMode )
                        {
                            case 0:
                                mater[ i ] = new Material( Shader.Find( "W3/SkinnedMeshBase" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 1:
                                mater[ i ] = new Material( Shader.Find( "W3/SkinnedMeshBaseAlphaCutoff" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 2:
                                mater[ i ] = new Material( Shader.Find( "W3/SkinnedMeshBaseAlpha" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 3:
                                mater[ i ] = new Material( Shader.Find( "W3/SkinnedMeshAdditive" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            case 4:
                                mater[ i ] = new Material( Shader.Find( "W3/SkinnedMeshBaseAlphaCutoff" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                            default:
                                mater[ i ] = new Material( Shader.Find( "W3/SkinnedMeshBase" ) );
                                mater[ i ].mainTexture = texture;
                                break;
                        }
                    }
                }

                //mater[ i ].color = new Color( 1.0f , 1.0f , 1.0f , xml.materials[ i ].layers[ j ].alpha );
            }

            if ( !File.Exists( pathT1 ) && mater[ i ].mainTexture != null &&
                xml.textures[ xml.materials[ i ].layers[ 0 ].textureID ].replaceable != 1 &&
                xml.textures[ xml.materials[ i ].layers[ 0 ].textureID ].replaceable != 2 )
                AssetDatabase.CreateAsset( mater[ i ] , pathT1 );

            if ( File.Exists( pathT1 ) )
                mater[ i ] = AssetDatabase.LoadAssetAtPath<Material>( pathT1 );
        }
    }


    [ MenuItem( "W3 Tools/FBX/CreateMaterial" ) ]
	static void FBXCreateMaterialEditor()
	{
		UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );  

		for ( int i = 0 ; i < arr.Length ; i++ )
		{
            string path = Path.ChangeExtension( AssetDatabase.GetAssetPath( arr[ i ] ) , ".mat" );
            path = path.Insert( path.Length - Path.GetFileName( AssetDatabase.GetAssetPath( arr[ i ] ) ).Length , "Materials/" );

            Texture textur = (Texture)AssetDatabase.LoadAssetAtPath( AssetDatabase.GetAssetPath( arr[ i ] ) , typeof( Texture ) ) as Texture;

            Material mater = new Material( Shader.Find( "W3/MeshAdditive" ) );
            mater.mainTexture = textur;

            AssetDatabase.CreateAsset( mater , path );
        }

    }

    [MenuItem( "W3 Tools/FBX/CreateTexture" )]
    static void FBXCreateTextureEditor()
    {
        Texture2D t2d = new Texture2D( 256 , 256 );

        for ( int i = 0 ; i < t2d.width ; i++ )
        {
            for ( int j = 0 ; j < t2d.height ; j++ )
            {
                if ( j % 16 == 0 || i % 16 == 0 )
                {
                    t2d.SetPixel( j , i , new Color( 1.0f , 0.0f , 0.0f , 0.0f ) );
                }

            }
        }

       byte[] pngData = t2d.EncodeToPNG();
       File.WriteAllBytes( "Assets/" + "test256.png" , pngData );
    }

    // 	[ MenuItem( "W3 Tools/FBX/CreateMaterialTexture" ) ]
    // 	static void FBXCreateMaterialTextureEditor()
    // 	{
    // 		UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );  
    // 
    // 		Dictionary< string , int > dicv3 = new Dictionary<string, int>();
    // 
    // 		for (int k = 0; k < (int)W3AnimationType.count ; k++ ) 
    // 		{
    // 			string str11 = ((W3AnimationType)k).ToString();
    // 			dicv3.Add( str11 , k );
    // 		}
    // 
    // 		for ( int i = 0 ; i < arr.Length ; i++ )
    // 		{
    // 			//Debug.LogError(Application.dataPath.Substring(0,Application.dataPath.LastIndexOf('/'))+"/"+ AssetDatabase.GetAssetPath(arr[0]));
    // 
    // 			//GameObject prefab = PrefabUtility.InstantiatePrefab( arr[ i ] ) as GameObject;
    // 
    // 			string pt = "Assets/Objects/Materials/" + Path.GetFileNameWithoutExtension( AssetDatabase.GetAssetPath( arr[i] ) );
    // 			pt += ".mat";
    // 
    // 			Texture textur = (Texture)AssetDatabase.LoadAssetAtPath( AssetDatabase.GetAssetPath( arr[i] ) , typeof(Texture) ) as Texture;
    // 
    // 			Material mater = new Material( Shader.Find ( "Mobile/Diffuse" ) );
    // 			mater.mainTexture = textur;
    // 
    // 			AssetDatabase.CreateAsset( mater , pt );
    // 		}
    // 
    // 	}

    [ MenuItem( "W3 Tools/FBX/CreatePrefabBuilding" ) ]
	static void FBXCreatePrefabBuilding()
	{
        if ( GameObject.Find( "testBuildings" ) != null )
            GameObject.DestroyImmediate( GameObject.Find( "testBuildings" ) );
        GameObject objTest = new GameObject( "testBuildings" );
        List<string> objTest2 = new List<string>();

        UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );

        if ( arr.Length == 0 )
        {
            return;
        }

        GameObject[] objs = new GameObject[ arr.Length ];

        for ( int i = 0 ; i < arr.Length ; i++ )
        {
            string pathDir = Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) );
            pathDir = pathDir.Replace( '\\' , '/' );

            int n1 = pathDir.IndexOf( "Buildings/" );
            int n2 = pathDir.Substring( n1 + 10 , pathDir.Length - n1 - 11 ).IndexOf( '/' );

            string pathDir2 = pathDir.Substring( n1 + 10 , n2 );

            if ( objTest2.Find( o => o == pathDir2 ) == null )
            {
                GameObject obj2 = new GameObject( pathDir2 );
                obj2.transform.parent = objTest.transform;
                obj2.transform.localPosition = new Vector3( 0.0f , 0.0f , objTest2.Count * 15 );
                objTest2.Add( pathDir2 );
            }

            GameFBXXml x = new GameFBXXml();
            x.load( AssetDatabase.GetAssetPath( arr[ i ] ) );


            string path = Path.ChangeExtension( AssetDatabase.GetAssetPath( arr[ i ] ) , ".prefab" );
            path = path.Replace( '\\' , '/' );
            path = path.Replace( "Objects" , "Resources/Prefabs" );

            if ( !Directory.Exists( Path.GetDirectoryName( path ) ) )
            {
                Directory.CreateDirectory( Path.GetDirectoryName( path ) );
            }



            GameObject obj1 = new GameObject();
            GameObject objUnit = GameObject.Instantiate( (GameObject)arr[ i ] );
            objUnit.name = "Building";
            objUnit.transform.parent = obj1.transform;

            GameObject objSelection = (GameObject)GameObject.Instantiate( Resources.Load( "W3SelectUnit" ) );
            objSelection.name = "Selection";
            objSelection.transform.parent = obj1.transform;
            objSelection.SetActive( false );


            //             GameObject objShadow = (GameObject)GameObject.Instantiate( Resources.Load( "W3Shadow" ) );
            //             objShadow.name = "Shadow";
            //             objShadow.transform.parent = obj1.transform;
            //             MeshRenderer shadowmr = objShadow.GetComponent<MeshRenderer>();

            W3UnitBalanceConfig config7 = UnityEngine.Object.FindObjectOfType<W3UnitBalanceConfig>();
            W3UnitBalanceConfigData unitData1 = new W3UnitBalanceConfigData();

            W3UnitUIConfig config6 = UnityEngine.Object.FindObjectOfType<W3UnitUIConfig>();
            W3UnitUIConfigData unitData = new W3UnitUIConfigData();

            W3UnitDataConfig config5 = UnityEngine.Object.FindObjectOfType<W3UnitDataConfig>();
            W3UnitDataConfigData unitData2 = new W3UnitDataConfigData();

            for ( int j = 0 ; j < config6.list.Count ; j++ )
            {
                if ( path.ToLower().Contains( config6.list[ j ].file.Replace( '\\' , '/' ).ToLower() ) )
                {
                    unitData = config6.list[ j ];
                    break;
                }
            }

            for ( int j = 0 ; j < config7.list.Count ; j++ )
            {
                if ( config7.list[ j ].unitBalanceID == unitData.unitID )
                {
                    unitData1 = config7.list[ j ];
                    break;
                }
            }

            for ( int j = 0 ; j < config5.list.Count ; j++ )
            {
                if ( config5.list[ j ].unitID == unitData.unitID )
                {
                    unitData2 = config5.list[ j ];
                    break;
                }
            }


            W3UberSplatDataConfig config8 = UnityEngine.Object.FindObjectOfType<W3UberSplatDataConfig>();
            W3UberSplatDataConfigData splatData = null;
            for (int j = 0; j < config8.list.Count; j++)
            {
                if (config8.list[j].name == unitData.uberSplat)
                {
                    splatData = config8.list[j];
                    break;
                }
            }

            objSelection.transform.localPosition = new Vector3( 0 , 0.1f , 0 );
            W3TerrainMoveableSprite selectionms = objSelection.GetComponent<W3TerrainMoveableSprite>();
            selectionms.initSprite(unitData.scale, unitData.scale, unitData.scale * 0.5f, unitData.scale * 0.5f);

            if ( splatData != null )
            {
                GameObject objSplat = (GameObject)GameObject.Instantiate(Resources.Load("W3UberSplat"));
                objSplat.name = "UberSplat";
                objSplat.transform.parent = obj1.transform;

                objSplat.transform.localPosition = new Vector3(0, 0.0f, 0);
                W3TerrainMoveableSprite splatms = objSplat.GetComponent<W3TerrainMoveableSprite>();
//                 splatms.initSprite(unitData.scale * splatData.scale / 256 , unitData.scale * splatData.scale / 256 ,
//                     unitData.scale * splatData.scale * 0.5f / 256 , unitData.scale * splatData.scale * 0.5f / 256 );

                splatms.initSprite( splatData.scale * 2 , splatData.scale * 2 ,
                 splatData.scale , splatData.scale );

                MeshRenderer splatr = objSplat.GetComponent<MeshRenderer>();
                splatr.sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>( "Assets/Resources/" + splatData.dir + "/Materials/" + splatData.file + ".mat");
                splatr.sharedMaterial.shader = Shader.Find( "W3/TerrainBaseAlpha" );
            }

            if ( unitData.unitShadow != null &&
                unitData.unitShadow.Length > 0 )
            {
                // tree or something movable.

                GameObject objShadow = (GameObject)GameObject.Instantiate( Resources.Load( "W3Shadow" ) );
                objShadow.name = "Shadow";
                objShadow.transform.parent = obj1.transform;
                MeshRenderer shadowmr = objShadow.GetComponent<MeshRenderer>();

                Texture2D texture3232 = (Texture2D)AssetDatabase.LoadAssetAtPath( "Assets/Resources/ReplaceableTextures/Shadows/" + unitData.unitShadow + ".TGA" , typeof( Texture2D ) ) as Texture2D;
                string path3232 = "Assets/Resources/ReplaceableTextures/Shadows/Materials/" + unitData.unitShadow + ".mat";
                Material mater3232;

                if ( File.Exists( path3232 ) )
                    mater3232 = AssetDatabase.LoadAssetAtPath<Material>( path3232 );
                else
                {
                    mater3232 = new Material( Shader.Find( "W3/UnitSelection" ) );
                    AssetDatabase.CreateAsset( mater3232 , path3232 );
                }

                mater3232.mainTexture = texture3232;
                shadowmr.material = mater3232;

                objShadow.transform.localPosition = new Vector3( 0.0f , 0.0f , 0.0f );

                W3TerrainMoveableSprite shadowms = objShadow.GetComponent<W3TerrainMoveableSprite>();
                shadowms.initSprite( unitData.shadowW - 16 , unitData.shadowH - 16 , unitData.shadowX , unitData.shadowY );
            }
            else
            {
                //objShadow.SetActive( false );
            }


            GameObject obj = (GameObject)PrefabUtility.CreatePrefab( path , obj1 );

            GameObject.DestroyImmediate( obj1 );



            //          MeshFilter filter = obj.GetComponentInChildren<MeshFilter>();
            //          MeshCollider collider = filter.gameObject.AddComponent<MeshCollider>();
            //          collider.sharedMesh = filter.sharedMesh;

            W3Unit unit = obj.AddComponent<W3Unit>();
            unit.startAnimation = false;

            Animation ani = obj.GetComponentInChildren<Animation>();
            W3AnimationType type = W3AnimationType.none;

//            W3MoveController w3mc = obj.AddComponent<W3MoveController>();
//            w3mc.moveSpeed = unitData1.spd;
            

            if ( ani != null )
            {
                ani.playAutomatically = false;
                ani.cullingType = AnimationCullingType.BasedOnRenderers;

                foreach ( AnimationState state in ani )
                {
                    if ( state.name.Contains( "Stand" ) )
                    {
                        type = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , state.name );
                        ani.clip = state.clip;
                        break;
                    }
                }
                if ( type == W3AnimationType.none )
                {
                    foreach ( AnimationState state in ani )
                    {
                        if ( state.name.Contains( "Portrait" ) )
                        {
                            type = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , state.name );
                            ani.clip = state.clip;
                            break;
                        }
                    }
                }
                if ( type == W3AnimationType.none )
                {
                    foreach ( AnimationState state in ani )
                    {
                        if ( state.name.Contains( "Birth" ) )
                        {
                            type = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , state.name );
                            ani.clip = state.clip;
                            break;
                        }
                    }
                }

            }
            
            unit.animationType = type;
            unit.isBuilding = true;

            bool skinned = false;
            for ( int j = 0 ; j < x.geoset.Count ; j++ )
            {
                string n = x.name + ( j + 1 );
                Transform t = GetTransform( obj.transform , n );
                SkinnedMeshRenderer r = t.GetComponent<SkinnedMeshRenderer>();

                if ( r != null )
                {
                    skinned = true;
                    break;
                }
            }

            Material[] mater = new Material[ x.materials.Count ];
            Material[] materSkinned = new Material[ x.materials.Count ];
            
            Texture2D[] t2d = CreateMateral( mater , x , Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) ) );

            if ( skinned )
            {
                CreateMateralSkinned( materSkinned , x , Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) ) );
            }

            for ( int j = 0 ; j < x.geoset.Count ; j++ )
            {
                string n = x.name + ( j + 1 );
                Transform t = GetTransform( obj.transform , n );
                Renderer r = t.GetComponent<Renderer>();
                SkinnedMeshRenderer r1 = t.GetComponent<SkinnedMeshRenderer>();
                r.enabled = true;

                int tID = x.materials[ x.geoset[ j ].materialID ].layers[ 0 ].textureID;

                if ( x.textures[ tID ].replaceable == 1 )
                {
                    if ( r1 != null )
                    {
                        W3SkinnedMeshColor c = t.gameObject.AddComponent<W3SkinnedMeshColor>();
                        if ( x.materials[ x.geoset[ j ].materialID ].layers.Count > 1 )
                        {
                            c.texture2d = t2d[ x.materials[ x.geoset[ j ].materialID ].layers[ 1 ].textureID ];
                        }
                    }
                    else
                    {
                        W3UnitMeshColor c = t.gameObject.AddComponent<W3UnitMeshColor>();
                        if ( x.materials[ x.geoset[ j ].materialID ].layers.Count > 1 )
                        {
                            c.texture2d = t2d[ x.materials[ x.geoset[ j ].materialID ].layers[ 1 ].textureID ];
                        }

                        t.gameObject.AddComponent<W3StaicUVMesh>();
                    }
                    
                    r.sharedMaterial = null;
                }
                else if ( x.textures[ tID ].replaceable == 2 )
                {
                    W3TeamGlow c = t.gameObject.AddComponent<W3TeamGlow>();
                    r.sharedMaterial = null;

                    if ( r1 == null )
                        t.gameObject.AddComponent<W3StaicUVMesh>();
                }
                else
                {
                    if ( x.textures[ tID ].replaceable > 2 )
                    {
                        W3ReplaceableTexture c = t.gameObject.AddComponent<W3ReplaceableTexture>();
                        c.replaceable = x.textures[ tID ].replaceable;

                        if ( x.materials[ x.geoset[ j ].materialID ].layers.Count > 1 )
                        {
                            //c.texture2d = t2d[ x.materials[ x.geoset[ j ].materialID ].layers[ 1 ].textureID ];
                        }
                    }

                    if ( r1 != null )
                    {
                        r.sharedMaterial = materSkinned[ x.geoset[ j ].materialID ];
                    }
                    else
                    {
                        r.sharedMaterial = mater[ x.geoset[ j ].materialID ];

                        if ( mater[ x.geoset[ j ].materialID ].shader.name.Contains( "MeshAlphaColor3" ) ||
                            mater[ x.geoset[ j ].materialID ].shader.name.Contains( "MeshAdditive" ) ||
                            mater[ x.geoset[ j ].materialID ].shader.name.Contains( "MeshBillboard" ) )
                        {

                        }
                        else
                        {
                            t.gameObject.AddComponent<W3StaicUVMesh>();
                        }
                    }
                }
            }

            FBXCreateAnimations( x , obj );

            Renderer[] rrr = obj.GetComponentsInChildren<Renderer>();
            objs[ i ] = (GameObject)PrefabUtility.InstantiatePrefab( obj );
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        for ( int i = 0 ; i < objs.Length ; i++ )
        {
            for ( int j = 0 ; j < objTest2.Count ; j++ )
            {
                if ( AssetDatabase.GetAssetPath( arr[ i ] ).Contains( objTest2[ j ] ) )
                {
                    objs[ i ].transform.parent = GameObject.Find( "testBuildings/" + objTest2[ j ] ).transform;
                    objs[ i ].transform.localPosition = new Vector3( GameObject.Find( "testBuildings/" + objTest2[ j ] ).transform.childCount * 15.0f , 0 , 0 );
                    break;
                }
            }
        }
    }


    [MenuItem( "W3 Tools/FBX/CreatePrefabDoodad" )]
    static void FBXCreatePrefabDoodad()
    {
        if ( GameObject.Find( "testDoodads" ) != null )
            GameObject.DestroyImmediate( GameObject.Find( "testDoodads" ) );

        GameObject objTest = new GameObject( "testDoodads" );
        List<string> objTest2 = new List<string>();

        UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );

        if ( arr.Length == 0 )
        {
            return;
        }

        GameObject[] objs = new GameObject[ arr.Length ];

        for ( int i = 0 ; i < arr.Length ; i++ )
        {
            string pathDir = Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) );
            pathDir = pathDir.Replace( '\\' , '/' );

            int n1 = pathDir.IndexOf( "Doodads/" );
            int n2 = pathDir.Substring( n1 + 8 , pathDir.Length - n1 - 9 ).IndexOf( '/' );

            string pathDir2 = pathDir.Substring( n1 + 8 , n2 );

            if ( objTest2.Find( o => o == pathDir2 ) == null )
            {
                GameObject obj2 = new GameObject( pathDir2 );
                obj2.transform.parent = objTest.transform;
                obj2.transform.localPosition = new Vector3( 0.0f , 0.0f , 200 + objTest2.Count * 15 );
                objTest2.Add( pathDir2 );
            }

            GameFBXXml x = new GameFBXXml();
            x.load( AssetDatabase.GetAssetPath( arr[ i ] ) );


            string path = Path.ChangeExtension( AssetDatabase.GetAssetPath( arr[ i ] ) , ".prefab" );
            path = path.Replace( '\\' , '/' );
            path = path.Replace( "Objects" , "Resources/Prefabs" );

            if ( !Directory.Exists( Path.GetDirectoryName( path ) ) )
            {
                Directory.CreateDirectory( Path.GetDirectoryName( path ) );
            }

            GameObject obj = (GameObject)PrefabUtility.CreatePrefab( path , (GameObject)arr[ i ] );

            //          MeshFilter filter = obj.GetComponentInChildren<MeshFilter>();
            //          MeshCollider collider = filter.gameObject.AddComponent<MeshCollider>();
            //          collider.sharedMesh = filter.sharedMesh;

            W3Doodad w3doodad = obj.AddComponent<W3Doodad>();
            w3doodad.startAnimation = false;

            Animation ani = obj.GetComponent<Animation>();
            W3AnimationType type = W3AnimationType.none;

            if ( ani != null )
            {
                ani.playAutomatically = false;
                ani.cullingType = AnimationCullingType.BasedOnRenderers;

                foreach ( AnimationState state in ani )
                {
                    if ( state.name.Contains( "Stand" ) )
                    {
                        type = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , state.name );
                        ani.clip = state.clip;
                        break;
                    }
                }
                if ( type == W3AnimationType.none )
                {
                    foreach ( AnimationState state in ani )
                    {
                        if ( state.name.Contains( "Portrait" ) )
                        {
                            type = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , state.name );
                            ani.clip = state.clip;
                            break;
                        }
                    }
                }
                if ( type == W3AnimationType.none )
                {
                    foreach ( AnimationState state in ani )
                    {
                        if ( state.name.Contains( "Birth" ) )
                        {
                            type = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , state.name );
                            ani.clip = state.clip;
                            break;
                        }
                    }
                }
            }

            w3doodad.animationType = type;

            bool skinned = false;
            for ( int j = 0 ; j < x.geoset.Count ; j++ )
            {
                string n = x.name + ( j + 1 );
                Transform t = GetTransform( obj.transform , n );

                if ( t != null )
                {
                    SkinnedMeshRenderer r = t.GetComponent<SkinnedMeshRenderer>();

                    if ( r != null )
                    {
                        skinned = true;
                        break;
                    }
                }
            }

            Material[] mater = new Material[ x.materials.Count ];
            Material[] materSkinned = new Material[ x.materials.Count ];

            Texture2D[] t2d = CreateMateral( mater , x , Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) ) );

            if ( skinned )
            {
                CreateMateralSkinned( materSkinned , x , Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) ) );
            }

            for ( int j = 0 ; j < x.geoset.Count ; j++ )
            {
                string n = x.name + ( j + 1 );
                Transform t = GetTransform( obj.transform , n );

                if ( t != null )
                {
                    Renderer r = t.GetComponent<Renderer>();
                    SkinnedMeshRenderer r1 = t.GetComponent<SkinnedMeshRenderer>();

                    if ( r != null )
                    {
                        r.enabled = true;

                        int tID = x.materials[ x.geoset[ j ].materialID ].layers[ 0 ].textureID;

                        if ( x.textures[ tID ].replaceable == 1 )
                        {
                            if ( r1 != null )
                            {
                                W3SkinnedMeshColor c = t.gameObject.AddComponent<W3SkinnedMeshColor>();
                                if ( x.materials[ x.geoset[ j ].materialID ].layers.Count > 1 )
                                {
                                    c.texture2d = t2d[ x.materials[ x.geoset[ j ].materialID ].layers[ 1 ].textureID ];
                                }
                            }
                            else
                            {
                                W3MeshColor c = t.gameObject.AddComponent<W3MeshColor>();
                                if ( x.materials[ x.geoset[ j ].materialID ].layers.Count > 1 )
                                {
                                    c.texture2d = t2d[ x.materials[ x.geoset[ j ].materialID ].layers[ 1 ].textureID ];
                                }

                                t.gameObject.AddComponent<W3StaicUVMesh>();
                            }

                            r.sharedMaterial = null;
                        }
                        else if ( x.textures[ tID ].replaceable == 2 )
                        {
                            W3TeamGlow c = t.gameObject.AddComponent<W3TeamGlow>();
                            r.sharedMaterial = null;

                            if ( r1 == null )
                            {
                                t.gameObject.AddComponent<W3StaicUVMesh>();
                            }
                        }
                        else
                        {
                            if ( x.textures[ tID ].replaceable > 2 )
                            {
                                W3ReplaceableTexture c = t.gameObject.AddComponent<W3ReplaceableTexture>();
                                c.replaceable = x.textures[ tID ].replaceable;

                                if ( x.materials[ x.geoset[ j ].materialID ].layers.Count > 1 )
                                {
                                    //c.texture2d = t2d[ x.materials[ x.geoset[ j ].materialID ].layers[ 1 ].textureID ];
                                }
                            }

                            if ( r1 != null )
                            {
                                r.sharedMaterial = materSkinned[ x.geoset[ j ].materialID ];
                            }
                            else
                            {
                                r.sharedMaterial = mater[ x.geoset[ j ].materialID ];

                                if (mater[x.geoset[j].materialID].shader.name.Contains("MeshAlphaColor3") ||
                                mater[x.geoset[j].materialID].shader.name.Contains("MeshAdditive") ||
                                mater[x.geoset[j].materialID].shader.name.Contains("MeshBillboard"))
                                {

                                }
                                else
                                {
                                    t.gameObject.AddComponent<W3StaicUVMesh>();
                                }
                            }

                            
                        }
                    }
                }
            }

            FBXCreateAnimations( x , obj );

            objs[ i ] = (GameObject)PrefabUtility.InstantiatePrefab( obj );

            AssetDatabase.SaveAssets();
        }

        AssetDatabase.Refresh();

        for ( int i = 0 ; i < objs.Length ; i++ )
        {
            for ( int j = 0 ; j < objTest2.Count ; j++ )
            {
                if ( AssetDatabase.GetAssetPath( arr[ i ] ).Contains( objTest2[ j ] ) )
                {
                    objs[ i ].transform.parent = GameObject.Find( "testDoodads/" + objTest2[ j ] ).transform;
                    objs[ i ].transform.localPosition = new Vector3( GameObject.Find( "testDoodads/" + objTest2[ j ] ).transform.childCount * 15.0f , 0 , 0 );
                    break;
                }
            }
        }
    }


    [ MenuItem( "W3 Tools/FBX/CreatePrefabCliff" ) ]
	static void FBXCreatePrefabCliff()
	{
		UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );  

		for ( int i = 0 ; i < arr.Length ; i++ )
		{
			//Debug.LogError(Application.dataPath.Substring(0,Application.dataPath.LastIndexOf('/'))+"/"+ AssetDatabase.GetAssetPath(arr[0]));

			//GameObject prefab = PrefabUtility.InstantiatePrefab( arr[ i ] ) as GameObject;

			string path = Path.ChangeExtension( AssetDatabase.GetAssetPath( arr[i] ) , ".prefab");
            path = path.Replace( '\\' , '/' );
            path = path.Replace( "Objects" , "Resources/Prefabs" );

            if ( !Directory.Exists( Path.GetDirectoryName( path ) ) )
            {
                Directory.CreateDirectory( Path.GetDirectoryName( path ) );
            }

            GameObject obj = PrefabUtility.CreatePrefab( path , (GameObject)arr[ i ] );

			W3TerrainMeshVertex mv = obj.AddComponent< W3TerrainMeshVertex >();

			mv.v = new sbyte[ (int)W3TerrainNode.Type.ccount ];
			mv.v2 = new sbyte[ (int)W3TerrainNode.Type.ccount ];

			for (int k = 0; k < (int)W3TerrainNode.Type.ccount ; k++) 
			{
				mv.v[ k ] = (sbyte)-1;
			}
			for (int k = 0; k < (int)W3TerrainNode.Type.ccount ; k++) 
			{
				mv.v2[ k ] = (sbyte)-1;
			}

			Dictionary< sbyte , sbyte > used = new Dictionary<sbyte, sbyte>();

			MeshFilter filter = obj.GetComponentInChildren< MeshFilter >();
			Mesh mesh = filter.sharedMesh;

            MeshRenderer render = obj.GetComponentInChildren< MeshRenderer >();
            render.material = null;
            render.sharedMaterial = null;

            //MeshCollider collider = filter.gameObject.AddComponent< MeshCollider >();
            //collider.sharedMesh = filter.sharedMesh;

            for ( int k = 0 ; k < mesh.vertexCount ; k++ )
			{
				if ( ( mesh.vertices[ k ].x < 0.001f && mesh.vertices[ k ].x > -0.001f )
					&& ( mesh.vertices[ k ].y < 0.001f && mesh.vertices[ k ].y > -0.001f ) )
				{
					if ( mv.v[ (int)W3TerrainNode.Type.bottomLeft ] != -1 )
						mv.v2[ (int)W3TerrainNode.Type.bottomLeft ] = (sbyte)k;
					else
						mv.v[ (int)W3TerrainNode.Type.bottomLeft ] = (sbyte)k;

					used[ (sbyte)k ] = (sbyte)-1;
				}
				if ( ( mesh.vertices[ k ].x < 0.001f && mesh.vertices[ k ].x > -0.001f )
					&& ( mesh.vertices[ k ].y < 1.575f && mesh.vertices[ k ].y > 1.574f ) )
				{
					if ( mv.v[ (int)W3TerrainNode.Type.topLeft ] != -1 )
						mv.v2[ (int)W3TerrainNode.Type.topLeft ] = (sbyte)k;
					else
						mv.v[ (int)W3TerrainNode.Type.topLeft ] = (sbyte)k;

					used[ (sbyte)k ] = (sbyte)-1;
				}
				if ( ( mesh.vertices[ k ].x < -1.574f && mesh.vertices[ k ].x > -1.575f ) &&
					( mesh.vertices[ k ].y < 0.001f && mesh.vertices[ k ].y > -0.001f ) )
				{
					if ( mv.v[ (int)W3TerrainNode.Type.bottomRight ] != -1 )
						mv.v2[ (int)W3TerrainNode.Type.bottomRight ] = (sbyte)k;
					else
						mv.v[ (int)W3TerrainNode.Type.bottomRight ] = (sbyte)k;

					used[ (sbyte)k ] = (sbyte)-1;
				}
				if ( ( mesh.vertices[ k ].x < -1.574f && mesh.vertices[ k ].x > -1.575f ) &&
					( mesh.vertices[ k ].y < 1.575f && mesh.vertices[ k ].y > 1.574f ) )
				{
					if ( mv.v[ (int)W3TerrainNode.Type.topRight ] != -1 )
						mv.v2[ (int)W3TerrainNode.Type.topRight ] = (sbyte)k;
					else
						mv.v[ (int)W3TerrainNode.Type.topRight ] = (sbyte)k;

					used[ (sbyte)k ] = (sbyte)-1;
				}

				if ( ( mesh.vertices[ k ].x < -1.574f * 2.0f && mesh.vertices[ k ].x > -1.575f * 2.0f ) &&
					( mesh.vertices[ k ].y < 1.575f && mesh.vertices[ k ].y > 1.574f ) )
				{
					if ( mv.v[ (int)W3TerrainNode.Type.topRR ] != -1 )
						mv.v2[ (int)W3TerrainNode.Type.topRR ] = (sbyte)k;
					else
						mv.v[ (int)W3TerrainNode.Type.topRR ] = (sbyte)k;

					used[ (sbyte)k ] = (sbyte)-1;
				}
				if ( ( mesh.vertices[ k ].x < -1.574f * 2.0f && mesh.vertices[ k ].x > -1.575f * 2.0f ) &&
					( mesh.vertices[ k ].y < 0.001f && mesh.vertices[ k ].y > -0.001f ) )
				{
					if ( mv.v[ (int)W3TerrainNode.Type.bottomRR ] != -1 )
						mv.v2[ (int)W3TerrainNode.Type.bottomRR ] = (sbyte)k;
					else
						mv.v[ (int)W3TerrainNode.Type.bottomRR ] = (sbyte)k;

					used[ (sbyte)k ] = (sbyte)-1;
				}

				if ( ( mesh.vertices[ k ].x < 0.001f && mesh.vertices[ k ].x > -0.001f ) &&
					( mesh.vertices[ k ].y < 1.575f * 2.0f && mesh.vertices[ k ].y > 1.574f * 2.0f ) )
				{
					if ( mv.v[ (int)W3TerrainNode.Type.ttLeft ] != -1 )
						mv.v2[ (int)W3TerrainNode.Type.ttLeft ] = (sbyte)k;
					else
						mv.v[ (int)W3TerrainNode.Type.ttLeft ] = (sbyte)k;

					used[ (sbyte)k ] = (sbyte)-1;
				}
				if ( ( mesh.vertices[ k ].x < -1.574f && mesh.vertices[ k ].x > -1.575f ) &&
					( mesh.vertices[ k ].y < 1.575f * 2.0f && mesh.vertices[ k ].y > 1.574f * 2.0f ) )
				{
					if ( mv.v[ (int)W3TerrainNode.Type.ttRight ] != -1 )
						mv.v2[ (int)W3TerrainNode.Type.ttRight ] = (sbyte)k;
					else
						mv.v[ (int)W3TerrainNode.Type.ttRight ] = (sbyte)k;

					used[ (sbyte)k ] = (sbyte)-1;
				}


			}


			mv.nBLTL = new List< sbyte >();
			mv.nTLTR = new List< sbyte >();
			mv.nTRBR = new List< sbyte >();
			mv.nBRBL = new List< sbyte >();
			mv.nM = new List< sbyte >();


			for ( int k = 0 ; k < mesh.vertexCount ; k++ )
			{

				if ( ( mesh.vertices[ k ].x < -1.574f && mesh.vertices[ k ].x > -1.575f ) )
				{
					int lc = mv.nTRBR.Count;

					bool b = false;
					for ( int l = 0 ; l < lc ; l++ )
					{
						if ( mv.nTRBR[ l ] == k )
						{
							b = true;
						}
					}

					if ( !b
						&& k != mv.v[ (int)W3TerrainNode.Type.topRight ] 
						&& k != mv.v2[ (int)W3TerrainNode.Type.topRight ] 
						&& k != mv.v[ (int)W3TerrainNode.Type.bottomRight ] 
						&& k != mv.v2[ (int)W3TerrainNode.Type.bottomRight ] )
					{
						mv.nTRBR.Add( (sbyte)k );

						used[ (sbyte)k ] = (sbyte)-1;
					}

//					if ( mesh.vertices[ mv.v[ (int)W3TerrainNode.Type.topRight ] ].z < mesh.vertices[ k ].z )
//					{
						mv.nTRBR.Sort( (x , y ) => mesh.vertices[ x ].z.CompareTo( mesh.vertices[ y ].z ) );
//					}
//					else
//					{
//						mv.nTRBR.Sort((x, y) => -mesh.vertices[ x ].z.CompareTo( mesh.vertices[ y ].z ) );
//					}
				}

				if ( ( mesh.vertices[ k ].y < 1.575f && mesh.vertices[ k ].y > 1.574f ) )
				{
					int lc = mv.nTLTR.Count;

					bool b = false;
					for ( int l = 0 ; l < lc ; l++ )
					{
						if ( mv.nTLTR[ l ] == k )
						{
							b = true;
						}
					}

					if ( !b
						&& k != mv.v[ (int)W3TerrainNode.Type.topRight ] 
						&& k != mv.v2[ (int)W3TerrainNode.Type.topRight ] 
						&& k != mv.v[ (int)W3TerrainNode.Type.topLeft ] 
						&& k != mv.v2[ (int)W3TerrainNode.Type.topLeft ] )
					{
						mv.nTLTR.Add( (sbyte)k );

						used[ (sbyte)k ] = (sbyte)-1;
					}

//					if ( mesh.vertices[ mv.v[ (int)W3TerrainNode.Type.topLeft ] ].z < mesh.vertices[ k ].z )
//					{
						mv.nTLTR.Sort( (x , y ) => mesh.vertices[ x ].z.CompareTo( mesh.vertices[ y ].z ) );
//					}
//					else
//					{
//						mv.nTLTR.Sort((x, y) => -mesh.vertices[ x ].z.CompareTo( mesh.vertices[ y ].z ) );
//					}
				}

				if ( ( mesh.vertices[ k ].x < 0.001f && mesh.vertices[ k ].x > -0.001f ) )
				{
					int lc = mv.nBLTL.Count;

					bool b = false;
					for ( int l = 0 ; l < lc ; l++ )
					{
						if ( mv.nBLTL[ l ] == k )
						{
							b = true;
						}
					}

					if ( !b
						&& k != mv.v[ (int)W3TerrainNode.Type.bottomLeft ] 
						&& k != mv.v2[ (int)W3TerrainNode.Type.bottomLeft ] 
						&& k != mv.v[ (int)W3TerrainNode.Type.topLeft ] 
						&& k != mv.v2[ (int)W3TerrainNode.Type.topLeft ] )
					{
						mv.nBLTL.Add( (sbyte)k );

						used[ (sbyte)k ] = (sbyte)-1;
					}

//					if ( mesh.vertices[ mv.v[ (int)W3TerrainNode.Type.bottomLeft ] ].z < mesh.vertices[ k ].z )
//					{
						mv.nBLTL.Sort( (x , y ) => mesh.vertices[ x ].z.CompareTo( mesh.vertices[ y ].z ) );
//					}
//					else
//					{
//						mv.nBLTL.Sort((x, y) => -mesh.vertices[ x ].z.CompareTo( mesh.vertices[ y ].z ) );
//					}
				}

				if ( ( mesh.vertices[ k ].y < 0.001f && mesh.vertices[ k ].y > -0.001f ) )
				{
					int lc = mv.nBRBL.Count;

					bool b = false;
					for ( int l = 0 ; l < lc ; l++ )
					{
						if ( mv.nBRBL[ l ] == k )
						{
							b = true;
						}
					}

					if ( !b
						&& k != mv.v[ (int)W3TerrainNode.Type.bottomLeft ] 
						&& k != mv.v2[ (int)W3TerrainNode.Type.bottomLeft ] 
						&& k != mv.v[ (int)W3TerrainNode.Type.bottomRight ] 
						&& k != mv.v2[ (int)W3TerrainNode.Type.bottomRight ] )
					{
						mv.nBRBL.Add( (sbyte)k );

						used[ (sbyte)k ] = (sbyte)-1;
					}
				}

//				if ( mesh.vertices[ mv.v[ (int)W3TerrainNode.Type.bottomRight ] ].z < mesh.vertices[ k ].z )
//				{
					mv.nBRBL.Sort( (x , y ) => mesh.vertices[ x ].z.CompareTo( mesh.vertices[ y ].z ) );
//				}
//				else
//				{
//					mv.nBRBL.Sort((x, y) => -mesh.vertices[ x ].z.CompareTo( mesh.vertices[ y ].z ) );
//				}

			}

			for ( int k = 0 ; k < mesh.vertexCount ; k++ )
			{
				if ( !used.ContainsKey( (sbyte)k ) )
				{
					mv.nM.Add( (sbyte)k );
				}
			}

			mv.nM.Sort((x, y) => mesh.vertices[ x ].z.CompareTo( mesh.vertices[ y ].z ) );



		}

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
	}

	static Transform GetTransform( Transform check , string name )
	{
		Transform forreturn = null;  

		foreach ( Transform t in check.GetComponentsInChildren< Transform >() )
		{
			if ( t.name == name && t.GetComponent< Renderer >() != null )
			{  
				forreturn = t;
				return t;
			}
		}

		return forreturn;
	}


	static void FBXCreateAnimations( GameFBXXml xml , GameObject obj )
	{
		if ( xml.animations.Count == 0 )
		{
			return;
		}

		W3AnimationController ac = obj.AddComponent< W3AnimationController >();

		W3AnimationVisiblity v3 = obj.AddComponent< W3AnimationVisiblity >();
		v3.frames = new List< W3AnimationVisiblityF >();
		ac.animations = new W3AnimationType[ xml.animations.Count ];

		for ( int k = 0 ; k < xml.animations.Count ; k++ )
		{
			try
			{
				ac.animations[ k ] = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , xml.animations[ k ].name );
			}
			catch ( Exception ex )
			{
				ac.animations[ k ] = W3AnimationType.none;
			}

            float lastA = 0.0f;

			for ( int kk = 0; kk < xml.geosetAnimations.Count ; kk++ ) 
			{
				for (int kk1 = 0; kk1 < xml.geosetAnimations[ kk ].animationAlpha.data.Count ; kk1++) 
				{
					if ( xml.geosetAnimations[ kk ].animationAlpha.data[ kk1 ].time >= xml.animations[ k ].startTime && 
						xml.geosetAnimations[ kk ].animationAlpha.data[ kk1 ].time <= xml.animations[ k ].endTime )
					{
						int nnn = -1;

						for ( int kk44 = 0 ; kk44 < v3.frames.Count ; kk44++ )
						{
							if ( v3.frames[ kk44 ].type == ac.animations[ k ] ) 
							{
								nnn = kk44;
								break;
							}
						}

						if ( nnn == -1 ) 
						{
							W3AnimationVisiblityF ff111 = new W3AnimationVisiblityF();
							ff111.type = ac.animations[ k ];
							ff111.f = new List<W3AnimationVisiblityT>();

							v3.frames.Add( ff111 );
							nnn = v3.frames.Count - 1;
						}

						W3AnimationVisiblityT vv = new W3AnimationVisiblityT();
						vv.v = xml.geosetAnimations[ kk ].animationAlpha.data[ kk1 ].alpha;
						vv.time = (float)( xml.geosetAnimations[ kk ].animationAlpha.data[ kk1 ].time - xml.animations[ k ].startTime ) / (float)( xml.animations[ k ].endTime - xml.animations[ k ].startTime );
						vv.name = xml.name + ( xml.geosetAnimations[ kk ].geosetID + 1 );

                        bool last = false;
                        bool last1 = false;
                        for ( int i0 = 0 ; i0 < v3.frames[ nnn ].f.Count ; i0++ )
                        {
                            if ( v3.frames[ nnn ].f[ i0 ].name == vv.name && v3.frames[ nnn ].f[ i0 ].time < vv.time )
                            {
                                last = true;
                            }

                            if ( v3.frames[ nnn ].f[ i0 ].name == vv.name && v3.frames[ nnn ].f[ i0 ].v == vv.v )
                            {
                                last1 = true;
                            }
                        }

                        if ( !last && vv.time > 0.0f )
                        {
                            W3AnimationVisiblityT vv1 = new W3AnimationVisiblityT();
                            vv1.v = lastA;
                            vv1.time = 0.0f;
                            vv1.name = vv.name;

                            v3.frames[ nnn ].f.Add( vv1 );

                            if ( vv1.v != vv.v && !last1 )
                            {
                                v3.frames[ nnn ].f.Add( vv );
                            }
                        }
                        else if ( !last1 )
                        {
                            v3.frames[ nnn ].f.Add( vv );
                        }
                    }

                    lastA = xml.geosetAnimations[ kk ].animationAlpha.data[ kk1 ].alpha;
                }
			}

		}


		W3AnimationAlpha a3 = obj.AddComponent< W3AnimationAlpha >();
		a3.frames = new List< W3AnimationAlphaF >();


		for ( int k1 = 0 ; k1 < xml.materials.Count ; k1++ )
		{
			for ( int k11 = 0 ; k11 < xml.materials[ k1 ].layers.Count ; k11++ )
			{
				W3AnimationAlphaF a3F = null;

				for ( int k12 = 0 ; k12 < xml.materials[ k1 ].layers[ k11 ].layerAlpha.data.Count ; k12++ )
				{
					int geoset = 0;
					for ( int k22 = 0 ; k22 < xml.geoset.Count ; k22++ )
					{
						if ( xml.geoset[ k22 ].materialID == k1 )
						{
							geoset = k22 + 1;
							break;
						}
					}


					W3AnimationAlphaT a3t = new W3AnimationAlphaT();
					a3t.alpha = xml.materials[ k1 ].layers[ k11 ].layerAlpha.data[ k12 ].alpha;

					for ( int k = 0 ; k < xml.animations.Count ; k++ )
					{
						if ( xml.materials[ k1 ].layers[ k11 ].layerAlpha.data[ k12 ].time >= xml.animations[ k ].startTime &&
							xml.materials[ k1 ].layers[ k11 ].layerAlpha.data[ k12 ].time <= xml.animations[ k ].endTime )
						{
							W3AnimationType tt = W3AnimationType.none;
							try
							{
								tt = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , xml.animations[ k ].name );
							}
							catch ( Exception ex )
							{
							}

							if ( a3F == null || a3F.type != tt )
							{
								a3F = new W3AnimationAlphaF();
								a3F.texture = k11;
								a3F.type = tt;
								a3F.material = xml.name + geoset;
								a3F.alpha = xml.materials[ k1 ].layers[ k11 ].alpha;

								a3F.f = new List<W3AnimationAlphaT>();

								a3.frames.Add( a3F );
							}

							a3t.time = (float)( xml.materials[ k1 ].layers[ k11 ].layerAlpha.data[ k12 ].time - xml.animations[ k ].startTime ) / (float)( xml.animations[ k ].endTime - xml.animations[ k ].startTime );
						}
					}

                    if ( a3F != null )
                    {
                        a3F.f.Add( a3t );
                    }
                }
			}
		}

	}

	[ MenuItem( "W3 Tools/FBX/CreatePrefabTerrainObj" ) ]
	static void FBXCreatePrefabTerrainObj()
	{
		UnityEngine.Object[] arr = Selection.GetFiltered( typeof( UnityEngine.Object ) , SelectionMode.TopLevel );  

		for ( int i = 0 ; i < arr.Length ; i++ )
		{
			GameFBXXml x = new GameFBXXml();
			x.load( AssetDatabase.GetAssetPath( arr[ i ] ) );

			string path = Path.ChangeExtension( AssetDatabase.GetAssetPath( arr[i] ) , ".prefab" );
            path = path.Replace( '\\' , '/' );
            path = path.Replace( "Objects" , "Resources/Prefabs" );

			if ( !Directory.Exists( Path.GetDirectoryName( path ) ) )
			{
				Directory.CreateDirectory( Path.GetDirectoryName( path ) );
			}

			GameObject obj = PrefabUtility.CreatePrefab( path , (GameObject)arr[ i ] );
            // 			MeshFilter filter = obj.GetComponentInChildren< MeshFilter >();
            // 			MeshCollider collider = filter.gameObject.AddComponent< MeshCollider >();
            // 			collider.sharedMesh = filter.sharedMesh;

            bool skinned = false;
            for ( int j = 0 ; j < x.geoset.Count ; j++ )
            {
                string n = x.name + ( j + 1 );
                Transform t = GetTransform( obj.transform , n );
                SkinnedMeshRenderer r = t.GetComponent<SkinnedMeshRenderer>();

                if ( r != null )
                {
                    skinned = true;
                    break;
                }
            }

            Material[] mater = new Material[ x.materials.Count ];
            Material[] materSkinned = new Material[ x.materials.Count ];

            Texture2D[] t2d = CreateMateral( mater , x , Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) ) );

            if ( skinned )
            {
                CreateMateralSkinned( materSkinned , x , Path.GetDirectoryName( AssetDatabase.GetAssetPath( arr[ i ] ) ) );
            }


            for (int j = 0; j < x.geoset.Count; j++)
            {
                string n = x.name + (j + 1);
                Transform t = GetTransform(obj.transform, n);
                Renderer r = t.GetComponent<Renderer>();
                SkinnedMeshRenderer r1 = t.GetComponent<SkinnedMeshRenderer>();

                r.enabled = true;

                if ( r1 != null )
                {
                    r.sharedMaterial = materSkinned[ x.geoset[ j ].materialID ];
                }
                else
                {
                    r.sharedMaterial = mater[ x.geoset[ j ].materialID ];
                }

                for (int k = 0; k < x.materials[x.geoset[j].materialID].layers.Count; k++)
                {
                    int tID = x.materials[x.geoset[j].materialID].layers[k].textureID;

                    if (x.textures[tID].replaceable == 1)
                    {
                        if ( r1 != null )
                        {
                            W3SkinnedMeshColor c = t.gameObject.AddComponent<W3SkinnedMeshColor>();
                            c.texture2d = (Texture2D)mater[ x.geoset[ j ].materialID ].mainTexture;
                        }
                        else
                        {
                            W3MeshColor c = t.gameObject.AddComponent<W3MeshColor>();
                            c.texture2d = (Texture2D)mater[ x.geoset[ j ].materialID ].mainTexture;
                        }

                        r.sharedMaterial = null;
                    }
                    else if (x.textures[tID].replaceable == 2)
                    {
                        W3TeamGlow c = t.gameObject.AddComponent<W3TeamGlow>();
                        r.sharedMaterial = null;
                    }
                }
            }

            FBXCreateAnimations(x, obj);




        }
	}




}


