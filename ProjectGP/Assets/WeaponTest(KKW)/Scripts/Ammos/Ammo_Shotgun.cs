using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Shotgun : MonoBehaviour
{
    public float speed; // �Ѿ� �߻� �ӵ�
    private float speed0; 
    void Start()
    {
        Vector3 fwb = transform.TransformDirection(Vector3.up);
        GetComponent<Rigidbody>().AddForce(fwb * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� �浹��
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy");
            Destroy(gameObject);

            // ȿ��
            //
            //
        }
        // ��ź �Ѿ˳��� �浹 ����
        else if (other.tag == "Weapon")
        {
            Debug.Log("Bullet");
        }
        // 

    }
}
