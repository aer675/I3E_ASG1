/*
 * Author: Aerica Gan Chai Ting
 * Date: 24 May 2026
 * Description: Manages player health, including taking damage, healing, and handling death.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
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
    public void TakeDamage(int amount)
    {
        curHealth -= amount;
        print("Player took damage, current health: " + curHealth);
        healthBar.SetHealth(curHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(curHealth);
        }

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
    public void Heal(int amount)
    {
        curHealth += amount;
        healthBar.SetHealth(curHealth);

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(curHealth);
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
        // !!!!!!!! Add death animation and game over logic here!!!!!!!!
    }
}