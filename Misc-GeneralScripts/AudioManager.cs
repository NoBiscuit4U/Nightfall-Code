using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager:MonoBehaviour{
    
    public AudioSource[] sfx;
    public AudioSource[] bgm;

    public static AudioManager instance;

    public AudioMixer mixer;
    
    // Start is called before the first frame update
    void Start(){
        instance = this;
 
        
        DontDestroyOnLoad(this.gameObject);
        
        if(PlayerPrefs.HasKey("MasterVol"))
        {
            mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        }

        if(PlayerPrefs.HasKey("MusicVol"))
        {
            mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        }

        if(PlayerPrefs.HasKey("MasterVol"))
        {
            mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
        }
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void PlaySFX(int soundToPlay)
    {
       if(soundToPlay < sfx.Length)
       {
           sfx[soundToPlay].Play();
       }
       
    }
    
    public void PlayBGM(int musicToPlay)
    {
        if(!bgm[musicToPlay].isPlaying)
        {
        
         StopMusic();
        
          if(musicToPlay < bgm.Length)
          { 
            bgm[musicToPlay].Play();
          }
     
        }
    }

    public void StopMusic()
    {
        for(int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
