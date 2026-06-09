/*
 * Author: Aerica Gan Chai Ting
 * Date: 8 June 2026
 * Description: Manages the health bar UI, updating it based on the player's current health.
 */

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// Sets the health bar UI based on the player's current health.
    /// </summary> <param name="health">The player's current health.</param>
    public Image healthBarImage; // Reference to the health bar image component
    public void SetHealth(int health)
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)health / 100f; // Max health is 100. Casting float ensures correct division.
        }
    }
}
