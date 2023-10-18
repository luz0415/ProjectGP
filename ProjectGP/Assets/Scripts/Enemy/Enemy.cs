using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action onDeath;

    private void OnTriggerEnter(Collider other)
    {
        if (onDeath != null) onDeath();
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
