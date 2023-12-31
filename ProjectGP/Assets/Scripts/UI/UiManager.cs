using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.ComponentModel;
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
    public GameObject RestartGame;
    public PlayerItem playerItem;
    public TextMeshProUGUI playerCoin;

    public RectTransform UIHeart;
    public GameObject heartPrefab;
    public List<Image> hearts = new List<Image>();

    public int health;
    bool OptionMenu_On = false;

    public GameObject MapUI;

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
        health = 0;
        OptionMenu.SetActive(false);
        RestartGame.SetActive(false);

        int maxHealth = GameManager.instance.player.GetComponent<PlayerHP>().startingHP;
        MaxHealthUp(maxHealth);
        SetCoinUI(0);
    }

    public void Restart()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.RestartGame();
        }
        SceneManager.LoadScene("KJS_TestScene");
    }

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
        for (int i = 0; i < upHealth; i++)
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
            if(health == hearts.Count)
            {
                health--;
            }
            hearts.RemoveAt(hearts.Count - 1);
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

    public void SetCoinUI(int coin)
    {
        playerCoin.text = string.Format("{0:n0}", coin);
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

        if(health == 0 && (SceneManager.GetActiveScene().name != "KJS_TestScene"))
        {
            Time.timeScale = 0f;
            RestartGame.SetActive(true);
        }
    }
}
