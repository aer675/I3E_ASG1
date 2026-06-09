/*
 * Author: Aerica Gan Chai Ting
 * Date: 9 June 2026
 * Description: Core manager handling game state, inventory tracking, score, and win/lose conditions.
 */

using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary> Singleton instance allowing global access to the GameManager. </summary>
    public static GameManager Instance { get; private set; }

    [Header("Inventory & Score Tracking")]
    /// <summary> Tracks the total number of map fragments the player has collected. </summary>
    public int mapCount = 0;

    /// <summary> Tracks the player's total score from collecting items. </summary>
    public int score = 0;

    [Header("Security Clearance")]
    /// <summary> Flag indicating if the player has unlocked Level 1 door access. </summary>
    public bool hasLevel1Card = false;

    /// <summary> Flag indicating if the player has unlocked Level 2 door access. </summary>
    public bool hasLevel2Card = false;

    /// <summary>
    /// Initializes the Singleton instance when the script loads.
    /// Ensures only one GameManager ever exists in the scene.
    /// </summary>
    void Awake()
    {
        // Singleton setup logic
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Increments the map fragment count and checks if the Level 1 keycard should spawn.
    /// </summary>
    public void AddMapFragment()
    {
        mapCount++;
        print("Map fragment collected! Total maps: " + mapCount);
        
        // TODO: Update your Map UI counter here!
        
        if (mapCount >= 5)
        {
            hasLevel1Card = true;
            print("Level 1 Keycard Authorized!");
            // TODO: Trigger a UI message or sound effect here!
        }
    }

    /// <summary>
    /// Adds points to the player's total score.
    /// </summary>
    /// <param name="points">The amount of points to award the player.</param>
    public void AddScore(int points)
    {
        score += points;
        print("Score updated! Current score: " + score);
        
        // TODO: Update your Score UI text here!
    }
}
