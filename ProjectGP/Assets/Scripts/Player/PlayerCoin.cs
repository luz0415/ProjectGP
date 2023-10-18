using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : MonoBehaviour
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
}
