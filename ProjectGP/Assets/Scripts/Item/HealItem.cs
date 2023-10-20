using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour, IItem
{
    public void Use(GameObject target)
    {
        // 최대체력 증가
        PlayerHP playerHP = target.GetComponent<PlayerHP>();
        playerHP.RestoreHP(1);
    }
}
