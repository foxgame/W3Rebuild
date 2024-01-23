//
// AOOJass.JassCompiler
// by BlacKDick

using System;
using System.Collections.Generic;
using System.Text;

public class JassCompiler
{
	//	internal struct JassClass
	//	{
	//		public Dictionary<string, JassFunction> Function;
	//	}

	struct JassFunction
	{
		public Dictionary< string , string > Parameter;
		public string Returns;
		//		public bool IsPublic;
		public List< String > OriginalCode;
	}

	StringBuilder builder = new StringBuilder();


	JassCompiler()
	{

		s_JassBaseType1.Add("array", "[]" );
		s_JassBaseType1.Add("or", "||" );
		s_JassBaseType1.Add("and", "&&" );
		s_JassBaseType1.Add("not", "!" );
		s_JassBaseType1.Add("(not", "(!" );
		s_JassBaseType1.Add("call", "" );
		s_JassBaseType1.Add("loop", "while( true )" );
		s_JassBaseType1.Add("exitwhen", "if" );

		s_JassBaseType1.Add("then", "" );
		s_JassBaseType1.Add("elseif", "else if" );
		s_JassBaseType1.Add("if", "if" );
		s_JassBaseType1.Add("if(", "if(" );
		s_JassBaseType1.Add("elseif(", "else if(" );
		s_JassBaseType1.Add("Filter(function", "(" );
		s_JassBaseType1.Add("function", "" );
		s_JassBaseType1.Add("StartThread(function", "StartThread(" );

			
		s_JassBaseType.Add("nothing", "void" );
		s_JassBaseType.Add("boolean", "bool" );
		s_JassBaseType.Add("integer", "int" );
		s_JassBaseType.Add("real", "double" );
		s_JassBaseType.Add("handle", "System.Object" );


		s_JassBaseType.Add("location", "BJLocation" );
		s_JassBaseType.Add("rect", "BJRect" );
		s_JassBaseType.Add("trigger", "BJTrigger" );
		s_JassBaseType.Add("camerasetup", "BJCameraSetup" );
		s_JassBaseType.Add("camerafield", "BJCameraField" );
		s_JassBaseType.Add("player", "BJPlayer" );
		s_JassBaseType.Add("unit", "BJUnit" );
		s_JassBaseType.Add("item", "BJItem" );
		s_JassBaseType.Add("button", "BJButton" );
		s_JassBaseType.Add("dialog", "BJDialog" );
		s_JassBaseType.Add("alliancetype", "BJAllianceType" );
		s_JassBaseType.Add("playergameresult", "BJPlayerGameResult" );
		s_JassBaseType.Add("quest", "BJQuest" );
		s_JassBaseType.Add("questitem", "BJQuestItem" );
		s_JassBaseType.Add("force", "BJForce" );
		s_JassBaseType.Add("event", "BJEvent" );
		s_JassBaseType.Add("boolexpr", "BJBoolExpr" );
		s_JassBaseType.Add("limitop", "BJLimitOP" );
		s_JassBaseType.Add("region", "BJRegion" );
		s_JassBaseType.Add("weathereffect", "BJWeatherEffect" );
		s_JassBaseType.Add("sound", "BJSound" );
		s_JassBaseType.Add("volumegroup", "BJVolumeGroup" );
		s_JassBaseType.Add("fogmodifier", "BJFogModifier" );
		s_JassBaseType.Add("fogstate", "BJfogState" );
		s_JassBaseType.Add("effect", "BJEffect" );
		s_JassBaseType.Add("widget", "BJWidget" );
		s_JassBaseType.Add("group", "BJGroup" );
		s_JassBaseType.Add("unitstate", "BJUnitState" );
		s_JassBaseType.Add("destructable", "BJDestructAble" );
		s_JassBaseType.Add("code", "BJCode" );
		s_JassBaseType.Add("mapcontrol", "BJMapControl" );
		s_JassBaseType.Add("defeatcondition", "BJDefeatCondition" );
		s_JassBaseType.Add("timer", "BJTimer" );
		s_JassBaseType.Add("timerdialog", "BJTimerDialog" );
		s_JassBaseType.Add("leaderboard", "BJLeaderBoard" );
		s_JassBaseType.Add("playercolor", "BJPlayerColor" );
		s_JassBaseType.Add("blendmode", "BJBlendMode" );
		s_JassBaseType.Add("gamecache", "BJGameCache" );
		s_JassBaseType.Add("playerslotstate", "BJPlayerSlotState" );
		s_JassBaseType.Add("playerstate", "BJPlayerState" );
		s_JassBaseType.Add("playerunitevent", "BJPlayerUnitEvent" );
		s_JassBaseType.Add("race", "BJRace" );
		s_JassBaseType.Add("racepreference", "BJRacePreference" );
		s_JassBaseType.Add("igamestate", "BJIgameState" );
		s_JassBaseType.Add("fgamestate", "BJFGameState" );
		s_JassBaseType.Add("gameevent", "BJGameEvent" );
		s_JassBaseType.Add("playerevent", "BJPlayerEvent" );
		s_JassBaseType.Add("widgetevent", "BJWidgetEvent" );
		s_JassBaseType.Add("dialogevent", "BJDialogEvent" );
		s_JassBaseType.Add("unittype", "BJUnitType" );
		s_JassBaseType.Add("gamespeed", "BJGameSpeed" );
		s_JassBaseType.Add("placement", "BJPlacement" );
		s_JassBaseType.Add("startlocprio", "BJStartLocPrio" );
		s_JassBaseType.Add("gamedifficulty", "BJGameDifficulty" );
		s_JassBaseType.Add("gametype", "BJGameType" );
		s_JassBaseType.Add("mapflag", "BJMapFlag" );
		s_JassBaseType.Add("mapvisibility", "BJMapVisibility" );
		s_JassBaseType.Add("mapsetting", "BJMapSetting" );
		s_JassBaseType.Add("mapdensity", "BJMapDensity" );
		s_JassBaseType.Add("raritycontrol", "BJRarityControl" );
		s_JassBaseType.Add("texmapflags", "BJTexMapFlags" );
		s_JassBaseType.Add("effecttype", "BJEffectType" );
		s_JassBaseType.Add("unitevent", "BJUnitEvent" );
		s_JassBaseType.Add("eventid", "BJEventID" );
		s_JassBaseType.Add("conditionfunc", "BJConditionFunc" );
		s_JassBaseType.Add("filterfunc", "BJFilterFunc" );
		s_JassBaseType.Add("gamestate", "BJGameState" );
		s_JassBaseType.Add("trackable", "BJTrackAble" );
		s_JassBaseType.Add("triggercondition", "BJTriggerCondition" );
		s_JassBaseType.Add("unitpool", "BJUnitPool" );
		s_JassBaseType.Add("itempool", "BJItemPool" );
		s_JassBaseType.Add("triggeraction", "BJTriggerAction" );
		s_JassBaseType.Add("itemtype", "BJItemType" );
		s_JassBaseType.Add("terraindeformation", "BJTerrainDeformation" );
		s_JassBaseType.Add("texttag", "BJTextTag" );
		s_JassBaseType.Add("version", "BJVersion" );
		s_JassBaseType.Add("aidifficulty", "BJAIdifficulty" );
		s_JassBaseType.Add("ability", "BJAbility" );
		s_JassBaseType.Add("multiboard", "BJMultiBoard" );
		s_JassBaseType.Add("multiboarditem", "BJMultiBoardItem" );
//		s_JassBaseType.Add("version", "BJVersion" );
//		s_JassBaseType.Add("version", "BJVersion" );
//		s_JassBaseType.Add("version", "BJVersion" );
//		s_JassBaseType.Add("version", "BJVersion" );
//		s_JassBaseType.Add("version", "BJVersion" );
//		s_JassBaseType.Add("version", "BJVersion" );
//		s_JassBaseType.Add("version", "BJVersion" );

	}

