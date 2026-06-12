/*
 * Author: Aerica Gan Chai Ting
 * Date: 11 June 2026
 * Description: Manages player interactions using Raycasts, handles UI prompts, teleports to checkpoints, and plays footstep audio.
 */

using UnityEngine;
using System.Collections; // Required for the IEnumerator time delay!

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

    [Header("Footstep Audio")]
    public AudioSource footstepSource;
    public AudioClip footstepSound;
    public float stepSpeed = 0.5f; // How fast the steps play (lower number = faster steps)
    private float stepTimer = 0f;

    // Notice this is now an IEnumerator Start() so we can use the time delay!
    IEnumerator Start()
    {
        // Wait for exactly 0.1 seconds to let Unity's physics engine fully load
        yield return new WaitForSeconds(0.1f);

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

        // Check for player movement every frame to play footsteps
        HandleFootsteps();
    }
    
    /// <summary>
    /// Plays footstep audio when the CharacterController is moving on the ground.
    /// </summary>
    private void HandleFootsteps()
    {
        CharacterController cc = GetComponent<CharacterController>();

        // 1. Are we touching the ground AND moving?
        if (cc != null && cc.isGrounded && cc.velocity.magnitude > 0.1f)
        {
            // 2. Count down the timer
            stepTimer -= Time.deltaTime;

            // 3. When the timer hits 0, play a step!
            if (stepTimer <= 0f)
            {
                if (footstepSource != null && footstepSound != null)
                {
                    // Slightly randomize the pitch so it sounds like real, natural walking!
                    footstepSource.pitch = Random.Range(0.85f, 1.15f);
                    footstepSource.PlayOneShot(footstepSound);
                }
                
                // Reset the timer for the next step
                stepTimer = stepSpeed; 
            }
        }
        else
        {
            // Reset the timer to 0 when we stop, so the very first step plays instantly when we start walking again
            stepTimer = 0f; 
        }
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