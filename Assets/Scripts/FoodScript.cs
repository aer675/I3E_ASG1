/*
 * Author: Aerica Gan Chai Ting
 * Date: 12 June 2026
 * Description: Manages food item interactions, allowing the player to heal themselves by consuming food and providing feedback through UI and sound effects.
 */

using UnityEngine;

// We use IInteractable so your Raycast knows you can press 'E' on it!
public class FoodScript : MonoBehaviour, IInteractable
{
    [Header("Item Settings")]
    public int healAmount = 25; 

    [Header("Game Juice")]
    public AudioClip eatSound; // Drag your crunch/eating sound effect here!

    public void Interact()
    {
        // 1. Find the PlayerHealth script currently active in the scene
    PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            // 2. Only let the player eat it if they are actually missing health!
            if (playerHealth.curHealth < playerHealth.maxHealth)
            {
                // Heal the player
                playerHealth.Heal(healAmount);
                
                // Show a nice UI message
                GameManager.Instance.ShowMessage("Ate Canned Food: +" + healAmount + " HP");

                // Play the eating sound effect using our Ghost Speaker trick
                if (eatSound != null)
                {
                    AudioSource.PlayClipAtPoint(eatSound, transform.position);
                }

                // Destroy the food object from the world
                Destroy(gameObject);
            }
            else
            {
                // 3. If they are at full health, don't let them waste the item!
                GameManager.Instance.ShowMessage("Health is already full!");
            }
        }
    }
}