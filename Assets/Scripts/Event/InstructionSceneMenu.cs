using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionSceneMenu : MonoBehaviour
{
    [SerializeField] private float textSpeed = 0.1f;
    [SerializeField] private GameObject[] instructions;

    private int _index;
    private MusicObject _musicObject;
    [SerializeField] private Button nextButton;

    private void Start()
    {
        _musicObject = GameObject.FindGameObjectWithTag("MusicObject").GetComponent<MusicObject>();
    }

    public void Next()
    {
        // _musicObject.ClickedSound();

        if (_index >= instructions.Length)
        {
            Skip();
            return;
        }
        
        for (var i = 0; i < instructions.Length; i++)
        {
            instructions[i].SetActive(i == _index);
        }

        _index++;
    }

    public void Skip()
    {
        SceneManager.LoadScene("Play0");
    }
}
