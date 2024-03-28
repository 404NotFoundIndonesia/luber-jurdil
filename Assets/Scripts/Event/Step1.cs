using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Step1 : MonoBehaviour
{
    
    private MusicObject _musicObject;

    private void Start()
    {
        _musicObject = GameObject.FindGameObjectWithTag("MusicObject").GetComponent<MusicObject>();
    }
    
    public void Correct()
    {
        _musicObject.ClickedSound();
        PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 10);
        SceneManager.LoadScene("Play1");
    }

    public void Wrong()
    {
        _musicObject.ClickedSound();
        PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") - 10);
    }
}
