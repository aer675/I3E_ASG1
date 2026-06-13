/*
 * Author: Aerica Gan Chai Ting
 * Date: 11 June 2026
 * Description: Detects when the player reaches a checkpoint and saves the player's position in the GameManager.
 */

using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Save the checkpoint position
            GameManager.Instance.SaveCheckpoint(transform.position);
        }
    }
}