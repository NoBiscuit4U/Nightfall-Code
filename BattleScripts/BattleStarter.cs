using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStarter : MonoBehaviour
{
    public InBattleZoneUi OutOfZone;
    
    public BattleType[] potentialBattles;

    public static BattleStarter instance;
    
    public bool activateOnEnter, activeOnStay, activateOnExit;

    private bool inArea;
    public float timeBetweenBattles;
    private float betweenBattlerCounter;
    
    public bool deactivateAfterStarting;

    public bool cannotRun;
    public bool shouldCompleteQuest;
    public string QuestToComplete;
    
    public SpriteRenderer inBattleZone;
    public Sprite inZoneSprite, outOfZoneSprite;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        betweenBattlerCounter = Random.Range(timeBetweenBattles * .5f, timeBetweenBattles * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(inArea && PlayerController.instance.canMove)
        {
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                betweenBattlerCounter -= Time.deltaTime;
            }
        
            if(betweenBattlerCounter <= 0)
            {
                betweenBattlerCounter = Random.Range(timeBetweenBattles * .5f, timeBetweenBattles * 1.5f);
            
                StartCoroutine(StartBattleCo());
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(activateOnEnter)
            {
                StartCoroutine(StartBattleCo());
            }else
            {
              OutOfZone = GameMenu.instance.battleDetectorImageInZone;
                
               OutOfZone.sprite.SetActive(true);
               inBattleZone.sprite = inZoneSprite;
               
               inArea = true;
            }
            
        }
    }

     public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(activateOnExit)
            {
                StartCoroutine(StartBattleCo());
            }else
            {
                OutOfZone = GameMenu.instance.battleDetectorImageInZone;
                
                OutOfZone.sprite.SetActive(false);
                inBattleZone.sprite = outOfZoneSprite;
                inArea = false;
            }
            
        }
    }

    public IEnumerator StartBattleCo()
    {
        UIFade.instance.FadeToBlack();
        GameManager.instance.battleActive = true;

        int selectedBattle = Random.Range(0,potentialBattles.Length);

        BattleManager.instance.rewardItems = potentialBattles[selectedBattle].rewardItems;
        BattleManager.instance.rewardXP = potentialBattles[selectedBattle].rewardXP;

        yield return new WaitForSeconds(1.5f);
        BattleManager.instance.BattleStart(potentialBattles[selectedBattle].enemies, cannotRun);
        UIFade.instance.FadeFromBlack();

        if(deactivateAfterStarting == true)
        {
            gameObject.SetActive(false);
        }
    
        BattleReward.instance.markQuestComplete = shouldCompleteQuest;
        BattleReward.instance.questToMark = QuestToComplete;
    }
}
