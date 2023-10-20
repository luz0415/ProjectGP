using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // 무기 종류(게임 오브젝트)
    public GameObject[] weapons;
    public int weaponIndex = 0;

    // 장착 무기
    public GameObject[] equip;
    

    // 무기 스크립트
    public Rifle rifle;
    public GrenadeLauncher grenadeLauncher;
    public Handgun handgun;
    public Shotgun shotgun;

    // 플레이어
    public TestPlayer player;

    // 무기 속성
    public float maxBullet;     // 최대 총알 수
    float currentBullet;        // 현재 남은 총알 수
    public float fireDamp;      // 연사 지연 시간
    float currentDamp;          // 실제 연산에 쓰일 지연시간
    public float reloadTime;    // 재장전에 걸리는 시간
    bool isReload = false;


    // Start is called before the first frame update
    void Start()
    {
        // 모든 무기 비활성화
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
}
