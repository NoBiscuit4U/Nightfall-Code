using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamAchivementsExample : MonoBehaviour
{
    public static SteamAchivementsExample instance;

    public int cBA = 0;

    public int goldAchieveOne = 0;
    
    void Update()
    {
      if(!SteamManager.Initialized)
      {
         return;
      }

      if(Input.GetButtonDown("Fire1"))
      {
         SteamUserStats.SetAchievement("Start_The_Game");
         //SteamUserStats.SetAchievement("Easer_Event_Played");
      }

      if(cBA == 1)
      {
         SteamUserStats.SetAchievement("Beat_Cave_Boss");
      }

      if(GameManager.instance.currentGold == 1000)
      {
         goldAchieveOne = 1;
         SteamUserStats.SetAchievement("Money_One");
      }
       
      SteamUserStats.ResetAllStats(false);

      SteamUserStats.StoreStats();
    }

    void Start()
    {
       instance = this;
    }
}
