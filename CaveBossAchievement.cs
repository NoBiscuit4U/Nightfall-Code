using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBossAchievement : MonoBehaviour
{
    public Collider2D trigger;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player")
       {
           SteamAchivementsExample.instance.cBA = 1;
           CaveBossDestroyer.instance.hasBeatCB = 1;
           Destroy(trigger);
       }     
    }
}
