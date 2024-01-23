using UnityEngine;
using System;
using System.Collections.Generic;


public class W3Leaderboard
{
}

public class W3LeaderboardManager : SingletonMono< W3LeaderboardManager >
{

    public int createLeaderboard()
    {
        return 0;
    }

    public void destroyLeaderboard( int id )
    {
    }

    public void leaderboardDisplay( int id , bool show )
    {
    }

    public bool isLeaderboardDisplayed( int id )
    {
        return false;
    }

    public int leaderboardGetItemCount( int id )
    {
        return 0;
    }

    public void leaderboardSetSizeByItemCount( int id , int count )
    {
    }

    public void leaderboardAddItem( int id , string label , int value , int pid )
    {
    }

    public void leaderboardRemoveItem( int id , int index )
    {
    }

    public void leaderboardRemovePlayerItem( int id , int pid )
    {
    }

    public void leaderboardClear( int id )
    {
    }

    public void leaderboardSortItemsByValue( int id , bool ascending )
    {
    }

    public void leaderboardSortItemsByPlayer( int id , bool ascending )
    {
    }

    public void leaderboardSortItemsByLabel( int id , bool ascending )
    {
    }

    public bool leaderboardHasPlayerItem( int id , int pid )
    {
        return false;
    }

    public int leaderboardGetPlayerIndex( int id , int pid )
    {
        return 0;
    }

    public void leaderboardSetLabel( int id , string label )
    {
    }

    public string leaderboardGetLabelText( int id )
    {
        return "";
    }

    public void playerSetLeaderboard( int pid , int id )
    {
    }

    public int playerGetLeaderboard( int pid )
    {
        return 0;
    }

    public void leaderboardSetLabelColor( int id , int red , int green , int blue , int alpha )
    {
    }

    public void leaderboardSetValueColor( int id , int red , int green , int blue , int alpha )
    {
    }

    public void leaderboardSetStyle( int id , bool showLabel , bool showNames , bool showValues , bool showIcons )
    {
    }

    public void leaderboardSetItemValue( int id , int whichItem , int val )
    {
    }

    public void leaderboardSetItemLabel( int id , int whichItem , string val )
    {
    }

    public void leaderboardSetItemStyle( int id , int whichItem , bool showLabel , bool showValue , bool showIcon )
    {
    }

    public void leaderboardSetItemLabelColor( int id , int whichItem , int red , int green , int blue , int alpha )
    {
    }

    public void leaderboardSetItemValueColor( int id , int whichItem , int red , int green , int blue , int alpha )
    {
    }






    public int createMultiboard()
    {
        return 0;
    }

    public void destroyMultiboard( int id )
    {
    }

    public void multiboardDisplay( int id , bool show )
    {
    }

    public bool isMultiboardDisplayed( int id )
    {
        return false;
    }

    public void multiboardMinimize( int id , bool minimize )
    {
    }

    public bool isMultiboardMinimized( int id )
    {
        return false;
    }

    public void multiboardClear( int id )
    {
    }

    public void multiboardSetTitleText( int id , string label )
    {
    }

    public string multiboardGetTitleText( int id )
    {
        return "";
    }

    public void multiboardSetTitleTextColor( int id , int red , int green , int blue , int alpha )
    {
    }

    public int multiboardGetRowCount( int id )
    {
        return 0;
    }

    public int multiboardGetColumnCount( int id )
    {
        return 0;
    }

    public void multiboardSetColumnCount( int id , int count )
    {
    }

    public void multiboardSetRowCount( int id , int count )
    {
    }

    public void multiboardSetItemsStyle( int id , bool showValues , bool showIcons )
    {
    }

    public void multiboardSetItemsValue( int id , string value )
    {
    }

    public void multiboardSetItemsValueColor( int id , int red , int green , int blue , int alpha )
    {
    }

    public void multiboardSetItemsWidth( int id , float width )
    {
    }

    public void multiboardSetItemsIcon( int id , string iconPath )
    {
    }

    public int multiboardGetItem( int id , int row , int column )
    {
        return 0;
    }

    public void multiboardReleaseItem( int id )
    {
    }

    public void multiboardSetItemStyle( int id , bool showValue , bool showIcon )
    {
    }

    public void multiboardSetItemValue( int id , string val )
    {
    }

    public void multiboardSetItemValueColor( int id , int red , int green , int blue , int alpha )
    {
    }

    public void MultiboardSetItemWidth( int id , float width )
    {
    }

    public void multiboardSetItemIcon( int id , string iconFileName )
    {
    }

    public void multiboardSuppressDisplay( bool flag )
    {
    }






}

