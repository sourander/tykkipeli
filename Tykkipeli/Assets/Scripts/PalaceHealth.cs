using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PalaceHealth : NetworkBehaviour
{

    public float startingHealth = 100;

    [SyncVar]
    public float currentHealth = 100;

    bool isDead = false;

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
