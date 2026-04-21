using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private PlayerInputController inputs;

    private bool canPickUp = true;

    private PlayerActionScript actionScript;

    private void Start()
    {
        actionScript = GetComponent<PlayerActionScript>();
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

        CharacterEvents.PlayerSharedKeyPress.Invoke('Q', gameObject.name);
    }

    private void OnAction()
    {
        if (actionScript.enabled == true)
        {
            if (actionScript.ReturnActionString() == "Ladder")
            {
                return;
            }
        }
       
        Vector3 displacement = new Vector3(GetComponent<PlayerMovementScript>().FacingDirection.x, GetComponent<PlayerMovementScript>().FacingDirection.y, 0);
        ContactFilter2D contactFilter = new ContactFilter2D();
        RaycastHit2D[] hits = new RaycastHit2D[10];
        Physics2D.Raycast(transform.position + displacement, GetComponent<PlayerMovementScript>().FacingDirection, contactFilter, hits, 1.5f);
        Debug.DrawRay(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, Color.red);

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Hole")
                {
                    if (hit.collider.gameObject.GetComponent<HoleJumpScript>().CanWeJump())
                    {
                        hit.collider.gameObject.GetComponent<HoleJumpScript>().InitiateTP(gameObject);
                    }
                }
            }
        }

        
    }

    private void OnPause()
    {
        CharacterEvents.PlayerSharedKeyPress.Invoke('b', gameObject.name);
    }

    /// <summary>
    /// Controls whether or not the player can interact
    /// </summary>
    public void CanPickUp(bool option)
    {
        canPickUp = option;
    }
}


