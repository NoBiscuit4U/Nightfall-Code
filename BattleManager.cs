using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour{
    
    public static BattleManager instance;
    
    public bool battleActive;
    
    public GameObject battleScene;
    
    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    public BattleChar[] playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers = new List<BattleChar>();

    public int currentTurn;
    public bool turnWaiting;

    public GameObject uiButtonsHolder;
    
    public BattleMoves[] movesList;

    public GameObject enemyAttackEffect;

    public DamageNumbers theDamageNumber;

    public Text[] playerNames, playerHP, playerMp;

    public GameObject targetMenu;

    public BattleTarget[] targetButtons;

    public GameObject abilityMenu;
    public BattleAbilitySelect[] abilityButtons;

    public BattleNotification battleNotice;

    public int chanceToRun = 1;

    public GameObject itemMenu;

    public BattleItemSelect[] itemButtons;

    public Item activeItem;

    public Text itemName, itemDescription, useButtonText;

    public GameObject itemCharChoiceMenu;

    public Text[] itemCharChoiceNames;

    public CharStats[] playerStats;

    public string gameOverScene;

    private bool running;

    public int rewardXP;
    public string[] rewardItems;

    public bool cannotRun;

    public int evasionToRemove1, evasionToRemove2;

    // Start is called before the first frame update
    void Start(){
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update(){
        
    
        if(battleActive)
        {
            if(turnWaiting)
            {
                if(activeBattlers[currentTurn].isPlayer)
                {
                    uiButtonsHolder.SetActive(true);
                }else
                {
                    uiButtonsHolder.SetActive(false);
                
                    //enemy turn
                    StartCoroutine(EnemyMoveCo());
                }
            }
           
        
        }

        UpdateUIStats();
    }


    public void BattleStart(string[] enemiesToSpawn, bool setCannotRun)
    {
        
        if(!battleActive)
        {
            cannotRun = setCannotRun;
            
            battleActive = true;

            GameManager.instance.battleActive = true;
            
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            battleScene.SetActive(true);

            AudioManager.instance.PlayBGM(12);
        
            for(int i = 0; i < playerPositions.Length; i++)
            {
                if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
                {
                    for(int j = 0; j < playerPrefabs.Length; j++)
                    {
                         if(playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
                         {
                            BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
                            newPlayer.transform.parent = playerPositions[i];
                            activeBattlers.Add(newPlayer);

                            CharStats thePlayer = GameManager.instance.playerStats[i];
                            activeBattlers[i].currentHP = thePlayer.currentHP;
                            activeBattlers[i].maxHP = thePlayer.maxHP;
                            activeBattlers[i].currentMP = thePlayer.currentMP;
                            activeBattlers[i].maxMP = thePlayer.maxMP;
                            activeBattlers[i].strength = thePlayer.strength;
                            activeBattlers[i].defence = thePlayer.defence;
                            activeBattlers[i].wpnPwr = thePlayer.wpnPwr;
                            activeBattlers[i].armrPwr = thePlayer.armrPwr;
                            activeBattlers[i].critChance = thePlayer.critChance;
                            activeBattlers[i].evasionChance = thePlayer.evasionChance;
                         }
                    } 
                }
            }
        
            for(int i = 0; i < enemiesToSpawn.Length; i++)
            {
                if(enemiesToSpawn[i] != "")
                {
                    for(int j = 0; j < enemyPrefabs.Length; j ++)
                    {
                        if(enemyPrefabs[j].charName == enemiesToSpawn[i])
                        {
                           BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);
                           newEnemy.transform.parent = enemyPositions[i];
                           activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }
        
            turnWaiting = true;
            currentTurn = 0;
        
            UpdateUIStats();
        }
    }

    public void NextTurn()
    {
        currentTurn++;
        if(currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
        }
    
        turnWaiting = true;
    
        UpdateUIStats();
        UpdateBattle();
        
    }
   
    public void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayersDead = true;

        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].currentHP < 0)
            {
                activeBattlers[i].currentHP = 0;
            }
         
            if(activeBattlers[i].currentHP == 0)
            {
                //Handle Dead Battler
                if(activeBattlers[i].isPlayer)
                {
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].deadSprite;
                }
            }else
            {
                if(activeBattlers[i].isPlayer)
                {
                    allPlayersDead = false;
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].aliveSprite;
                }else
                {
                    allEnemiesDead = false;
                }
            }
        
        }
     
            if(allEnemiesDead || allPlayersDead)
            {
                if(allEnemiesDead)
                {
                    //Victory
                    StartCoroutine(EndBattleCo());
                
                }else
                {
                    //Loss
                    StartCoroutine(GameOverCo());
                }
            
                }else
            {
                while(activeBattlers[currentTurn].currentHP == 0)
                {
                    currentTurn++;
                    if(currentTurn >= activeBattlers.Count)
                    {
                        currentTurn = 0;
                    }
                }
            }
    
            for(int i = 0; i < activeBattlers.Count; i++)
            {
             if(activeBattlers[i].isPlayer)
             {
                for(int j = 0; j < GameManager.instance.playerStats.Length; j++)
                {
                    if(activeBattlers[i].charName == GameManager.instance.playerStats[j].charName)
                    {
                        activeBattlers[i].currentHP = GameManager.instance.playerStats[j].currentHP;
                        activeBattlers[i].currentMP = GameManager.instance.playerStats[j].currentMP;
                        GameManager.instance.playerStats[j].currentHP = activeBattlers[i].currentHP;
                        GameManager.instance.playerStats[j].currentMP = activeBattlers[i].currentMP;
                    }
                }
             }
            }
    }

    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(2f); 
        EnemyAttack();
        yield return new WaitForSeconds(2f);
        NextTurn();
    }
    
    public void EnemyAttack()
    {
        List<int> players = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].isPlayer && activeBattlers[i].currentHP > 0)
            {
                players.Add(i);
            }
        }
    
        int selectedTarget = players[Random.Range(0, players.Count)];
        int movePower = 0;
        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvailable.Length);
        for(int i = 0; i < movesList.Length; i++)
        {
            if(movesList[i].moveName == activeBattlers[currentTurn].movesAvailable[selectAttack])
            {
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }
        
        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        
        DealDamage(selectedTarget, movePower);
        
    }

    public void DealDamage(int target, int movePower)
    {
        float atkPwr = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].wpnPwr;
        float defPwr = activeBattlers[target].defence + activeBattlers[target].armrPwr;

        int critChance = activeBattlers[currentTurn].critChance;
        int evasionChance = activeBattlers[target].evasionChance;

        float damageCalc = (atkPwr / defPwr) * movePower;
        int damageToGive = Mathf.RoundToInt(damageCalc);

        bool hasEvaded = false; 
        bool hasCrit = false;

        Debug.Log(activeBattlers[currentTurn].charName + "dealt " + damageCalc + " (" + damageToGive + " ) damage done to " + activeBattlers[target].charName + " hasCrit " + hasCrit + " hasEvaded " +hasEvaded);
    
        if(activeBattlers[target].isPlayer)
        {

            CharStats selectedChar = GameManager.instance.playerStats[target];
            selectedChar.currentHP -= damageToGive;
            activeBattlers[target].currentHP -= damageToGive;
            
            /*if(Random.Range(0,100) >= evasionChance)
            {
                //Evades Attack
                hasEvaded = true;
            }else
            {
                //Normal Attack
                selectedChar.currentHP -= damageToGive;
                activeBattlers[target].currentHP -= damageToGive;
            }*/
        }       

        

        if(!activeBattlers[target].isPlayer)
        {
            activeBattlers[target].currentHP -= damageToGive; 
            
            /*if(Random.Range(0,100) >= critChance)
            {
               //Attacker Crits
               damageToGive = damageToGive * 2;
               activeBattlers[target].currentHP -= damageToGive;
               hasCrit = true;   
            }else
            {
               if(Random.Range(0,100) >= evasionChance)
               {
                  //Evades Attack
                  hasEvaded = true;
               }else
               {
                   //Normal Attack
                   activeBattlers[target].currentHP -= damageToGive;
               }
               
            }*/
        }       

        if(hasCrit)
        {
            
            Instantiate(theDamageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive);
            hasCrit = false;
        }else
        {
            if(hasEvaded)
            {
                
                hasEvaded = false;
            }else
            {
                Instantiate(theDamageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive);
            }
        }
    
        UpdateUIStats();
    }

    public void UpdateUIStats()
    {
        for(int i = 0; i < playerNames.Length; i++)
        {
            if(activeBattlers.Count > i)
            {
                if(activeBattlers[i].isPlayer)
                {
                   BattleChar playerData = activeBattlers[i];

                    playerNames[i].gameObject.SetActive(true);
                    playerNames[i].text = playerData.charName;
                    playerHP[i].text = Mathf.Clamp(playerData.currentHP, 0, int.MaxValue) + "/" + playerData.maxHP;
                    playerMp[i].text = Mathf.Clamp(playerData.currentMP, 0, int.MaxValue) + "/" + playerData.maxMP;
        
                }else
                {
                    playerNames[i].gameObject.SetActive(false);
                }
            }else
            {
                playerNames[i].gameObject.SetActive(false);
            }
        }
    }

   public void PlayerAttack(string moveName, int selectedTarget)
   {
        int movePower = 0;
        for(int i = 0; i < movesList.Length; i++)
        {
            if(movesList[i].moveName == moveName)
            {
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }
            Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        
            DealDamage(selectedTarget, movePower);

            uiButtonsHolder.SetActive(false);
            targetMenu.SetActive(false);
            
            NextTurn();
        
   }

   public void OpenTargetMenu(string moveName)
   {
      targetMenu.SetActive(true);

      List<int> Enemies = new List<int>();
      for(int i = 0; i < activeBattlers.Count; i++)
      {
          if(!activeBattlers[i].isPlayer)
          {
              Enemies.Add(i);
          }
      }
       
       for(int i = 0; i< targetButtons.Length; i++)
       {
           if(Enemies.Count > i && activeBattlers[Enemies[i]].currentHP > 0)
           {
               targetButtons[i].gameObject.SetActive(true);

               targetButtons[i].moveName = moveName;
               targetButtons[i].activeBattlerTarget = Enemies[i];
               targetButtons[i].targetName.text = activeBattlers[Enemies[i]].charName;
           }else
           {
               targetButtons[i].gameObject.SetActive(false);
           }
       }
   }

 public void OpenAbilityMenu()
 {
    abilityMenu.SetActive(true);

    for(int i = 0; i < abilityButtons.Length; i++)
    {
        if(activeBattlers[currentTurn].movesAvailable.Length > i)
        {
            abilityButtons[i].gameObject.SetActive(true);

            abilityButtons[i].spellName = activeBattlers[currentTurn].movesAvailable[i];
            abilityButtons[i].nameText.text = abilityButtons[i].spellName;

            for(int j = 0; j < movesList.Length; j++)
            {
                if(movesList[j].moveName == abilityButtons[i].spellName)
                {
                    abilityButtons[i].spellCost = movesList[j].moveCost;
                    abilityButtons[i].costText.text = abilityButtons[i].spellCost.ToString();
                }
            }
        }else
        {
            abilityButtons[i].gameObject.SetActive(false);
        }
    }
 }

  public void Run()
  {
     if(cannotRun)
     {
        battleNotice.theText.text = "You cannot run!";
        battleNotice.Activate();
     }else
     {

     
     int runSuccess = Random.Range(1, 6);
     if(runSuccess <= chanceToRun)
     {
        
        running = true;
        StartCoroutine(EndBattleCo());
     }else
     {
         NextTurn();
         battleNotice.theText.text = "Couldn't run! ";
         battleNotice.Activate();

     }
  
     }
  }

  public void Evade(int evasionToAdd)
  {
    evasionToAdd = 2;
    activeBattlers[currentTurn].evasionChance += evasionToAdd;
    if(currentTurn == 0)
    {
        evasionToRemove1 = evasionToAdd + evasionToRemove1;
    }
    
    if(currentTurn == 1)
    {
        evasionToRemove2 = evasionToAdd + evasionToRemove2;
    }
    NextTurn();
  }

  public void ShowItem()
    {

        GameManager.instance.SortItems();
        
        itemMenu.SetActive(true);

        for (int i = 0; i < itemButtons.Length; i++)
        {

            itemButtons[i].buttonValue = i;



            if (GameManager.instance.itemsHeld[i] != "")
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

        }



        if(activeItem.isWeapon || activeItem.isArmor)
        {

            useButtonText.text = "Equip";

        }



        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
   }



    public void OpenItemCharChoice()
    {

        itemCharChoiceMenu.SetActive(true);



        for(int i = 0; i < itemCharChoiceNames.Length; i++)
        {

            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;

            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);



        }



    }



    public void CloseItemCharChoice()
    {

        itemCharChoiceMenu.SetActive(false);

    }

    public void ExitItemMenu()
    {
        itemMenu.SetActive(false);
    }

    public void Useitem(int selectedChar)
    {



        activeItem.Use(selectedChar);
           
        UpdateUIStats();

        CloseItemCharChoice();

        itemMenu.SetActive(false);       

        NextTurn();

    }

    public IEnumerator EndBattleCo()
    {
        battleActive = false;
        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
        abilityMenu.SetActive(false);
        itemMenu.SetActive(false);

        yield return new WaitForSeconds(.5f);

        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);
        
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].isPlayer)
            {
                for(int j = 0; j < GameManager.instance.playerStats.Length; j++)
                {
                    if(activeBattlers[i].charName == GameManager.instance.playerStats[j].charName)
                    {
                        activeBattlers[i].currentHP = GameManager.instance.playerStats[j].currentHP;
                        activeBattlers[i].currentMP = GameManager.instance.playerStats[j].currentMP;
                        GameManager.instance.playerStats[j].currentHP = activeBattlers[i].currentHP;
                        GameManager.instance.playerStats[j].currentMP = activeBattlers[i].currentMP;
                    }
                }
            }
        
            GameManager.instance.removeEvasion = true;
            Destroy(activeBattlers[i].gameObject);
        }
    
        UIFade.instance.FadeFromBlack();
        battleScene.SetActive(false);
        activeBattlers.Clear();
        currentTurn = 0;
        if(running)
        {
            GameManager.instance.battleActive = false;
            running = false;
        }else
        {
            BattleReward.instance.OpenRewardScreen(rewardXP, rewardItems);
        }

        AudioManager.instance.PlayBGM(FindObjectOfType<CameraController>().musicToPlay);
    }

    public IEnumerator GameOverCo()
    {
        battleActive = false;
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        battleScene.SetActive(false);
        SceneManager.LoadScene(gameOverScene);
    }
      
    public void CloseTargetMenu()
    {
        targetMenu.SetActive(false);
    }

    public void CloseAbilityMenu()
    {
        abilityMenu.SetActive(false);
    }
}

    
