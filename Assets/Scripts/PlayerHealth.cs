using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    // Health
    public int curHealth = 0;

    // Max Health
    public int maxHealth = 100;
    
    // Health Bar
    public HealthBar healthBar;

    void Start()
    {
        curHealth = maxHealth;
    }

    // Take damage to the player by a certain amount
    void TakeDamage(int amount)
    {
        curHealth -= amount;
        healthBar.SetHealth(curHealth);

        if (curHealth <= 0)
        {
            //Dead
            // Show Game Over Screen
            Die();
        }
    }

    // Heal the player by a certain amount
        void Heal(int amount)
    {
        curHealth += amount;
        healthBar.SetHealth(curHealth);

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }
}