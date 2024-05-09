using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleChar : MonoBehaviour{
    
    public bool isPlayer;
    public string[] movesAvailable;
    
    public static BattleChar instance;
    public string charName;
    public int currentHP, maxHP, currentMP, maxMP, strength, defence, wpnPwr, armrPwr , critChance, evasionChance;
    public bool hasDied;

    public SpriteRenderer theSprite;
    public Sprite deadSprite;
    public Sprite aliveSprite;
    public Animator animator;

    public DamageNumbers theDamageNumber;
   
   // Start is called before the first frame update
    void Start(){
        instance = this;
    }

    // Update is called once per frame
    void Update(){
        
        if(currentHP == 0)
        {
            Destroy(animator);
            theSprite.sprite = deadSprite;
        }
    
        if(evasionChance >= 50)
        {
            evasionChance = 50;
        }
    }
    

}

