using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager:MonoBehaviour{
    
    public static GameManager instance;

    public CharStats[] playerStats;

    public int cutSceneID;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive, battleActive;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;
    
    public int currentGold;

    public int rememberCutSceneID;

    public bool isEmpty;

    public string alreadyEquippedWpn;

    public Image nullImage;

    public GameObject miniMap2;

    public bool gotHitByProj = false;
    public bool gotHitBySpike = false;

    public int[] skinIDNums;

    public bool removeEvasion = false;
    
    // Start is called before the first frame update
    void Start(){
        instance = this;

        DontDestroyOnLoad(gameObject);
    
        miniMap2 = GameMenu.instance.miniMap;
    SortItems();
    }

    // Update is called once per frame
    void Update(){
       if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive || battleActive) 
       {
           PlayerController.instance.canMove = false;
           miniMap2.SetActive(false);
       }else
       {
           PlayerController.instance.canMove = true;
           miniMap2.SetActive(true);
       }
       
       if(gotHitByProj)
       {
           for(int i = 0; i < playerStats.Length; i++)
           {
               playerStats[i].currentHP -= TrapProjectile.instance.damageToDealP;
               gotHitByProj = false;
           }
       }

       if(gotHitByProj)
       {
           for(int i = 0; i < playerStats.Length; i++)
           {
               playerStats[i].currentHP -= GroundTrap.instance.damageToDealS;
               gotHitBySpike= false;
           }
       }

       for(int i = 0; i < playerStats.Length; i++)
       {
           if(playerStats[i].currentHP <= 0)
           {
               playerStats[i].currentHP = 0;
           }

           if(playerStats[i].currentHP == 0)
           {
                PlayerController.instance.dead = true;
           }
       }
       
       miniMap2 = GameMenu.instance.miniMap;
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }








        return null;
    }


    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
          itemAfterSpace = false;
          for(int i = 0; i < itemsHeld.Length - 1; i++)
          {
            if(itemsHeld[i] == "")
            {
                itemsHeld[i] = itemsHeld[i + 1];
                itemsHeld[i + 1] = "";

                numberOfItems[i] = numberOfItems[i + 1];
                numberOfItems[i + 1] = 0;
            
                if(itemsHeld[i] != "")
                {
                    itemAfterSpace = true;
                }
             }
          }
       }
    }


    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }
    
        if(foundSpace)
        {
            bool itemExists = false;
            for(int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;

                    i = referenceItems.Length;
                }
            }
        
            if(itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }else
            {
                Debug.LogError(itemToAdd + " ITEM not found");
            }
        }
    
        GameMenu.instance.ShowItems();
    }
    
    public void RemoveItem(string itemToRemove)
    {
         bool foundItem = false;
         int ItemPosition = 0;

         for(int i = 0; i < itemsHeld.Length; i++)
         {
             if(itemsHeld[i] == itemToRemove)
             {
                 foundItem = true;
                 ItemPosition = i;

                 i = itemsHeld.Length;
             }
         }
       
         if(foundItem)
         {
            numberOfItems[ItemPosition]--;

            if(numberOfItems[ItemPosition] <= 0)
            {
                itemsHeld[ItemPosition] = "";
                GameMenu.instance.DefaultToNoItemSelected();
            }
         
            GameMenu.instance.ShowItems();
         }else
         {
            GameMenu.instance.DefaultToNoItemSelected();
         }
    
    }

    public void SaveData()
    {
        //store inventory data
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
            
        }
        
        PlayerPrefs.SetString("Current_Scene" , SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);
    
        //save character data
        for(int i = 0; i < playerStats.Length; i ++)
        {
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 1);
            }else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 0);
            }
    
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_Level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_CurrentExp", playerStats[i].currentEXP);
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_CurrentHP", playerStats[i].currentHP);
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_MaxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_CurrentMP", playerStats[i].currentMP);
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_MaxMP", playerStats[i].maxMP);
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_Strength", playerStats[i].strength);
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_Defence", playerStats[i].defence);
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_WpnPwr", playerStats[i].wpnPwr);
            PlayerPrefs.SetInt("Player" + playerStats[i].charName + "_ArmrPwr", playerStats[i].armrPwr);
            PlayerPrefs.SetInt("SneakersBeenUnlocked", GameMenu.instance.hasUnlockedSneakers);
            PlayerPrefs.SetString("Player" + playerStats[i].charName + "_EquippedWpn", playerStats[i].equippedWpn);
            PlayerPrefs.SetString("Player" + playerStats[i].charName + "_EquippedArmr", playerStats[i].equippedArmr);
            PlayerPrefs.SetInt("IsCBDead", SteamAchivementsExample.instance.cBA);
            PlayerPrefs.SetInt("GoldAchieveOne", SteamAchivementsExample.instance.goldAchieveOne);
            PlayerPrefs.SetInt("CurrentGold_", currentGold);
        }
    
    }

    public void LoadData()
    {
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));
    
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }

        for(int i = 0; i < playerStats.Length; i++)
        {
            if(PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }else
            {
                playerStats[i].gameObject.SetActive(true);
            }
            
            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_Level");
            playerStats[i].currentEXP = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_CurrentExp");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_CurrentHP");
            playerStats[i].maxHP = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_MaxHP");
            playerStats[i].currentMP = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_CurrentMP");
            playerStats[i].maxMP = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_MaxMP");
            playerStats[i].strength = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_Strength");
            playerStats[i].defence = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_Defence");
            playerStats[i].wpnPwr = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_WpnPwr");
            playerStats[i].armrPwr = PlayerPrefs.GetInt("Player" + playerStats[i].charName + "_ArmrPwr");
            playerStats[i].equippedWpn = PlayerPrefs.GetString("Player" + playerStats[i].charName + "_EquippedWpn");
            playerStats[i].equippedArmr = PlayerPrefs.GetString("Player" + playerStats[i].charName + "_EquippedArmr");
            currentGold = PlayerPrefs.GetInt("CurrentGold_");
            
            if(PlayerPrefs.GetInt("SneakersBeenUnlocked", GameMenu.instance.hasUnlockedSneakers) == 1)
            {
                GameMenu.instance.hasUnlockedSneakers = 1;
            }
        
            if(PlayerPrefs.GetInt("IsCBDead", SteamAchivementsExample.instance.cBA) == 1)
            {
                SteamAchivementsExample.instance.cBA = 1;
            }
            
            if(PlayerPrefs.GetInt("GoldAchieveOne", SteamAchivementsExample.instance.goldAchieveOne) == 1)
            {
                SteamAchivementsExample.instance.goldAchieveOne = 1;
            }
        }
        
    }

}   
