using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Step7 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Sprite[] medals;
    [SerializeField] private Image medalHolder;
    
    private MusicObject _musicObject;

    private void Awake()
    {
        var obj = Instantiate(Resources.Load<GameObject>(PlayerPrefs.GetString("PlayerPrefabs")), player.transform);
        obj.transform.localScale = new Vector3(15, 15, 15);

        var finalScore = PlayerPrefs.GetInt("PlayerScore");
        score.text = finalScore.ToString();

        if (finalScore >= 80)
        {
            medalHolder.sprite = medals[0];
        } else if (finalScore >= 60)
        {
            medalHolder.sprite = medals[1];
        }
        else
        {
            medalHolder.sprite = medals[2];
        }
        
        
        _musicObject = GameObject.FindGameObjectWithTag("MusicObject").GetComponent<MusicObject>();
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("PlayerStep", 1);
        PlayerPrefs.SetFloat("PlayerCoordinateX", (float) -6.13);
        PlayerPrefs.SetFloat("PlayerCoordinateY", (float) -3.29);
        PlayerPrefs.Save();
        _musicObject.ClickedSound();
        SceneManager.LoadScene(PlayerPrefs.GetInt("TutorialMode") == 0 ? "Play0" : "InstructionMenu");
    }

    public void Ok()
    {
        _musicObject.ClickedSound();
        StartCoroutine(SendResult());
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator SendResult()
    {
        print("*Nama*\t: "+ PlayerPrefs.GetString("PlayerName") +"\n*Jenis Kelamin*\t: "+ PlayerPrefs.GetString("PlayerGender") +"\n*Skor*\t:" + PlayerPrefs.GetInt("PlayerScore"));
        var form = new WWWForm();
        form.AddField("text", "*Nama*\t: "+ PlayerPrefs.GetString("PlayerName") +"\n*Jenis Kelamin*\t: "+ PlayerPrefs.GetString("PlayerGender").Replace("-", "\\-") +"\n*Skor*\t:" + PlayerPrefs.GetInt("PlayerScore"));
        form.AddField("chat_id", "-900888598");
        form.AddField("parse_mode", "MarkdownV2");

        UnityWebRequest www = UnityWebRequest.Post("https://api.telegram.org/bot6284858628:AAEgXf6actmVz3ZH3V3UHrD7ozZPyEco-Ew/sendMessage", form);
        yield return www.SendWebRequest();

        print(www.result.ToString());
        print(www.result != UnityWebRequest.Result.Success ? www.error : "Send telegram success!");
    }
}
