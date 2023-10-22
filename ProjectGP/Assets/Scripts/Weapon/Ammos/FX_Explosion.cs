using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Explosion : MonoBehaviour
{
    private SphereCollider _collider;

    public int damage = 1;
    public bool didPlayerShoot = true;

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
        LivingEntity livingEntity = other.GetComponent<LivingEntity>();
        if (livingEntity != null)
        {
            if (other.tag == "Player")
            {
                if (didPlayerShoot)
                {
                    return;
                }
                else
                {
                    livingEntity.OnDamage(damage, transform.position, Vector3.zero);
                }
            }
            else if (other.tag == "Enemy")
            {
                if (didPlayerShoot)
                {
                    livingEntity.OnDamage(damage, transform.position, Vector3.zero);
                }
                else
                {
                    return;
                }
            }
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
