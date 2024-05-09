using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAbilitySelect : MonoBehaviour{
    
    public string spellName;
    public int spellCost;
    public Text nameText;
    public Text costText;
    
    public CharStats currentMP;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void Press()
    {
        if(BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP >= spellCost)
        {
           

           BattleManager.instance.abilityMenu.SetActive(false);
           BattleManager.instance.OpenTargetMenu(spellName);
           BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP -= spellCost;
           CharStats abilityCost = GameManager.instance.playerStats[BattleManager.instance.currentTurn];
           abilityCost.currentMP -= spellCost;
         
        }else
        {
            //not enough mana

            BattleManager.instance.battleNotice.theText.text = "Not Enough Mana";
            BattleManager.instance.battleNotice.Activate();
            BattleManager.instance.abilityMenu.SetActive(false);
        }

    }


}
