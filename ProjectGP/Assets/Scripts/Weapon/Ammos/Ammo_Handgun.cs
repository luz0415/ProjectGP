using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Handgun : MonoBehaviour
{
    public float speed; // �Ѿ� �߻� �ӵ�

    public int damage = 1;

    public int penetrate = 1;   // ���� ������ �� ��
    private int hitCount = 0;   // ���� ���� ���� ��

    public bool didPlayerShoot = true;

    void Start()
    {
        Vector3 fwb = transform.TransformDirection(Vector3.forward);
        GetComponent<Rigidbody>().AddForce(fwb * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            return;
        }
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
                    Destroy(gameObject);
                }
            }
            else if (other.tag == "Enemy")
            {
                if (didPlayerShoot)
                {
                    livingEntity.OnDamage(damage, transform.position, Vector3.zero);

                    hitCount++;
                    if (hitCount >= penetrate)
                        Destroy(gameObject);
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
