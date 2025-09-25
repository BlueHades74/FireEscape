using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private PlayerInputController inputs;

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
        PlayerEventSystem.current.ObjectPickedUp(transform.position);
    }
}


