using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Step2 : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI call;
    [SerializeField] private GameObject[] players;

    private readonly List<string> _names = new List<string>()
        { "Player Male A", "Player Male B", "Player Female A", "Player Female B" };

    private Animator _animator;
    private AudioSource _audioSource;

    [SerializeField] private float _walkAnimation = 3.5f;

    private void Awake()
    {
        canvas.SetActive(false);
        call.text = "Panggilan kepada " + PlayerPrefs.GetString("PlayerName");

        _audioSource = GetComponent<AudioSource>();

        for (var i = 0; i < players.Length; i++)
        {
            var instance = Instantiate(CharacterGameObject(i), players[i].transform);
            instance.transform.localScale = new Vector3(10, 10, 10);
            instance.GetComponent<SpriteRenderer>().flipX = true;
            if (i == 0)
            {
                instance.GetComponent<SpriteRenderer>().sortingOrder = 100;
                _animator = instance.GetComponent<Animator>();
                _animator.SetBool("walk", true);
            }
        }
    }

    private GameObject CharacterGameObject(int index)
    {
        var savedName = PlayerPrefs.GetString("PlayerPrefabs");
        if (index == 0 || _names.Count <= 0)
        {
            _names.Remove(savedName);
            return Resources.Load<GameObject>(savedName);
        }

        var selectedName = _names[0];
        _names.Remove(selectedName);

        return Resources.Load<GameObject>(selectedName);
    }

    private void Start()
    {
        _animator.SetBool("walk", true);
    }

    private void FixedUpdate()
    {
        _walkAnimation -= Time.deltaTime;
        _animator.SetBool("walk", _walkAnimation > 0);

        if (_walkAnimation <= -5f && _walkAnimation >= -5.1f && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }

        canvas.SetActive(_walkAnimation <= -5f && _walkAnimation >= -7f);
        if (_walkAnimation <= -9f)
        {
            SceneManager.LoadScene("Play1");
        }
    }
}