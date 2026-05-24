using UnityEngine;

public class DoorScript : MonoBehaviour
{

// Player enter trigger zone, doors open 
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Player entered trigger zone, doors open.");
        }
    }

// Player exit trigger with collectible, doors close
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Player exited trigger zone, doors close.");
        }
    }
}
