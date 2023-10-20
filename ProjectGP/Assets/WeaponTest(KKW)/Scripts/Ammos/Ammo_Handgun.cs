using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ammo_Handgun : MonoBehaviour
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
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy");
            Destroy(gameObject);

            // ȿ��
            //
            //
        }
        else
            Destroy(gameObject);
        
    }
}
