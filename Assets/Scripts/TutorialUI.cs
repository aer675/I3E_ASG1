/*
 * Author: Aerica Gan Chai Ting
 * Date: 13 June 2026
 * Description: Simple script to handle the tutorial UI text, making it disappear after a set amount of time without freezing the game.
 */
using UnityEngine;
using System.Collections; 

public class TutorialUI : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("How many seconds should this text stay on the screen?")]
    public float displayTime = 5f; 

    void Start()
    {
        // Start the timer as soon as this object is enabled in the scene
        StartCoroutine(HideUIRoutine());
    }

    /// <summary>
    /// This is the timer logic. It waits for the specified amount of seconds, then turns off the UI text object.
    /// </summary>
    private IEnumerator HideUIRoutine()
    {
        // 1. Wait for the specified amount of time while allowing the game to continue running
        yield return new WaitForSeconds(displayTime);

        // 2. After the timer is done, disable the UI text object so it disappears from the screen
        gameObject.SetActive(false);
    }
}