using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Explosion : MonoBehaviour
{
    private SphereCollider _collider;


    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        // 3�� �� VFX �ı�
        Invoke("_Destroy", 3f);

        // ������ �浹 ȿ�� 0.1�� �� �ı�
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
