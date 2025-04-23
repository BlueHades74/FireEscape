using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private float playerMoveSpeed;
    [SerializeField]
    private int playerIndex;

    private Rigidbody2D rb;

    private ControlMap inputActions;

    private void Awake()
    {
        //Set our variables
        rb = GetComponent<Rigidbody2D>();
        inputActions = new ControlMap();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// When the object is enabled we need to subscribe to the required events
    /// </summary>
    private void OnEnable()
    {
        inputActions.Enable();

        if (playerIndex == 0 )
        {
            inputActions.Player.P1Movement.performed += OnPlayerMovement;
            inputActions.Player.P1Movement.canceled += OnPlayerMovement;
        }
        else
        {
            inputActions.Player.P2Movement.performed += OnPlayerMovement;
            inputActions.Player.P2Movement.canceled += OnPlayerMovement;
        }
    }

    /// <summary>
    /// When disabled we need to unsubscribe from the events.
    /// </summary>
    private void OnDisable()
    {
        inputActions.Disable();

        if (playerIndex == 0)
        {
            inputActions.Player.P1Movement.performed -= OnPlayerMovement;
            inputActions.Player.P1Movement.canceled -= OnPlayerMovement;
        }
        else
        {
            inputActions.Player.P2Movement.performed -= OnPlayerMovement;
            inputActions.Player.P2Movement.canceled -= OnPlayerMovement;
        }
    }

    /// <summary>
    /// Has the player move.
    /// </summary>
    /// <param name="context"></param>
    private void OnPlayerMovement(InputAction.CallbackContext context)
    {
        rb.linearVelocity = context.ReadValue<Vector2>() * playerMoveSpeed;
    }
}
