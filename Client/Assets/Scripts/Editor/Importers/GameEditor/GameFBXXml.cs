using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using Mono.Xml;
using System.Text;
using System.Security;

public class GameFBXXml
{
	public List< FBXTexture > textures = new List<FBXTexture>();
	public List< FBXTexture > texturesNormal = new List<FBXTexture>();
	public List< FBXTexture > texturesReplaceable = new List<FBXTexture>();

	public List< FBXMaterial > materials = new List<FBXMaterial>();
	public List< FBXAnimation > animations = new List<FBXAnimation>();
	public List< FBXGeoset > geoset = new List<FBXGeoset>();

	public List< FBXGeosetAnimation > geosetAnimations = new List<FBXGeosetAnimation>();
	public List< FBXTexturesAnimation > texturesAnimations = new List<FBXTexturesAnimation>();
	public List< FBXGlobalSequence > globalSequence = new List<FBXGlobalSequence>();

	public string name = "";

	public void load( string path )
	{
		path = path.Replace( ".fbx" , ".xml" );
		path = path.Replace( ".FBX" , ".xml" );

		FileStream stream = File.Open( path , FileMode.Open , FileAccess.Read );

		if ( stream == null )
		{
			return;
		}

		byte[] bytes = new byte[ stream.Length ];
		stream.Read( bytes , 0 ,(int)stream.Length );
		stream.Close();
		stream.Dispose();

		SecurityParser parser = new SecurityParser();
		parser.LoadXml( Encoding.UTF8.GetString( bytes ).Trim() );

		SecurityElement node = parser.ToXml();

		if ( node.Tag == "root" )
		{
			name = node.Attribute( "name" );
		}

		foreach( SecurityElement nodeList in node.Children )
		{
			if ( nodeList.Tag == "textures" )
			{
				if ( nodeList.Children != null )
				{
					foreach ( SecurityElement nodeList1 in nodeList.Children )
					{
						if ( nodeList1.Tag == "t" )
						{
							FBXTexture t = new FBXTexture();
							t.replaceable = int.Parse( nodeList1.Attribute( "replaceable" ) );
							t.flags = int.Parse( nodeList1.Attribute( "flags" ) );
							t.name = nodeList1.Attribute( "name" );

							textures.Add( t );

							if ( t.replaceable == 0 )
								texturesNormal.Add( t );
							else
								texturesReplaceable.Add( t );
						}
					}

				}
			}
			else if ( nodeList.Tag == "materials" )
			{
				if ( nodeList.Children != null )
				{
					foreach ( SecurityElement nodeList1 in nodeList.Children )
					{
						FBXMaterial material = null;

						if ( nodeList1.Tag == "layer" )
						{
							material = new FBXMaterial();
							FBXMaterialLayer layer = null;

							foreach ( SecurityElement nodeList2 in nodeList1.Children )
							{
								if ( nodeList2.Tag == "l" )
								{
									layer = new FBXMaterialLayer();

									layer.filterMode = int.Parse( nodeList2.Attribute( "filterMode" ) );
									layer.shadingFlags = int.Parse( nodeList2.Attribute( "shadingFlags" ) );
									layer.textureID = int.Parse( nodeList2.Attribute( "textureID" ) );
									layer.textureAnimationID = int.Parse( nodeList2.Attribute( "textureAnimationID" ) );
									layer.coordinateID = int.Parse( nodeList2.Attribute( "coordinateID" ) );
									layer.alpha = float.Parse( nodeList2.Attribute( "alpha" ) );

									if ( nodeList2.Children != null )
									{
										foreach ( SecurityElement nodeList3 in nodeList2.Children )
										{
											if ( nodeList3.Tag == "alpha" )
											{
												if ( nodeList3.Attribute( "alphaType" ) != null && nodeList3.Attribute( "alphaType" ).Length > 0 )
												{
													layer.layerAlpha.alphaType = int.Parse( nodeList3.Attribute( "alphaType" ) );
													layer.layerAlpha.alphaGlobalSeqID = int.Parse( nodeList3.Attribute( "alphaGlobalSeqID" ) );
												}

												if ( nodeList3.Children != null )
												{
													foreach ( SecurityElement nodeList4 in nodeList3.Children )
													{
														if ( nodeList4.Tag == "a" )
														{
															FBXMaterialLayerAlphaData a = new FBXMaterialLayerAlphaData();

															a.time = int.Parse( nodeList4.Attribute( "time" ) );
															a.alpha = float.Parse( nodeList4.Attribute( "alpha" ) );

															if ( nodeList4.Attribute( "inTan" ) != null && nodeList4.Attribute( "inTan" ).Length > 0 )
															{
																a.inTan = float.Parse( nodeList4.Attribute( "inTan" ) );
																a.outTan = float.Parse( nodeList4.Attribute( "outTan" ) );
															}

															layer.layerAlpha.data.Add( a );
														}
													}
												}

											}
											else if ( nodeList3.Tag == "texture" )
											{
												if ( nodeList3.Attribute( "TextureIDType" ) != null && nodeList3.Attribute( "TextureIDType" ).Length > 0 )
												{
													layer.layerTexture.TextureIDType = int.Parse( nodeList3.Attribute( "TextureIDType" ) );
													layer.layerTexture.TextureIDGlobalSeqID = int.Parse( nodeList3.Attribute( "TextureIDGlobalSeqID" ) );
												}

												if ( nodeList3.Children != null )
												{
													foreach ( SecurityElement nodeList4 in nodeList3.Children )
													{
														if ( nodeList4.Tag == "t" )
														{
															FBXMaterialLayerTextureData t = new FBXMaterialLayerTextureData();

															t.time = int.Parse( nodeList4.Attribute( "time" ) );
															t.textureID = float.Parse( nodeList4.Attribute( "textureID" ) );

															if ( nodeList4.Attribute( "inTan" ) != null && nodeList4.Attribute( "inTan" ).Length > 0 )
															{
																t.inTan = float.Parse( nodeList4.Attribute( "inTan" ) );
																t.outTan = float.Parse( nodeList4.Attribute( "outTan" ) );
															}

															layer.layerTexture.data.Add( t );
														}
													}
												}

											}

										}
									}


								}

								material.layers.Add( layer );

							}

						}

						materials.Add( material );
					}
				}

			}
			else if ( nodeList.Tag == "animations" )
			{
				if ( nodeList.Children != null )
				{
					foreach ( SecurityElement nodeList1 in nodeList.Children )
					{
						if ( nodeList1.Tag == "a" )
						{
							FBXAnimation a = new FBXAnimation();

							a.name = nodeList1.Attribute( "name" );

							a.name = a.name.Replace( "-" , "" );
							a.name = a.name.Replace( "\r" , "" );
							a.name = a.name.Replace( "\n" , "" );
							a.name = a.name.Replace( " " , "" );
							a.name = a.name.Replace( "standbirthalternateworkupgradefirstsecond" , "StandBirthAlternateWorkUpgradeFirstSecond" );
							a.name = a.name.Replace( "spell" , "Spell" );
							a.name = a.name.Replace( "alternate" , "Alternate" );
							a.name = a.name.Replace( "ATTACKALTERNATE" , "AttackAlternate" );
							a.name = a.name.Replace( "stand" , "Stand" );
							a.name = a.name.Replace( "hit" , "Hit" );
							a.name = a.name.Replace( "Portrait" , "Portrait" );
							a.name = a.name.Replace( "flesh" , "Flesh" );
							a.name = a.name.Replace( "decay" , "Decay" );
							a.name = a.name.Replace( "BLANK" , "Blank" );
							a.name = a.name.Replace( "swim" , "Swim" );
							a.name = a.name.Replace( "Alternateattack" , "AlternateAttack" );
							a.name = a.name.Replace( "Attackgold" , "AttackGold" );
							a.name = a.name.Replace( "Standworkgold" , "StandWorkGold" );
							a.name = a.name.Replace( "Standitchhead" , "StandItchHead" );
							a.name = a.name.Replace( "StandTalkgesture" , "StandTalkGesture" );
							a.name = a.name.Replace( "Attacktow" , "AttackTow" );
							a.name = a.name.Replace( "Attackone" , "AttackOne" );
							a.name = a.name.Replace( "Attackslam" , "AttackSlam" );
							a.name = a.name.Replace( "Spellswim" , "SpellSwim" );
							a.name = a.name.Replace( "Scorescreen" , "ScoreScreen" );
							a.name = a.name.Replace( "ready" , "Ready" );
							a.name = a.name.Replace( "DEATH" , "Death" );
							a.name = a.name.Replace( "BONE" , "Bone" );
							a.name = a.name.Replace( "bone" , "Bone" );
							a.name = a.name.Replace( "cinematic" , "Cinematic" );
							a.name = a.name.Replace( "channel" , "Channel" );
							a.name = a.name.Replace( "missle" , "Missle" );
							a.name = a.name.Replace( "blink" , "Blink" );
							a.name = a.name.Replace( "third" , "Third" );
							a.name = a.name.Replace( "MORPH" , "Morph" );
							a.name = a.name.Replace( "Portait" , "Portrait" );
							a.name = a.name.Replace( "Bith" , "Birth" );
							a.name = a.name.Replace( ";" , "" );
							a.name = a.name.Replace( "NOTES" , "Notes" );
							a.name = a.name.Replace( "talk" , "Talk" );
							a.name = a.name.Replace( "slam" , "Slam" );
							a.name = a.name.Replace( "second" , "Second" );
							a.name = a.name.Replace( "ONHOLD" , "OnHold" );
							a.name = a.name.Replace( "upgrade" , "Upgrade" );
							a.name = a.name.Replace( "medium" , "Medium" );
							a.name = a.name.Replace( "work" , "Work" );
							a.name = a.name.Replace( "ALTERNATE" , "Alternate" );
							a.name = a.name.Replace( "gold" , "Gold" );
							a.name = a.name.Replace( "two" , "Two" );
							a.name = a.name.Replace( "tree" , "Tree" );

							a.startTime = int.Parse( nodeList1.Attribute( "startTime" ) );
							a.endTime = int.Parse( nodeList1.Attribute( "endTime" ) );
							a.moveSpeed = int.Parse( nodeList1.Attribute( "moveSpeed" ) );
							a.flags = int.Parse( nodeList1.Attribute( "flags" ) );

							a.rarity = float.Parse( nodeList1.Attribute( "rarity" ) );
							a.syncPoint = float.Parse( nodeList1.Attribute( "syncPoint" ) );
							a.radius = float.Parse( nodeList1.Attribute( "radius" ) );

							a.min0 = float.Parse( nodeList1.Attribute( "min0" ) );
							a.min1 = float.Parse( nodeList1.Attribute( "min1" ) );
							a.min2 = float.Parse( nodeList1.Attribute( "min2" ) );

							a.max0 = float.Parse( nodeList1.Attribute( "max0" ) );
							a.max1 = float.Parse( nodeList1.Attribute( "max1" ) );
							a.max2 = float.Parse( nodeList1.Attribute( "max2" ) );

							animations.Add( a );
						}
					}
				}

			}
			else if ( nodeList.Tag == "geoset" )
			{
				if ( nodeList.Children != null )
				{
					foreach ( SecurityElement nodeList1 in nodeList.Children )
					{
						if ( nodeList1.Tag == "g" )
						{
							FBXGeoset g = new FBXGeoset();

							g.materialID = int.Parse( nodeList1.Attribute( "materialID" ) );
							g.selectionGroup = int.Parse( nodeList1.Attribute( "selectionGroup" ) );
							g.selectionFlags = int.Parse( nodeList1.Attribute( "selectionFlags" ) );

							g.boundingRadius = float.Parse( nodeList1.Attribute( "boundingRadius" ) );

							g.min0 = float.Parse( nodeList1.Attribute( "min0" ) );
							g.min1 = float.Parse( nodeList1.Attribute( "min1" ) );
							g.min2 = float.Parse( nodeList1.Attribute( "min2" ) );

							g.max0 = float.Parse( nodeList1.Attribute( "max0" ) );
							g.max1 = float.Parse( nodeList1.Attribute( "max1" ) );
							g.max2 = float.Parse( nodeList1.Attribute( "max2" ) );

							geoset.Add( g );
						}
					}
				}

			}
			else if ( nodeList.Tag == "geosetAnimations" )
			{
				if ( nodeList.Children != null )
				{
					foreach ( SecurityElement nodeList1 in nodeList.Children )
					{
						if ( nodeList1.Tag == "g" )
						{
							FBXGeosetAnimation g = new FBXGeosetAnimation();

							g.flags = int.Parse( nodeList1.Attribute( "flags" ) );
							g.geosetID = int.Parse( nodeList1.Attribute( "geosetID" ) );

							g.alpha = float.Parse( nodeList1.Attribute( "alpha" ) );

							g.r = float.Parse( nodeList1.Attribute( "color0" ) );
							g.g = float.Parse( nodeList1.Attribute( "color1" ) );
							g.b = float.Parse( nodeList1.Attribute( "color2" ) );

							geosetAnimations.Add( g );

							foreach ( SecurityElement nodeList2 in nodeList1.Children )
							{
								if ( nodeList2.Tag == "alpha" )
								{
									if ( nodeList2.Attribute( "alphaType" ) != null && nodeList2.Attribute( "alphaType" ).Length > 0 )
									{
										g.animationAlpha.alphaType = int.Parse( nodeList2.Attribute( "alphaType" ) );
										g.animationAlpha.alphaGlobalSeqID = int.Parse( nodeList2.Attribute( "alphaGlobalSeqID" ) );
									}

									if ( nodeList2.Children != null )
									{
										foreach ( SecurityElement nodeList3 in nodeList2.Children )
										{
											if ( nodeList3.Tag == "a" )
											{
												FBXGeosetAnimationsAlphaTime a = new FBXGeosetAnimationsAlphaTime();

												a.time = int.Parse( nodeList3.Attribute( "time" ) );
												a.alpha = float.Parse( nodeList3.Attribute( "alpha" ) );

												if ( nodeList3.Attribute( "alphaType" ) != null )
												{
													a.inTan = float.Parse( nodeList3.Attribute( "inTan" ) );
													a.outTan = float.Parse( nodeList3.Attribute( "outTan" ) );
												}

												g.animationAlpha.data.Add( a );
											}
										}
									}

								}
								else if ( nodeList2.Tag == "color" )
								{
									if ( nodeList2.Attribute( "colorType" ) != null && nodeList2.Attribute( "colorType" ).Length > 0 )
									{
										g.animationColor.colorType = int.Parse( nodeList2.Attribute( "colorType" ) );
										g.animationColor.colorGlobalSeqID = int.Parse( nodeList2.Attribute( "colorGlobalSeqID" ) );
									}

									if ( nodeList2.Children != null )
									{
										foreach ( SecurityElement nodeList3 in nodeList2.Children )
										{
											if ( nodeList3.Tag == "c" )
											{
												FBXGeosetAnimationsColorTime c = new FBXGeosetAnimationsColorTime();

												c.time = int.Parse( nodeList3.Attribute( "time" ) );
												c.r = float.Parse( nodeList3.Attribute( "color0" ) );
												c.g = float.Parse( nodeList3.Attribute( "color1" ) );
												c.b = float.Parse( nodeList3.Attribute( "color2" ) );

												if ( nodeList3.Attribute( "inTan0" ) != null && nodeList3.Attribute( "inTan0" ).Length > 0 )
												{
													c.inTanR = float.Parse( nodeList3.Attribute( "inTan0" ) );
													c.inTanG = float.Parse( nodeList3.Attribute( "inTan1" ) );
													c.inTanB = float.Parse( nodeList3.Attribute( "inTan2" ) );

													c.outTanR = float.Parse( nodeList3.Attribute( "outTan0" ) );
													c.outTanG = float.Parse( nodeList3.Attribute( "outTan1" ) );
													c.outTanB = float.Parse( nodeList3.Attribute( "outTan2" ) );
												}

												g.animationColor.data.Add( c );
											}
										}
									}

								}
							}
						}
					}
				}


			}
			else if ( nodeList.Tag == "texturesAnimations" )
			{
				if ( nodeList.Children != null )
				{
					foreach ( SecurityElement nodeList1 in nodeList.Children )
					{
						if ( nodeList1.Tag == "t" )
						{
							FBXTexturesAnimation a = new FBXTexturesAnimation();

							foreach ( SecurityElement nodeList2 in nodeList1.Children )
							{
								FBXTexturesAnimationT at = new FBXTexturesAnimationT();

								if ( nodeList2.Tag == "translation" )
								{
									if ( nodeList2.Attribute( "translationType" ) != null && nodeList2.Attribute( "translationType" ).Length > 0 )
									{
										at.trans.type = int.Parse( nodeList2.Attribute( "translationType" ) );
										at.trans.globalSeqID = int.Parse( nodeList2.Attribute( "translationGlobalSeqID" ) );

										a.a.Add( at );
									}

									if ( nodeList2.Children != null )
									{
										foreach ( SecurityElement nodeList3 in nodeList2.Children )
										{
											FBXTexturesAnimationTime at0 = new FBXTexturesAnimationTime();

											at0.time = int.Parse( nodeList3.Attribute( "time" ) );
											at0.v.x = float.Parse( nodeList3.Attribute( "trans0" ) );
											at0.v.y = float.Parse( nodeList3.Attribute( "trans1" ) );
											at0.v.z = float.Parse( nodeList3.Attribute( "trans2" ) );

											if ( nodeList3.Attribute( "inTan0" ) != null && nodeList3.Attribute( "inTan0" ).Length > 0 )
											{
												at0.inTan.x = float.Parse( nodeList3.Attribute( "inTan0" ) );
												at0.inTan.y = float.Parse( nodeList3.Attribute( "inTan1" ) );
												at0.inTan.z = float.Parse( nodeList3.Attribute( "inTan2" ) );
												at0.outTan.x = float.Parse( nodeList3.Attribute( "outTan0" ) );
												at0.outTan.y = float.Parse( nodeList3.Attribute( "outTan1" ) );
												at0.outTan.z = float.Parse( nodeList3.Attribute( "outTan2" ) );
											}

											at.trans.t.Add( at0 );
										}
									}
								}
								else if ( nodeList2.Tag == "rotation" )
								{
									if ( nodeList2.Attribute( "rotationType" ) != null && nodeList2.Attribute( "rotationType" ).Length > 0 )
									{
										at.rot.type = int.Parse( nodeList2.Attribute( "rotationType" ) );
										at.rot.globalSeqID = int.Parse( nodeList2.Attribute( "rotationGlobalSeqID" ) );

										a.a.Add( at );
									}

									if ( nodeList2.Children != null )
									{
										foreach ( SecurityElement nodeList3 in nodeList2.Children )
										{
											FBXTexturesAnimationTime at0 = new FBXTexturesAnimationTime();

											at0.time = int.Parse( nodeList3.Attribute( "time" ) );
											at0.v.x = float.Parse( nodeList3.Attribute( "rot0" ) );
											at0.v.y = float.Parse( nodeList3.Attribute( "rot1" ) );
											at0.v.z = float.Parse( nodeList3.Attribute( "rot2" ) );

											if ( nodeList3.Attribute( "inTan0" ) != null && nodeList3.Attribute( "inTan0" ).Length > 0 )
											{
												at0.inTan.x = float.Parse( nodeList3.Attribute( "inTan0" ) );
												at0.inTan.y = float.Parse( nodeList3.Attribute( "inTan1" ) );
												at0.inTan.z = float.Parse( nodeList3.Attribute( "inTan2" ) );
												at0.outTan.x = float.Parse( nodeList3.Attribute( "outTan0" ) );
												at0.outTan.y = float.Parse( nodeList3.Attribute( "outTan1" ) );
												at0.outTan.z = float.Parse( nodeList3.Attribute( "outTan2" ) );
											}

											at.rot.t.Add( at0 );
										}

									}
								}
								else if ( nodeList2.Tag == "scale" )
								{
									if ( nodeList2.Attribute( "rotationType" ) != null && nodeList2.Attribute( "rotationTypes" ).Length > 0 )
									{
										at.scale.type = int.Parse( nodeList2.Attribute( "rotationType" ) );
										at.scale.globalSeqID = int.Parse( nodeList2.Attribute( "rotationGlobalSeqID" ) );

										a.a.Add( at );
									}

									if ( nodeList2.Children != null )
									{
										foreach ( SecurityElement nodeList3 in nodeList2.Children )
										{
											FBXTexturesAnimationTime at0 = new FBXTexturesAnimationTime();

											at0.time = int.Parse( nodeList3.Attribute( "time" ) );
											at0.v.x = float.Parse( nodeList3.Attribute( "scale0" ) );
											at0.v.y = float.Parse( nodeList3.Attribute( "scale1" ) );
											at0.v.z = float.Parse( nodeList3.Attribute( "scale2" ) );

											if ( nodeList3.Attribute( "inTan0" ) != null && nodeList3.Attribute( "inTan0" ).Length > 0 )
											{
												at0.inTan.x = float.Parse( nodeList3.Attribute( "inTan0" ) );
												at0.inTan.y = float.Parse( nodeList3.Attribute( "inTan1" ) );
												at0.inTan.z = float.Parse( nodeList3.Attribute( "inTan2" ) );
												at0.outTan.x = float.Parse( nodeList3.Attribute( "outTan0" ) );
												at0.outTan.y = float.Parse( nodeList3.Attribute( "outTan1" ) );
												at0.outTan.z = float.Parse( nodeList3.Attribute( "outTan2" ) );
											}

											at.scale.t.Add( at0 );
										}
									}


								}


							}

							texturesAnimations.Add( a );
						}
					}
				}

			}
			else if ( nodeList.Tag == "globalSequence" )
			{
				if ( nodeList.Children != null )
				{
					foreach ( SecurityElement nodeList1 in nodeList.Children )
					{
						if ( nodeList1.Tag == "t" )
						{
							FBXGlobalSequence s = new FBXGlobalSequence();

							s.duration = int.Parse( nodeList1.Attribute( "duration" ) );

							globalSequence.Add( s );
						}
					}

				}
			}



		}

//		Debug.Log( "" );
	}

