using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneStarter : MonoBehaviour
{
    public static CutSceneStarter instance;
    
    public GameObject cutSceneStarter, cutSceneStarterPF;
    public VideoPlayer videoPlayer;
    public GameObject canvas;
    public Collider2D trigger;

    public int cutScenePersonalID;

    public int doDestroy  = 0;
    
    public bool hasBeenWatched;

    public bool isPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //1 = true 2 = false
        if(doDestroy == 1)
        {
            Destroy(trigger);
        }
        
        if(isPlaying == true)
        {
            PlayerController.instance.canMove = false;
        }else
        {
            PlayerController.instance.canMove = true;
        }
        
        if(( videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
           StartCoroutine(EndCutScene());
        }
        
        if(Input.GetKeyDown("g"))
        {
            CloseCinematic();
        }
    }

     public IEnumerator EndCutScene()
     {
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(2f);
        canvas.SetActive(false);
        isPlaying = false;
        yield return new WaitForSeconds(1f);
        UIFade.instance.FadeFromBlack();
        doDestroy  = 1;
        
     }
     
     
    public void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player")
       {
          OpenVideoPlayer();
          
          isPlaying = true;
        }
    }

    public void OpenVideoPlayer()
    {
        canvas.SetActive(true);
    } 

    public IEnumerator GameStartCSCO()
    {
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1f);
        
        
        UIFade.instance.FadeFromBlack();
        yield return new WaitForSeconds(1f);
        
    }

    public void CloseCinematic()
    {
        Destroy(cutSceneStarter);
    }

}
