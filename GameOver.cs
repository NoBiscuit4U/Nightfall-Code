using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour{
    
    public string mainMenuScene;
    public string loadGameScene;

    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start(){
        
        AudioManager.instance.PlayBGM(7);

        PlayerController.instance.gameObject.SetActive(false);
        GameMenu.instance.gameObject.SetActive(false);
        BattleManager.instance.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitToMenu()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);

        
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadLastSave()
    {
        StartCoroutine(LoadLastSaveCo());
    }

    public IEnumerator LoadLastSaveCo()
    {
        Destroy(BattleManager.instance.gameObject);
        yield return new WaitForSeconds(0.2f);
        Destroy(GameManager.instance.gameObject);
        yield return new WaitForSeconds(0.2f);
        Destroy(PlayerController.instance.gameObject);
        yield return new WaitForSeconds(0.2f);
        Destroy(GameMenu.instance.gameObject);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(loadGameScene);
        yield return new WaitForSeconds(0.5f);
        Destroy(canvas);
        Destroy(BattleManager.instance.gameObject);

    }
}
