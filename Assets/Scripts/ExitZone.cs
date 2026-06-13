/*
 * Author: Aerica Gan Chai Ting
 * Date: 11 June 2026
 * Description: Detects when the player reaches the exit zone and triggers the win condition in the GameManager.
 */

using UnityEngine;

public class ExitZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            print("Player reached the exit!");
            GameManager.Instance.TriggerWin(); // Call the GameManager's win function to handle the end of the game
        }
    }
}