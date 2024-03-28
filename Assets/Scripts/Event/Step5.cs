using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Step5 : MonoBehaviour
{

    private float _changeTime = 3f;
    private void Awake()
    {
        var instance = Instantiate(Resources.Load<GameObject>(PlayerPrefs.GetString("PlayerPrefabs")),
            GameObject.FindGameObjectWithTag("Player").transform);
        instance.transform.localScale = new Vector3(15, 15, 15);
    }

    private void FixedUpdate()
    {
        _changeTime -= Time.deltaTime;
        if (_changeTime <= 0)
        {
            SceneManager.LoadScene("Play1");
        }
    }
}
