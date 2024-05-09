using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscordLink : MonoBehaviour
{
    
    public GameObject discordLinkWin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLinkWin()
    {
        discordLinkWin.SetActive(true);
    }
 
    public void CloseLinkWin()
    {
        discordLinkWin.SetActive(false);
    }

}


