using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider soundSlider;

    private MusicObject _musicObject;
    
    private void Start()
    {
        _musicObject = GameObject.FindGameObjectWithTag("MusicObject").GetComponent<MusicObject>();
        
        bgmSlider.onValueChanged.AddListener(ChangeBgmVolume);
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume");
        
        soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
        soundSlider.value = PlayerPrefs.GetFloat("SfxVolume");
    }

    private void ChangeBgmVolume(float value)
    {
        _musicObject.ChangeBGMVolume(value);
    }

    private void ChangeSoundVolume(float value)
    {
        _musicObject.ChangeSfxVolume(value);
    }

    public void Play(int value)
    {
        ClickedSound();
        PlayerPrefs.SetInt("TutorialMode", value);
        PlayerPrefs.Save();
        SceneManager.LoadScene("PlayerMenu");
    }

    public void StoreSetting()
    {
        ClickedSound();
        PlayerPrefs.SetFloat("BgmVolume", bgmSlider.value);
        PlayerPrefs.SetFloat("SfxVolume", soundSlider.value);
        PlayerPrefs.Save();
    }

    public void ClickedSound()
    {
        _musicObject.ClickedSound();
    }
}