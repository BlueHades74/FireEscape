using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private float playerMoveSpeed;

    private Rigidbody2D rb;

    private PlayerInputController inputs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set our variables
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// When the object is enabled we need to subscribe to the required events
    /// </summary>
    public void OnEnable()
    {
        inputs = GetComponent<PlayerInputController>();
        if (inputs.PlayerIndex == 0)
        {
            inputs.InputActions.Player.P1Movement.performed += OnPlayerMovement;
            inputs.InputActions.Player.P1Movement.canceled += OnPlayerMovement;
        }
        else
        {
            inputs.InputActions.Player.P2Movement.performed += OnPlayerMovement;
            inputs.InputActions.Player.P2Movement.canceled += OnPlayerMovement;
        }
    }

    /// <summary>
    /// When disabled we need to unsubscribe from the events.
    /// </summary>
    public void DisableInputs(int playerIndex, ControlMap inputActions)
    {
        if (inputs.PlayerIndex == 0)
        {
            inputs.InputActions.Player.P1Movement.performed -= OnPlayerMovement;
            inputs.InputActions.Player.P1Movement.canceled -= OnPlayerMovement;
        }
        else
        {
            inputs.InputActions.Player.P2Movement.performed -= OnPlayerMovement;
            inputs.InputActions.Player.P2Movement.canceled -= OnPlayerMovement;
        }
    }

    /// <summary>
    /// Has the player move.
    /// </summary>
    /// <param name="context"></param>
    private void OnPlayerMovement(InputAction.CallbackContext context)
    {
        Debug.Log(inputs.PlayerIndex);
        rb.linearVelocity = context.ReadValue<Vector2>() * playerMoveSpeed;
    }
}
