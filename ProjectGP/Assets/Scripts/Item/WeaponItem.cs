using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour, IItem
{
    public int thisWeaponIndex;
    public void Use(GameObject target)
    {
        // 무기 교체
        int weaponCount = 0;
        for(int i = 0;  i < WeaponManager.instance.hasWeapon.Length; i++)
        {
            if (WeaponManager.instance.hasWeapon[i])
                weaponCount++;
        }

        if(weaponCount == 2)
        {
            WeaponManager.instance.hasWeapon[WeaponManager.instance.weaponIndex] = false;
        }
        WeaponManager.instance.hasWeapon[thisWeaponIndex] = true;
        WeaponManager.instance.weaponIndex = thisWeaponIndex;
    }
}
