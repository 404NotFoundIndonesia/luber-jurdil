using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Play0 : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Button[] _buttons;
    private int _countWrong = 3;
    private MusicObject _musicObject;

    private string[] _names =
        { "Zurahdi", "Wawongso", "Budirah", "Fulan", "Fulanah", "Joko Prabowo", "Widodo Subianto" };

    private void Awake()
    {
        var obj = Instantiate(Resources.Load<GameObject>(PlayerPrefs.GetString("PlayerPrefabs")), player.transform);
        obj.transform.localScale = new Vector3(10, 10, 10);

        _buttons = GameObject.FindWithTag("TextObject").GetComponentsInChildren<Button>();

        var random = new Random();
        var correctIndex = random.Next(_buttons.Length);

        for (var i = 0; i < _buttons.Length; i++)
        {
            if (i == correctIndex)
            {
                _buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerName");
                _buttons[i].onClick.AddListener(Correct);
            }
            else
            {
                _buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = _names[random.Next(_names.Length)];
                _buttons[i].onClick.AddListener(Wrong);
            }
        }
        _musicObject = GameObject.FindGameObjectWithTag("MusicObject").GetComponent<MusicObject>();
    }

    private void Wrong()
    {
        _countWrong--;
        if (_countWrong == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void Correct()
    {
        _musicObject.ClickedSound();
        PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 10);
        SceneManager.LoadScene("Play1");
    }
}