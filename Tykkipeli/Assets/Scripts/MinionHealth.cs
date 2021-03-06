﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MinionHealth : NetworkBehaviour
{

    public float startingHealth = 100;

    [SyncVar]
    public float currentHealth = 100;



    bool isDead = false;

    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }

    }

    public void TakeDamage(int amountInInt)
    {
        if (isDead) return;

        currentHealth -= amountInInt;

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    public void Death()
    {
        isDead = true;
        currentHealth = 0;
        Destroy(gameObject);
    }
}