	public JassCompiler( string jassCode , string classname )
	{
		builder.Append("// Generated by ." + "\r\n" + "\r\n");
		builder.Append("using System;\r\n\r\n");
		builder.Append("namespace Jass\r\n");
		builder.Append("{\r\n");
		builder.Append("\tpublic partial class BJ \r\n");
		builder.Append("\t{\r\n");

		if ( classname.Length > 1 )
		{
			builder.Append("\r\n");
			builder.Append("\t\tpublic class " + classname + "\r\n");
			builder.Append("\t\t{\r\n");
		}


        

		if (jassCode == null)
			throw new ArgumentNullException("jassCode", "Argument_Null");
		if (jassCode.Length == 0)
			throw new ArgumentException("Argument_Empty", "jassCode");

		string[] jassLine = jassCode.Split(JASS_CRLF, StringSplitOptions.None);

		for ( int i = 0 ; i < jassLine.Length ; i++ )
		{
			jassLine[ i ] = jassLine[ i ].Replace( "\t" , "    " );
		}

		for (int i = 0; i < jassLine.Length; i++)
		{
			string[] jassWord;
			jassLine[i] = StripComments(jassLine[i] , "\t\t\t" ).Trim();
			jassWord = jassLine[i].Split(JASS_SPACE, StringSplitOptions.RemoveEmptyEntries);

			if ( jassWord == null || jassWord.Length == 0 )
			{
				continue;
			}

			if ( jassWord[ 0 ] == "constant" )
			{
				string[] jassWord1 = new string[ jassWord.Length ];

				for ( int i0 = 0 ; i0 < jassWord.Length ; i0++ ) 
				{
					jassWord1[ i0 ] = jassWord[ i0 ];
				}

				jassWord = new string[ jassWord1.Length - 1 ];

				for ( int i0 = 0 ; i0 < jassWord.Length ; i0++ ) 
				{
					jassWord[ i0 ] = jassWord1[ i0 + 1 ];
				}
			}

//             public System.Object getData( string str )
//             {
//                 switch ( str )
//                 {
//                     case "aa":
//                         return 1;
//                     default:
//                         break;
//                 }
//                 return null;
//             }

            switch (jassWord[0])
			{
				case "globals":
					{
						while ( true )
						{
//							UnityEngine.Debug.Log( "i " + i );

							string tab = classname.Length > 1 ? "\t\t\t" : "\t\t";

							i++;
							jassLine[ i ] = StripComments( jassLine[ i ] , tab ).Trim();
							jassWord = jassLine[i].Split(JASS_SPACE, StringSplitOptions.RemoveEmptyEntries);

							if ( jassLine[ i ].StartsWith( "endglobals" ) )
							{
                                builder.Append( "\r\n" );
                                builder.Append( tab + "public System.Object GetGlobalString( string str )" + "\r\n" );
                                builder.Append( tab + "{" + "\r\n" );
                                builder.Append( tab + "\t" + "switch ( str )" + "\r\n" );
                                builder.Append( tab + "\t" + "{" + "\r\n" );

                                foreach ( KeyValuePair< string , string > kvp in s_JassGlobals )
                                {
                                    builder.Append( tab + "\t\t" + "case \"" + kvp.Key + "\":" + "\r\n" );
                                    builder.Append( tab + "\t\t\t" + "return " + kvp.Key + ";" + "\r\n" );
                                }

                                builder.Append( tab + "\t" + "}" + "\r\n" );
                                builder.Append( tab + "\t" + "return null;" + "\r\n" );
                                builder.Append( tab + "}" + "\r\n" );
                                builder.Append( "\r\n" );

                                break;
							}

							if ( jassWord == null || jassWord.Length == 0 )
							{
								continue;
							}



							if ( jassWord[ 0 ] == "constant" )
							{
								builder.Append( tab + "public " );

								if ( s_JassBaseType.ContainsKey( jassWord[ 1 ] ) )
									builder.Append( s_JassBaseType[ jassWord[ 1 ] ] );
								else
									builder.Append( jassWord[ 1 ] );

// 								if ( s_JassBaseType1.ContainsKey( jassWord[ 2 ] ) )
//                                 {
//                                     builder.Append( s_JassBaseType1[ jassWord[ 2 ] ] );
//                                     s_JassGlobals.Add( s_JassBaseType1[ jassWord[ 2 ] ] , "" );
//                                 }
// 								else
//                                 {
                                    builder.Append( " " + jassWord[ 2 ] );
                                    s_JassGlobals.Add( jassWord[ 2 ] , "" );
//                                     s_JassBaseType1.Add( "\"" + jassWord[ 2 ] + "\"," , "GetGlobalString( \"" + jassWord[ 2 ] + "\" ), " );
//                                }


                                for ( int i0 = 3 ; i0 < jassWord.Length ; i0++ )
								{
									if ( jassWord[ i0 ].Length > 5 && jassWord[ i0 ][ 0 ] == '\'' && jassWord[ i0 ][ 5 ] == '\'' )
									{
										jassWord[ i0 ] = jassWord[ i0 ].Replace( "\'" , "\"" );
										jassWord[ i0 ] = jassWord[ i0 ].Insert( 6 , " )" );
										jassWord[ i0 ] = jassWord[ i0 ].Insert( 0 , "UnitId( " );
									}

									if ( s_JassBaseType1.ContainsKey( jassWord[ i0 ] ) )
										builder.Append( " " + s_JassBaseType1[ jassWord[ i0 ] ]  );
									else
										builder.Append( " " + jassWord[ i0 ] );
								}

								builder.Append( ";" );
								builder.Append( "\r\n" );
							}
							else
							{
								builder.Append( tab + "public " );

								if ( s_JassBaseType.ContainsKey( jassWord[ 0 ] ) )
									builder.Append( s_JassBaseType[ jassWord[ 0 ] ] + " " );
								else
									builder.Append( jassWord[ 0 ] + " " );

// 								if ( s_JassBaseType1.ContainsKey( jassWord[ 1 ] ) )
//                                 {
//                                     builder.Append( s_JassBaseType1[ jassWord[ 1 ] ] );
//                                     s_JassGlobals.Add( s_JassBaseType1[ jassWord[ 1 ] ] , "" );
//                                 }
//                                 else
//                                 {
                                    s_JassGlobals.Add( jassWord[ 1 ] , "" );
                                    builder.Append( " " + jassWord[ 1 ] );
//                                    s_JassBaseType1.Add( "\"" + jassWord[ 1 ] + "\"," , "GetGlobalString( \"" + jassWord[ 1 ] + "\" ), " );
//                                 }

                                for ( int i0 = 2 ; i0 < jassWord.Length ; i0++ )
								{
									if ( jassWord[ i0 ].Length > 5 && jassWord[ i0 ][ 0 ] == '\'' && jassWord[ i0 ][ 5 ] == '\'' )
									{
										jassWord[ i0 ] = jassWord[ i0 ].Replace( "\'" , "\"" );
										jassWord[ i0 ] = jassWord[ i0 ].Insert( 6 , " )" );
										jassWord[ i0 ] = jassWord[ i0 ].Insert( 0 , "UnitId( " );
									}

									if ( s_JassBaseType1.ContainsKey( jassWord[ i0 ] ) )
										builder.Append( " " + s_JassBaseType1[ jassWord[ i0 ] ]  );
									else
										builder.Append( " " + jassWord[ i0 ] );
								}

								builder.Append( ";" );
								builder.Append( "\r\n" );
							}
						}
						i--;


					}
					break;
				case "native":
					{
						string currentFunction = jassWord[ 1 ];

						JassFunction jFunction = new JassFunction();
						jFunction.Parameter = new Dictionary< string , string >();
						jFunction.OriginalCode = new List< string >();
						jFunction.Returns = jassWord[ jassWord.Length - 1 ];

						if ( jassWord[ 3 ] != "nothing" )
						{
							int takesIndex = jassLine[ i ].IndexOf( "takes" );
							int returnsIndex = jassLine[ i ].IndexOf( "returns" );
							string jStrParams = jassLine[ i ].Substring( 6 + takesIndex , returnsIndex - takesIndex - 7 );
							jassWord = jStrParams.Split( JASS_COMMA , StringSplitOptions.RemoveEmptyEntries );
							for ( int k = 0 ; k < jassWord.Length ; k++ )
							{
								string[] jParams = jassWord[ k ].Split( JASS_SPACE , StringSplitOptions.RemoveEmptyEntries );
								jFunction.Parameter.Add( jParams[ 1 ] , jParams[ 0 ] );
							}
						}


						builder.Append( "\t\t" );
						builder.Append( "public " );

						if ( s_JassBaseType.ContainsKey( jFunction.Returns ) )
							builder.Append( s_JassBaseType[ jFunction.Returns ] + " " );
						else
							builder.Append( jFunction.Returns + " " );

						builder.Append( currentFunction + "( " );

						Dictionary<string, string>.Enumerator jParamEnum = jFunction.Parameter.GetEnumerator();
						if ( jParamEnum.MoveNext() )
						{
							if ( s_JassBaseType.ContainsKey( jParamEnum.Current.Value ) )
								builder.Append( s_JassBaseType[ jParamEnum.Current.Value ] + " " + jParamEnum.Current.Key );
							else
								builder.Append( jParamEnum.Current.Value + " " + jParamEnum.Current.Key );
						}

						while ( jParamEnum.MoveNext() )
						{
							if ( s_JassBaseType.ContainsKey( jParamEnum.Current.Value ) )
								builder.Append( " , " + s_JassBaseType[ jParamEnum.Current.Value ] + " " + jParamEnum.Current.Key );
							else
								builder.Append( " , " + jParamEnum.Current.Value + " " + jParamEnum.Current.Key );
						}

						builder.Append( " )\r\n" );
						builder.Append( "\t\t{\r\n" );
						builder.Append( "\t\t\t// native code\r\n" );
						foreach ( string originalJass in jFunction.OriginalCode )
						{
							builder.Append( "\t\t\t// " + originalJass + "\r\n" );
						}
						switch ( jFunction.Returns )
						{
							case "boolean":
								builder.Append( "\t\t\treturn false;\r\n" );
								break;
							case "integer":
								builder.Append( "\t\t\treturn 0;\r\n" );
								break;
							case "real":
								builder.Append( "\t\t\treturn 0.0f;\r\n" );
								break;
							case "handle":
								builder.Append( "\t\t\treturn new Object();\r\n" );
								break;
							case "nothing":
								break;
							default:
								builder.Append( "\t\t\treturn null;\r\n" );
								break;
						}
						builder.Append( "\t\t}\r\n\r\n" );
					}
					break;
				case "function":
					{
						string currentFunction = jassWord[ 1 ];

						JassFunction jFunction = new JassFunction();
						jFunction.Parameter = new Dictionary< string , string >();
						jFunction.OriginalCode = new List< string >();
						jFunction.Returns = jassWord[ jassWord.Length - 1 ];

						if ( jassWord[3] != "nothing" )
						{
							int takesIndex = jassLine[i].IndexOf( "takes" );
							int returnsIndex = jassLine[i].IndexOf( "returns" );
							string jStrParams = jassLine[i].Substring( 6 + takesIndex , returnsIndex - takesIndex - 7 );
							jassWord = jStrParams.Split( JASS_COMMA , StringSplitOptions.RemoveEmptyEntries );
							for (int k = 0 ; k < jassWord.Length ; k++ )
							{
								string[] jParams = jassWord[ k ].Split( JASS_SPACE , StringSplitOptions.RemoveEmptyEntries );
								jFunction.Parameter.Add( jParams[1] , jParams[0] );
							}
						}

						string tab = classname.Length > 1 ? "\t\t\t" : "\t\t";

						builder.Append( tab );
						builder.Append( "public " );

						if ( s_JassBaseType.ContainsKey( jFunction.Returns )  )
							builder.Append( s_JassBaseType[ jFunction.Returns ] + " " );
						else
							builder.Append( jFunction.Returns + " " );

						builder.Append( currentFunction + "( " );

						Dictionary<string, string>.Enumerator jParamEnum = jFunction.Parameter.GetEnumerator();
						if (jParamEnum.MoveNext()) 
						{
							if ( s_JassBaseType.ContainsKey( jParamEnum.Current.Value )  )
								builder.Append( s_JassBaseType[ jParamEnum.Current.Value ] + " " + jParamEnum.Current.Key );
							else
								builder.Append( jParamEnum.Current.Value + " " + jParamEnum.Current.Key );
						}

						while (jParamEnum.MoveNext())
						{
							if ( s_JassBaseType.ContainsKey( jParamEnum.Current.Value )  )
								builder.Append( " , " + s_JassBaseType[ jParamEnum.Current.Value ] + " " + jParamEnum.Current.Key );
							else
								builder.Append( " , " + jParamEnum.Current.Value + " " + jParamEnum.Current.Key );
						}

						builder.Append( " )\r\n" );
						builder.Append( tab + "{\r\n" );
						builder.Append( tab + "\t// Original JassCode\r\n" );

						tab += "\t";

						i++;
						jassLine[ i ] = StripComments( jassLine[ i ] , tab ).Trim();

                        if ( currentFunction == "main" && s_JassGlobals.Count > 0 )
                        {
                            builder.Append( tab + "BJ.GetGlobalString = GetGlobalString;\r\n" );
                        }

                        while ( !jassLine[ i ].StartsWith("endfunction") )
						{
                            jassWord = jassLine[ i ].Split(JASS_SPACE, StringSplitOptions.RemoveEmptyEntries);

							if ( jassWord == null || jassWord.Length == 0 )
							{
								jFunction.OriginalCode.Add( jassLine[ i ] );
								i++;
								jassLine[ i ] = StripComments( jassLine[ i ] , tab ).Trim();
								continue;
							}

							if ( jassWord[ 0 ] == "debug" )
							{
								string[] jassWord1 = new string[ jassWord.Length ];

								for ( int i0 = 0 ; i0 < jassWord.Length ; i0++ ) 
								{
									jassWord1[ i0 ] = jassWord[ i0 ];
								}

								jassWord = new string[ jassWord1.Length - 1 ];

								for ( int i0 = 0 ; i0 < jassWord.Length ; i0++ ) 
								{
									jassWord[ i0 ] = jassWord1[ i0 + 1 ];
								}
							}

							switch ( jassWord[ 0 ] )
							{
								case "loop":
									{
										builder.Append( tab );
										builder.Append( s_JassBaseType1[ jassWord[ 0 ] ] );
										builder.Append( "\r\n" + tab + "{\r\n" );
										tab += "\t";
									}
									break;
								case "endloop":
									{
										tab = tab.Remove( tab.Length - 1 , 1 );
										builder.Append( tab + "}\r\n" );
									}
									break;
								case "exitwhen":
									{
										builder.Append( tab );

										for ( int i0 = 0 ; i0 < jassWord.Length ; i0++ )
										{
											if ( jassWord[ i0 ].Length > 5 && jassWord[ i0 ][ 0 ] == '\'' && jassWord[ i0 ][ 5 ] == '\'' )
											{
												jassWord[ i0 ] = jassWord[ i0 ].Replace( "\'" , "\"" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( 6 , " )" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( 0 , "UnitId( " );
											}

											if ( i0 == 1 )
											{
												builder.Append( "( " );
											}

											if ( s_JassBaseType1.ContainsKey( jassWord[ i0 ] ) )
												builder.Append( ( i0 > 0 ? " " : "" ) + s_JassBaseType1[ jassWord[ i0 ] ] );
											else
												builder.Append( ( i0 > 0 ? " " : "" ) + jassWord[ i0 ] );
										}

										builder.Append( " )" );
										builder.Append( "\r\n" + tab + "\tbreak;\r\n" );
									}
									break;
								case "elseif(":
								case "elseif":
									{
										tab = tab.Remove( tab.Length - 1 , 1 );

										builder.Append( tab + "}\r\n" );
										builder.Append( tab );

										for ( int i0 = 0 ; i0 < jassWord.Length ; i0++ )
										{
											if ( jassWord[ i0 ].Length > 5 && jassWord[ i0 ][ 0 ] == '\'' && jassWord[ i0 ][ 5 ] == '\'' )
											{
												jassWord[ i0 ] = jassWord[ i0 ].Replace( "\'" , "\"" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( 6 , " )" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( 0 , "UnitId( " );
											}

											if ( jassWord[ 0 ] != "elseif(" && i0 == 1 )
											{
												builder.Append( "( " );
											}

											if ( s_JassBaseType1.ContainsKey( jassWord[ i0 ] ) )
												builder.Append( ( i0 > 0 ? " " : "" ) + s_JassBaseType1[ jassWord[ i0 ] ] );
											else
												builder.Append( ( i0 > 0 ? " " : "" ) + jassWord[ i0 ] );
										}

										if ( jassWord[ 0 ] != "elseif(" )
										{
											builder.Append( " )" );
										}
										builder.Append( "\r\n" + tab + "{\r\n" );

										tab += "\t";
									}
									break;
								case "if(":
								case "if":
									{
										builder.Append( tab );

										for ( int i0 = 0 ; i0 < jassWord.Length ; i0++ )
										{
											if ( jassWord[ i0 ].Length > 5 && jassWord[ i0 ][ 0 ] == '\'' && jassWord[ i0 ][ 5 ] == '\'' )
											{
												jassWord[ i0 ] = jassWord[ i0 ].Replace( "\'" , "\"" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( 6 , " )" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( 0 , "UnitId( " );
											}

											if ( jassWord[ 0 ] != "if(" && i0 == 1 )
											{
												builder.Append( "( " );
											}

											if ( s_JassBaseType1.ContainsKey( jassWord[ i0 ] ) )
												builder.Append( ( i0 > 0 ? " " : "" ) + s_JassBaseType1[ jassWord[ i0 ] ] );
											else
												builder.Append( ( i0 > 0 ? " " : "" ) + jassWord[ i0 ] );
										}

										if ( jassWord[ 0 ] != "if(" )
										{
											builder.Append( " )" );
										}

										builder.Append( "\r\n" + tab + "{\r\n" );

										tab += "\t";
									}
									break;
								case "else":
									{
										tab = tab.Remove( tab.Length - 1 , 1 );

										builder.Append( tab + "}\r\n" );
										builder.Append( tab + "else\r\n" );
										builder.Append( tab + "{\r\n" );

										tab += "\t";
									}
									break;
								case "endif":
									{
										tab = tab.Remove( tab.Length - 1 , 1 );
										builder.Append( tab + "}\r\n" );
									}
									break;
								case "return":
									{
										builder.Append( tab );

										for ( int i0 = 0 ; i0 < jassWord.Length ; i0++ )
										{
											if ( jassWord[ i0 ].Length > 5 && jassWord[ i0 ][ 0 ] == '\'' && jassWord[ i0 ][ 5 ] == '\'' )
											{
												jassWord[ i0 ] = jassWord[ i0 ].Replace( "\'" , "\"" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( 6 , " )" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( 0 , "UnitId( " );
											}

											if ( s_JassBaseType1.ContainsKey( jassWord[ i0 ] ) )
												builder.Append( ( i0 > 0 ? " " : "" ) + s_JassBaseType1[ jassWord[ i0 ] ]  );
											else
												builder.Append( ( i0 > 0 ? " " : "" ) + jassWord[ i0 ] );
										}

										builder.Append( ";" );
										builder.Append( "\r\n" );
									}
									break;
								case "call":
								case "set":
									{
										builder.Append( tab );

										for ( int i0 = 1 ; i0 < jassWord.Length ; i0++ )
										{
                                            int indexi = jassWord[ i0 ].IndexOf( '\'' );
											if ( indexi >= 0 && jassWord[ i0 ].Length > 5 && jassWord[ i0 ][ indexi + 5 ] == '\'' )
											{
												jassWord[ i0 ] = jassWord[ i0 ].Replace( "\'" , "\"" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( indexi + 6 , " )" );
												jassWord[ i0 ] = jassWord[ i0 ].Insert( indexi , "UnitId( " );
											}

											jassWord[ i0 ] = jassWord[ i0 ].Replace( "(not" , "(!" );
                                            jassWord[ i0 ] = jassWord[ i0 ].Replace( "not" , "!" );

                                            if ( s_JassBaseType1.ContainsKey( jassWord[ i0 ] ) )
												builder.Append( ( i0 > 1 ? " " : "" ) + s_JassBaseType1[ jassWord[ i0 ] ]  );
											else
												builder.Append( ( i0 > 1 ? " " : "" ) + jassWord[ i0 ] );
										}

										builder.Append( ";" );
										builder.Append( "\r\n" );
									}
									break;
								case "local":
									{
										builder.Append( tab );

										if ( s_JassBaseType.ContainsKey( jassWord[ 1 ] ) )
											builder.Append( s_JassBaseType[ jassWord[ 1 ] ] );
										else
											builder.Append( jassWord[ 1 ] );

										if ( s_JassBaseType1.ContainsKey( jassWord[ 2 ] ) )
											builder.Append( s_JassBaseType1[ jassWord[ 2 ] ] );
										else
											builder.Append( " " + jassWord[ 2 ] );

										for ( int i0 = 3 ; i0 < jassWord.Length ; i0++ )
										{
											if ( s_JassBaseType1.ContainsKey( jassWord[ i0 ] ) )
												builder.Append( " " + s_JassBaseType1[ jassWord[ i0 ] ]  );
											else
												builder.Append( " " + jassWord[ i0 ] );

										}

										builder.Append( ";" );
										builder.Append( "\r\n" );
									}
									break;
								default:
									UnityEngine.Debug.LogError( jassLine[ i ] );
									builder.Append( tab + "" + jassLine[ i ] + "\r\n" );
									break;
							}

							jFunction.OriginalCode.Add( jassLine[ i ] );
							i++;
							jassLine[ i ] = StripComments( jassLine[ i ] , tab ).Trim();
						}
						i--;

//						switch ( jFunction.Returns )
//						{
//							case "boolean":
//								builder.Append( "\t\t\treturn false;\r\n" );
//								break;
//							case "integer":
//								builder.Append( "\t\t\treturn 0;\r\n" );
//								break;
//							case "real":
//								builder.Append( "\t\t\treturn 0.0f;\r\n" );
//								break;
//							case "handle":
//								builder.Append( "\t\t\treturn new Object();\r\n" );
//								break;
//							case "nothing":
//								break;
//							default:
//								builder.Append( "\t\t\treturn null;\r\n" );
//								break;
//						}

						tab = tab.Remove( tab.Length - 1 , 1 ); 
						builder.Append( tab + "}\r\n\r\n" );

					}


					break;
			}
		}

		if ( classname.Length > 1 )
		{
			builder.Append( "\t\t} // class " + classname + " \r\n" );
			builder.Append("\r\n");
		}

		builder.Append( "\t}\r\n" );
		builder.Append( "\r\n" );
	}


	public string GetCSharpCode()
	{
		//		StringBuilder builder = new StringBuilder();
		//		builder.Append("// Generated by AOOJassCompiler." + "\r\n" + "\r\n");
		//		builder.Append("using System;\r\n\r\n");
		//		builder.Append("namespace Jass\r\n");
		//		builder.Append("{\r\n");
		////		Dictionary<string, Type>.Enumerator jBaseTypeEnum = s_JassBaseType.GetEnumerator();
		////		while (jBaseTypeEnum.MoveNext())
		////		{
		////			builder.Append("\tusing " + jBaseTypeEnum.Current.Key
		////				+ " = " + jBaseTypeEnum.Current.Value.ToString() + ";\r\n");
		////		}
		//
		//		builder.Append("\r\n");
		//		Dictionary<string, JassClass>.Enumerator jClassEnum = m_JassClass.GetEnumerator();
		//		while (jClassEnum.MoveNext())
		//		{
		//			builder.Append("\tpartial class " + jClassEnum.Current.Key + "\r\n");
		//			builder.Append("\t{\r\n");
		//			Dictionary<string, JassFunction>.Enumerator jFuncEnum = jClassEnum.Current.Value.Function.GetEnumerator();
		//			while (jFuncEnum.MoveNext())
		//			{
		//				builder.Append("\t\t");
		//				if (jFuncEnum.Current.Value.IsPublic)
		//					builder.Append("public ");
		//
		//				if ( s_JassBaseType.ContainsKey( jFuncEnum.Current.Value.Returns )  )
		//					builder.Append( s_JassBaseType[ jFuncEnum.Current.Value.Returns.ToString() ] + " ");
		//				else
		//					builder.Append(jFuncEnum.Current.Value.Returns.ToString() + " ");
		//				builder.Append(jFuncEnum.Current.Key + "( ");
		//
		//				Dictionary<string, string>.Enumerator jParamEnum = jFuncEnum.Current.Value.Parameter.GetEnumerator();
		//				if (jParamEnum.MoveNext()) 
		//				{
		//					if ( s_JassBaseType.ContainsKey( jParamEnum.Current.Value )  )
		//						builder.Append( s_JassBaseType[ jParamEnum.Current.Value ] + " " + jParamEnum.Current.Key);
		//					else
		//						builder.Append(jParamEnum.Current.Value + " " + jParamEnum.Current.Key);
		//				}
		//
		//				while (jParamEnum.MoveNext())
		//				{
		//					if ( s_JassBaseType.ContainsKey( jParamEnum.Current.Value )  )
		//						builder.Append(" , " + s_JassBaseType[ jParamEnum.Current.Value ] + " " + jParamEnum.Current.Key);
		//					else
		//						builder.Append(" , " + jParamEnum.Current.Value + " " + jParamEnum.Current.Key);
		//				}
		//
		//				builder.Append( " )\r\n");
		//				builder.Append("\t\t{\r\n");
		//				builder.Append("\t\t\t// Original JassCode\r\n");
		//				foreach (string originalJass in jFuncEnum.Current.Value.OriginalCode)
		//				{
		//					builder.Append("\t\t\t// " + originalJass + "\r\n");
		//				}
		//				switch (jFuncEnum.Current.Value.Returns)
		//				{
		//					case "boolean":
		//						builder.Append("\t\t\treturn false;\r\n");
		//						break;
		//					case "integer":
		//						builder.Append("\t\t\treturn 0;\r\n");
		//						break;
		//					case "real":
		//						builder.Append("\t\t\treturn 0.0f;\r\n");
		//						break;
		//					case "handle":
		//						builder.Append("\t\t\treturn new Object();\r\n");
		//						break;
		//					case "nothing":
		//						break;
		//					default:
		//						builder.Append("\t\t\treturn null;\r\n");
		//						break;
		//				}
		//				builder.Append("\t\t}\r\n\r\n");
		//
		//			}
		//			builder.Append("\t} // class " + jClassEnum.Current.Key + "\r\n");
		//		}
		//		builder.Append("\r\n");
		return builder.ToString();
	}

	string StripComments( string jassLine , string sss )
	{
//		jassLine.Replace( "Debug" , "" );

		if ( jassLine.Length > 0 )
		{
			int commentPosition = jassLine.IndexOf( "//" );

			if ( commentPosition == -1 )
				return jassLine;

			builder.Append( sss + jassLine.Substring( commentPosition , jassLine.Length - commentPosition ) + "\r\n" );

			return jassLine.Substring( 0 , commentPosition );
		}

		return jassLine;
	}


	readonly string[] JASS_CRLF = { "\r\n" };
	readonly string[] JASS_SPACE = { " " };
	readonly string[] JASS_COMMA = { "," };
	Dictionary<string, string> s_JassBaseType = new Dictionary<string, string>();
	Dictionary<string, string> s_JassBaseType1 = new Dictionary<string, string>();

    Dictionary<string , string> s_JassGlobals = new Dictionary<string , string>();

    //	Dictionary<string, JassClass> m_JassClass = new Dictionary<string, JassClass>();
}
