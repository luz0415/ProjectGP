using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Rifle : MonoBehaviour
{
    public float speed; // 총알 발사 속도

    void Start()
    {
        Vector3 fwb = transform.TransformDirection(Vector3.forward);
        GetComponent<Rigidbody>().AddForce(fwb * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        // 적과 충돌시
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy");
            Destroy(gameObject);

            // 효과
            //
            //
        }
        // 적 아닌 다른 것과 충돌
        else
            Destroy(gameObject);
    }
}
