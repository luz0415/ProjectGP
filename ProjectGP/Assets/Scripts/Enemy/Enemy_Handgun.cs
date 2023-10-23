using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy_Handgun : Weapon
{
    private Animator animator;

    void Awake()
    {
        currentBullet = maxBullet;
        currentDamp = 0;

        source = GetComponent<AudioSource>();
        _light = GetComponentInChildren<WFX_LightFlicker>();
        animator = GetComponentInParent<Animator>();
    }

    // 총 다시 들었을 때 재장전
    private void OnEnable()
    {
        Reload();

        _light.GetComponent<Light>().enabled = false;
    }

    void Update()
    {
        currentDamp -= Time.deltaTime;
    }

    public override void BulletFire()
    {
        if (GameManager.instance.isGamePaused) return;
        // currentDamp마다 총 발사
        if (currentDamp <= 0 && currentBullet > 0 && !isReload)
        {
            animator.SetTrigger("shot");

            currentDamp = fireDamp;
            currentBullet--;

            // 총 소리
            source.PlayOneShot(fireSFX);

            // 총구 화염 구현
            _light.MuzzleLight();
            StartCoroutine("MuzzleFlash");

            // 총알 인스턴스화

            Ammo_Handgun ammo = Instantiate(bullet, firePos.position, animator.transform.rotation).GetComponent<Ammo_Handgun>();
            ammo.didPlayerShoot = false;
        }
        // 총알 다 쓴 경우 재장전
        else if (currentBullet <= 0 && !isReload)
        {
            Reload();
        }
    }

    void Reload()
    {
        animator.SetTrigger("reload");

        isReload = true;
        StartCoroutine(ReloadBullet());
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

        isReload = false;
        currentBullet = maxBullet;
    }
}
