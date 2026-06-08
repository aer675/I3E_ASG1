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
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Apply 1 damage per second
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            print("Player exited damage zone, no more damage applied.");
        }
    }
}
