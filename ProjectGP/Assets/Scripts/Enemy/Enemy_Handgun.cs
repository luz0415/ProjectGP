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

    // �� �ٽ� ����� �� ������
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
        // currentDamp���� �� �߻�
        if (currentDamp <= 0 && currentBullet > 0 && !isReload)
        {
            animator.SetTrigger("shot");

            currentDamp = fireDamp;
            currentBullet--;

            // �� �Ҹ�
            source.PlayOneShot(fireSFX);

            // �ѱ� ȭ�� ����
            _light.MuzzleLight();
            StartCoroutine("MuzzleFlash");

            // �Ѿ� �ν��Ͻ�ȭ

            Ammo_Handgun ammo = Instantiate(bullet, firePos.position, animator.transform.rotation).GetComponent<Ammo_Handgun>();
            ammo.didPlayerShoot = false;
        }
        // �Ѿ� �� �� ��� ������
        else if (currentBullet <= 0 && !isReload)
        {
            Reload();
        }
    }

    void Reload()
    {
        animator.SetTrigger("reload");

        Debug.Log("Reload Start");
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

        Debug.Log("Reload End");
        isReload = false;
        currentBullet = maxBullet;
    }
}
