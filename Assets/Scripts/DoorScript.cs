/*
 * Author: Aerica Gan Chai Ting
 * Date: 12 June 2026
 * Description: Manages automatic door behavior, including animations, UI security feedback, and audio.
 */

using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Door Settings")]
    public bool requiresKeycard = false;
    public int doorLevel = 1;
    
    [Header("Animation")]
    public Animator doorAnimator;

    [Header("Game Juice")]
    public AudioSource doorAudio; 

    private bool isCurrentlyOpen = false; 
    

    /// <summary>
    /// Opens the door when the player enters, checking for security clearance if required.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // 1. Does this specific door require a keycard?
            if (requiresKeycard)
            {
                // 2. It does! Check if it's a Level 1 door and if the player has the Level 1 card
                if (doorLevel == 1 && GameManager.Instance.hasLevel1Card)
                {
                    print("Level 1 Access Granted!");
                    OpenDoor();
                }
                // 3. Or check if it's a Level 2 door and if the player has the Level 2 card
                else if (doorLevel == 2 && GameManager.Instance.hasLevel2Card)
                {
                    print("Level 2 Access Granted!");
                    OpenDoor();
                }
                // 4. They don't have the right card! Keep it closed and show the UI message.
                else
                {
                    GameManager.Instance.ShowMessage("Access Denied! You need a Level " + doorLevel + " Keycard.");
                    
                    // 5. Update the permanent objective tracker
                    GameManager.Instance.UpdateObjective("Objective: Find Level " + doorLevel + " Keycard");
                }
            }
            else
            {
                // If the door doesn't require a keycard at all, just open normally!
                OpenDoor();
            }
        }
    }

    /// <summary>
    /// Core method that handles the opening animation state and plays audio.
    /// </summary>
    public void OpenDoor()
    {
        // Safety Check: If it's already open, do nothing!
        if (isCurrentlyOpen) return; 

        print("Door opened!");
        isCurrentlyOpen = true; // Update memory
        
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", true);
        }

        if (doorAudio != null)
        {
            doorAudio.Play();
        }
    }

    /// <summary>
    /// Closes the door when the player walks out of the trigger zone.
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    /// <summary>
    /// Core method that handles the closing animation state and plays audio.
    /// </summary>
    public void CloseDoor()
    {
        if (!isCurrentlyOpen) return; 

        print("Door closed!");
        isCurrentlyOpen = false; // Update memory
        
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", false);
        }

        if (doorAudio != null)
        {
            doorAudio.Play();
        }
    }
}