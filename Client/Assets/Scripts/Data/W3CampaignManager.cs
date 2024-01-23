using UnityEngine;
using System;
using System.Collections.Generic;

public class W3CampaignManager : SingletonMono<W3CampaignManager>
{
    public int defaultDifficulty = 0;


    public void setTutorialCleared( bool cleared )
    {

    }

    public void setMissionAvailable( int campaignNumber , int missionNumber , bool available )
    {

    }

    public void setCampaignAvailable( int campaignNumber , bool available )
    {

    }

    public void setOpCinematicAvailable( int campaignNumber , bool available )
    {

    }

    public void setEdCinematicAvailable( int campaignNumber , bool available )
    {

    }

    public void setCustomCampaignButtonVisible( int whichButton , bool visible )
    {

    }
    
    public bool getCustomCampaignButtonVisible( int whichButton )
    {
        return false;
    }

    public void doNotSaveReplay()
    {
    }



}


