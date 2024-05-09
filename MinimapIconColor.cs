using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MinimapIconColor : MonoBehaviour
{
    public bool isExit, isPlayer, isPlayerSample, isExitSample;

    SpriteRenderer sprite;

    public Image image;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if(isExit)
        {
            float mR;
            float mG;
            float mB;
            mR = PlayerPrefs.GetFloat("ExitIconRed");
            mG = PlayerPrefs.GetFloat("ExitIconGreen");
            mB = PlayerPrefs.GetFloat("ExitIconBlue");
            sprite.color = new Color(mR, mG, mB);
        }
        
        if(isPlayer)
        {
            float pR;
            float pG;
            float pB;
            pR = PlayerPrefs.GetFloat("PlayerIconRed");
            pG =  PlayerPrefs.GetFloat("PlayerIconGreen");
            pB = PlayerPrefs.GetFloat("PlayerIconBlue");
            sprite.color = new Color(pR, pG, pB);
        }

        if(isExitSample)
        {
            float mR;
            float mG;
            float mB;
            mR = PlayerPrefs.GetFloat("ExitIconRed");
            mG = PlayerPrefs.GetFloat("ExitIconGreen");
            mB = PlayerPrefs.GetFloat("ExitIconBlue");
            image.color = new Color(mR, mG, mB);
        }
        
        if(isPlayerSample)
        {
            float pR;
            float pG;
            float pB;
            pR = PlayerPrefs.GetFloat("PlayerIconRed");
            pG =  PlayerPrefs.GetFloat("PlayerIconGreen");
            pB = PlayerPrefs.GetFloat("PlayerIconBlue");
            image.color = new Color(pR, pG, pB);
        }

    }
}
