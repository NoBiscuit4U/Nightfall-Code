using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinButtons : MonoBehaviour
{
    public static SkinButtons instance;
    
    public int IDNum;
    
    public Sprite skinSprite;

    public bool isSkinUnlocked = false;

    public string skinName;

    void Start()
    {
        instance = this;
    }
}
