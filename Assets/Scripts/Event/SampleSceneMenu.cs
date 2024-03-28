using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleSceneMenu : MonoBehaviour
{
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}