using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ammo_Handgun : MonoBehaviour
{
    public float speed; // 총알 발사 속도

    public float damage = 1f;

    public int penetrate = 1;   // 관통 가능한 적 수
    private int hitCount = 0;   // 현재 맞은 적의 수

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
            
            hitCount++;
            if(hitCount >= penetrate)
                Destroy(gameObject);


            // 효과
            //
            //
        }
        else
            Destroy(gameObject);
        
    }
}
