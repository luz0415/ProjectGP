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

    public TextMeshPro itemText;
    private IItem item;
    private GameObject targetObject;
    private PlayerItem playerItem;

    private bool canBuy;


    private void Start()
    {
        item = GetComponent<IItem>();

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

            itemText.text = CreateItemText();
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
