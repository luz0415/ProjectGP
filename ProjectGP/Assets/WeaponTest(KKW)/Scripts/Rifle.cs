using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public float maxBullet;     // �ִ� �Ѿ� ��
    float currentBullet;        // ���� ���� �Ѿ� ��
    public float fireDamp;      // ���� ���� �ð�
    float currentDamp;          // ���� ���꿡 ���� �����ð�
    public float reloadTime;    // �������� �ɸ��� �ð�
    bool isReload = false;      // ���������ΰ�?

    public GameObject bullet;   // �Ѿ� ������
    public Transform firePos;   // �ѱ� ��ġ
    public GameObject player;   // �÷��̾�

    public AudioClip fireSFX;
    private AudioSource source = null;

    void Start()
    {
        currentBullet = maxBullet;
        currentDamp = 0;

        source = GetComponent<AudioSource>();
    }
    
    void BulletFire()
    {
        // currentDamp���� �� �߻�
        if(currentDamp <= 0 && currentBullet >0 && !isReload)
        {
            currentDamp = fireDamp;
            currentBullet--;

            source.PlayOneShot(fireSFX);

            Instantiate(bullet, firePos.position, player.transform.rotation);
        } 
        // �Ѿ� �� �� ��� ������
        else if(currentBullet <= 0 && !isReload)
        {
            Debug.Log("Reload Start");
            isReload = true;
            StartCoroutine(ReloadBullet());
           
        }
    }

    IEnumerator ReloadBullet()
    {
        for(float i = reloadTime; i > 0; i -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Reload End");
        isReload = false;
        currentBullet = maxBullet;
    }

    void Update()
    {
        currentDamp -= Time.deltaTime;
        if(Input.GetMouseButtonDown(0))
        {
            BulletFire();
        }
    }
}