	public class FBXTexture
	{
		public int replaceable = 0;
		public int flags = 0;
		public string name = "";
	}

	public class FBXMaterial
	{
		public List< FBXMaterialLayer > layers = new List< FBXMaterialLayer >();
	}

	public class FBXMaterialLayer
	{
		public int filterMode = 0;
		public int shadingFlags = 0;
		public int textureID = -1;
		public int textureAnimationID = -1;
		public int coordinateID = 0;

		public float alpha = 0.0f;

		public FBXMaterialLayerAlpha layerAlpha = new FBXMaterialLayerAlpha();
		public FBXMaterialLayerTexture layerTexture = new FBXMaterialLayerTexture();
	}

	public class FBXMaterialLayerAlpha
	{
		public int alphaType = 0;
		public int alphaGlobalSeqID = -1;

		public List< FBXMaterialLayerAlphaData > data = new List<FBXMaterialLayerAlphaData>();
	}

	public class FBXMaterialLayerAlphaData
	{
		public int time = 0;
		public float alpha = 1.0f;

		public float inTan = 1.0f;
		public float outTan = 1.0f;
	}

	public class FBXMaterialLayerTexture
	{
		public int TextureIDType = 0;
		public float TextureIDGlobalSeqID = -1;

		public List< FBXMaterialLayerTextureData > data = new List<FBXMaterialLayerTextureData>();
	}

