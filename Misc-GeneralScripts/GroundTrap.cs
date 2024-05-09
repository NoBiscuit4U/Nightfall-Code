using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrap : MonoBehaviour
{
    public bool hasBeenTriggeredS = false;

    public static GroundTrap instance;

    [Header("GroundTrapStats")]
    public int damageToDealS;

    void Start()
    {
        instance = this;
    }
    
    void Update() 
    {
        if(hasBeenTriggeredS)
        {
            
            hasBeenTriggeredS = false;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.gotHitBySpike = true;
            hasBeenTriggeredS = true;
        }
    }
}
