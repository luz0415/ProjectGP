using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Explosion : MonoBehaviour
{
    private SphereCollider _collider;


    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        // 3초 후 VFX 파괴
        Invoke("_Destroy", 3f);

        // 터지는 충돌 효과 0.1초 후 파괴
        Invoke("EnabledCollider", 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }

    void EnabledCollider()
    {
        _collider.enabled = false;
    }

    void _Destroy()
    {
        Destroy(gameObject);
    }
}
