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

        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.I))
            LoadDeathScene();
#endif
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadDeathScene()
    {
        SceneManager.LoadScene("DeathScene");
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }
}
