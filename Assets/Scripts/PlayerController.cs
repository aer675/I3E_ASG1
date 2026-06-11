/*
 * Author: Aerica Gan Chai Ting
 * Date: 11 June 2026
 * Description: Manages player interactions using Raycasts, handles UI prompts, and teleports the player to checkpoints.
 */

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Interaction Settings")]
    /// <summary> How far the player can reach to grab an item. </summary>
    public float interactRange = 3f; 
    
    /// <summary> Reference to the player's view. </summary>
    public Camera playerCamera; 

    [Header("UI References")]
    /// <summary> Reference to the UI Text object that says [E] Interact. </summary>
    public GameObject interactPromptUI;

    void Start()
    {
        // If the player has touched a checkpoint, teleport them there immediately upon loading the scene!
        if (GameManager.hasReachedCheckpoint)
        {
            CharacterController cc = GetComponent<CharacterController>();
            
            // We must turn off the controller, move the player, and turn it back on!
            // Unity's physics engine will fight the teleportation if we don't.
            if (cc != null) cc.enabled = false;
            
            transform.position = GameManager.savedCheckpointPosition;
            
            if (cc != null) cc.enabled = true;
        }
    }

    void Update()
    {
        // Run the scanner every single frame to check what the player is looking at
        ScanForInteractables();
    }
    
    /// <summary>
    /// Shoots an invisible raycast from the center of the screen to detect objects and toggle the UI.
    /// </summary>
    private void ScanForInteractables()
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
                // 4. We are looking at a valid item! Turn ON the UI Prompt.
                if (interactPromptUI != null)
                {
                    interactPromptUI.SetActive(true);
                }

                // 5. If they press E while looking directly at it, run the interaction!
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactableObj.Interact();
                }
            }
            else
            {
                // We hit a normal wall or floor. Turn OFF the UI.
                if (interactPromptUI != null) interactPromptUI.SetActive(false);
            }
        }
        else
        {
            // The laser hit absolutely nothing (looking at the sky). Turn OFF the UI.
            if (interactPromptUI != null) interactPromptUI.SetActive(false);
        }
    }
}