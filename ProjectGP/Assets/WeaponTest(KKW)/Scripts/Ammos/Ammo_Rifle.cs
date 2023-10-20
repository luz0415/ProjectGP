using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Rifle : MonoBehaviour
{
    public float speed; // �Ѿ� �߻� �ӵ�

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
