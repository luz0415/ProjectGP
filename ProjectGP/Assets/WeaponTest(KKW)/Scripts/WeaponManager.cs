using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // ���� ����(���� ������Ʈ)
    public GameObject[] weapons;
    public int weaponIndex = 0;

    // ���� ����
    public GameObject[] equip;
    

    // ���� ��ũ��Ʈ
    public Rifle rifle;
    public GrenadeLauncher grenadeLauncher;
    public Handgun handgun;
    public Shotgun shotgun;

    // �÷��̾�
    public TestPlayer player;

    // ���� �Ӽ�
    public float maxBullet;     // �ִ� �Ѿ� ��
    float currentBullet;        // ���� ���� �Ѿ� ��
    public float fireDamp;      // ���� ���� �ð�
    float currentDamp;          // ���� ���꿡 ���� �����ð�
    public float reloadTime;    // �������� �ɸ��� �ð�
    bool isReload = false;


    // Start is called before the first frame update
    void Start()
    {
        // ��� ���� ��Ȱ��ȭ
        AllWeaponsDeactive();
    }

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

    // ��� ���� ��Ȱ��ȭ
    void AllWeaponsDeactive()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    // ���� ��� �ִ� ���⿡ ���� �ִϸ��̼� ���
    void SetWeaponAnimaition(int i)
    {
        // ��� �ִϸ��̼� ���� false
        player.animator.SetBool("isRifle", false);
        player.animator.SetBool("isGL", false);
        player.animator.SetBool("isHG", false);
        player.animator.SetBool("isSG", false);

        // ���� ��� �ִ� ���� �ִϸ��̼� ���� true
        if (weapons[i].name == "Rifle")
        {
            player.animator.SetBool("isRifle", true);
        }
        else if (weapons[i].name == "Grenade Launcher")
        {
            player.animator.SetBool("isGL", true);
        }
        else if (weapons[i].name == "Handgun")
        {
            player.animator.SetBool("isHG", true);
        }
        else if (weapons[i].name == "Shotgun")
        {
            player.animator.SetBool("isSG", true);
        }
    }
}
