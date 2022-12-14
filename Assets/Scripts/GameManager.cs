using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.Patterns;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Space))
            LoadDeathScene();
    }

    public void LoadDeathScene()
    {
        SceneManager.LoadScene("DeathScene");
    }
}
