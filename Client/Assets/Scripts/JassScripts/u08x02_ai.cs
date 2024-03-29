// Generated by .


	public partial class GameDefine 
	{

		public class u08x02_ai
		{
		//==================================================================================================
		//  $Id: u08x02.ai,v 1.14.2.7 2003/05/12 20:47:05 abond Exp $
		//==================================================================================================
			public int ILLIDAN_TOWN_X = 4400;
			public int ILLIDAN_TOWN_Y = 6300;
			public int ILLIDAN_DEF_X = 5050;
			public int ILLIDAN_DEF_Y = 6600;
			public BJPlayer USER = PlayerEx(1);
			public int OFFSET_X = 600;
			public int OFFSET_Y = 500;
			public int NAGA_COUATL2 = UnitId( "n000" );
			public int NAGA_REAVER2 = UnitId( "n001" );
			public int NAGA_ROYAL2 = UnitId( "n002" );
			public int NAGA_MYRMIDON2 = UnitId( "n003" );
			public int NAGA_SNAP_DRAGON2 = UnitId( "n004" );
			public int NAGA_TURTLE2 = UnitId( "n005" );
			public int NAGA_SIREN2 = UnitId( "n007" );
			public int BUCKET_COUATL = 0;
			public int BUCKET_SNAP_DRAGON = 1;
			public int BUCKET_REAVER = 2;
			public int BUCKET_MYRMIDON = 3;
			public int BUCKET_SIREN = 4;
			public int BUCKET_TURTLE = 5;
			public int BUCKET_ROYAL = 6;
			public int BUCKET_END = 7;
			public int SET_TOWN_1_X = 1;
			public int SET_TOWN_1_Y = 2;
			public int SET_TOWN_2_X = 3;
			public int SET_TOWN_2_Y = 4;
			public int SET_TOWN_3_X = 5;
			public int SET_TOWN_3_Y = 6;
			public int SET_TOWN_4_X = 7;
			public int SET_TOWN_4_Y = 8;
			public int PEONS_LOST = 9;
			public int SLOT_LOST = 10;
			public int SLOT_GAINED = 11;
			public int SLOT_STEPPED_ON = 12;
			public int STARTUP_CMDS = 12;
			public bool NORMAL_ATTACK = true;
			public bool PEON_ATTACK = false;
			public int NO_CONTROL = 1;
			public int ILLIDAN_CONTROL = 2;
			public int ARTHAS_CONTROL = 3;
			public int NOT_POSSIBLE = 1;
			public int START_SLOT = 2;
			public int  startup = 0;
			public bool  first_attack = true;
			public int  action_state = 10;
			public int  next_slot = -1;
			public int  attack_delay = 1;
			public int  attack_index = 0;
			public bool  rebuild_Illidan = true;
			public bool  stepped_on = false;
			public bool  suicide_mode = false;
			public bool  did_suicide = false;
			public bool  timeout_failure = false;
			public int [] town_x;
			public int [] town_y;
			public bool [] peons_lost;
			public bool [] slot_owned;
			public int [] slot_control;
			public int [] Illidan0;
			public int [] Illidan1;
			public int [] Illidan2;
			public int [] Illidan3;
			public int [] attack_min_COPs;
			public int [] attack_max_COPs;
			public int [] attack_qty;
			public int [] attack_bucket;
			public int [] bucket_qty;
			public int [] bucket_unitid;
			public double [] food_factor;
			public double  dragon_factor;
		//--------------------------------------------------------------------------------------------------
		//  set_attack_group
		//--------------------------------------------------------------------------------------------------
			public void set_attack_group( int min_COPs , int max_COPs , int normal , int hard , int idx )
			{
				// Original JassCode
				attack_min_COPs[attack_index] = min_COPs;
				attack_max_COPs[attack_index] = max_COPs;
				attack_bucket[attack_index] = idx;
				if(  difficulty==EASY || difficulty==NORMAL  )
				{
					attack_qty[attack_index] = normal;
				}
				else
				{
					attack_qty[attack_index] = hard;
				}
				attack_index = attack_index + 1;
			}

		//--------------------------------------------------------------------------------------------------
		//  wave_time
		//--------------------------------------------------------------------------------------------------
			public int wave_time( int norm , int hard )
			{
				// Original JassCode
				if(  difficulty==EASY || difficulty==NORMAL  )
				{
					return norm;
				}
				else
				{
					return hard;
				}
			}

		//--------------------------------------------------------------------------------------------------
		//  balance_factor
		//--------------------------------------------------------------------------------------------------
			public double balance_factor( double norm , double hard )
			{
				// Original JassCode
				if(  difficulty==EASY || difficulty==NORMAL  )
				{
					return norm;
				}
				else
				{
					return hard;
				}
			}

		//--------------------------------------------------------------------------------------------------
		//  balance_info
		//--------------------------------------------------------------------------------------------------
			public void balance_info(  )
			{
				// Original JassCode
				// bigger is harder
				food_factor[0] = balance_factor( 0.40, 0.50 );
				food_factor[1] = balance_factor( 0.45, 0.55 );
				food_factor[2] = balance_factor( 0.50, 0.60 );
				food_factor[3] = balance_factor( 0.50, 0.60 );
				food_factor[4] = balance_factor( 0.50, 0.60 );
				dragon_factor = balance_factor( 1.00, 1.50 );
				// these should not happen unless the user somehow kills Illidan right away
				//
				Illidan0[0] = wave_time( M1, M1 );
				Illidan0[1] = wave_time( M1, M1 );
				Illidan0[2] = wave_time( M1, M1 );
				Illidan0[3] = wave_time( M1, M1 );
				Illidan0[4] = wave_time( M1, M1 );
				// these are some of the most common cases
				//
				Illidan1[0] = wave_time( M3, M3 );
				Illidan1[1] = wave_time( M3, M3 );
				Illidan1[2] = wave_time( M2, M2 );
				Illidan1[3] = wave_time( M2, M2 );
				Illidan2[0] = wave_time( M4, M4 );
				Illidan2[1] = wave_time( M4, M4 );
				Illidan2[2] = wave_time( M3, M2 );
				Illidan2[3] = NOT_POSSIBLE;
				// Illidan is near to winning
				//
				Illidan3[0] = wave_time( M5, M5 );
				Illidan3[1] = wave_time( M5, M5 );
				Illidan3[2] = NOT_POSSIBLE;
				Illidan3[3] = NOT_POSSIBLE;
				//                    from  to  norm hard 
				//                    COPs COPs  qty  qty unit
				//                    ---- ---- ---- ---- --------------------
				// special: based on wyrms
				set_attack_group( 0, 3, 0, 0, BUCKET_COUATL );
				//------------------------------------------------------------
				set_attack_group( 0, 3, 1, 1, BUCKET_SNAP_DRAGON );
				set_attack_group( 0, 1, 2, 2, BUCKET_REAVER );
				set_attack_group( 2, 3, 1, 1, BUCKET_MYRMIDON );
				set_attack_group( 2, 3, 0, 1, BUCKET_ROYAL );
				set_attack_group( 0, 3, 1, 1, BUCKET_SIREN );
				set_attack_group( 0, 3, 1, 1, BUCKET_SNAP_DRAGON );
				set_attack_group( 0, 1, 2, 2, BUCKET_REAVER );
				set_attack_group( 2, 3, 1, 1, BUCKET_MYRMIDON );
				set_attack_group( 3, 3, 1, 0, BUCKET_ROYAL );
				set_attack_group( 0, 3, 2, 2, BUCKET_SNAP_DRAGON );
				set_attack_group( 0, 3, 2, 2, BUCKET_SIREN );
				set_attack_group( 0, 3, 2, 2, BUCKET_TURTLE );
				set_attack_group( 0, 3, 2, 2, BUCKET_MYRMIDON );
				set_attack_group( 0, 3, 3, 3, BUCKET_SNAP_DRAGON );
				//------------------------------------------------------------
				set_attack_group( 0, 3, 99, 99, BUCKET_MYRMIDON );
			}

		//--------------------------------------------------------------------------------------------------
		//  unit_info
		//--------------------------------------------------------------------------------------------------
			public void unit_info(  )
			{
				// Original JassCode
				bucket_unitid[ BUCKET_COUATL ] = NAGA_COUATL2;
				bucket_unitid[ BUCKET_SNAP_DRAGON ] = NAGA_SNAP_DRAGON2;
				bucket_unitid[ BUCKET_REAVER ] = NAGA_REAVER2;
				bucket_unitid[ BUCKET_MYRMIDON ] = NAGA_MYRMIDON2;
				bucket_unitid[ BUCKET_SIREN ] = NAGA_SIREN2;
				bucket_unitid[ BUCKET_TURTLE ] = NAGA_TURTLE2;
				bucket_unitid[ BUCKET_ROYAL ] = NAGA_ROYAL2;
			}

		//--------------------------------------------------------------------------------------------------
		//  slots_controlled
		//--------------------------------------------------------------------------------------------------
			public int slots_controlled( int who )
			{
				// Original JassCode
				int sum = 0;
				int i = 1;
				while( true )
				{
					if(  slot_control[i] == who  )
					{
						sum = sum + 1;
					}
					i = i + 1;
					if(  i > 4 )
						break;
				}
				return sum;
			}

		//--------------------------------------------------------------------------------------------------
		//  message_loop
		//--------------------------------------------------------------------------------------------------
			public void message_loop(  )
			{
				// Original JassCode
				int cmd;
				int data;
				int i = 1;
				while( true )
				{
					town_x[i] = 0;
					town_y[i] = 0;
					peons_lost[i] = false;
					slot_owned[i] = true;
					slot_control[i] = NO_CONTROL;
					i = i + 1;
					if(  i > 4 )
						break;
				}
				while( true )
				{
					while( true )
					{
						if(  CommandsWaiting() > 0 )
							break;
						Sleep(0.1);
					}
					cmd = GetLastCommand();
					data = GetLastData();
					PopLastCommand();
					startup = startup + 1;
					TraceIII("COMMAND[{0}] ({1},{2})\n",startup,cmd,data);
					//------------------------------------------------------------------------------------------
					if(  cmd == SET_TOWN_1_X  )
					{
						//------------------------------------------------------------------------------------------
						town_x[1] = data;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SET_TOWN_1_Y  )
					{
						//------------------------------------------------------------------------------------------
						town_y[1] = data;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SET_TOWN_2_X  )
					{
						//------------------------------------------------------------------------------------------
						town_x[2] = data;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SET_TOWN_2_Y  )
					{
						//------------------------------------------------------------------------------------------
						town_y[2] = data;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SET_TOWN_3_X  )
					{
						//------------------------------------------------------------------------------------------
						town_x[3] = data;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SET_TOWN_3_Y  )
					{
						//------------------------------------------------------------------------------------------
						town_y[3] = data;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SET_TOWN_4_X  )
					{
						//------------------------------------------------------------------------------------------
						town_x[4] = data;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SET_TOWN_4_Y  )
					{
						//------------------------------------------------------------------------------------------
						town_y[4] = data;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == PEONS_LOST  )
					{
						//------------------------------------------------------------------------------------------
						TraceI("PEONS_LOST {0}\n",data);
						peons_lost[data] = true;
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SLOT_LOST  )
					{
						//------------------------------------------------------------------------------------------
						TraceI("SLOT_LOST {0}\n",data);
						slot_owned[data] = false;
						if(  next_slot == -1  )
						{
							next_slot = data;
						}
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SLOT_GAINED  )
					{
						//------------------------------------------------------------------------------------------
						TraceI("SLOT_GAINED {0}\n",data);
						slot_owned[data] = true;
						peons_lost[data] = false;
						stepped_on = false;
						if(  startup > STARTUP_CMDS  )
						{
							slot_control[data] = ILLIDAN_CONTROL;
						}
						//------------------------------------------------------------------------------------------
					}
					else if(  cmd == SLOT_STEPPED_ON  )
					{
						//------------------------------------------------------------------------------------------
						Trace("SLOT_STEPPED_ON\n");
						stepped_on = true;
					}
					// possibly speed up next wave when Arthas takes one back
					//
					if(  (cmd==PEONS_LOST || cmd==SLOT_LOST) && (startup > STARTUP_CMDS)  )
					{
						slot_control[data] = ARTHAS_CONTROL;
						if(  slots_controlled(ARTHAS_CONTROL) >= 2  )
						{
							Trace("ATTACK NOW\n");
							sleep_seconds = 1;
						}
					}
					TraceI("Arthas has {0} slots\n",slots_controlled(ARTHAS_CONTROL));
					TraceI("Illidan has {0} slots\n",slots_controlled(ILLIDAN_CONTROL));
				}
			}

		//--------------------------------------------------------------------------------------------------
		//  get_start_commands
		//--------------------------------------------------------------------------------------------------
			public void get_start_commands(  )
			{
				// Original JassCode
				while( true )
				{
					if(  startup >= STARTUP_CMDS )
						break;
					Sleep(1);
				}
				Trace("all startup commands received\n");
			}

		//--------------------------------------------------------------------------------------------------
		//  pick_next_slot
		//--------------------------------------------------------------------------------------------------
			public void pick_next_slot(  )
			{
				// Original JassCode
				int count = 0;
				int[] needed = new int[4];
				int i;
				int debug1;
				i = 1;
				while( true )
				{
					if(  slot_owned[i]  )
					{
						debug1 = 10;
					}
					else
					{
						debug1 = 0;
					}
					if(  peons_lost[i]  )
					{
						debug1 = debug1 + 1;
					}
					TraceIII("slot[{0}] owned/lost={1} control={2}\n",i,debug1,slot_control[i]);
					i = i + 1;
					if(  i > 4 )
						break;
				}
				// recover any COP I control but lost the guardian town
				//
				i = 1;
				while( true )
				{
					if(  slot_owned[i] && peons_lost[i]  )
					{
						count = count + 1;
						needed[count] = i;
					}
					i = i + 1;
					if(  i > 4 )
						break;
				}
				if(  count != 0  )
				{
					next_slot = needed[GetRandomInt(1,count)];
					action_state = PEONS_LOST;
					return;
				}
				// gain the north COP before all others
				//
				if(  ! slot_owned[1]  )
				{
					next_slot = 1;
					action_state = SLOT_LOST;
					return;
				}
				// gain a COP that's currently considered neutral
				//
				i = 2;
				while( true )
				{
					if(  (! slot_owned[i]) && (slot_control[i] == NO_CONTROL)  )
					{
						count = count + 1;
						needed[count] = i;
					}
					i = i + 1;
					if(  i > 4 )
						break;
				}
				if(  count != 0  )
				{
					next_slot = needed[GetRandomInt(1,count)];
					action_state = SLOT_LOST;
					return;
				}
				// no neutral COPs left, so take one from Arthas
				//
				i = 2;
				while( true )
				{
					if(  ! slot_owned[i]  )
					{
						count = count + 1;
						needed[count] = i;
					}
					i = i + 1;
					if(  i > 4 )
						break;
				}
				if(  count == 0  )
				{
					// Illidan won!
					SleepForever();
				}
				next_slot = needed[GetRandomInt(1,count)];
				action_state = SLOT_LOST;
			}

		//--------------------------------------------------------------------------------------------------
		//  Illidan_count
		//--------------------------------------------------------------------------------------------------
			public int Illidan_count( bool onlyDone )
			{
				// Original JassCode
				if(  GetUnitCountDone(ILLIDAN) > 0 || GetUnitCountDone(ILLIDAN_DEMON) > 0  )
				{
					return 1;
				}
				if(  onlyDone  )
				{
					return 0;
				}
				if(  GetUnitCount(ILLIDAN) > 0 || GetUnitCount(ILLIDAN_DEMON) > 0  )
				{
					return 1;
				}
				return 0;
			}

		//--------------------------------------------------------------------------------------------------
		//  attack_move
		//--------------------------------------------------------------------------------------------------
			public void attack_move( int dx , int dy )
			{
				// Original JassCode
				if(  CaptainIsEmpty() || Illidan_count(true) < 1  )
				{
					if(  CaptainIsEmpty()  )
					{
						TraceI("SKIP attack_move [empty=1, Illidan={0}]\n",Illidan_count(true));
					}
					else
					{
						TraceI("SKIP attack_move [empty=0, Illidan={0}]\n",Illidan_count(true));
					}
					return;
				}
				TraceII("attack_move( {0}, {1} )\n",dx,dy);
				AttackMoveXY( town_x[next_slot]+dx, town_y[next_slot]+dy );
				Sleep(5);
				SleepUntilAtGoal();
				SleepInCombat();
				RemoveInjuries();
			}

		//--------------------------------------------------------------------------------------------------
		//  set_attack_rate
		//--------------------------------------------------------------------------------------------------
			public void set_attack_rate(  )
			{
				// Original JassCode
				int Islots = slots_controlled(ILLIDAN_CONTROL);
				int Aslots = slots_controlled(ARTHAS_CONTROL);
				if(  Aslots >= 0 && Aslots <= 3  )
				{
					if(  Islots == 0  )
					{
						attack_delay = Illidan0[Aslots];
					}
					else if(  Islots == 1  )
					{
						attack_delay = Illidan1[Aslots];
					}
					else if(  Islots == 2  )
					{
						attack_delay = Illidan2[Aslots];
					}
					else if(  Islots == 3  )
					{
						attack_delay = Illidan3[Aslots];
					}
					else
					{
						attack_delay = NOT_POSSIBLE;
					}
				}
				else
				{
					attack_delay = NOT_POSSIBLE;
				}
				TraceI("set_attack_rate = {0} seconds\n",attack_delay);
			}

		//--------------------------------------------------------------------------------------------------
		//  Illidan_attack
		//--------------------------------------------------------------------------------------------------
			public void Illidan_attack( bool normal_attack )
			{
				// Original JassCode
				int last_x;
				int last_y;
				int dx;
				int dy;
				int i;
				if(  normal_attack  )
				{
					TraceI("Illidan_attack: normal attack (in {0} seconds)\n",attack_delay);
					sleep_seconds = attack_delay;
				}
				else
				{
					Trace("Illidan_attack: QUICK attack\n");
					sleep_seconds = 1;
				}
				Trace("Illidan_attack: wait for Illidan\n");
				while( true )
				{
					if(  Illidan_count(true) > 0 )
						break;
					SuicideSleep(1);
				}
				Trace("Illidan_attack: Illidan ready to attack, form group\n");
				while( true )
				{
					FormGroup(1,false);
					if(  sleep_seconds < 1 )
						break;
					TraceI("Illidan_attack: exit in {0} seconds\n",sleep_seconds);
					SuicideSleep(5);
					if(  Illidan_count(true) < 1  )
					{
						TraceI("Illidan dead, suspend Illidan_attack for {0} seconds\n",sleep_seconds);
						Sleep(sleep_seconds);
						return;
					}
				}
				if(  first_attack  )
				{
					first_attack = false;
				}
				else
				{
					pick_next_slot();
					TraceI("Illidan_attack: next_slot = {0}\n",next_slot);
				}
				last_x = town_x[next_slot];
				last_y = town_y[next_slot];
				SetCaptainHome(ATTACK_CAPTAIN,last_x,last_y);
				attack_move(0,0);
				attack_move(+OFFSET_X,+OFFSET_Y);
				attack_move(+OFFSET_X,-OFFSET_Y);
				attack_move(-OFFSET_X,-OFFSET_Y);
				attack_move(-OFFSET_X,+OFFSET_Y);
				attack_move(+OFFSET_X,+OFFSET_Y);
				attack_move(0,0);
				if(  next_slot == 1  )
				{
					dx = 0;
					dy = +1000;
				}
				else if(  next_slot == 2  )
				{
					dx = +1000;
					dy = 0;
				}
				else if(  next_slot == 3  )
				{
					dx = 0;
					dy = -1000;
					// next_slot == 4
				}
				else
				{
					dx = -1000;
					dy = 0;
				}
				Trace("prepare for last move\n");
				rebuild_Illidan = false;
				Sleep(20);
				rebuild_Illidan = true;
				Trace("wait for Illidan to finish COP\n");
				while( true )
				{
					if(  ! stepped_on )
						break;
					Sleep(1);
				}
			}

		//--------------------------------------------------------------------------------------------------
		//  send_captain_home
		//--------------------------------------------------------------------------------------------------
			public void send_captain_home(  )
			{
				// Original JassCode
				SetCaptainHome(ATTACK_CAPTAIN,ILLIDAN_TOWN_X,ILLIDAN_TOWN_Y);
				TeleportCaptain(ILLIDAN_TOWN_X,ILLIDAN_TOWN_Y);
				ClearCaptainTargets();
			}

		//--------------------------------------------------------------------------------------------------
		//  let_Illidan_die
		//--------------------------------------------------------------------------------------------------
			public void let_Illidan_die(  )
			{
				// Original JassCode
				int timeout = 0;
				int force_size;
				int temp;
				Trace("start let_Illidan_die\n");
				while( true )
				{
					// wait for him to revive
					if(  Illidan_count(true) > 0 )
						break;
					Sleep(1);
				}
				Trace("let_Illidan_die: revived\n");
				while( true )
				{
					// wait for him to die
					if(  Illidan_count(true) < 1 )
						break;
					Sleep(1);
				}
				Trace("let_Illidan_die: Illidan dead, suicide what's left\n");
				suicide_mode = true;
				stepped_on = false;
				// stop reinforcing his group
				InitAssaultGroup();
				while( true )
				{
					if(  SuicidePlayer(USER,false) )
						break;
					Sleep(1);
				}
				Trace("let_Illidan_die: target picked, waiting to enter combat\n");
				while( true )
				{
					if(  CaptainIsEmpty() || CaptainInCombat(true) )
						break;
					Sleep(1);
					if(  timeout - (timeout/5)*5 == 0  )
					{
						TraceI("timeout in {0} seconds\n",M1-timeout);
					}
					timeout = timeout + 1;
					if(  timeout >= M1 )
						break;
				}
				Trace("let_Illidan_die: waiting to die\n");
				force_size = CaptainGroupSize();
				timeout = 0;
				while( true )
				{
					// wait for all to die
					if(  CaptainIsEmpty() )
						break;
					Sleep(1);
					if(  timeout - (timeout/5)*5 == 0  )
					{
						TraceI("timeout in {0} seconds\n",M1-timeout);
					}
					temp = CaptainGroupSize();
					if(  force_size != temp  )
					{
						force_size = temp;
						timeout = 0;
					}
					else
					{
						timeout = timeout + 1;
						if(  timeout >= M1 )
							break;
					}
				}
				Trace("let_Illidan_die: suicide done, changing force size\n");
				send_captain_home();
				suicide_mode = false;
			}

		//--------------------------------------------------------------------------------------------------
		//  extra_attackers
		//--------------------------------------------------------------------------------------------------
			public void extra_attackers(  )
			{
				// Original JassCode
				int COPs = slots_controlled(ARTHAS_CONTROL);
				double Arthas_factor = food_factor[COPs];
				double player_food = GetPlayerState(USER,PLAYER_STATE_RESOURCE_FOOD_USED) * Arthas_factor;
				double Illidan_food = 0;
				int idx;
				int food_each;
				int count;
				int use;
				int i;
				TraceI("calculating extra attackers versus {0} food\n",R2I(player_food));
				// clear all buckets
				//
				i = 0;
				while( true )
				{
					bucket_qty[i] = 0;
					i = i + 1;
					if(  i >= BUCKET_END )
						break;
				}
				// fill buckets as desired
				//
				i = 0;
				while( true )
				{
					if(  i >= attack_index )
						break;
					count = attack_qty[i];
					idx = attack_bucket[i];
					food_each = GetFoodUsed(bucket_unitid[idx]);
					if(  (COPs < attack_min_COPs[i]) || (COPs > attack_max_COPs[i])  )
					{
						count = 0;
					}
					if(  count > 0 && food_each > 0  )
					{
						use = R2I( (player_food - Illidan_food) / food_each );
						if(  use > count  )
						{
							use = count;
						}
						else if(  use < 0  )
						{
							use = 0;
						}
					}
					else
					{
						use = count;
					}
					TraceII("add {0} of entry {1}\n",use,i);
					if(  use > 0  )
					{
						bucket_qty[idx] = bucket_qty[idx] + use;
						Illidan_food = Illidan_food + use * food_each;
					}
					if(  use != count )
						break;
					i = i + 1;
				}
				// use buckets by filling attack captain
				//
				i = 0;
				while( true )
				{
					count = bucket_qty[i];
					TraceII("use {0} of bucket {1}\n",count,i);
					if(  count > 0  )
					{
						SetAssaultGroup(count,count,bucket_unitid[i]);
					}
					i = i + 1;
					if(  i >= BUCKET_END )
						break;
				}
			}

		//--------------------------------------------------------------------------------------------------
		//  set_Illidan_force
		//--------------------------------------------------------------------------------------------------
			public void set_Illidan_force(  )
			{
				// Original JassCode
				InitAssaultGroup();
				Trace("set_Illidan_force: clear force, wait for rebuild_Illidan\n");
				while( true )
				{
					if(  rebuild_Illidan )
						break;
					Sleep(1);
				}
				SetAssaultGroup(1,1,ILLIDAN);
				attack_qty[0] = R2I( GetPlayerUnitTypeCount(USER,FROST_WYRM) * dragon_factor );
				TraceI("dragon factor calls for {0} couatls\n",attack_qty[0]);
				extra_attackers();
			}

		//--------------------------------------------------------------------------------------------------
		//  Illidan_force
		//--------------------------------------------------------------------------------------------------
			public void Illidan_force(  )
			{
				// Original JassCode
				while( true )
				{
					Trace("Illidan_force: top of loop\n");
					set_Illidan_force();
					Trace("Illidan_force:  let_Illidan_die\n");
					let_Illidan_die();
				}
			}

		//--------------------------------------------------------------------------------------------------
		//  main
		//--------------------------------------------------------------------------------------------------
			public void main(  )
			{
				// Original JassCode
				CampaignAI(NAGA_CORAL,null);
				unit_info();
				balance_info();
				SetCaptainHome(DEFENSE_CAPTAIN,ILLIDAN_DEF_X,ILLIDAN_DEF_Y);
				DoCampaignFarms(false);
				GroupTimedLife(true);
				SetAmphibious();
				SetReplacements(9,9,9);
				SetPeonsRepair(true);
				SetFormGroupTimeouts(false);
				SetHeroesTakeItems(true);
				SetWoodPeons(3);
				SetGoldPeons(0);
				StartThread( message_loop);
				StartThread( Illidan_force);
				get_start_commands();
				send_captain_home();
				attack_delay = 30;
				Illidan_attack(NORMAL_ATTACK);
				SetBuildUnitEx( 1,1,1, NAGA_TEMPLE );
				SetBuildUnitEx( 1,1,1, NAGA_SLAVE );
				SetBuildUnitEx( 1,1,1, NAGA_ALTAR );
				SetBuildUnitEx( 2,2,2, NAGA_SLAVE );
				SetBuildUnitEx( 1,1,1, NAGA_SPAWNING );
				SetBuildUnitEx( 3,3,3, NAGA_SLAVE );
				SetBuildUnitEx( 1,1,1, NAGA_SHRINE );
				SetBuildUpgrEx( 1,1,1, UPG_SIREN );
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ENSNARE );
				SetBuildUpgrEx( 1,1,1, UPG_NAGA_ABOLISH );
				SetBuildUpgrEx( 2,2,2, UPG_SIREN );
				// only needs 1 of each to "pull" for attack waves
				//
				CampaignDefenderEx( 1,1,1, ILLIDAN );
				CampaignDefenderEx( 1,1,1, NAGA_SNAP_DRAGON );
				CampaignDefenderEx( 1,1,1, NAGA_SIREN );
				CampaignDefenderEx( 1,1,1, NAGA_MYRMIDON );
				CampaignDefenderEx( 1,1,1, NAGA_TURTLE );
				CampaignDefenderEx( 1,1,1, NAGA_COUATL2 );
				CampaignDefenderEx( 1,1,1, NAGA_ROYAL );
				while( true )
				{
					if(  suicide_mode  )
					{
						Trace("main while( true ) - suicide mode\n");
						while( true )
						{
							if(  ! suicide_mode )
								break;
							Sleep(1);
						}
						did_suicide = true;
					}
					else
					{
						Trace("main while( true ) - normal mode\n");
					}
					set_attack_rate();
					Trace("main loop:  set_Illidan_force\n");
					set_Illidan_force();
					// if Illidan is dead
					Trace("main loop:  Illidan_attack\n");
					if(  (action_state == PEONS_LOST) || did_suicide || timeout_failure  )
					{
						did_suicide = false;
						timeout_failure = false;
						Illidan_attack(PEON_ATTACK);
					}
					else
					{
						Illidan_attack(NORMAL_ATTACK);
					}
				}
			}

		} // class u08x02_ai 

	}

