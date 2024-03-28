using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Object : MonoBehaviour, Interactable
{
    [SerializeField] private string objectName;
    [SerializeField] private int playStep;
    
    public string Name()
    {
        return objectName;
    }

    public void Interact()
    {
        print("Step:" + PlayerPrefs.GetInt("PlayerStep"));
        if (playStep == PlayerPrefs.GetInt("PlayerStep"))
        {
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 10);
            switch (playStep)
            {
                case 4:
                    VoteRoom();
                    return;
                case 5:
                    VoteBox();
                    return;
                case 6:
                    Ink();
                    return;
                case 7:
                    Exit();
                    return;
            }
        }
    }

    private void VoteRoom()
    {
        PlayerPrefs.SetInt("PlayerStep", 5);
        SceneManager.LoadScene("Step4");
    }

    private void VoteBox()
    {
        PlayerPrefs.SetInt("PlayerStep", 6);
        SceneManager.LoadScene("Step5");
    }

    private void Ink()
    {
        PlayerPrefs.SetInt("PlayerStep", 7);
        SceneManager.LoadScene("Step6");
    }

    private void Exit()
    {
        print("keluar");
        SceneManager.LoadScene("Step7");
    }
}