	public class FBXMaterialLayerTextureData
	{
		public int time = 0;
		public float textureID = -1;

		public float inTan = 1.0f;
		public float outTan = 1.0f;
	}


	public class FBXAnimation
	{
		public string name;
		public int startTime = 0;
		public int endTime = 0;
		public int moveSpeed = 0;
		public int flags = 0;

		public float rarity = 0.0f;
		public float syncPoint = 0.0f;
		public float radius = 0.0f;

		public float min0 = 0.0f;
		public float min1 = 0.0f;
		public float min2 = 0.0f;

		public float max0 = 0.0f;
		public float max1 = 0.0f;
		public float max2 = 0.0f;
	}

	public class FBXGeoset
	{
		public int materialID = 0;
		public int selectionGroup = 0;
		public int selectionFlags = 0;

		public float boundingRadius = 0.0f;

		public float min0 = 0.0f;
		public float min1 = 0.0f;
		public float min2 = 0.0f;

		public float max0 = 0.0f;
		public float max1 = 0.0f;
		public float max2 = 0.0f;
	}

	public class FBXGlobalSequence
	{
		public int duration = 0;
	}

	public class FBXGeosetAnimationsAlpha
	{
		public int alphaType = 0;
		public int alphaGlobalSeqID = -1;
		public List< FBXGeosetAnimationsAlphaTime > data = new List<FBXGeosetAnimationsAlphaTime>();
	}
	public class FBXGeosetAnimationsAlphaTime
	{
		public int time = 0;
		public float alpha = 1.0f;
		public float inTan = 1.0f;
		public float outTan = 1.0f;
	}

