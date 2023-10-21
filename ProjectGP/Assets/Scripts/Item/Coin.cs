using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    private ParticleSystem particle;
    public GameObject pickupEffect;
    public int coin;

    private void OnEnable()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Play();
    }

    public void Use(GameObject target)
    {
        GetCoin(target.GetComponent<PlayerItem>());
        Pickup();
    }

    private void GetCoin(PlayerItem playerItem)
    {
        playerItem.coin += coin;
        UiManager.instance.SetCoinUI(playerItem.coin);
    }

    void Pickup()
    {
        pickupEffect.SetActive(true);
        Destroy(gameObject);
    }

}
