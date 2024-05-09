using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
    
    public string newGameScene;
    
    public GameObject continueButton;

    public string loadGameScene;
    
    // Start is called before the first frame update
    void Start(){
        if(PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
       SceneManager.LoadScene(loadGameScene);
    }

    public void NewGame()
    {
       SceneManager.LoadScene(newGameScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator GameStart()
    {
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(loadGameScene);
        yield return new WaitForSeconds(2f);
        UIFade.instance.FadeFromBlack();
    }
}
