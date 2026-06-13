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

    // Start is a Coroutine because we want to add a slight delay before teleporting the player to the checkpoint, to let Unity's physics engine fully load.
    IEnumerator Start()
    {
        // Wait for exactly 0.1 seconds to let Unity's physics engine fully load
        yield return new WaitForSeconds(0.1f);

        // Check if the GameManager says we have reached a checkpoint before, and if so, teleport the player to that checkpoint position!
        if (GameManager.hasReachedCheckpoint)
        {
            // Get the CharacterController component so we can disable it before teleporting (to avoid physics issues) and re-enable it after.
            CharacterController cc = GetComponent<CharacterController>();
            
            // Disable the CharacterController before teleporting.
            if (cc != null) cc.enabled = false;
            
            // Teleport the player to the saved checkpoint position stored in the GameManager.
            transform.position = GameManager.savedCheckpointPosition;
            
            // Re-enable the CharacterController after teleporting.
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

        // 1. Check if the CharacterController is grounded and has some velocity (is moving)
        if (cc != null && cc.isGrounded && cc.velocity.magnitude > 0.1f)
        {
            // 2. If so, count down the step timer by the time between frames
            stepTimer -= Time.deltaTime;

            // 3. When the timer reaches 0, play a footstep sound and reset the timer to the step speed for the next step.
            if (stepTimer <= 0f)
            {
                if (footstepSource != null && footstepSound != null)
                {
                    // Randomize the pitch slightly for variety, then play the footstep sound effect using our Ghost Speaker trick
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
        // Making sure a player camera is assigned, otherwise the raycast won't work.
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
            // 3. Check if the laser beam hit an object with an IInteractable script on it
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