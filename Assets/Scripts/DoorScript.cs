/*
 * Author: Aerica Gan Chai Ting
 * Date: 10 June 2026
 * Description: Manages door behavior, including opening and closing animations triggered by the player.
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
    /// Opens the door when the player enters the invisible trigger zone.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OpenDoor();
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