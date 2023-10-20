using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : LivingEntity
{
    private Animator playerAnimator;
    private Material playerMaterial;

    public float damageEffectTime = 0.1f;

    //private Slider playerHPSlider;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMaterial = GetComponentInChildren<Renderer>().material;
        //playerHPSlider = GetComponentInChildren<Slider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //playerHPSlider.value = startingHP;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        StartCoroutine(DamageEffect());
        //playerHPSlider.value -= damage;
    }

    private IEnumerator DamageEffect()
    {
        playerMaterial.color = Color.red;
        yield return new WaitForSeconds(damageEffectTime);
        playerMaterial.color = Color.white;
    }

    public override void RestoreHP(float restoreHP)
    {
        base.RestoreHP(restoreHP);
        //playerHPSlider.value = HP;
    }

    public void IncreaseStartHP(float increaseHP)
    {
        startingHP += increaseHP;
        HP += increaseHP;
        //playerHPSlider.maxValue = startingHP;
        //playerHPSlider.value = HP;
    }

    public override void Dead()
    {
        base.Dead();

        playerAnimator.SetTrigger("Die");

        //playerMovement.enabled = false;
        //playerShooter.enabled = false;

        GameManager.instance.EndGame();
    }
}