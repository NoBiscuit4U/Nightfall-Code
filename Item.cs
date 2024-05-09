using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item:MonoBehaviour{
    
    public static Item instance;
    
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;
    public bool isArtifact;
    public bool isGold;
    
    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Affects")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStr, affectDef;
    public bool doesEffectFire, doesEffectPosion, doesEffectIce;
    public bool doesAddFire, doesAddPosion, doesAddIce, isEEasterEgg, isGEasterEgg;
    public bool is16, is26, is36, is66;
    
    [Header("Rarity")]
    public bool isLegendary, isEpic, isRare, isUncommon, isCommon;
    
    public int weaponStrength;

    public int armorStrength;

    public Sprite equippedWpnImage, equippedArmrImage;

    public int charToUseOn;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int charToUseOn)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        if(isItem)
        {
            if(affectHP)
            {
                selectedChar.currentHP += amountToChange;

                if(selectedChar.currentHP > selectedChar.maxHP)
                {
                    selectedChar.currentHP = selectedChar.maxHP;
                }
            }
        
            if(affectMP)
            {
                selectedChar.currentMP += amountToChange;

                if(selectedChar.currentMP > selectedChar.maxMP)
                {
                    selectedChar.currentMP = selectedChar.maxMP;
                }
            }
       
            if(affectStr)
            {
                selectedChar.strength += amountToChange;
            }
            
            if(isWeapon)
            {
                if(selectedChar.equippedWpn != "")
                {
                    GameManager.instance.AddItem(selectedChar.equippedWpn);
                }
            
                selectedChar.equippedWpn = itemName;
                selectedChar.wpnPwr = weaponStrength;
                selectedChar.equippedWeaponImage = equippedWpnImage;
            }

            if(isArtifact)
            {
                if(doesAddFire)
                {
                    ArtifactFireOpen();
                }

                if(doesAddPosion)
                {
                    ArtifactPosionOpen();
                }

                if(doesAddIce)
                {
                    ArtifactIceOpen();
                }

                if(isEEasterEgg)
                {
                    EEasterEggOpen();
                }

                if(isGEasterEgg)
                {
                    GEasterEggOpen();
                }
            }
            
        
            if(isArmor)
            {
                if(selectedChar.equippedArmr != "")
                {
                    GameManager.instance.AddItem(selectedChar.equippedArmr);
                }
            
                selectedChar.equippedArmr = itemName;
                selectedChar.armrPwr = armorStrength;
                selectedChar.equippedArmorImage = equippedArmrImage;
            }
            
            if(isGold)
            {
                GameManager.instance.currentGold += amountToChange;
            }
        }
    
        GameManager.instance.RemoveItem(itemName);
        GameMenu.instance.CloseitemCharChoice();
    }


    public void BattleItemUse(int battleCharToUseOn)
    {

        BattleChar selectedBattleChar = BattleManager.instance.activeBattlers[battleCharToUseOn];
        

        if(affectHP)
        {

            selectedBattleChar.currentHP += amountToChange;
            
            if(selectedBattleChar.currentHP > selectedBattleChar.maxHP)
            {

                selectedBattleChar.currentHP = selectedBattleChar.maxHP;

            }

        }

        if(affectMP)
        {

            selectedBattleChar.currentMP += amountToChange;

            if(selectedBattleChar.currentMP > selectedBattleChar.maxMP)
            {

                selectedBattleChar.currentMP = selectedBattleChar.maxMP;

            }

        }
      
        GameManager.instance.RemoveItem(itemName);
    }


    public void ArtifactFireOpen()
    {
        if(Random.Range(1,100) == 1)
        {
            //GameManager.instance.AddItem("Health Potion");
        }else
        {
           if(Random.Range(1,100) >= 10)
           {
               //GameManager.instance.AddItem("Health Potion");
           }else
           {
               if(Random.Range(1,100) >= 15)
               {
                    //GameManager.instance.AddItem("Health Potion");
               }else
               {
                   if(Random.Range(1,100) >= 100)
                   {
                       //GameManager.instance.AddItem("Health Potion");
                   }
               }
           }
        }
    }

    public void ArtifactPosionOpen()
    {
        if(Random.Range(1,100) == 1)
        {

        }else
        {
           if(Random.Range(1,100) >= 10)
           {

           }else
           {
               if(Random.Range(1,100) >= 15)
               {

               }else
               {
                   if(Random.Range(1,100) >= 100)
                   {
                       
                   }
               }
           }
        }
    }

    public void ArtifactIceOpen()
    {
        if(Random.Range(1,100) == 1)
        {

        }else
        {
           if(Random.Range(1,100) >= 10)
           {

           }else
           {
               if(Random.Range(1,100) >= 15)
               {

               }else
               {
                   if(Random.Range(1,100) >= 100)
                   {
                       
                   }
               }
           }
        }
    }

    public void EEasterEggOpen()
    {
        if(Random.Range(1,100) == 1)
        {
            GameManager.instance.AddItem("The Omelette Maker");
        }else
        {
           if(Random.Range(1,100) <= 10)
           {
               GameManager.instance.AddItem("Eggcalibur");
           }else
           {
               if(Random.Range(1,100) <= 15)
               {
                    GameManager.instance.AddItem("Egg Hat");
               }else
               {
                   if(Random.Range(1,100) <= 100)
                   {
                       GameManager.instance.AddItem("Chocolate Egg");
                   }
               }
           }
        }
    }

    public void GEasterEggOpen()
    {
        if(Random.Range(1,100) == 10)
        {
             GameManager.instance.AddItem("The Omelette Maker");
        }else
        {
           if(Random.Range(1,100) <= 100)
           {
               GameManager.instance.AddItem("Chocolate Coin");
           }else
           {
              
           }
        }
    }
    
   /*public void UnequipArmor1()
   {
       CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];
       
       if(selectedChar.equippedArmr != "")
       {
             GameManager.instance.AddItem(selectedChar.equippedArmr);
       }
            
        selectedChar.equippedArmr = "None";
        selectedChar.armrPwr = 0;
        selectedChar.equippedArmorImage = GameMenu.instance.nullImage;
   }

   public void UnequipWeapon1()
    {
       CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];
       
       if(selectedChar.equippedWpn != "")
        {
            GameManager.instance.AddItem(selectedChar.equippedWpn);
        }
            
            selectedChar.equippedWpn = "None";
            selectedChar.wpnPwr = 0;
            selectedChar.equippedWeaponImage = GameMenu.instance.nullImage;
    }*/
}