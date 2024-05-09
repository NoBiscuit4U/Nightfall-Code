using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakersUnlockTrigger : MonoBehaviour
{
   public Collider2D trigger; 


public void OnTriggerEnter2D(Collider2D other)
{
    if(other.tag == "Player")
       {
         GameMenu.instance.hasUnlockedSneakers = 1;

         Destroy(trigger);
       }
}

}
