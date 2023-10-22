using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxBullet;     // 최대 총알 수
    protected float currentBullet;        // 현재 남은 총알 수
    public float fireDamp;      // 연사 지연 시간
    protected float currentDamp;          // 실제 연산에 쓰일 지연시간
    public float reloadTime;    // 재장전에 걸리는 시간
    protected bool isReload = false;      // 재장전중인가?

    public GameObject bullet;   // 총알 프리팹
    public Transform firePos;   // 총구 위치
    public GameObject player;   // 플레이어
    public TestPlayer testPlayer;

    // 총 SFX 관련
    public AudioClip fireSFX;
    protected AudioSource source = null;

    // 총구 화염 관련
    public ParticleSystem muzzleFlash;
    protected WFX_LightFlicker _light;

    public bool isArmed = false;

    // MachineArms(재장전 속도 20% 감소)
    public void DecreaseReloadTime(float ratio)
    {
        reloadTime *= ratio;
    }

    // PlasteelMagazine(탄창UP)
    public void IncreseMaxBullet(float ratio)
    {
        maxBullet = (int)(maxBullet * 1.2f);
    }

    // SuperDrink(공속 20%증가)
    public void DecreaseFireDamp(float ratio)
    {
        ratio = 2 - ratio;
        fireDamp *= 0.8f;
    }
}
