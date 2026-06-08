/*
 * Author: Aerica Gan Chai Ting
 * Date: 2 June 2026
 * Description: Manages door behavior, including opening and closing.
 */

using UnityEngine;

public class DoorScript : MonoBehaviour
{

    // ======================
    // DOOR OPENING 
    // ======================
    /// <summary>
    /// Handles the door opening when the player enters the trigger zone.
    /// </summary> <param name="other">The collider that entered the trigger zone.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            print("Player entered trigger zone, doors open.");
            // Add door opening animation here
        }
    }

    // ======================
    // DOOR CLOSING
    // ======================
    /// <summary>
    /// Handles the door closing when the player exits the trigger zone.
    /// </summary> <param name="other">The collider that exited the trigger zone.</param>
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            print("Player exited trigger zone, doors close.");
            // Add door closing animation here
        }
    }
}
