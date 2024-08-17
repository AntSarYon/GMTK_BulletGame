using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        //Debug.Log("Player takes " + amount + " damage, current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            NpcStateManager t = GetComponent<NpcStateManager>();
            t.SwitchState(t.defeatState);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}