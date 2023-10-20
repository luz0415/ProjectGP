using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ammo_Handgun : MonoBehaviour
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
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy");
            Destroy(gameObject);

            // 효과
            //
            //
        }
        else
            Destroy(gameObject);
        
    }
}
