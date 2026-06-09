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

    public void OpenDoor()
    {
        // Add logic to open the door (e.g., play animation, disable collider, etc.)
        print("Door opened!");
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Add logic to close the door (e.g., play animation, enable collider, etc.)
            print("Door closed!");
        }
    }
}
