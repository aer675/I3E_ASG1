/*
 * Author: Aerica Gan Chai Ting
 * Date: 2 June 2026
 * Description: Manages door behavior, including opening and closing.
 */

using UnityEngine;

public class DoorScript : MonoBehaviour
{

    /// <summary>
    /// Opens the door when the player enters the trigger zone, if the required conditions are met (e.g., having the correct keycard).
    /// </summary> <param name="other">The collider that entered the trigger zone.</param>
    
    public bool requiresKeycard = false; // Flag to indicate if a keycard is required to open the door
    public int doorLevel = 1; // Level of the door, can be used to determine if the player has the required keycard
    
    public Animator doorAnimator; // Reference to the Animator component for door animations

    public void OpenDoor()
        {
            print("Door opened!");
            
            // Change from SetTrigger to SetBool
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("isOpen", true); 
            }
        }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            print("Door closed!");
            
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("isOpen", false);
            }
        }
    }
}
