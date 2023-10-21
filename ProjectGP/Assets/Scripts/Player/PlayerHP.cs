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

    public override void OnDamage(int damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        UiManager.instance.HealthDown(damage);
        StartCoroutine(DamageEffect());
        //playerHPSlider.value -= damage;
    }

    private IEnumerator DamageEffect()
    {
        playerMaterial.color = Color.red;
        yield return new WaitForSeconds(damageEffectTime);
        playerMaterial.color = Color.white;
    }

    public override void RestoreHP(int restoreHP)
    {
        base.RestoreHP(restoreHP);
        UiManager.instance.HealthUp(restoreHP);
    }

    public void IncreaseStartHP(int increaseHP)
    {
        startingHP += increaseHP;
        HP += increaseHP;
        UiManager.instance.MaxHealthUp(increaseHP);
        //playerHPSlider.maxValue = startingHP;
        //playerHPSlider.value = HP;
    }

    public void DecreaseStartHP(int decreaseHP)
    {
        startingHP -= decreaseHP;
        if(HP > startingHP)
        {
            HP -= decreaseHP;
        }
        UiManager.instance.MaxHealthDown(decreaseHP);
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