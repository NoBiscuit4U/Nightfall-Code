using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InBattleZoneUi : MonoBehaviour
{
    public static InBattleZoneUi instance;

    public GameObject sprite;

    public Image battleDetectorImage;

    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
