using System.IO;

using UnityEditor;


public class SAImporter : AssetPostprocessor
{


	static void OnPostprocessAllAssets(string[] importedAssets,
	                                   string[] deletedAssets,
	                                   string[] movedAssets,
	                                   string[] movedFromAssetPaths)
	{
		bool refreshNeeded = false;
		
		foreach( var assetPath in importedAssets )
		{
//			if( Path.GetExtension( assetPath ) == ".img")
//			{
//				FileStream stream = File.Open( assetPath , FileMode.Open , FileAccess.Read );
//				byte[] readbytes = new byte[ stream.Length ];
//				stream.Read( readbytes , 0 , (int)stream.Length );
//
//				string newAssetPath = Path.ChangeExtension(assetPath, ".imgz");
//
//				byte[] com = GameDefine.Compress( readbytes );
//				FileStream streamWrite = File.Open( newAssetPath , FileMode.OpenOrCreate , FileAccess.Write );
//				streamWrite.Write( com , 0 , com.Length );
//
//				stream.Close();
//				streamWrite.Close();
//
//				File.Delete(assetPath);
//
//				//refreshNeeded = true;
//			}

//			if( Path.GetExtension( assetPath ) == ".act")
//			{
//				FileStream stream = File.Open( assetPath , FileMode.Open , FileAccess.Read );
//				byte[] readbytes = new byte[ stream.Length ];
//				stream.Read( readbytes , 0 , (int)stream.Length );
//				
//				string newAssetPath = Path.ChangeExtension(assetPath, ".actz");
//				
//				byte[] com = GameDefine.Compress( readbytes );
//				FileStream streamWrite = File.Open( newAssetPath , FileMode.OpenOrCreate , FileAccess.Write );
//				streamWrite.Write( com , 0 , com.Length );
//
//				stream.Close();
//				streamWrite.Close();
//
//				File.Delete(assetPath);
//				//refreshNeeded = true;
//			}
			
			if( Path.GetExtension( assetPath ) == ".map")
			{
				FileStream stream = File.Open( assetPath , FileMode.Open , FileAccess.Read );
				byte[] readbytes = new byte[ stream.Length ];
				stream.Read( readbytes , 0 , (int)stream.Length );
				
				string newAssetPath = Path.ChangeExtension(assetPath, ".mapz");
				
				byte[] com = GameDefine.Compress( readbytes );
				FileStream streamWrite = File.Open( newAssetPath , FileMode.OpenOrCreate , FileAccess.Write );
				streamWrite.Write( com , 0 , com.Length );

				stream.Close();
				streamWrite.Close();
				//refreshNeeded = true;
			}


//			if( Path.GetExtension( assetPath ) == ".xml" )
//			{
//				if ( !assetPath.Contains( "Addons" ) )
//				{
//					return;
//				}
//
//				FileStream stream = File.Open( assetPath , FileMode.Open , FileAccess.Read );
//				byte[] readbytes = new byte[ stream.Length ];
//				stream.Read( readbytes , 0 , (int)stream.Length );
//
//				string newAssetPath = Path.ChangeExtension(assetPath, ".xmlz");
//
//				byte[] com = GameDefine.CreateZipFile( readbytes );
//				FileStream streamWrite = File.Open( newAssetPath , FileMode.OpenOrCreate , FileAccess.Write );
//				streamWrite.Write( com , 0 , com.Length );
//
//				stream.Close();
//				streamWrite.Close();
//
//				File.Delete(assetPath);
//
//				//refreshNeeded = true;
//			}

		}
		
		if( refreshNeeded )
			AssetDatabase.Refresh();
	}



}
