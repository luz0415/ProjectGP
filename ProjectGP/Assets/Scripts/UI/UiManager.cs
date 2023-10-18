using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject OptionMenu;

    public Image[] UIhealth;

    public int health;
    bool OptionMenu_On = false;

    void Start()
    {
        OptionMenu.SetActive(false);
    }

    public void SceneChange() // -------------------------------------------------����ȭ��
    {
        SceneManager.LoadScene("KJS_TestScene 1");
    }

    public void gameOption()
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

    public void option_goto_main()
    {
        OptionMenu.SetActive(false);
        OptionMenu_On = false;
    } // -------------------------------------------------����ȭ��

    public void HealthDown()
    {
        health--;
        UIhealth[health].color = new Color(0, 0, 0, 0.4f);
    }

    public void HealthUp()
    {
        UIhealth[health].color = new Color(0.915f, 0.39f, 0.39f, 1);
        health++;
    }

    void Option_off()
    {
        OptionMenu.SetActive(false);
        OptionMenu_On = false;
    }

    void Option_on()
    {
        OptionMenu.SetActive(true);
        OptionMenu_On = true;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "KJS_TestScene 1" && Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(OptionMenu_On)
               Option_off();
            else
                Option_on();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HealthDown();
        }
        else if (Input.GetKeyDown(KeyCode.U)) 
        {
            HealthUp();
        }
    }
}