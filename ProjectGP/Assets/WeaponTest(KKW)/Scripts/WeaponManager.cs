using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    public int weaponIndex = 0;
    public Rifle rifle;
    public GrenadeLauncher grenadeLauncher;

    public TestPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        AllWeaponsDeactive();
    }

    void AllWeaponsDeactive()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            AllWeaponsDeactive();       
            
            weapons[weaponIndex].SetActive(true);
            SetWeaponAnimaition(weaponIndex);

            weaponIndex = (weaponIndex+1) % weapons.Length;
        }
    }

    void SetWeaponAnimaition(int i)
    {
        player.animator.SetBool("isRifle", false);
        player.animator.SetBool("isGL", false);

        if (weapons[i].name == "Rifle")
        {
            player.animator.SetBool("isRifle", true);
        }
        else if (weapons[i].name == "Grenade Launcher")
        {
            player.animator.SetBool("isGL", true);
        }
    }
}
