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
        // Did the player walk through the invisible box?
        if (other.CompareTag("Player"))
        {
            // Tell the GameManager to save this exact position!
            GameManager.Instance.SaveCheckpoint(transform.position);
        }
    }
}