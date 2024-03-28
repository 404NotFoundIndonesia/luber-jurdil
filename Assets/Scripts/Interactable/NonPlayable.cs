using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonPlayable : MonoBehaviour, Interactable
{
    [SerializeField] private float textSpeed = 0.1f;
    [SerializeField] private GameObject character;
    [SerializeField] private bool flip;
    [SerializeField] private int playStep;

    public string characterName;
    public string[] dialogs;

    private int _dialogIndex;
    private Animator _animator;

    private Player _player;
    private GameObject _canvas;
    private TextMeshProUGUI _dialogText;
    private AudioSource _audio;

    private void Awake()
    {
        var obj = Instantiate(character, gameObject.transform);
        obj.transform.localScale = new Vector3(4, 4, 4);
        Flip(flip);

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _audio = GetComponent<AudioSource>();
        _audio.volume = PlayerPrefs.GetFloat("SfxVolume");
    }

    private void Start()
    {
        _canvas = gameObject.transform.Find("Canvas").gameObject;
        _dialogText = _canvas.GetComponentInChildren<TextMeshProUGUI>();
        _animator = GetComponentInChildren<Animator>();
        _canvas.GetComponentsInChildren<TextMeshProUGUI>()[1].text = characterName;
    }

    public string Name()
    {
        return characterName;
    }

    public void Interact()
    {
        print("Step:" + PlayerPrefs.GetInt("PlayerStep"));
        if (playStep == PlayerPrefs.GetInt("PlayerStep"))
        {
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 10);
            switch (playStep)
            {
                case 1:
                    Presence();
                    return;
                case 2:
                    WaitingRoom();
                    return;
                case 3:
                    Registration();
                    return;
            }
        }

        Chat();
    }

    // Step 1
    private void Presence()
    {
        PlayerPrefs.SetInt("PlayerStep", 2);
        SceneManager.LoadScene("Step1");
    }

    // Step 2
    private void WaitingRoom()
    {
        PlayerPrefs.SetInt("PlayerStep", 3);
        SceneManager.LoadScene("Step2");
    }

    // Step 3
    private void Registration()
    {
        PlayerPrefs.SetInt("PlayerStep", 4);
        SceneManager.LoadScene("Step3");
    }

    private void Chat()
    {
        _canvas.SetActive(true);
        _audio.Play();

        var ourPosition = gameObject.GetComponent<BoxCollider2D>().transform.position.x;
        var position = _player.gameObject.GetComponent<BoxCollider2D>().transform.position.x;
        Flip(ourPosition < position);

        _dialogIndex++;
        if (_dialogIndex >= dialogs.Length)
        {
            _dialogIndex = 0;
        }

        StartCoroutine(TypeText(dialogs[_dialogIndex]));
    }

    private void Flip(bool flip)
    {
        GetComponentInChildren<SpriteRenderer>().flipX = flip;
    }

    private IEnumerator TypeText(string text)
    {
        _animator.SetTrigger("interact");
        _dialogText.text = "";
        foreach (var c in text)
        {
            _dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(2f);

        _canvas.SetActive(false);
    }
}