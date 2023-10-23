using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shotgun : Weapon
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
            AmmoPackage_SG ammo = Instantiate(bullet, firePos.position, animator.transform.rotation).GetComponent<AmmoPackage_SG>();
            Ammo_Shotgun[] ammo_Shotguns = ammo.GetComponentsInChildren<Ammo_Shotgun>();
            for(int i = 0; i < ammo_Shotguns.Length; i++)
            {
                ammo_Shotguns[i].GetComponent<Renderer>().material.color = Color.red;
                ammo_Shotguns[i].didPlayerShoot = false;
            }
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
