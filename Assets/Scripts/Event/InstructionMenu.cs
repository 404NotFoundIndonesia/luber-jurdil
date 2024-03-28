using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionMenu : MonoBehaviour
{
    [SerializeField] private float textSpeed = 0.1f;
    
    private TextMeshProUGUI _instructionText;

    private int _instructionIndex = 0;
    [SerializeField] private List<string> instructions = new List<string>();

    private MusicObject _musicObject;
    [SerializeField] private Button nextButton;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        _instructionText = GameObject.FindGameObjectWithTag("TextObject").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        instructions[_instructionIndex] = ("Halo, " + PlayerPrefs.GetString("PlayerName"));
        StartCoroutine(TypeText(instructions[_instructionIndex]));
        
        _musicObject = GameObject.FindGameObjectWithTag("MusicObject").GetComponent<MusicObject>();
    }

    private IEnumerator TypeText(string text)
    {
        animator.SetTrigger("interact");
        nextButton.interactable = false;
        _instructionText.text = "";

        foreach (var c in text)
        {
            _instructionText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        nextButton.interactable = true;
    }

    public void Next()
    {
        _musicObject.ClickedSound();
        _instructionIndex++;
        if (_instructionIndex >= instructions.Count)
        {
            SceneManager.LoadScene("InstructionScene");
            return;
        }

        StartCoroutine(TypeText(instructions[_instructionIndex]));
    }

    public void Skip()
    {
        SceneManager.LoadScene("Play0");
    }
}
