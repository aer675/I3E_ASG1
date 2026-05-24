using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    /// <summary> The player's current health. </summary>
    public int curHealth = 0;

    /// <summary> The player's maximum health. </summary>
    public int maxHealth = 100;
    
    /// <summary> The health bar UI element. </summary>
    public HealthBar healthBar;

    void Start()
    {
        curHealth = maxHealth;
    }

    // ======================
    // DAMAGE 
    // ======================

    /// <summary>
    /// Takes damage from the player by a certain amount.
    /// </summary> <param name="amount">The amount of damage to take.</param>
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

    // ======================
    // HEALING
    // ======================  

    /// <summary>
    /// Heals the player by a certain amount.  
    /// </summary> <param name="amount">The amount of health to restore.</param>
    
        void Heal(int amount)
    {
        curHealth += amount;
        healthBar.SetHealth(curHealth);

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }

    // ======================
    // DEATH
    // ======================
    /// <summary>
    /// Handles the player's death, including playing death animation and triggering game over logic.
    /// </summary>
    void Die()
    {
        print("Player has died.");
        // Add death animation and game over logic here
    }
}