using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Shotgun : MonoBehaviour
{
    public float speed; // 총알 발사 속도
    private float speed0; 
    void Start()
    {
        Vector3 fwb = transform.TransformDirection(Vector3.up);
        GetComponent<Rigidbody>().AddForce(fwb * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 적과 충돌시
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy");
            Destroy(gameObject);

            // 효과
            //
            //
        }
        // 산탄 총알끼리 충돌 방지
        else if (other.tag == "Weapon")
        {
            Debug.Log("Bullet");
        }
        // 

    }
}
