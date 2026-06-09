/*
 * Author: Aerica Gan Chai Ting
 * Date: 8 June 2026
 * Description: Manages damage zones, applying damage to the player when they enter the zone.
 */

using UnityEngine;

public class DamageZone : MonoBehaviour
{
    /// <summary>
    /// Applies damage to the player when they stay within the damage zone.
    /// </summary>
    /// <param name="other">The collider that is staying within the damage zone.</param>

    public int damageAmount = 10; // Amount of damage to apply per second
    
    // Add a timer variable to control how often damage is applied
    private float damageTimer = 0f;
    private const float damageInterval = 1f; // Apply damage every 1 second

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageInterval)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if(playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount); // Apply the specified damage per second
                }

                damageTimer = 0f; // Reset the timer
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            damageTimer = 0f; // Reset the timer when the player exits the damage zone
            print("Player exited damage zone, no more damage applied.");
        }
    }
}
