using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaveBossCinematic : MonoBehaviour
{
    public Camera customCamera;
    public GameObject cinematicCamera;
    public GameObject battleArea;
    public GameObject caveBossSprite;
    public GameObject caveBossSprite2;
    public GameObject collision;
    public Collider2D trigger;
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(CBC());
        }
    }
    
    public IEnumerator CBC()
    {
        UIFade.instance.FadeToBlack();
        cinematicCamera.SetActive(true);
        collision.SetActive(true);
        yield return new WaitForSeconds(1f);
        caveBossSprite.SetActive(true);
        yield return new WaitForSeconds(1f);
        UIFade.instance.FadeFromBlack();
        yield return new WaitForSeconds(1.55f);
        AudioManager.instance.PlaySFX(1);
        yield return new WaitForSeconds(3f);
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1f);
        cinematicCamera.SetActive(false);
        battleArea.SetActive(true);
        caveBossSprite.SetActive(false);
        yield return new WaitForSeconds(2f);
        UIFade.instance.FadeFromBlack();
        caveBossSprite2.SetActive(true);
        collision.SetActive(false);
        Destroy(battleArea);
        Destroy(trigger);
   }
}
