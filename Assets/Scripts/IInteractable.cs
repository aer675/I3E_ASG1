/*
 * Author: Aerica Gan Chai Ting
 * Date: 10 June 2026
 * Description: Interface contract that forces any interactable world object to implement a standard Interact method.
 */

public interface IInteractable
{
    /// <summary>
    /// Core interaction method called when the player looks at an object and presses the interaction key.
    /// </summary>
    void Interact(); // This method must be implemented by any class that implements IInteractable
}