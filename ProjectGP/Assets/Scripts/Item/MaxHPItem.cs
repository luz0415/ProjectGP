using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHPItem : MonoBehaviour, IItem
{
    public void Use(GameObject target)
    {
        // 최대체력 증가
        PlayerHP playerHP = target.GetComponent<PlayerHP>();
        playerHP.IncreaseStartHP(1);
    }
}
