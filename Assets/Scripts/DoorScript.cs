/*
 * Author: Aerica Gan Chai Ting
 * Date: 2 June 2026
 * Description: Manages door behavior, including opening and closing with animation and collider management.
 */

using UnityEngine;

public class DoorScript : MonoBehaviour
{
    /// <summary>
    /// Flag to indicate if a keycard is required to open the door.
    /// </summary>
    public bool requiresKeycard = false;

    /// <summary>
    /// Level of the door, can be used to determine if the player has the required keycard.
    /// </summary>
    public int doorLevel = 1;
    
    /// <summary>
    /// Reference to the Animator component for door animations.
    /// </summary>
    public Animator doorAnimator;

    /// <summary>
    /// Opens the door when the player enters the trigger zone.
    /// Sets the animator bool and disables the collider so the player can walk through.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    /// <summary>
    /// Core method that handles opening the door.
    /// Triggers the animator and disables the collider.
    /// </summary>
    public void OpenDoor()
    {
        print("Door opened!");
        
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", true);
        }
        
        // Disable the collider so player can walk through
        GetComponent<Collider>().enabled = false;
    }

    /// <summary>
    /// Closes the door when the player exits the trigger zone.
    /// Resets the animator bool and re-enables the collider.
    /// </summary>
    /// <param name="other">The collider that exited the trigger zone.</param>
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    /// <summary>
    /// Core method that handles closing the door.
    /// Resets the animator and re-enables the collider.
    /// </summary>
    public void CloseDoor()
    {
        print("Door closed!");
        
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", false);
        }
        
        // Re-enable the collider so the door blocks again
        GetComponent<Collider>().enabled = true;
    }
}