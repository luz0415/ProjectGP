using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // ���� ����(���� ������Ʈ)
    public Weapon[] weapons;
    public int weaponIndex = 0;

    // ������ �ִ� ����
    public bool[] hasWeapon;


    public Ammo_Rifle ammo_Rifle;
    public Ammo_GrenadeLauncher ammo_GL;
    public Ammo_Handgun ammo_Handgun;
    public AmmoPackage_SG ammos_Shotgun;
    private Ammo_Shotgun[] ammo_Shotgun;

    private const int AMMO_SHOTGUN_COUNT = 5;

    public FX_Explosion explosion;

    // �÷��̾�
    public TestPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        // ��� ���� ��Ȱ��ȭ
        AllWeaponsDeactive();
        hasWeapon[2] = true; // ����

        ammo_Shotgun = new Ammo_Shotgun[AMMO_SHOTGUN_COUNT];
        for(int i = 0; i < AMMO_SHOTGUN_COUNT; i++)
        {
            ammo_Shotgun[i] = ammos_Shotgun.transform.GetChild(i).GetComponent<Ammo_Shotgun>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Swap();
        }
    }

    void CheckIsIdle()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(1).IsName("Rifle@Idle") ||
            player.animator.GetCurrentAnimatorStateInfo(1).IsName("GL@Idle") ||
            player.animator.GetCurrentAnimatorStateInfo(1).IsName("Handgun@Idle") ||
            player.animator.GetCurrentAnimatorStateInfo(1).IsName("Shotgun@Idle") ||
            player.animator.GetCurrentAnimatorStateInfo(1).IsName("Init"))
        {
            player.isIdle = true;
        }
    }

    void Swap()
    {
        CheckIsIdle();
        if (player.isIdle != true)
            return;

        do {
            weaponIndex = (weaponIndex + 1) % weapons.Length;
        } while(hasWeapon[weaponIndex] == false);

        AllWeaponsDeactive();

        weapons[weaponIndex].gameObject.SetActive(true);
        SetWeaponAnimaition(weaponIndex);
    }

    // ��� ���� ��Ȱ��ȭ
    void AllWeaponsDeactive()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
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
    void DecreaseAllReloadTime(float ratio)
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].DecreaseReloadTime(ratio);
        }
    }

    // MithrilBullet(���� +1)
    void IncreasePenetrate()
    {
        ammo_Rifle.penetrate++;
        ammo_Handgun.penetrate++;
        for (int i = 0; i < AMMO_SHOTGUN_COUNT; i++)
        {
            ammo_Shotgun[i].penetrate++;
        }
    }

    // PlasteelMagazine(źâUP)
    void IncreseAllMaxBullet(float ratio)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].IncreseMaxBullet(ratio);
        }
    }

    // SuperDrink(���� 20%����)
    void DecreaseFireDamp(float ratio)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].DecreaseFireDamp(ratio);
        }
    }

    // BiochemicalWeapon(������ �� ��)
    void IncreaseDamage()
    {
        ammo_Rifle.damage *= 2f;
        explosion.damage *= 2f;
        ammo_Handgun.damage *= 2f;
        for (int i = 0; i < AMMO_SHOTGUN_COUNT; i++)
        {
            ammo_Shotgun[i].damage *= 2f;
        }
    }
}
