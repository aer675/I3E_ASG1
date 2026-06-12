/*
 * Author: Aerica Gan Chai Ting
 * Date: 12 June 2026
 * Description: Manages automatic door behavior, including animations and UI security feedback.
 */

using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Door Settings")]
    public bool requiresKeycard = false;
    public int doorLevel = 1;
    
    [Header("Animation")]
    public Animator doorAnimator;
    

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
    /// Core method that handles the opening animation state.
    /// </summary>
    public void OpenDoor()
    {
        print("Door opened!");
        
        if (doorAnimator != null)
        {
            // Tells the Animator to transition to the open state
            doorAnimator.SetBool("isOpen", true);
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
    /// Core method that handles the closing animation state.
    /// </summary>
    public void CloseDoor()
    {
        print("Door closed!");
        
        if (doorAnimator != null)
        {
            // Tells the Animator to transition back to the closed/idle state
            doorAnimator.SetBool("isOpen", false);
        }
    }
}