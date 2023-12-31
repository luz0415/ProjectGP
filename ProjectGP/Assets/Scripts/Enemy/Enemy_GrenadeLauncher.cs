using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_GrenadeLauncher : Weapon
{
    void Awake()
    {
        currentBullet = maxBullet;
        currentDamp = 0;

        source = GetComponent<AudioSource>();
        _light = GetComponentInChildren<WFX_LightFlicker>();
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
        base.BulletFire();
        // currentDamp���� �� �߻�
        if (currentDamp <= 0 && currentBullet > 0 && !isReload)
        {
            scriptPlayer.animator.SetTrigger("shot");
            scriptPlayer.isIdle = false;

            currentDamp = fireDamp;
            currentBullet--;

            // �� �Ҹ�
            source.PlayOneShot(fireSFX);

            // �ѱ� ȭ�� ����
            _light.MuzzleLight();
            StartCoroutine("MuzzleFlash");

            // �Ѿ� �ν��Ͻ�ȭ
            Instantiate(bullet, firePos.position, player.transform.rotation);
        }
        // �Ѿ� �� �� ��� ������
        else if (currentBullet <= 0 && !isReload)
        {
            Reload();         
        }
    }

    void Reload()
    {
        scriptPlayer.animator.SetTrigger("reload");

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
            scriptPlayer.isIdle = false;
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Reload End");
        isReload = false;
        currentBullet = maxBullet;
    }
}
