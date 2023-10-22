using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public GameObject OptionMenu;
    public GameObject MainMenu;

    bool OptionMenu_On = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "KJS_TestScene")
        {
            MainMenu.SetActive(true);
        }
        OptionMenu.SetActive(false);
    }

    public void GameStart() // -------------------------------------------------시작화면
    {
        MainMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Option_off()
    {
        OptionMenu.SetActive(false);
        OptionMenu_On = false;
    }

    public void Option_on()
    {
        OptionMenu.SetActive(true);
        OptionMenu_On = true;
    }

    public void gameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionMenu_On)
                Option_off();
            else
                Option_on();
        }
    }
}
