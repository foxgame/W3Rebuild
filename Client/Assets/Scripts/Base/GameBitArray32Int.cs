using System.Runtime;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[ StructLayout( LayoutKind.Sequential , Pack = 1 ) ]
public struct GameBitArray32Int //: IDisposable
{
	int a0;
	int a1;
	int a2;
	int a3;
	int a4;
	int a5;
	int a6;
	int a7;
	int a8;
	int a9;
	int a10;
	int a11;
	int a12;
	int a13;
	int a14;
	int a15;
	int a16;
	int a17;
	int a18;
	int a19;
	int a20;
	int a21;
	int a22;
	int a23;
	int a24;
	int a25;
	int a26;
	int a27;
	int a28;
	int a29;
	int a30;
	int a31;
	
	
	
	
	
	public int this[ int i ]
	{
		get
		{
			switch ( i ) 
			{
			case 0:
				return a0;
			case 1:
				return a1;
			case 2:
				return a2;
			case 3:
				return a3;
			case 4:
				return a4;
			case 5:
				return a5;
			case 6:
				return a6;
			case 7:
				return a7;
			case 8:
				return a8;
			case 9:
				return a9;
			case 10:
				return a10;
			case 11:
				return a11;
			case 12:
				return a12;
			case 13:
				return a13;
			case 14:
				return a14;
			case 15:
				return a15;
			case 16:
				return a16;
			case 17:
				return a17;
			case 18:
				return a18;
			case 19:
				return a19;
			case 20:
				return a20;
			case 21:
				return a21;
			case 22:
				return a22;
			case 23:
				return a23;
			case 24:
				return a24;
			case 25:
				return a25;
			case 26:
				return a26;
			case 27:
				return a27;
			case 28:
				return a28;
			case 29:
				return a29;
			case 30:
				return a30;
			case 31:
				return a31;
				
			default:
				return a0;
			}
		}
		set
		{
			switch ( i )
			{
			case 0:
				a0 = value;
				break;
			case 1:
				a1 = value;
				break;
			case 2:
				a2 = value;
				break;
			case 3:
				a3 = value;
				break;
			case 4:
				a4 = value;
				break;
			case 5:
				a5 = value;
				break;
			case 6:
				a6 = value;
				break;
			case 7:
				a7 = value;
				break;
			case 8:
				a8 = value;
				break;
			case 9:
				a9 = value;
				break;
			case 10:
				a10 = value;
				break;
			case 11:
				a11 = value;
				break;
			case 12:
				a12 = value;
				break;
			case 13:
				a13 = value;
				break;
			case 14:
				a14 = value;
				break;
			case 15:
				a15 = value;
				break;
			case 16:
				a16 = value;
				break;
			case 17:
				a17 = value;
				break;
			case 18:
				a18 = value;
				break;
			case 19:
				a19 = value;
				break;
			case 20:
				a20 = value;
				break;
			case 21:
				a21 = value;
				break;
			case 22:
				a22 = value;
				break;
			case 23:
				a23 = value;
				break;
			case 24:
				a24 = value;
				break;
			case 25:
				a25 = value;
				break;
			case 26:
				a26 = value;
				break;
			case 27:
				a27 = value;
				break;
			case 28:
				a28 = value;
				break;
			case 29:
				a29 = value;
				break;
			case 30:
				a30 = value;
				break;
			case 31:
				a31 = value;
				break;
			default:
				a0 = value;
				break;
			}
		}
	}
	
}

[ StructLayout( LayoutKind.Sequential , Pack = 1 ) ]
public struct GameBitArray32TeamInt //: IDisposable
{
	int a0;
	int a1;
	int a2;
	int a3;
	int a4;

	public int this[ int i ]
	{
		get
		{
			switch ( i ) 
			{
				case 0:
					return a0;
				case 1:
					return a1;
				case 2:
					return a2;
				case 3:
					return a3;
				case 4:
					return a4;
				

				default:
					return a0;
			}
		}
		set
		{
			switch ( i )
			{
				case 0:
					a0 = value;
					break;
				case 1:
					a1 = value;
					break;
				case 2:
					a2 = value;
					break;
				case 3:
					a3 = value;
					break;
				case 4:
					a4 = value;
					break;
				
				default:
					a0 = value;
					break;
			}
		}
	}

}

