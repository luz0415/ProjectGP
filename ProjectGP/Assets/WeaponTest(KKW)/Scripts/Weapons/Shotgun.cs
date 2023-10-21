using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
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
        // currentDamp���� �� �߻�
        if (currentDamp <= 0 && currentBullet > 0 && !isReload)
        {
            testPlayer.animator.SetTrigger("shot");

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
