using UnityEngine;
using System;
using System.Collections.Generic;

public class W3MapManager : SingletonMono< W3MapManager >
{
    public class W3MapAlliance
    {
        public Dictionary< int , bool >[] alliance = new Dictionary< int , bool >[ GameDefine.MAX_PLAYER_SLOTS ];
    }

    public class W3MapPlayerTaxRate
    {
        public Dictionary< int , int >[] rate = new Dictionary< int , int >[ GameDefine.MAX_PLAYER_SLOTS ];
    }

    public class W3MapStartLocPrio
    {
        public int index;
        public int Priority;
    }

    public override void initSingletonMono()
    {
        for ( int i = 0 ; i < GameDefine.MAX_PLAYER_SLOTS ; i++ )
        {
            PlayerAlliance[ i ] = new W3MapAlliance();

            for ( int j = 0 ; j < GameDefine.MAX_PLAYER_SLOTS ; j++ )
            {
                PlayerAlliance[ i ].alliance[ j ] = new Dictionary<int , bool>();
            }
        }
    }

    public bool isAlliance( int pid1 , int pid2 )
    {
        Dictionary<int , bool>[] alliance = PlayerAlliance[ pid1 ].alliance;

        for ( int i = 0 ; i < alliance.Length ; i++ )
        {
            if ( alliance[ i ].ContainsKey( pid2 ) )
            {
                return true;
            }
        }

        return false;
    }

    public bool isEnemy( int pid1 , int pid2 )
    {
        if ( pid1 == pid2 )
            return false;

        if ( isAlliance( pid1 , pid2 ) )
            return false;

        return true;
    }

    public string mapFile = "";

    public string mapName = "";
    public string mapDescription = "";

    public int teamCount;
    public int playerCount;

    public int[] playerStartLocation = new int[ GameDefine.MAX_PLAYER_SLOTS ];
    public int[] forcePlayerStartLocation = new int[ GameDefine.MAX_PLAYER_SLOTS ];
    public int[] playerColor = new int[ GameDefine.MAX_PLAYER_SLOTS ];
    public Vector2[] startLocation = new Vector2[ GameDefine.MAX_PLAYER_SLOTS ];
    public W3MapStartLocPrio[][] startLocationPriority = new W3MapStartLocPrio[ GameDefine.MAX_PLAYER_SLOTS ][];
    public int[] playerTeam = new int[ GameDefine.MAX_PLAYER_SLOTS ];
    public W3MapAlliance[] PlayerAlliance = new W3MapAlliance[ GameDefine.MAX_PLAYER_SLOTS ];
    public W3MapPlayerTaxRate[] playerTaxRate = new W3MapPlayerTaxRate[ GameDefine.MAX_PLAYER_SLOTS ];
    public int[] racePreference = new int[ GameDefine.MAX_PLAYER_SLOTS ];
    public bool[] playerRaceSelectable = new bool[ GameDefine.MAX_PLAYER_SLOTS ];
    public int[] playerController = new int[ GameDefine.MAX_PLAYER_SLOTS ];
    public string[] playerName = new string[ GameDefine.MAX_PLAYER_SLOTS ];
    public bool[] playerOnScoreScreen = new bool[ GameDefine.MAX_PLAYER_SLOTS ];
    public bool[] playerSelectable = new bool[ GameDefine.MAX_PLAYER_SLOTS ];
    public int[] playerSlotState = new int[ GameDefine.MAX_PLAYER_SLOTS ];

    public int gameTypeSelected = 0;
    public int gameTypeSupported = 0;
    public int mapFlag = 0;
    public int gamePlacement = 0;
    public int gameSpeed = 0;
    public int gameDifficulty = 0;
    public int resourceDensity = 0;
    public int creatureDensity = 0;

    public Rect worldBounds = new Rect();

    public bool isUnitInRegion( BJRegion r , int uid )
    {
        return false;
    }

    public bool isPointInRegion( BJRegion r , float x , float y )
    {
        return false;
    }

}

