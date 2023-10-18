using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private ParticleSystem particle;
    public GameObject pickupEffect;
    public int coin;

    private void OnEnable()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetCoin(other.gameObject.GetComponent<PlayerCoin>());
            Pickup();
        }
    }

    private void GetCoin(PlayerCoin playerCoin)
    {
        playerCoin.coin = coin;
    }

    void Pickup()
    {
        pickupEffect.SetActive(true);
        Destroy(gameObject);
    }

}
