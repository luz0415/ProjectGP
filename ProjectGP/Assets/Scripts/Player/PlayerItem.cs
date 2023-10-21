using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public int coin;

    public bool CanBuy(int cost)
    {
        if (coin >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Coin item = other.GetComponent<Coin>();
        if (item != null)
        {
            item.Use(gameObject);
        }
    }
}
