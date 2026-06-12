/*
 * Author: Aerica Gan Chai Ting
 * Date: 12 June 2026
 * Description: Core manager handling game state, inventory tracking, score, checkpoints, and win/lose conditions.
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    /// <summary> Singleton instance allowing global access to the GameManager. </summary>
    public static GameManager Instance { get; private set; }

    [Header("Checkpoint System")]
    /// <summary> Static variable to remember the position across scene reloads. </summary>
    public static Vector3 savedCheckpointPosition;
    
    /// <summary> Tracks if the player has actually touched a checkpoint yet. </summary>
    public static bool hasReachedCheckpoint = false;

    // --- NEW: Memory snapshots for your inventory! ---
    public static int savedMapCount = 0;
    public static int savedScore = 0;
    public static bool savedHasLevel1Card = false;
    public static bool savedHasLevel2Card = false;

    [Header("Keycard Spawning")]
    /// <summary> The 3D model/prefab of the keycard to spawn. </summary>
    public GameObject keycardPrefab;
    
    /// <summary> The exact spot in the starting zone where it should appear. </summary>
    public Transform keycardSpawnPoint;

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

    [Header("UI References")]
    public TextMeshProUGUI loreText; // Reference to the UI Text component for displaying lore messages
    public TextMeshProUGUI scoreText; // Reference to the UI Text component for displaying score

    [Header("UI Overlays")]
    /// <summary> Reference to the Game Over UI Panel. </summary>
    public GameObject gameOverPanel;
    
    /// <summary> Reference to the You Win UI Panel. </summary>
    public GameObject winPanel;

    private string[] mapLore = new string[]
    {
        "Fragment 1: 'The schematics show a hidden vent in the locker room...'",
        "Fragment 2: 'Warning: Underground sector hazardous. Chemical leak detected.'",
        "Fragment 3: 'Level 2 clearance is required for the main exit safe...'",
        "Fragment 4: 'Almost there. The final piece will trigger the main security override.'",
        "Fragment 5: 'Override complete. Level 1 Keycard has materialized at the starting zone!'"
    };

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

    void Start()
    {
        // If we are spawning at a checkpoint, restore the inventory snapshot!
        if (hasReachedCheckpoint)
        {
            mapCount = savedMapCount;
            score = savedScore;
            hasLevel1Card = savedHasLevel1Card;
            hasLevel2Card = savedHasLevel2Card;
        }

        // Refresh the UI so the screen immediately shows your saved numbers!
        UpdateUI();
    }

    /// <summary>
    /// Updates the global checkpoint location and saves inventory progress.
    /// </summary>
    public void SaveCheckpoint(Vector3 newPosition)
    {
        savedCheckpointPosition = newPosition;
        hasReachedCheckpoint = true;

        // Take a snapshot of the inventory at this exact moment!
        savedMapCount = mapCount;
        savedScore = score;
        savedHasLevel1Card = hasLevel1Card;
        savedHasLevel2Card = hasLevel2Card;

        print("Checkpoint Saved! Score and Inventory locked in.");
    }

    /// <summary>
    /// Refreshes the UI text to show the current score and how many maps remain.
    /// </summary>
    public void UpdateUI()
    {
        if (scoreText != null)
        {
            int mapsLeft = 5 - mapCount;
            if (mapsLeft < 0) mapsLeft = 0; // Prevents negative numbers

            // \n creates a new line so they stack nicely on the screen!
            scoreText.text = "Score: " + score + "\nMaps Left: " + mapsLeft;
        }
    }

    /// <summary>
    /// Increments the map fragment count and checks if the Level 1 keycard should spawn.
    /// </summary>
    public void AddMapFragment()
    {
        // 1. Trigger the lore popup BEFORE adding to the count
        if (mapCount < mapLore.Length && loreText != null)
        {
            StartCoroutine(ShowLoreText(mapLore[mapCount]));
        }

        // 2. Increase the count
        mapCount++;
        print("Map fragment collected! Total maps: " + mapCount);
        
        // Update the UI to show the new map count!
        UpdateUI(); 
        
        // 3. Check for the win condition
        if (mapCount >= 5)
        {
            hasLevel1Card = true;
            print("Level 1 Keycard Authorized!");

            // Spawn the keycard at the designated spawn point
            if (keycardPrefab != null && keycardSpawnPoint != null)
            {
                Instantiate(keycardPrefab, keycardSpawnPoint.position, keycardSpawnPoint.rotation);
            }
            else
            {
                Debug.LogWarning("Cannot spawn keycard: Prefab or Spawn Point is missing in GameManager!");
            }
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
        
        // Update the score display in the UI
        UpdateUI();
    }

    /// <summary>
    /// Public method to trigger the UI pop-up from ANY other script in the game!
    /// </summary>
    /// <param name="message">The custom text you want to display.</param>
    public void ShowMessage(string message)
    {
        if (loreText != null)
        {
            StartCoroutine(ShowLoreText(message));
        }
    }

    /// <summary>
    /// Displays a lore message on the screen for a short duration.
    /// </summary>
    /// <param name="message">The lore message to display.</param>
    private IEnumerator ShowLoreText(string message)
    {
        loreText.text = message;
        loreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f); // Display the message for 5 seconds
        loreText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Freezes the game, unlocks the mouse, and shows the Victory screen.
    /// </summary>
    public void TriggerWin()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        // Freeze game time
        Time.timeScale = 0f; 

        // Unlock and show the mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Freezes the game, unlocks the mouse, and shows the Game Over screen.
    /// </summary>
    public void TriggerGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Freeze game time so hazards and enemies stop moving
        Time.timeScale = 0f; 

        // Unlock and show the mouse cursor so the player can click the button
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// For the GAME OVER Screen: Unfreezes time, hides the panel, and reloads the scene.
    /// </summary>
    public void RetryLevel()
    {
        // Force the Game Over panel to hide!
        if (gameOverPanel != null) gameOverPanel.SetActive(false); 
        
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    /// <summary>
    /// For the WIN Screen: Wipes all inventory and score, then completely restarts the game.
    /// </summary>
    public void FullRestartGame()
    {
        // Force the Win panel to hide!
        if (winPanel != null) winPanel.SetActive(false); 

        // Wipe all player progress back to zero
        mapCount = 0;
        score = 0;
        hasLevel1Card = false;
        hasLevel2Card = false;

        // Completely reset the checkpoint and memory snapshots so they spawn at the very beginning with nothing!
        hasReachedCheckpoint = false; 
        savedMapCount = 0;
        savedScore = 0;
        savedHasLevel1Card = false;
        savedHasLevel2Card = false;

        // Unfreeze time BEFORE loading
        Time.timeScale = 1f; 
        
        // Reload the scene! 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}