using UnityEngine;
using System;
using System.Collections.Generic;



public class W3Dialog
{
    public int id;
}

public class W3DialogManager : SingletonMono< W3DialogManager >
{
    public int dialogID = 0;
    public List<W3Force> dialogs = new List<W3Force>();

    public int buttonID = 0;


    public int dialogCreate()
    {
        return dialogID;
    }

    public void dialogDestroy( int id )
    {

    }

    public void dialogClear( int id )
    {

    }

    public void dialogSetMessage( int id , string messageText )
    {

    }

    public int dialogAddButton( int id , string buttonText , int hotkey )
    {
        return buttonID;
    }

    public int dialogAddQuitButton( int id , bool doScoreScreen , string buttonText , int hotkey )
    {
        return buttonID;
    }

    public void dialogDisplay( int pid , int did , bool flag )
    {

    }

   

}


