/*
 * Author: Aerica Gan Chai Ting
 * Date: 10 June 2026
 * Description: Map fragment logic. Awards points, adds to inventory, triggers lore, plays pickup audio, and destroys itself.
 */

using UnityEngine;

public class CollectibleScript : MonoBehaviour, IInteractable
{
    [Header("Collectible Settings")]
    public int pointsValue = 50; 

    [Header("Game Juice")]
    public AudioClip pickupSound;

    public void Interact()
    {
        // Give points and trigger the GameManager inventory/lore
        GameManager.Instance.AddScore(pointsValue);
        GameManager.Instance.AddMapFragment();

        // Play the pickup sound at this exact 3D location before the object is destroyed!
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        // Destroy the map fragment
        Destroy(gameObject);
    }
}