using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;

    public TMP_Text resolutionLabel;

    public GameMenu gameMenu;

    public Toggle fullscreenTog;

    public AudioMixer mixer;

    public Text masterValue, musicValue, sfxValue;
    public Slider masterSlid, musicSlid, sfxSlid;
    public Slider pRSlid, pBSlid, pGSlid, mRSlid, mBSlid, mGSlid;

    Settings settings;

    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        instance = this;
        
        bool foundRes = false;
        for(int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal  && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;

                selectedResolution = i;

                UpdateResLabel();
            }
        }

        if(!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedResolution = resolutions.Count -1;

            UpdateResLabel();
        }
        float vol = 0f;
        mixer.GetFloat("MasterVol", out vol);
        masterSlid.value = vol;
        mixer.GetFloat("MusicVol", out vol);
        musicSlid.value = vol;
        mixer.GetFloat("SFXVol", out vol);
        sfxSlid.value = vol;
        
        masterValue.text = Mathf.RoundToInt(masterSlid.value + 80).ToString();
        musicValue.text = Mathf.RoundToInt(musicSlid.value + 80).ToString();
        sfxValue.text = Mathf.RoundToInt(sfxSlid.value + 80).ToString();
        
        pRSlid.value = PlayerPrefs.GetFloat("PlayerIconRed");
        pGSlid.value = PlayerPrefs.GetFloat("PlayerIconGreen");
        pBSlid.value = PlayerPrefs.GetFloat("PlayerIconBlue");
        mRSlid.value = PlayerPrefs.GetFloat("ExitIconRed");
        mGSlid.value = PlayerPrefs.GetFloat("ExitIconGreen");
        mBSlid.value = PlayerPrefs.GetFloat("ExitIconBlue");
      
    }

    public void ResLeft()
    {
       selectedResolution--;
       if(selectedResolution < 0)
       {
           selectedResolution = 0;
       }
       UpdateResLabel();
    }

    public void ResRight()
    {
       selectedResolution++;
       if(selectedResolution > resolutions.Count - 1)
       {
           selectedResolution = resolutions.Count - 1;
       }
       UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + "x" + resolutions[selectedResolution].vertical.ToString();
    }

    public void ApplySettings()
    {
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }

    public void SetMasterVol()
    {
        masterValue.text = Mathf.RoundToInt(masterSlid.value + 80).ToString();
        mixer.SetFloat("MasterVol", masterSlid.value);
        PlayerPrefs.SetFloat("MasterVol", masterSlid.value);
    }

    public void SetMusicVol()
    {
        musicValue.text = Mathf.RoundToInt(musicSlid.value + 80).ToString();
        mixer.SetFloat("MusicVol", musicSlid.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlid.value);
    }

    public void SetSFXVol()
    {
        sfxValue.text = Mathf.RoundToInt(sfxSlid.value + 80).ToString();
        mixer.SetFloat("SFXVol", sfxSlid.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlid.value);
    }

    public void RChangeAreaExitMinimapIcon()
    {
        PlayerPrefs.SetFloat("ExitIconRed", mRSlid.value);
    }

    public void GChangeAreaExitMinimapIcon()
    {
        PlayerPrefs.SetFloat("ExitIconGreen", mGSlid.value);
    }
    
    public void BChangeAreaExitMinimapIcon()
    {
        PlayerPrefs.SetFloat("ExitIconBlue", mBSlid.value);
    }

    public void RChangePlayerMinimapIcon()
    {
        PlayerPrefs.SetFloat("PlayerIconRed", pRSlid.value);
    }

    public void GChangePlayerMinimapIcon()
    {
        PlayerPrefs.SetFloat("PlayerIconGreen", pGSlid.value);
    }

    public void BChangePlayerMinimapIcon()
    {
       PlayerPrefs.SetFloat("PlayerIconBlue", pBSlid.value);
    }
}

 [System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}

