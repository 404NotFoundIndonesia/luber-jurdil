using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ribbon : MonoBehaviour
{
    [SerializeField] private Sprite[] starStates;
    private Image[] stars;

    private void Awake()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text =
            PlayerPrefs.GetString("PlayerName");
        stars = GameObject.FindGameObjectWithTag("StarContainer").GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        var score = PlayerPrefs.GetInt("PlayerScore");
        if (score >= 100)
        {
            stars[0].sprite = FullStar();
            stars[1].sprite = FullStar();
            stars[2].sprite = FullStar();
            stars[3].sprite = FullStar();
            stars[4].sprite = FullStar();
        }
        else if (score >= 90)
        {
            stars[0].sprite = FullStar();
            stars[1].sprite = FullStar();
            stars[2].sprite = FullStar();
            stars[3].sprite = FullStar();
            stars[4].sprite = HalfStar();
        }
        else if (score >= 80)
        {
            stars[0].sprite = FullStar();
            stars[1].sprite = FullStar();
            stars[2].sprite = FullStar();
            stars[3].sprite = FullStar();
            stars[4].sprite = NoStar();
        }
        else if (score >= 70)
        {
            stars[0].sprite = FullStar();
            stars[1].sprite = FullStar();
            stars[2].sprite = FullStar();
            stars[3].sprite = HalfStar();
            stars[4].sprite = NoStar();
        }
        else if (score >= 60)
        {
            stars[0].sprite = FullStar();
            stars[1].sprite = FullStar();
            stars[2].sprite = FullStar();
            stars[3].sprite = NoStar();
            stars[4].sprite = NoStar();
        }
        else if (score >= 50)
        {
            stars[0].sprite = FullStar();
            stars[1].sprite = FullStar();
            stars[2].sprite = HalfStar();
            stars[3].sprite = NoStar();
            stars[4].sprite = NoStar();
        }
        else if (score >= 40)
        {
            stars[0].sprite = FullStar();
            stars[1].sprite = FullStar();
            stars[2].sprite = NoStar();
            stars[3].sprite = NoStar();
            stars[4].sprite = NoStar();
        }
        else if (score >= 30)
        {
            stars[0].sprite = FullStar();
            stars[1].sprite = HalfStar();
            stars[2].sprite = NoStar();
            stars[3].sprite = NoStar();
            stars[4].sprite = NoStar();
        }
        else if (score >= 20)
        {
            stars[0].sprite = FullStar();
            stars[1].sprite = NoStar();
            stars[2].sprite = NoStar();
            stars[3].sprite = NoStar();
            stars[4].sprite = NoStar();
        }
        else if (score >= 10)
        {
            stars[0].sprite = HalfStar();
            stars[1].sprite = NoStar();
            stars[2].sprite = NoStar();
            stars[3].sprite = NoStar();
            stars[4].sprite = NoStar();
        }
        else
        {
            stars[0].sprite = NoStar();
            stars[1].sprite = NoStar();
            stars[2].sprite = NoStar();
            stars[3].sprite = NoStar();
            stars[4].sprite = NoStar();
        }
    }

    private Sprite NoStar()
    {
        return starStates[0];
    }

    private Sprite HalfStar()
    {
        return starStates[1];
    }

    private Sprite FullStar()
    {
        return starStates[2];
    }
}