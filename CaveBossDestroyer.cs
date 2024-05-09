using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBossDestroyer : MonoBehaviour
{
    public static CaveBossDestroyer instance;
    
    public int hasBeatCB = 0;

    public GameObject cBPrefab;

    public GameObject cBTPrefab;
    
    void Start()
    {
        instance = this;
    }

    void Update()
    {
         if(hasBeatCB == 1)
       {
          Destroy(cBPrefab);
          Destroy(cBTPrefab);
       }
    }
}
