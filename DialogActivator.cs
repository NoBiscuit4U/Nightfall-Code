using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour{
    
    public string[] lines;

    public static DialogActivator instance; 
    
    private bool canActivate;

    public bool isPerson = true;

    public bool shouldActivateQuest, activateOnEnter, activateViaButton;
    public string questToMark;
    public bool markComplete;

    public GameObject trigger;
    
    
    // Start is called before the first frame update
    void Start(){
        instance = this;
    }

    // Update is called once per frame
    void Update(){
        if(canActivate && activateViaButton && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines, isPerson);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
            
        }

        if(canActivate && activateOnEnter && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines, isPerson);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
            Destroy(DialogActivator.instance.trigger);
        }
    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            DialogManager.instance.LeftClickNotifier();
            canActivate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActivate = false;
            DialogManager.instance.LeftClickNotifierBlank();
        }
    }
    
}
