using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    private Animator enemyAnimator;
    private Material enemyMaterial;

    public float damageEffectTime = 0.1f;

    public ParticleSystem hitEffect;
    public ParticleSystem deadEffect;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyMaterial = GetComponentInChildren<Renderer>().material;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDamage(int damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        StartCoroutine(DamageEffect());
    }

    private IEnumerator DamageEffect()
    {
        hitEffect.Play();

        enemyMaterial.color = Color.red;
        yield return new WaitForSeconds(damageEffectTime);
        enemyMaterial.color = Color.white;
    }

    public override void RestoreHP(int restoreHP)
    {
        base.RestoreHP(restoreHP);
    }

    public override void Dead()
    {
        base.Dead();

        StartCoroutine(DeadCoroutine());
    }

    private IEnumerator DeadCoroutine()
    {
        Instantiate(deadEffect, transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(0.0f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead && other.tag == "Player")
        {
            OnDamage(1, Vector3.zero, Vector3.zero);
        }
    }
}
