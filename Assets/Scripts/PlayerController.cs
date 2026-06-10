/*
 * Author: Aerica Gan Chai Ting
 * Date: 10 June 2026
 * Description: Manages player interactions using Raycasts to detect and trigger IInteractable objects.
 */

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Interaction Settings")]
    /// <summary> How far the player can reach to grab an item. </summary>
    public float interactRange = 3f; 
    
    /// <summary> Reference to the player's view. </summary>
    public Camera playerCamera; 

    void Update()
    {
        // Listen for the player pressing the 'E' key every frame
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }
    

    /// <summary>
    /// Shoots an invisible raycast from the center of the screen forward to detect interactable objects.
    /// </summary>
    private void TryInteract()
    {
        // Safety check: ensure we actually linked the camera in the Inspector!
        if (playerCamera == null) 
        {
            Debug.LogWarning("Player Camera is not assigned in the PlayerController!");
            return;
        }

        // 1. Create the laser beam from the exact center of the screen
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hitInfo;

        // 2. Shoot the laser beam out by the distance of our interactRange
        if (Physics.Raycast(ray, out hitInfo, interactRange))
        {
            // 3. We hit something! Check if it has a script using the IInteractable rule attached
            IInteractable interactableObj = hitInfo.collider.GetComponent<IInteractable>();

            if (interactableObj != null)
            {
                // 4. It does! Trigger its specific Interact function (like picking up the map)
                interactableObj.Interact();
            }
        }
    }
}