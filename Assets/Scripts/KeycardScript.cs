/*
 * Author: Aerica Gan Chai Ting
 * Date: 11 June 2026
 * Description: Manages the behavior of the keycard item, including interactions and pickup logic.
 */

using UnityEngine;

public class KeycardScript : MonoBehaviour, IInteractable
{
    [Header("Keycard Settings")]
    [Tooltip("Enter 1 for Level 1, 2 for Level 2, etc.")]
    public int keycardLevel = 1; 

    /// <summary>
    /// Triggered by the Player's Raycast. Grants the specific clearance level.
    /// </summary>
    public void Interact()
    {
        // 1. Check which level this card is, and update the GameManager and UI!
        if (keycardLevel == 1)
        {
            GameManager.Instance.hasLevel1Card = true; // Tells the GameManager you have it!
            GameManager.Instance.ShowMessage("Level 1 Keycard Collected! Access Granted.");
            print("Level 1 Keycard Picked Up!");
        }
        else if (keycardLevel == 2)
        {
            GameManager.Instance.hasLevel2Card = true; 
            GameManager.Instance.ShowMessage("Level 2 Keycard Collected! Security Override.");
            print("Level 2 Keycard Picked Up!");
        }
        
        // 2. Destroy the physical 3D object from the world
        Destroy(gameObject);
    }
}