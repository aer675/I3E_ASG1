/*
 * Author: Aerica Gan Chai Ting
 * Date: 10 June 2026
 * Description: Map fragment logic. Awards points, adds to inventory, triggers lore, and destroys itself.
 */

using UnityEngine;

public class CollectibleScript : MonoBehaviour, IInteractable
{
    [Header("Collectible Settings")]
    public int pointsValue = 50; 

    public void Interact()
    {
        // Give points, trigger the GameManager, and destroy the object
        GameManager.Instance.AddScore(pointsValue);
        GameManager.Instance.AddMapFragment();
        Destroy(gameObject);
    }
}