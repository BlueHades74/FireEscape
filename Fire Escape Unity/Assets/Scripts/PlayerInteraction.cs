using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerInputController inputs;

    private void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnEnable()
    {
        inputs = GetComponent<PlayerInputController>();
        if (inputs.PlayerIndex == 0)
        {
            inputs.InputActions.Player.P1Interact.performed += Interaction;
        }
        else
        {
            inputs.InputActions.Player.P2Interact.performed += Interaction;
        }
    }

    public void OnDisable()
    {
        if (inputs.PlayerIndex == 0)
        {
            inputs.InputActions.Player.P1Interact.performed -= Interaction;
        }
        else
        {
            inputs.InputActions.Player.P2Interact.performed -= Interaction;
        }
    }

    private void Interaction(InputAction.CallbackContext context)
    {
        PlayerEventSystem.current.ObjectPickedUp(transform.position);
    }
}


