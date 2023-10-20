using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public float maxBullet;     // 최대 총알 수
    float currentBullet;        // 현재 남은 총알 수
    public float fireDamp;      // 연사 지연 시간
    float currentDamp;          // 실제 연산에 쓰일 지연시간
    public float reloadTime;    // 재장전에 걸리는 시간
    bool isReload = false;      // 재장전중인가?

    public GameObject bullet;   // 총알 프리팹
    public Transform firePos;   // 총구 위치
    public GameObject player;   // 플레이어
    public TestPlayer testPlayer;

    // 총 SFX 관련
    public AudioClip fireSFX;
    private AudioSource source = null;

    // 총구 화염 관련
    public ParticleSystem muzzleFlash;
    private WFX_LightFlicker _light;

    void Start()
    {
        currentBullet = maxBullet;
        currentDamp = 0;

        source = GetComponent<AudioSource>();
        _light = GetComponentInChildren<WFX_LightFlicker>();
    }

    void Update()
    {
        currentDamp -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Fire");
            BulletFire();
        }
    }

    void BulletFire()
    {
        // currentDamp마다 총 발사
        if (currentDamp <= 0 && currentBullet > 0 && !isReload)
        {
            testPlayer.animator.SetTrigger("shot");

            currentDamp = fireDamp;
            currentBullet--;

            // 총 소리
            source.PlayOneShot(fireSFX);

            // 총구 화염 구현
            _light.MuzzleLight();
            StartCoroutine("MuzzleFlash");

            // 총알 인스턴스화
            Instantiate(bullet, firePos.position, player.transform.rotation);
        }
        // 총알 다 쓴 경우 재장전
        else if (currentBullet <= 0 && !isReload)
        {
            testPlayer.animator.SetTrigger("reload");

            Debug.Log("Reload Start");
            isReload = true;
            StartCoroutine(ReloadBullet());

        }
    }
    IEnumerator MuzzleFlash()
    {
        muzzleFlash.Play();

        yield return new WaitForSeconds(0.1f);

        muzzleFlash.Stop();
    }

    IEnumerator ReloadBullet()
    {
        for (float i = reloadTime; i > 0; i -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Reload End");
        isReload = false;
        currentBullet = maxBullet;
    }
}
