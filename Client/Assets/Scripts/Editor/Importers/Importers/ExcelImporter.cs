using System.IO;

using UnityEditor;

public class ExcelImporter : AssetPostprocessor
{
	static void OnPostprocessAllAssets(string[] importedAssets,
	                                   string[] deletedAssets,
	                                   string[] movedAssets,
	                                   string[] movedFromAssetPaths)
	{
		bool refreshNeeded = false;
		
		foreach(var assetPath in importedAssets)
		{
			if(Path.GetExtension(assetPath) == ".xlsx")
			{
				// encode here ,,

				return;


				string newAssetPath = Path.ChangeExtension(assetPath, ".cfg");
				
				if(File.Exists(newAssetPath))
					AssetDatabase.DeleteAsset(newAssetPath);

				//GameConfigManager.instance.loadAll();

				FileUtil.MoveFileOrDirectory(assetPath, newAssetPath);
				
				refreshNeeded = true;
			}
		}
		
		if(refreshNeeded)
			AssetDatabase.Refresh();
	}
}
