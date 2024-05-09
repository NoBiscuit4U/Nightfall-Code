using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour{
    
    public GameObject theMenu;
    public GameObject theSettingsMenu;
    public GameObject[] windows;
    public GameObject[] tutorialWindows;
    
    public CharStats[] playerStats;

    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public GameObject[] statusButtons;

    public Text statusName, statusHP, statusMP, statusStr, statusDef, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, statusArmrPwr, statusExp, statusEvasion, statusCrit;
    public Image statusImage;

    public ItemButtons[] itemButtons;
    public SkinButtons[] skinButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;
    
    public static GameMenu instance;
    
    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;
    public Text goldText;

    public string mainMenuName;

    public InBattleZoneUi battleDetectorImageInZone;

    public int hasUnlockedSneakers = 0;
    public GameObject sneakersCharStats;
    public GameObject sneakersStatsButton;

    public GameObject miniMap;

    public Image weaponImage, armrImage;
    
    public Sprite nullImage;

    public Text armorWeaponPowerTxt;

    // Start is called before the first frame update
    void Start(){
        instance = this;
        
        if(PlayerPrefs.HasKey("ExitIconRed"))
        {
            PlayerPrefs.GetFloat("ExitIconRed");
        }

        if(PlayerPrefs.HasKey("ExitIconGreen"))
        {
           PlayerPrefs.GetFloat("ExitIconGreen");
        }

        if(PlayerPrefs.HasKey("ExitIconBlue"))
        {
            PlayerPrefs.GetFloat("ExitIconBlue");
        }

         if(PlayerPrefs.HasKey("PlayerIconRed"))
        {
            PlayerPrefs.GetFloat("PlayerIconRed");
        }

        if(PlayerPrefs.HasKey("PlayerIconGreen"))
        {
            PlayerPrefs.GetFloat("PlayerIconGreen");
        }

        if(PlayerPrefs.HasKey("PlayerIconBlue"))
        {
            PlayerPrefs.GetFloat("PlayerIconBlue");
        }
    }

    
    
    // Update is called once per frame
    void Update(){

        if(hasUnlockedSneakers == 1)
        {
           sneakersCharStats.SetActive(true);
           sneakersStatsButton.SetActive(true);
        }else
        {
           sneakersCharStats.SetActive(false);
           sneakersStatsButton.SetActive(false);
        }
    
        
        if(GameManager.instance.battleActive == true)
        {
             
        }else   
        {     
            if(Input.GetButtonDown("Fire2"))
              {
                if(theMenu.activeInHierarchy)
                {
                  theMenu.SetActive(false);
                  GameManager.instance.gameMenuOpen = false;
            
                  CloseMenu();
                } else
                {
                  theMenu.SetActive(true);
                  UpdateMainStats();
                  GameManager.instance.gameMenuOpen = true;
                }
        
             AudioManager.instance.PlaySFX(0);
        
              }
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           if(theSettingsMenu.activeInHierarchy)
           {
                theSettingsMenu.SetActive(false);
                GameManager.instance.gameMenuOpen = false;
            
                CloseMenu();
            } else
            {
                theSettingsMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        
             AudioManager.instance.PlaySFX(0);
        
        }
    }
    
    
    
    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;
    
        for(int i = 0; i < playerStats.Length; i++)
        {
            if(playerStats[i].gameObject.activeInHierarchy)
            {
               charStatHolder[i].SetActive(true);

               nameText[i].text = playerStats[i].charName;
               hpText[i].text = "HP: " + Mathf.Clamp(playerStats[i].currentHP, 0, int.MaxValue) + "/" + playerStats[i].maxHP;
               mpText[i].text = "MP: " + Mathf.Clamp(playerStats[i].currentMP, 0, int.MaxValue) + "/" + playerStats[i].maxMP;
               lvlText[i].text = "LVL: " + playerStats[i].playerLevel;
               expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
               expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
               expSlider[i].value = playerStats[i].currentEXP;
               charImage[i].sprite = playerStats[i].charImage;
            
            }else
            {
                charStatHolder[i].SetActive(false);
            }
        }   
   
        
        goldText.text = GameManager.instance.currentGold.ToString() + "G";
    }


    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();
        
        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }else
            {
                windows[i].SetActive(false);
            }
        }
    
        itemCharChoiceMenu.SetActive(false);
    }

    public void ToggleTutorialWindow(int TutorialNumber)
    {
        UpdateMainStats();
        
        for(int i = 0; i < tutorialWindows.Length; i++)
        {
            if(i == TutorialNumber)
            {
                tutorialWindows[i].SetActive(!tutorialWindows[i].activeInHierarchy);
            }else
            {
                tutorialWindows[i].SetActive(false);
            }
        }
    
    }
    
    public void CloseMenu()
    {
        for(int i = 0; i < windows.Length; i ++)
        {
            windows[i].SetActive(false);
        }
    
        theMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;
    
        itemCharChoiceMenu.SetActive(false);
    }


  public void OpenStatus()
  {
      UpdateMainStats();
      
      //update the information that is shown
      StatusChar(0);
     
     
     for(int i = 0; i < statusButtons.Length; i++)
      {
          statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
          statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
      }
  }

 
 public void StatusChar(int selected)
 {
      if(playerStats[selected].equippedWpn == "")
      {
        statusWpnEqpd.text = "NONE";
      }
     
     statusName.text = playerStats[selected].charName;
     statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
     statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
     statusStr.text = playerStats[selected].strength.ToString();
     statusDef.text = playerStats[selected].defence.ToString();
     statusEvasion.text = playerStats[selected].evasionChance.ToString() + "%";
     statusCrit.text = playerStats[selected].critChance.ToString() + "%";
     if(playerStats[selected].equippedWpn != "")
     {
        statusWpnEqpd.text = playerStats[selected].equippedWpn;
        weaponImage.sprite = playerStats[selected].equippedWeaponImage;
     }else
     {
        statusWpnEqpd.text = "None";
        weaponImage.sprite = nullImage;
     }
     statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();
     if(playerStats[selected].equippedArmr != "")
     {
        statusArmrEqpd.text = playerStats[selected].equippedArmr;
        armrImage.sprite = playerStats[selected].equippedArmorImage;
     }else
     {
        statusArmrEqpd.text = "None";
        armrImage.sprite = nullImage;
     }
     statusArmrPwr.text = playerStats[selected].armrPwr.ToString();
     statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP).ToString();
     statusImage.sprite = playerStats[selected].charImage;

   }


    public void ShowItems()
    {
        GameManager.instance.SortItems();
        
        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;
        
        
            if(GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
                
            }else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        
        }
        
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if(activeItem.isItem)
        {
            useButtonText.text = "Use";
            armorWeaponPowerTxt.text = "";
        }
    
        if(activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
            
            if(activeItem.isArmor)
            {
                armorWeaponPowerTxt.text = ("Armor Strength " + activeItem.armorStrength);
            }

            if(activeItem.isWeapon)
            {
                armorWeaponPowerTxt.text = ("Weapon Strength " + activeItem.weaponStrength);
            }
        }

        if(activeItem.isArtifact)
        {
            useButtonText.text = "Open";
        }

        if(activeItem.isGold)
        {
            useButtonText.text = "Redeem";
        }
    
        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }


    public void DiscardItem()
    {
        if(activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }



    public void OpenitemCharChoice()
    {
        if (activeItem == null) { return; }
        itemCharChoiceMenu.SetActive(true);

        for(int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }




    public void CloseitemCharChoice()
    {
       itemCharChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectedChar)
    {
        activeItem.Use(selectedChar);
        CloseitemCharChoice();
    }


    public void SaveGame()
    {
        GameManager.instance.SaveData();
        QuestManager.instance.SaveQuestData();
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(0);
    }


    public void QuitGame()
    {
        SaveGame();
        SceneManager.LoadScene(mainMenuName);
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
        Destroy(gameObject);
    }

    public void DefaultToNoItemSelected()
    {
        Shop.instance.selectedItem = null;
        activeItem = null;
        itemName.text = "Item:";
        itemDescription.text = "No item selected.";
        itemCharChoiceMenu.SetActive(false);
        
    }

}






