using UnityEngine;

public class DoorScript : MonoBehaviour
{

// ======================
// DOOR OPENING 
// ======================
/// <summary>
/// Handles the door opening when the player enters the trigger zone.
/// </summary>

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
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
/// </summary>

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Player exited trigger zone, doors close.");
            // Add door closing animation here
        }
    }
}
