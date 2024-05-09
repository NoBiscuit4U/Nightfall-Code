using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {
    
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameMan;
    public GameObject audioMan;
    public GameObject battleMan;
    public GameObject steamMan;
    public GameObject steamAchieve;
    //public GameObject cutSceneMan;
    
    // Start is called before the first frame update
    void Start(){
        if(UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }
    
        if(PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }
        if(GameManager.instance == null)
        {
            Instantiate(gameMan);
        }
    
        if(AudioManager.instance == null)
        {
            Instantiate(audioMan);
        }
         
        if(BattleManager.instance == null)
        {
            Instantiate(battleMan);
        }

        if(SteamManager.instance == null)
        {
            Instantiate(steamMan);
        }

        if(SteamAchivementsExample.instance == null)
        {
            Instantiate(steamAchieve);
        }

        /*if(CutSceneStarter.instance == null)
        {
            Instantiate(cutSceneMan);
        }*/
    }

    // Update is called once per frame
    void Update(){
        
    }
}
