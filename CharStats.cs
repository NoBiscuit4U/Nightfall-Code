using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour{
    
    public static CharStats instance;
    
    public string charName;
    public int playerLevel = 1;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 150;
    public int baseEXP = 250;
    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;
    public int[] mpLvlBonus;
    public int strength;
    public int defence;
    public int wpnPwr;
    public int armrPwr;
    public int critChance = 5;
    public int evasionChance;
    public string equippedWpn;
    public Sprite equippedWeaponImage;
    public string equippedArmr;
    public Sprite equippedArmorImage;
    public Sprite charImage;
    private int actualXP;
    public int abilityCost;

    void Start()
    {
        
        instance = this;

        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for(int i = 2; i < expToNextLevel.Length; i++)
        {
           expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    void Update()
    {
        if(evasionChance >= 50)
        {
            evasionChance = 50;
        }
        
    }

   public void AddExp(int expToAdd)
   {
       currentEXP += expToAdd;

    if((playerLevel < maxLevel))
    { 
       while(currentEXP > expToNextLevel[playerLevel])
       {
           currentEXP -= expToNextLevel[playerLevel];
           
           playerLevel++;
       
       
           //determine whether to add to str of def based on odd or even
           if(playerLevel%2 == 0)
           {
               strength++;
           }else
           {
               defence++;
           }
       
           maxHP = Mathf.FloorToInt(maxHP * 1.02f);
           currentHP = maxHP;

           maxMP += mpLvlBonus[playerLevel];
           currentMP = maxMP;
       }
    }
       if(playerLevel >= maxLevel)
       {
           currentEXP = 0;
       }
   
   }

}



