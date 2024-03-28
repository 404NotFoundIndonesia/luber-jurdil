using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step6 : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        print(PlayerPrefs.GetString("PlayerPrefabs"));
        switch (PlayerPrefs.GetString("PlayerPrefabs"))
        {
            case "Player Male A":
                _spriteRenderer.sprite = _sprites[0];
                return;
            case "Player Male B":
                _spriteRenderer.sprite = _sprites[1];
                return;
            case "Player Female A":
                _spriteRenderer.sprite = _sprites[2];
                return;
            case "Player Female B":
                _spriteRenderer.sprite = _sprites[3];
                return;
        }
    }
}
