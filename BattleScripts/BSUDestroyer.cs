using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSUDestroyer  : MonoBehaviour
{
    public static BSUDestroyer instance;

    public GameObject BSU;

    public bool keepDisabled0;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameMenu.instance.hasUnlockedSneakers == 1)
        {
            Destroy(BSU);
        }
        
        
    }

}

