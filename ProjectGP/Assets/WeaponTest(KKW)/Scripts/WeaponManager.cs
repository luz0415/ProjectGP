using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // ���� ����(���� ������Ʈ)
    public GameObject[] weapons;
    public int weaponIndex = 0;

    // ������ �ִ� ����
    public bool[] hasWeapon;   

    // ���� ��ũ��Ʈ
    public Rifle rifle;
    public GrenadeLauncher grenadeLauncher;
    public Handgun handgun;
    public Shotgun shotgun;
    public Ammo_Rifle ammo_Rifle;
    public Ammo_GrenadeLauncher ammo_GL;
    public FX_Explosion explosion;
    public Ammo_Handgun ammo_Handgun;
    public Ammo_Shotgun ammo_Shotgun;

    // �÷��̾�
    public TestPlayer player;

    // ���� �Ӽ�
    public float maxBullet;     // �ִ� �Ѿ� ��
    float currentBullet;        // ���� ���� �Ѿ� ��
    public float fireDamp;      // ���� ���� �ð�
    float currentDamp;          // ���� ���꿡 ���� �����ð�
    public float reloadTime;    // �������� �ɸ��� �ð�
    bool isReload = false;
    bool isIdle = false;

    // Start is called before the first frame update
    void Start()
    {
        // ��� ���� ��Ȱ��ȭ
        AllWeaponsDeactive();
        hasWeapon[2] = true; // ����
    }

    void Update()
    {
        CheckIsIdle();
        if (Input.GetKeyDown(KeyCode.Tab) && isIdle == false)
        {
            Swap();
        }
    }

    void CheckIsIdle()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(1).IsName("Rifle@Idle") ||
            player.animator.GetCurrentAnimatorStateInfo(1).IsName("GL@Idle") ||
            player.animator.GetCurrentAnimatorStateInfo(1).IsName("Handgun@Idle") ||
            player.animator.GetCurrentAnimatorStateInfo(1).IsName("Shotgun@Idle"))
        {
            isIdle = true;
        }
        else
            isIdle = false;
    }

    void Swap()
    {
        do {
            weaponIndex = (weaponIndex + 1) % weapons.Length;
        } while(hasWeapon[weaponIndex] == false);

        AllWeaponsDeactive();

        weapons[weaponIndex].SetActive(true);
        SetWeaponAnimaition(weaponIndex);
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

    // MachineArms(������ �ӵ� 20% ����)
    void DecreaseReloadTime()
    {
        rifle.reloadTime *= 0.8f;
        grenadeLauncher.reloadTime *= 0.8f;
        handgun.reloadTime *= 0.8f;
        shotgun.reloadTime *= 0.8f;
    }

    // MithrilBullet(���� +1)
    void IncreasePenetrate()
    {
        ammo_Rifle.penetrate++;
        ammo_Handgun.penetrate++;
        ammo_Shotgun.penetrate++;
    }

    // PlasteelMagazine(źâUP)
    void IncreseMaxBullet()
    {
        rifle.maxBullet *= 1.2f;
        grenadeLauncher.maxBullet *= 1.2f;
        handgun.maxBullet *= 1.2f;
        shotgun.maxBullet *= 1.2f;
    }

    // SuperDrink(���� 20%����)
    void DecreaseFireDamp()
    {
        rifle.fireDamp *= 0.8f;
        grenadeLauncher.fireDamp *= 0.8f;
        handgun.fireDamp *= 0.8f;
        shotgun.fireDamp *= 0.8f;
    }

    // BiochemicalWeapon(������ �� ��)
    void IncreaseDamage()
    {
        ammo_Rifle.damage *= 2f;
        explosion.damage *= 2f;
        ammo_Handgun.damage *= 2f;
        ammo_Shotgun.damage *= 2f;
    }
}
