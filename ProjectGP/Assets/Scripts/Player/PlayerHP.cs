using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : LivingEntity
{
    private Animator playerAnimator;
    private Material playerMaterial;

    public float damageEffectTime = 0.1f;

    public bool canRevive = false;
    public bool damageDouble = false;
    public bool isDodging = false;

    public ParticleSystem bloodEffect;

    //private Slider playerHPSlider;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMaterial = GetComponentInChildren<Renderer>().material;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDamage(int damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (isDodging) return;
        if (damageDouble) damage *= 2;

        base.OnDamage(damage, hitPoint, hitNormal);
        UiManager.instance.HealthDown(damage);
        StartCoroutine(DamageEffect());
    }

    private IEnumerator DamageEffect()
    {
        bloodEffect.Play();

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
    }

    public void DecreaseStartHP(int decreaseHP)
    {
        startingHP -= decreaseHP;
        if(HP > startingHP)
        {
            HP -= decreaseHP;
        }
        UiManager.instance.MaxHealthDown(decreaseHP);
    }

    public override void Dead()
    {
        if(canRevive)
        {
            RestoreHP(startingHP);
            canRevive = false;
            return;
        }

        base.Dead();

        GameManager.instance.EndGame();
    }
}