	public class FBXGeosetAnimationsColor
	{
		public int colorType = 0;
		public int colorGlobalSeqID = -1;
		public List< FBXGeosetAnimationsColorTime > data = new List<FBXGeosetAnimationsColorTime>();
	}
	public class FBXGeosetAnimationsColorTime
	{
		public int time = 0;
		public float r = 1.0f;
		public float g = 1.0f;
		public float b = 1.0f;

		public float inTanR = 1.0f;
		public float inTanG = 1.0f;
		public float inTanB = 1.0f;
		public float outTanR = 1.0f;
		public float outTanG = 1.0f;
		public float outTanB = 1.0f;
	}

	public class FBXGeosetAnimation
	{
		public float alpha = 1.0f;
		public int flags = 0;

		public float r = 1.0f;
		public float g = 1.0f;
		public float b = 1.0f;

		public int geosetID = 0;

		public FBXGeosetAnimationsAlpha animationAlpha = new FBXGeosetAnimationsAlpha();
		public FBXGeosetAnimationsColor animationColor = new FBXGeosetAnimationsColor();
	}

	public class FBXTexturesAnimation
	{
		public List< FBXTexturesAnimationT > a = new List<FBXTexturesAnimationT>();
	}

	public class FBXTexturesAnimationT
	{
		public FBXTexturesAnimationTranslation trans = new FBXTexturesAnimationTranslation();
		public FBXTexturesAnimationRotation rot = new FBXTexturesAnimationRotation();
		public FBXTexturesAnimationScale scale = new FBXTexturesAnimationScale();
	}

	public class FBXTexturesAnimationTime
	{
		public int time = 0;

		public Vector3 v;
		public Vector3 inTan;
		public Vector3 outTan;
	}

	public class FBXTexturesAnimationTranslation
	{
		public int type;
		public int globalSeqID = 0;

		public List< FBXTexturesAnimationTime > t = new List<FBXTexturesAnimationTime>();
	}
	public class FBXTexturesAnimationRotation
	{
		public int type;
		public int globalSeqID = 0;

		public List< FBXTexturesAnimationTime > t = new List<FBXTexturesAnimationTime>();
	}
	public class FBXTexturesAnimationScale
	{
		public int type;
		public int globalSeqID = 0;

		public List< FBXTexturesAnimationTime > t = new List<FBXTexturesAnimationTime>();
	}
}

