using System;
using UnityEngine;

public class MusicObject : MonoBehaviour
{
    [SerializeField] private AudioSource[] sfx;
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("MusicObject").Length > 1)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeBGMVolume(PlayerPrefs.GetFloat("BgmVolume"));
        ChangeSfxVolume(PlayerPrefs.GetFloat("SfxVolume"));
    }

    public void ChangeBGMVolume(float value)
    {
        GetComponent<AudioSource>().volume = value;
    }

    public void ChangeSfxVolume(float value)
    {
        foreach (var source in sfx)
        {
            source.volume = value;
        }
    }

    public void ClickedSound()
    {
        sfx[0].Play();
    }
}