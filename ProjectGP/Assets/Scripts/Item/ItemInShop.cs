using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemInShop : MonoBehaviour
{
    public int cost;
    public string itemType;
    public string itemName;
    public string itemDescription;

    private string itemString;
    public TextMeshPro itemText;
    private IItem item;
    private GameObject targetObject;
    private PlayerItem playerItem;

    private bool canBuy;


    private void Start()
    {
        item = GetComponent<IItem>();
        itemString = CreateItemText();
        itemText.text = itemString;

        itemText.fontSize = 1.5f;
        canBuy = false;
    }

    private string CreateItemText()
    {
        string text = itemType + " - " + itemName + "\r\n\r\n" + itemDescription + "\r\n\r\n<sprite=0> " + cost.ToString() + " (Ctrl·Î ±¸¸Å)";
        return text;
    }

    private void Update()
    {
        if(canBuy && Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(playerItem.coin >= cost)
            {
                playerItem.coin -= cost;
                UiManager.instance.SetCoinUI(playerItem.coin);
                item.Use(targetObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targetObject = other.gameObject;
            if(playerItem == null)
            {
               playerItem = targetObject.GetComponent<PlayerItem>();
            }

            itemText.gameObject.SetActive(true);

            if (playerItem.coin < cost)
            {
                itemText.color = Color.red;
            }

            canBuy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            itemText.gameObject.SetActive(false);
            canBuy = false;
        }
    }
}
