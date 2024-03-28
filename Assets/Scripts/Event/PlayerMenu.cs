using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputName;
    [SerializeField] private TMP_Dropdown inputGender;

    public GameObject selectedChar;
    public List<Character> characters;

    private int _selectedIndex;
    private MusicObject _musicObject;

    private void Start()
    {
        var obj = Instantiate(characters[_selectedIndex].prefab, selectedChar.transform);
        obj.transform.localScale = new Vector3(10, 10, 10);
        _musicObject = GameObject.FindGameObjectWithTag("MusicObject").GetComponent<MusicObject>();
    }

    public void NextChar()
    {
        _selectedIndex++;
        if (_selectedIndex >= characters.Count)
        {
            _selectedIndex = 0;
        }

        SetChar(_selectedIndex);
    }

    public void PrevChar()
    {
        _selectedIndex--;
        if (_selectedIndex < 0)
        {
            _selectedIndex = characters.Count - 1;
        }

        SetChar(_selectedIndex);
    }

    private void SetChar(int index)
    {
        _musicObject.ClickedSound();
        Destroy(selectedChar.transform.GetChild(0).gameObject);
        var obj = Instantiate(characters[index].prefab, selectedChar.transform);
        obj.transform.localScale = new Vector3(10, 10, 10);
    }

    public void Back()
    {
        _musicObject.ClickedSound();
        SceneManager.LoadScene("MainMenu");
    }
    
    // Score => 100
    //
    public void Submit()
    {
        _musicObject.ClickedSound();
        PlayerPrefs.SetString("PlayerName", inputName.text);
        PlayerPrefs.SetString("PlayerGender", inputGender.captionText.text);
        PlayerPrefs.SetString("PlayerPrefabs", characters[_selectedIndex].name);
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("PlayerStep", 1);
        PlayerPrefs.SetFloat("PlayerCoordinateX", (float) -6.13);
        PlayerPrefs.SetFloat("PlayerCoordinateY", (float) -3.29);
        PlayerPrefs.Save();

        SceneManager.LoadScene(PlayerPrefs.GetInt("TutorialMode") == 0 ? "Play0" : "InstructionMenu");
    }
}