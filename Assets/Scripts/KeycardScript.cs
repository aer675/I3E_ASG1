/*
 * Author: Aerica Gan Chai Ting
 * Date: 11 June 2026
 * Description: Manages the behavior of the keycard item, including interactions and pickup logic.
 */


using UnityEngine;

public class KeycardScript : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Triggered by the Player's Raycast when they press 'E'.
    /// </summary>
public void Interact()
    {
        print("Level 1 Keycard Picked Up!");
        GameManager.Instance.ShowMessage("Level 1 Keycard Collected! Access to Sector 1 Granted.");
        Destroy(gameObject);
    }
}