/*
 * Author: Aerica Gan Chai Ting
 * Date: 8 June 2026
 * Description: Manages the health bar UI, updating it based on the player's current health.
 */

using UnityEngine;

public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// Sets the health bar UI based on the player's current health.
    /// </summary> <param name="health">The player's current health.</param>

    public void
    SetHealth(int health)
    {
        // Update health bar UI based on current health
        // Add reference to image component and set fill amount based on health percentage
        //Image healthBarImage = GetComponent<Image>();
        //if (healthBarImage != null)
        //{
            //healthBarImage.fillAmount = (float)health / 100f; //Max health is 100
        //}  
    }
}
