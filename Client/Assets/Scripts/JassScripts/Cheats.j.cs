// Generated by .


	public partial class GameDefine 
	{
		//===========================================================================
		// Cheats.j
		//===========================================================================
		// Debug-cheat globals
		public static double DEBUG_CAMFIELD_SPEED = 0;
		public static string DEBUG_CHAT_GIMME = "gimme";
		public static string DEBUG_CHAT_DEMO = "demo";
		public static string DEBUG_CHAT_TELEPORT = "teleport";
		public static string DEBUG_CHAT_TELEPORT2 = "ttt";
		public static string DEBUG_CHAT_UNITINFO = "unitinfo";
		public static string DEBUG_CHAT_UNITINFO2 = "ui";
		public static string DEBUG_CHAT_CAMINFO = "caminfo";
		public static string DEBUG_CHAT_CAMINFO2 = "ci";
		public static string DEBUG_CHAT_CAMDIST = "camdist";
		public static string DEBUG_CHAT_CAMFARZ = "camfarz";
		public static string DEBUG_CHAT_CAMAOA = "camaoa";
		public static string DEBUG_CHAT_CAMFOV = "camfov";
		public static string DEBUG_CHAT_CAMROLL = "camroll";
		public static string DEBUG_CHAT_CAMROT = "camrot";
		public static string DEBUG_CHAT_CAMRESET = "camreset";
		public static string DEBUG_CHAT_CLONE = "clone";
		public static string DEBUG_CHAT_DISPEL = "dispel";
		public static string DEBUG_CHAT_GOTOX = "gotox";
		public static string DEBUG_CHAT_GOTOY = "gotoy";
		public static string DEBUG_CHAT_GOTOXY = "gotoxy";
		public static string DEBUG_CHAT_GOTOUNIT = "gotounit";
		public static string DEBUG_CHAT_BLACKMASK = "blackmask";
		public static string DEBUG_CHAT_BLACKMASK2 = "bm";
		public static string DEBUG_CHAT_DIFFICULTY = "difficulty";
		public static string DEBUG_CHAT_FINGEROFDEATH = "fingerofdeath";
		public static BJTrigger  debugGimmeTrig;
		public static BJTrigger  debugDemoTrig;
		public static BJTrigger  debugTeleportTrig;
		public static BJTrigger  debugUnitInfoTrig;
		public static BJTrigger  debugCamInfoTrig;
		public static BJTrigger  debugCamDistTrig;
		public static BJTrigger  debugCamFarzTrig;
		public static BJTrigger  debugCamAoaTrig;
		public static BJTrigger  debugCamFovTrig;
		public static BJTrigger  debugCamRollTrig;
		public static BJTrigger  debugCamRotTrig;
		public static BJTrigger  debugCamResetTrig;
		public static BJTrigger  debugCloneTrig;
		public static BJTrigger  debugDispelTrig;
		public static BJTrigger  debugGotoXTrig;
		public static BJTrigger  debugGotoYTrig;
		public static BJTrigger  debugGotoXYTrig;
		public static BJTrigger  debugGotoUnitTrig;
		public static BJTrigger  debug_BlackMaskTrig;
		public static BJTrigger  debugDifficultyTrig;
		public static BJTrigger [] debugFingerOfDeathTrig;
		public static BJTrigger [] debugToolOfDeathTrig;
		public static bool [] debugFingerOfDeathEnabled;
		public static double  debugGotoUnitX = 0;
		public static double  debugGotoUnitY = 0;
		public static int  debugGotoUnits = 0;
		//***************************************************************************
		//*
		//*  Debug cheats
		//*
		//***************************************************************************
		//===========================================================================
		public static void DebugGimmeEnum(  )
		{
			// Original JassCode
			BJPlayer thePlayer = GetEnumPlayer();
			DisplayTextToPlayer(thePlayer, 0, 0, "Player "+I2S(GetPlayerId(GetTriggerPlayer())+1)+" cheated: Give 5000 gold && 5000 lumber to all players");
			SetPlayerState(thePlayer, PLAYER_STATE_RESOURCE_GOLD, GetPlayerState(thePlayer, PLAYER_STATE_RESOURCE_GOLD) + 5000);
			SetPlayerState(thePlayer, PLAYER_STATE_RESOURCE_LUMBER, GetPlayerState(thePlayer, PLAYER_STATE_RESOURCE_LUMBER) + 5000);
		}

		//===========================================================================
		public static void DebugGimme(  )
		{
			// Original JassCode
			ForForce(FORCE_ALL_PLAYERS,  DebugGimmeEnum);
		}

		//===========================================================================
		public static void DebugDemoEnum(  )
		{
			// Original JassCode
			BJPlayer thePlayer = GetEnumPlayer();
			SetPlayerState(thePlayer, PLAYER_STATE_RESOURCE_GOLD, 0);
			SetPlayerState(thePlayer, PLAYER_STATE_RESOURCE_LUMBER, 0);
		}

		//===========================================================================
		public static void DebugDemo(  )
		{
			// Original JassCode
			BJPlayer thePlayer = GetTriggerPlayer();
			int gold = GetRandomInt(750, 1500);
			int lumber = GetRandomInt(200, 450);
			ForForce(FORCE_ALL_PLAYERS,  DebugDemoEnum);
			MultiboardSuppressDisplay(true);
			if(  (GetLocalPlayer() == GetTriggerPlayer())  )
			{
				Cheat("warnings");
				Cheat("fastbuild");
				Cheat("techtree");
				Cheat("research");
				Cheat("food");
				Cheat("mana");
				Cheat("dawn");
				Cheat("gold " + I2S(gold));
				Cheat("lumber " + I2S(lumber));
			}
		}

		//===========================================================================
		public static void DebugTeleportEnum(  )
		{
			// Original JassCode
			BJUnit u = GetEnumUnit();
			SetUnitPosition(u, GetCameraTargetPositionX(), GetCameraTargetPositionY());
		}

		//===========================================================================
		public static void DebugTeleport(  )
		{
			// Original JassCode
			BJGroup g = CreateGroup();
			SyncSelections();
			GroupEnumUnitsSelected(g, GetTriggerPlayer(), null);
			ForGroup(g,  DebugTeleportEnum);
		}

		//===========================================================================
		public static string TertiaryStringOp( bool expr , string a , string b )
		{
			// Original JassCode
			if(  (expr)  )
			{
				return a;
			}
			else
			{
				return b;
			}
		}

		//===========================================================================
		// Convert a integer id value into a 4-letter id code.
		//
		public static string DebugIdInteger2IdString( int value )
		{
			// Original JassCode
			string charMap = ".................................!.#$%&'()*+,-./0123456789:;<=>.@ABCDEFGHIJKLMNOPQRSTUVWXYZ[.]^_`abcdefghijklmnopqrstuvwxyz{|}~.................................................................................................................................";
			string result = "";
			int remainingValue = value;
			int charValue;
			int byteno;
			byteno = 0;
			while( true )
			{
				charValue = ModuloInteger(remainingValue, 256);
				remainingValue = remainingValue / 256;
				result = SubString(charMap, charValue, charValue + 1) + result;
				byteno = byteno + 1;
				if(  byteno == 4 )
					break;
			}
			return result;
		}

		//===========================================================================
		public static void DebugUnitInfoEnum(  )
		{
			// Original JassCode
			BJPlayer thePlayer = GetTriggerPlayer();
			BJUnit theUnit = GetEnumUnit();
			string message;
			message = "Player " + I2S(GetPlayerId(GetOwningPlayer(theUnit))+1);
			message = message + " '" + DebugIdInteger2IdString(GetUnitTypeId(theUnit)) + "'";
			message = message + " " + GetUnitName(theUnit);
			message = message + " (" + R2SW(GetUnitX(theUnit), 0, 0) + ", " + R2SW(GetUnitY(theUnit), 0, 0);
			message = message + ": " + R2SW(GetUnitFacing(theUnit), 0, 0) + ") ";
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_HERO), " Hero", "");
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_DEAD), " Dead", "");
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_STRUCTURE), " Structure", "");
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_GROUND), " Grnd", "");
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_FLYING), " Air", "");
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_ATTACKS_GROUND), " VsGrnd", "");
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_ATTACKS_FLYING), " VsAir", "");
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_MELEE_ATTACKER), " Melee", "");
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_RANGED_ATTACKER), " Ranged", "");
			message = message + TertiaryStringOp(IsUnitType(theUnit, UNIT_TYPE_SUMMONED), " Summoned", "");
			DisplayTextToPlayer(thePlayer, 0, 0, message);
		}

		//===========================================================================
		public static void DebugUnitInfo(  )
		{
			// Original JassCode
			BJGroup g = CreateGroup();
			SyncSelections();
			GroupEnumUnitsSelected(g, GetTriggerPlayer(), null);
			ForGroup(g,  DebugUnitInfoEnum);
		}

		//===========================================================================
		public static void DebugCamInfo(  )
		{
			// Original JassCode
			BJPlayer thePlayer = GetTriggerPlayer();
			string message;
			message = "Targ(" + R2SW(GetCameraTargetPositionX(), 0, 0);
			message = message + "," + R2SW(GetCameraTargetPositionY(), 0, 0);
			message = message + "," + R2SW(GetCameraTargetPositionZ(), 0, 0);
			message = message + ")";
			message = message + ", Dist=" + R2SW(GetCameraField(CAMERA_FIELD_TARGET_DISTANCE), 0, 0);
			message = message + ", FarZ=" + R2SW(GetCameraField(CAMERA_FIELD_FARZ), 0, 0);
			message = message + ", AoA=" + R2SW(GetCameraField(CAMERA_FIELD_ANGLE_OF_ATTACK) * RADTODEG, 0, 2);
			message = message + ", FoV=" + R2SW(GetCameraField(CAMERA_FIELD_FIELD_OF_VIEW) * RADTODEG, 0, 2);
			message = message + ", Roll=" + R2SW(GetCameraField(CAMERA_FIELD_ROLL) * RADTODEG, 0, 2);
			message = message + ", Rot=" + R2SW(GetCameraField(CAMERA_FIELD_ROTATION) * RADTODEG, 0, 2);
			DisplayTextToPlayer(thePlayer, 0, 0, message);
		}

		//===========================================================================
		public static void DebugCamField( BJCameraField whichField , int cheatLength , double defaultValue )
		{
			// Original JassCode
			string param = SubString(GetEventPlayerChatString(), cheatLength, 50);
			double value = S2R(param);
			// Remove any excess preceding whitespace
			while( true )
			{
				if(  ! (SubString(param, 0, 1) == " ") )
					break;
				param = SubString(param, 1, 50);
			}
			if(  param == ""  )
			{
				value = defaultValue;
			}
			if(  (whichField == CAMERA_FIELD_FARZ) && (value <= CAMERA_MIN_FARZ)  )
			{
				return;
			}
			SetCameraFieldForPlayer(GetTriggerPlayer(), whichField, value, DEBUG_CAMFIELD_SPEED);
		}

		//===========================================================================
		public static void DebugCamDist(  )
		{
			// Original JassCode
			DebugCamField(CAMERA_FIELD_TARGET_DISTANCE, 7, 1600);
		}

		//===========================================================================
		public static void DebugCamFarZ(  )
		{
			// Original JassCode
			DebugCamField(CAMERA_FIELD_FARZ, 7, 4000);
		}

		//===========================================================================
		public static void DebugCamFOV(  )
		{
			// Original JassCode
			DebugCamField(CAMERA_FIELD_FIELD_OF_VIEW, 6, 65);
		}

		//===========================================================================
		public static void DebugCamAOA(  )
		{
			// Original JassCode
			DebugCamField(CAMERA_FIELD_ANGLE_OF_ATTACK, 6, 310);
		}

		//===========================================================================
		public static void DebugCamRoll(  )
		{
			// Original JassCode
			DebugCamField(CAMERA_FIELD_ROLL, 7, 0);
		}

		//===========================================================================
		public static void DebugCamRot(  )
		{
			// Original JassCode
			DebugCamField(CAMERA_FIELD_ROTATION, 6, 90);
		}

		//===========================================================================
		public static void DebugCamReset(  )
		{
			// Original JassCode
			ResetToGameCamera(0);
			EnableUserControl(true);
		}

		//===========================================================================
		public static void DebugCloneUnitEnum(  )
		{
			// Original JassCode
			BJUnit u = GetEnumUnit();
			CreateUnit(GetOwningPlayer(u), GetUnitTypeId(u), GetUnitX(u), GetUnitY(u), GetUnitFacing(u));
		}

		//===========================================================================
		public static void DebugCloneUnit(  )
		{
			// Original JassCode
			BJGroup g = CreateGroup();
			SyncSelections();
			GroupEnumUnitsSelected(g, GetTriggerPlayer(), null);
			ForGroup(g,  DebugCloneUnitEnum);
		}

		//===========================================================================
		public static void DebugDispelUnitEnum(  )
		{
			// Original JassCode
			UnitRemoveBuffs(GetEnumUnit(), true, true);
		}

		//===========================================================================
		public static void DebugDispelUnit(  )
		{
			// Original JassCode
			BJGroup g = CreateGroup();
			SyncSelections();
			GroupEnumUnitsSelected(g, GetTriggerPlayer(), null);
			ForGroup(g,  DebugDispelUnitEnum);
		}

		//===========================================================================
		public static void DebugGotoX(  )
		{
			// Original JassCode
			string chatString = GetEventPlayerChatString();
			if(  (DEBUG_CHAT_GOTOX + " " == SubString(chatString, 0, 6))  )
			{
				SetCameraPositionForPlayer(GetTriggerPlayer(), S2R(SubString(chatString, 6, 50)), GetCameraTargetPositionY());
			}
		}

		//===========================================================================
		public static void DebugGotoY(  )
		{
			// Original JassCode
			string chatString = GetEventPlayerChatString();
			if(  (DEBUG_CHAT_GOTOY + " " == SubString(chatString, 0, 6))  )
			{
				SetCameraPositionForPlayer(GetTriggerPlayer(), GetCameraTargetPositionX(), S2R(SubString(chatString, 6, 50)));
			}
		}

		//===========================================================================
		public static void DebugGotoXY(  )
		{
			// Original JassCode
			string chatString = GetEventPlayerChatString();
			int index;
			bool inParam1;
			if(  (DEBUG_CHAT_GOTOXY + " " == SubString(chatString, 0, 7))  )
			{
				inParam1 = false;
				index = 7;
				while( true )
				{
					if(  (SubString(chatString, index, index + 1) != " ")  )
					{
						inParam1 = true;
					}
					if(  (inParam1 && SubString(chatString, index, index + 1) == " ") )
						break;
					if(  index > 50 )
						break;
					index = index + 1;
				}
				if(  (index > 50)  )
				{
					DisplayTextToPlayer(GetTriggerPlayer(), 0, 0, "Usage: GotoXY x y");
				}
				else
				{
					if(  (GetLocalPlayer() == GetTriggerPlayer())  )
					{
						SetCameraPositionForPlayer(GetTriggerPlayer(), S2R(SubString(chatString, 7, index)), S2R(SubString(chatString, index, 50)));
					}
				}
			}
		}

		//===========================================================================
		public static void DebugGotoUnitEnum(  )
		{
			// Original JassCode
			BJUnit u = GetEnumUnit();
			debugGotoUnitX = debugGotoUnitX + GetUnitX(u);
			debugGotoUnitY = debugGotoUnitY + GetUnitY(u);
			debugGotoUnits = debugGotoUnits + 1;
		}

		//===========================================================================
		public static void DebugGotoUnit(  )
		{
			// Original JassCode
			BJGroup g = CreateGroup();
			debugGotoUnitX = 0;
			debugGotoUnitY = 0;
			debugGotoUnits = 0;
			SyncSelections();
			GroupEnumUnitsSelected(g, GetTriggerPlayer(), null);
			ForGroup(g,  DebugGotoUnitEnum);
			if(  (debugGotoUnits != 0)  )
			{
				debugGotoUnitX = debugGotoUnitX / debugGotoUnits;
				debugGotoUnitY = debugGotoUnitY / debugGotoUnits;
				SetCameraPositionForPlayer(GetTriggerPlayer(), debugGotoUnitX, debugGotoUnitY);
			}
		}

		//===========================================================================
		public static void DebugBlackMask(  )
		{
			// Original JassCode
			SetFogStateRect(GetTriggerPlayer(), FOG_OF_WAR_MASKED, GetWorldBounds(), true);
		}

		//===========================================================================
		public static void DebugDifficulty(  )
		{
			// Original JassCode
			BJPlayer thePlayer = GetTriggerPlayer();
			BJGameDifficulty theDiff = GetGameDifficulty();
			if(  (theDiff == MAP_DIFFICULTY_EASY)  )
			{
				DisplayTextToPlayer(thePlayer, 0, 0, "Easy Difficulty");
			}
			else if(  (theDiff == MAP_DIFFICULTY_NORMAL)  )
			{
				DisplayTextToPlayer(thePlayer, 0, 0, "Normal Difficulty");
			}
			else if(  (theDiff == MAP_DIFFICULTY_HARD)  )
			{
				DisplayTextToPlayer(thePlayer, 0, 0, "Hard Difficulty");
			}
			else
			{
				DisplayTextToPlayer(thePlayer, 0, 0, "ERROR! Unrecognized Difficulty");
			}
		}

		//===========================================================================
		public static void DebugToolOfDeath(  )
		{
			// Original JassCode
			KillUnit(GetTriggerUnit());
		}

		//===========================================================================
		public static void DebugToggleFingerOfDeath(  )
		{
			// Original JassCode
			int index = GetPlayerId(GetTriggerPlayer());
			if(  (debugFingerOfDeathEnabled[index])  )
			{
				DisplayTextToPlayer(Player(index), 0, 0, "Finger Of Death Disabled");
				DisableTrigger(debugToolOfDeathTrig[index]);
			}
			else
			{
				DisplayTextToPlayer(Player(index), 0, 0, "Finger Of Death Enabled");
				EnableTrigger(debugToolOfDeathTrig[index]);
			}
			debugFingerOfDeathEnabled[index] = ! debugFingerOfDeathEnabled[index];
		}

		//===========================================================================
		public static bool InitDebugTriggers(  )
		{
			// Original JassCode
			BJPlayer indexPlayer;
			int index;
			index = 0;
			while( true )
			{
				indexPlayer = Player(index);
				if(  (GetPlayerSlotState(indexPlayer) == PLAYER_SLOT_STATE_PLAYING)  )
				{
					debugGimmeTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugGimmeTrig, indexPlayer, DEBUG_CHAT_GIMME, true);
					TriggerAddAction(debugGimmeTrig,  DebugGimme);
					debugDemoTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugDemoTrig, indexPlayer, DEBUG_CHAT_DEMO, true);
					TriggerAddAction(debugDemoTrig,  DebugDemo);
					debugTeleportTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugTeleportTrig, indexPlayer, DEBUG_CHAT_TELEPORT, true);
					TriggerRegisterPlayerChatEvent(debugTeleportTrig, indexPlayer, DEBUG_CHAT_TELEPORT2, true);
					TriggerAddAction(debugTeleportTrig,  DebugTeleport);
					debugUnitInfoTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugUnitInfoTrig, indexPlayer, DEBUG_CHAT_UNITINFO, true);
					TriggerRegisterPlayerChatEvent(debugUnitInfoTrig, indexPlayer, DEBUG_CHAT_UNITINFO2, true);
					TriggerAddAction(debugUnitInfoTrig,  DebugUnitInfo);
					debugCamInfoTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugCamInfoTrig, indexPlayer, DEBUG_CHAT_CAMINFO, true);
					TriggerRegisterPlayerChatEvent(debugCamInfoTrig, indexPlayer, DEBUG_CHAT_CAMINFO2, true);
					TriggerAddAction(debugCamInfoTrig,  DebugCamInfo);
					debugCamDistTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugCamDistTrig, indexPlayer, DEBUG_CHAT_CAMDIST, false);
					TriggerAddAction(debugCamDistTrig,  DebugCamDist);
					debugCamFarzTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugCamFarzTrig, indexPlayer, DEBUG_CHAT_CAMFARZ, false);
					TriggerAddAction(debugCamFarzTrig,  DebugCamFarZ);
					debugCamFovTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugCamFovTrig, indexPlayer, DEBUG_CHAT_CAMFOV, false);
					TriggerAddAction(debugCamFovTrig,  DebugCamFOV);
					debugCamAoaTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugCamAoaTrig, indexPlayer, DEBUG_CHAT_CAMAOA, false);
					TriggerAddAction(debugCamAoaTrig,  DebugCamAOA);
					debugCamRollTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugCamRollTrig, indexPlayer, DEBUG_CHAT_CAMROLL, false);
					TriggerAddAction(debugCamRollTrig,  DebugCamRoll);
					debugCamRotTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugCamRotTrig, indexPlayer, DEBUG_CHAT_CAMROT, false);
					TriggerAddAction(debugCamRotTrig,  DebugCamRot);
					debugCamResetTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugCamResetTrig, indexPlayer, DEBUG_CHAT_CAMRESET, true);
					TriggerAddAction(debugCamResetTrig,  DebugCamReset);
					debugCloneTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugCloneTrig, indexPlayer, DEBUG_CHAT_CLONE, true);
					TriggerAddAction(debugCloneTrig,  DebugCloneUnit);
					debugDispelTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugDispelTrig, indexPlayer, DEBUG_CHAT_DISPEL, true);
					TriggerAddAction(debugDispelTrig,  DebugDispelUnit);
					debugGotoXTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugGotoXTrig, indexPlayer, DEBUG_CHAT_GOTOX, false);
					TriggerAddAction(debugGotoXTrig,  DebugGotoX);
					debugGotoYTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugGotoYTrig, indexPlayer, DEBUG_CHAT_GOTOY, false);
					TriggerAddAction(debugGotoYTrig,  DebugGotoY);
					debugGotoXYTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugGotoXYTrig, indexPlayer, DEBUG_CHAT_GOTOXY, false);
					TriggerAddAction(debugGotoXYTrig,  DebugGotoXY);
					debugGotoUnitTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugGotoUnitTrig, indexPlayer, DEBUG_CHAT_GOTOUNIT, true);
					TriggerAddAction(debugGotoUnitTrig,  DebugGotoUnit);
					debug_BlackMaskTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debug_BlackMaskTrig, indexPlayer, DEBUG_CHAT_BLACKMASK, true);
					TriggerRegisterPlayerChatEvent(debug_BlackMaskTrig, indexPlayer, DEBUG_CHAT_BLACKMASK2, true);
					TriggerAddAction(debug_BlackMaskTrig,  DebugBlackMask);
					debugDifficultyTrig = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugDifficultyTrig, indexPlayer, DEBUG_CHAT_DIFFICULTY, true);
					TriggerAddAction(debugDifficultyTrig,  DebugDifficulty);
					debugFingerOfDeathEnabled[index] = false;
					debugToolOfDeathTrig[index] = CreateTrigger();
					TriggerRegisterPlayerUnitEvent(debugToolOfDeathTrig[index], indexPlayer, EVENT_PLAYER_UNIT_SELECTED, null);
					TriggerAddAction(debugToolOfDeathTrig[index],  DebugToolOfDeath);
					DisableTrigger(debugToolOfDeathTrig[index]);
					debugFingerOfDeathTrig[index] = CreateTrigger();
					TriggerRegisterPlayerChatEvent(debugFingerOfDeathTrig[index], indexPlayer, DEBUG_CHAT_FINGEROFDEATH, true);
					TriggerAddAction(debugFingerOfDeathTrig[index],  DebugToggleFingerOfDeath);
				}
				index = index + 1;
				if(  index == MAX_PLAYERS )
					break;
			}
			return true;
		}

	}

