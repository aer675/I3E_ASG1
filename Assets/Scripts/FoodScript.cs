/*
 * Author: Aerica Gan Chai Ting
 * Date: 12 June 2026
 * Description: Manages food item interactions, allowing the player to heal themselves by consuming food and providing feedback through UI and sound effects.
 */

using UnityEngine;

// This class implements the IInteractable interface, meaning it must have an Interact() method that defines what happens when the player interacts with this food item in the world.
public class FoodScript : MonoBehaviour, IInteractable
{
    [Header("Item Settings")]
    public int healAmount = 25; 

    [Header("Game Juice")]
    public AudioClip eatSound; 

    public void Interact()
    {
        // Find the PlayerHealth script in the scene to access the player's health information and healing method.
    PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            // Check if the player's current health is below the maximum health before allowing them to consume the food and heal.
            if (playerHealth.curHealth < playerHealth.maxHealth)
            {
                // Heal the player by the specified heal amount using the Heal method in the PlayerHealth script.
                playerHealth.Heal(healAmount);
                
                // Provide feedback to the player by showing a message in the UI that indicates how much health was restored.
                GameManager.Instance.ShowMessage("Ate Canned Food: +" + healAmount + " HP");

                // Play the eating sound effect at the location of the food item before it is destroyed, giving audio feedback for the interaction.
                if (eatSound != null)
                {
                    AudioSource.PlayClipAtPoint(eatSound, transform.position);
                }

                // Destroy the food object from the world
                Destroy(gameObject);
            }
            else
            {
                // If the player's health is already full, show a message indicating that they cannot consume the food and that their health is already at maximum.
                GameManager.Instance.ShowMessage("Health is already full!");
            }
        }
    }
}