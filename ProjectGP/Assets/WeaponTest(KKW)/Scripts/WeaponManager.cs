using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // 무기 종류(게임 오브젝트)
    public GameObject[] weapons;
    public int weaponIndex = 0;

    // 가지고 있는 무기
    public bool[] hasWeapon;   

    // 무기 스크립트
    public Rifle rifle;
    public GrenadeLauncher grenadeLauncher;
    public Handgun handgun;
    public Shotgun shotgun;
    public Ammo_Rifle ammo_Rifle;
    public Ammo_GrenadeLauncher ammo_GL;
    public FX_Explosion explosion;
    public Ammo_Handgun ammo_Handgun;
    public Ammo_Shotgun ammo_Shotgun;

    // 플레이어
    public TestPlayer player;

    // 무기 속성
    public float maxBullet;     // 최대 총알 수
    float currentBullet;        // 현재 남은 총알 수
    public float fireDamp;      // 연사 지연 시간
    float currentDamp;          // 실제 연산에 쓰일 지연시간
    public float reloadTime;    // 재장전에 걸리는 시간
    bool isReload = false;
    bool isIdle = false;

    // Start is called before the first frame update
    void Start()
    {
        // 모든 무기 비활성화
        AllWeaponsDeactive();
        hasWeapon[2] = true; // 권총
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

    // 모든 무기 비활성화
    void AllWeaponsDeactive()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    // 현재 들고 있는 무기에 따른 애니메이션 재생
    void SetWeaponAnimaition(int i)
    {
        // 모든 애니메이션 변수 false
        player.animator.SetBool("isRifle", false);
        player.animator.SetBool("isGL", false);
        player.animator.SetBool("isHG", false);
        player.animator.SetBool("isSG", false);

        // 현재 들고 있는 무기 애니메이션 변수 true
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

    // MachineArms(재장전 속도 20% 감소)
    void DecreaseReloadTime()
    {
        rifle.reloadTime *= 0.8f;
        grenadeLauncher.reloadTime *= 0.8f;
        handgun.reloadTime *= 0.8f;
        shotgun.reloadTime *= 0.8f;
    }

    // MithrilBullet(관통 +1)
    void IncreasePenetrate()
    {
        ammo_Rifle.penetrate++;
        ammo_Handgun.penetrate++;
        ammo_Shotgun.penetrate++;
    }

    // PlasteelMagazine(탄창UP)
    void IncreseMaxBullet()
    {
        rifle.maxBullet *= 1.2f;
        grenadeLauncher.maxBullet *= 1.2f;
        handgun.maxBullet *= 1.2f;
        shotgun.maxBullet *= 1.2f;
    }

    // SuperDrink(공속 20%증가)
    void DecreaseFireDamp()
    {
        rifle.fireDamp *= 0.8f;
        grenadeLauncher.fireDamp *= 0.8f;
        handgun.fireDamp *= 0.8f;
        shotgun.fireDamp *= 0.8f;
    }

    // BiochemicalWeapon(데미지 두 배)
    void IncreaseDamage()
    {
        ammo_Rifle.damage *= 2f;
        explosion.damage *= 2f;
        ammo_Handgun.damage *= 2f;
        ammo_Shotgun.damage *= 2f;
    }
}
