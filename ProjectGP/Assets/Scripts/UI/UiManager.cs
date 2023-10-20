using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.ExceptionServices;

public class UiManager : MonoBehaviour
{
    public static UiManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UiManager>();
            }
            return m_instance;
        }
    }

    private static UiManager m_instance;

    public GameObject OptionMenu;
    public TextMeshProUGUI playerCoin;

    public RectTransform UIHeart;
    public GameObject heartPrefab;
    private List<Image> hearts = new List<Image>();

    public int health;
    bool OptionMenu_On = false;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        OptionMenu.SetActive(false);

        health = 0;

        int maxHealth = GameManager.instance.player.GetComponent<PlayerHP>().startingHP;
        MaxHealthUp(maxHealth);
        SetCoinUI(0);
    }

    public void SceneChange() // -------------------------------------------------시작화면
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
    } // -------------------------------------------------시작화면

    public void HealthDown(int downHealth)
    {
        for (int i = 0; i < downHealth; i++)
        {
            if (health == 0) return;
            health--;
            hearts[health].color = new Color(0, 0, 0, 0.4f);
        }
    }

    public void HealthUp(int upHealth)
    {
        for(int i = 0; i < upHealth; i++)
        {
            if (health == hearts.Count) return;
            hearts[health].color = new Color(0.915f, 0.39f, 0.39f, 1);
            health++;
        }
    }

    public void MaxHealthDown(int downHealth)
    {
        if (health == 1) return;
        for (int i = 0; i < downHealth; i++)
        {
            GameObject destroyObject = hearts[hearts.Count - 1].gameObject;
            health--;
            hearts.RemoveAt(health);
            Destroy(destroyObject);
        }
    }

    public void MaxHealthUp(int upHealth)
    {
        for (int i = 0; i < upHealth; i++)
        {
            Image newHealth;
            if (health == 0)
            {
                Vector3 newHealthPosition = new Vector3(90f, 995f, 0f);
                newHealth = Instantiate(heartPrefab, newHealthPosition, Quaternion.identity).GetComponent<Image>();
            }
            else
            {
                Vector3 newHealthPosition =
                    new Vector3(hearts[hearts.Count - 1].rectTransform.position.x + 100, hearts[hearts.Count - 1].rectTransform.position.y, hearts[hearts.Count - 1].rectTransform.position.z);
                newHealth = Instantiate(hearts[hearts.Count - 1].gameObject, newHealthPosition, Quaternion.identity).GetComponent<Image>();
            }
            
            hearts.Add(newHealth);
            newHealth.rectTransform.parent = UIHeart;
            newHealth.color = new Color(0, 0, 0, 0.4f);
             
            HealthUp(1);
        }
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

    public void SetCoinUI(int coin)
    {
        playerCoin.text = string.Format("{0:n0}", coin);
    }

    void LateUpdate()
    {

        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(OptionMenu_On)
               Option_off();
            else
                Option_on();
        }
    }
}
