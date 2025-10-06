using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private PlayerInputController inputs;

    private bool canPickUp = true;

    private void Start()
    {
        
    }

    void Update()
    {
        
    }
    /// <summary>
    /// Interact with an item based on player input
    /// </summary>
    private void OnInteract()
    {
        if (canPickUp)
        {
            PlayerEventSystem.current.ObjectPickedUp(transform.position);
        }
    }

    /// <summary>
    /// Controls whether or not the player can interact
    /// </summary>
    public void CanPickUp(bool option)
    {
        canPickUp = option;
    }
}


