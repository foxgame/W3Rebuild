// FbxAnimListPostprocessor.cs : Use an external text file to import a list of

// splitted animations for FBX 3D models.
//
// Put this script in your "Assets/Editor" directory. When Importing or
// Reimporting a FBX file, the script will search a text file with the
// same name and the ".txt" extension.
// File format: one line per animation clip "firstFrame-lastFrame loopFlag animationName"
// The keyworks "loop" or "noloop" are optional.
// Example:
// 0-50 loop Move forward
// 100-190 die

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class FbxAnimListPostprocessor : AssetPostprocessor
{
	public void OnPreprocessModel()
	{
		if (Path.GetExtension(assetPath).ToLowerInvariant() == ".fbx"
			&& !assetPath.Contains("@"))
		{
			try
			{
				// Remove 6 chars because dataPath and assetPath both contain "assets" directory
				string fileAnim1 = Application.dataPath + Path.ChangeExtension(assetPath, ".xml").Substring(6);

				System.Collections.ArrayList List = new ArrayList();
				ParseAnimFile( fileAnim1 , ref List );

				ModelImporter modelImporter = assetImporter as ModelImporter;
				modelImporter.animationType = ModelImporterAnimationType.Legacy;
				modelImporter.clipAnimations = (ModelImporterClipAnimation[])
					List.ToArray(typeof(ModelImporterClipAnimation));

				modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;

                // i don't know way... make sure 32 / per grid. // 1.2303125?? * 32 + 0.001f
                modelImporter.globalScale = 39.371f;
			}
			catch {
				Debug.LogError( assetPath );
			}
		}
	}

	void ParseAnimFile( string fileAnim1 , ref System.Collections.ArrayList List )
	{
		GameFBXXml xml = new GameFBXXml();
		xml.load( fileAnim1 );

		for ( int i = 0 ; i < xml.animations.Count ; i++ )
		{
			ModelImporterClipAnimation clip = new ModelImporterClipAnimation();

			clip.name = xml.animations[ i ].name;
//			clip.name = clip.name.Replace( "-" , "" );
//			clip.name = clip.name.Replace( "\r" , "" );
//			clip.name = clip.name.Replace( "\n" , "" );
//			clip.name = clip.name.Replace( " " , "" );
//			clip.name = clip.name.Replace( "standbirthalternateworkupgradefirstsecond" , "StandBirthAlternateWorkUpgradeFirstSecond" );
//			clip.name = clip.name.Replace( "spell" , "Spell" );
//			clip.name = clip.name.Replace( "alternate" , "Alternate" );
//			clip.name = clip.name.Replace( "ATTACKALTERNATE" , "AttackAlternate" );
//			clip.name = clip.name.Replace( "stand" , "Stand" );
//			clip.name = clip.name.Replace( "hit" , "Hit" );
//			clip.name = clip.name.Replace( "Portrait" , "Portrait" );
//			clip.name = clip.name.Replace( "flesh" , "Flesh" );
//			clip.name = clip.name.Replace( "decay" , "Decay" );
//			clip.name = clip.name.Replace( "BLANK" , "Blank" );
//			clip.name = clip.name.Replace( "swim" , "Swim" );
//			clip.name = clip.name.Replace( "Alternateattack" , "AlternateAttack" );
//			clip.name = clip.name.Replace( "Attackgold" , "AttackGold" );
//			clip.name = clip.name.Replace( "Standworkgold" , "StandWorkGold" );
//			clip.name = clip.name.Replace( "Standitchhead" , "StandItchHead" );
//			clip.name = clip.name.Replace( "StandTalkgesture" , "StandTalkGesture" );
//			clip.name = clip.name.Replace( "Attacktow" , "AttackTow" );
//			clip.name = clip.name.Replace( "Attackone" , "AttackOne" );
//			clip.name = clip.name.Replace( "Attackslam" , "AttackSlam" );
//			clip.name = clip.name.Replace( "Spellswim" , "SpellSwim" );
//			clip.name = clip.name.Replace( "Scorescreen" , "ScoreScreen" );
//			clip.name = clip.name.Replace( "ready" , "Ready" );
//			clip.name = clip.name.Replace( "DEATH" , "Death" );
//			clip.name = clip.name.Replace( "BONE" , "Bone" );
//			clip.name = clip.name.Replace( "bone" , "Bone" );
//			clip.name = clip.name.Replace( "cinematic" , "Cinematic" );
//			clip.name = clip.name.Replace( "channel" , "Channel" );
//			clip.name = clip.name.Replace( "missle" , "Missle" );
//			clip.name = clip.name.Replace( "blink" , "Blink" );
//			clip.name = clip.name.Replace( "third" , "Third" );
//			clip.name = clip.name.Replace( "MORPH" , "Morph" );
//			clip.name = clip.name.Replace( "Portait" , "Portrait" );
//			clip.name = clip.name.Replace( "Bith" , "Birth" );
//			clip.name = clip.name.Replace( ";" , "" );
//			clip.name = clip.name.Replace( "NOTES" , "Notes" );
//			clip.name = clip.name.Replace( "talk" , "Talk" );
//			clip.name = clip.name.Replace( "slam" , "Slam" );
//			clip.name = clip.name.Replace( "second" , "Second" );
//			clip.name = clip.name.Replace( "ONHOLD" , "OnHold" );
//			clip.name = clip.name.Replace( "upgrade" , "Upgrade" );
//			clip.name = clip.name.Replace( "medium" , "Medium" );
//			clip.name = clip.name.Replace( "work" , "Work" );
//			clip.name = clip.name.Replace( "ALTERNATE" , "Alternate" );
//			clip.name = clip.name.Replace( "gold" , "Gold" );
//			clip.name = clip.name.Replace( "two" , "Two" );
//			clip.name = clip.name.Replace( "tree" , "Tree" );

			if ( clip.name.Length > 1 )
			{
				string str = clip.name.Substring( 0 , 1 ).ToUpperInvariant();
				clip.name = clip.name.Remove( 0 , 1 );
				clip.name = clip.name.Insert( 0 , str );

				try
				{
					W3AnimationType t = (W3AnimationType)Enum.Parse( typeof( W3AnimationType ) , clip.name );
				}
				catch ( Exception ex )
				{
					Debug.LogError( "clip.name error " + xml.animations[ i ].name );
				}
			}

			ClipAnimationInfoCurve[] curve = clip.curves;

			clip.firstFrame = xml.animations[ i ].startTime / 32;
			clip.lastFrame = xml.animations[ i ].endTime / 32;

//             if ( clip.firstFrame > (int)clip.firstFrame )
//             {
//                 clip.firstFrame = (int)clip.firstFrame + 1;
//             }
//             if ( clip.lastFrame > (int)clip.lastFrame )
//             {
//                 clip.lastFrame = (int)clip.lastFrame + 1;
//             }

            if ( clip.name.Contains( "hit" ) || 
				clip.name.Contains( "Hit" ) )
			{
				clip.wrapMode = WrapMode.Once;
			}
			else if ( clip.name.Contains( "Stand" ) ||
			     clip.name.Contains( "Walk" ) )
			{
				clip.wrapMode = WrapMode.Loop;
			}
			else if ( clip.name.Contains( "Attack" ) ||
				clip.name.Contains( "Death" ) ||
				clip.name.Contains( "Spell" ) ||
				clip.name.Contains( "Decay" ) ||
				clip.name.Contains( "Dissipate" ) ||
				clip.name.Contains( "Birth"  )  ||
				clip.name.Contains( "Base"  )   ||
				clip.name.Contains( "Portrait"  ) ||
				clip.name.Contains( "Morph"  ) )
			{
				clip.wrapMode = WrapMode.Once;
			}
			else
			{
				clip.wrapMode = WrapMode.Once;
				Debug.LogWarning( "clip.wrapMode " + clip.name );
			}

			List.Add(clip);
		}



	}

}