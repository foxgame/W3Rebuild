using UnityEngine;
using System;
using System.Collections.Generic;


public class W3Quest
{

}

public class W3QuestManager : SingletonMono<W3QuestManager>
{




    public int createQuest()
    {
        return 0;
    }

    public void destroyQuest( int id )
    {
    }

    public void questSetTitle( int id , string title )
    {
    }

    public void questSetDescription( int id , string description )
    {
    }

    public void questSetIconPath( int id , string iconPath )
    {
    }

    public void questSetRequired( int id , bool required )
    {
    }

    public void questSetCompleted( int id , bool completed )
    {
    }

    public void questSetDiscovered( int id , bool discovered )
    {
    }

    public void questSetFailed( int id , bool failed )
    {
    }

    public void questSetEnabled( int id , bool enabled )
    {
    }

    public bool isQuestRequired( int id )
    {
        return false;
    }

    public bool isQuestCompleted( int id )
    {
        return false;
    }

    public bool isQuestDiscovered( int id )
    {
        return false;
    }

    public bool isQuestFailed( int id )
    {
        return false;
    }

    public bool isQuestEnabled( int id )
    {
        return false;
    }

    public int questCreateItem( int id )
    {
        return 0;
    }

    public void questItemSetDescription( int id , string description )
    {
    }

    public void questItemSetCompleted( int id , bool completed )
    {
    }

    public bool isQuestItemCompleted( int id )
    {
        return false;
    }

    public int createDefeatCondition()
    {
        return 0;
    }

    public void destroyDefeatCondition( int id )
    {
    }

    public void DefeatConditionSetDescription( int id , string description )
    {
    }

    public void flashQuestDialogButton()
    {
    }

    public void forceQuestDialogUpdate()
    {
    }





}

