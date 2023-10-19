using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action onDeath;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (onDeath != null) onDeath();
            Destroy(gameObject);
        }
    }
}
