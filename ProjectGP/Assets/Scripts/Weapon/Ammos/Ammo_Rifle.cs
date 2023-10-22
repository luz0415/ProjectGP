using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Rifle : MonoBehaviour
{
    public float speed; // �Ѿ� �߻� �ӵ�

    public float damage = 1f;

    public int penetrate = 1;   // ���� ������ �� ��
    private int hitCount = 0;   // ���� ���� ���� ��

    void Start()
    {
        Vector3 fwb = transform.TransformDirection(Vector3.forward);
        GetComponent<Rigidbody>().AddForce(fwb * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        // ���� �浹��
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy");

            hitCount++;
            if (hitCount >= penetrate)
                Destroy(gameObject);
            
            // ȿ��
            //
            //
        }
        // �� �ƴ� �ٸ� �Ͱ� �浹
        else
            Destroy(gameObject);
    }
}
