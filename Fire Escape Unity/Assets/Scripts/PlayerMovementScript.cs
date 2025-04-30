using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private float playerMoveSpeed;

    private Rigidbody2D rb;

    private PlayerInputController inputs;

    private Vector2 facingDirection;

    public Vector2 FacingDirection { get => facingDirection; }

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
    public void OnDisable()
    {
        inputs = GetComponent<PlayerInputController>();
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
        rb.linearVelocity = context.ReadValue<Vector2>() * playerMoveSpeed;
    }

    private void SetFacingDirection (Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            direction = TurnVectorIntoSingleDirection(direction);

            facingDirection = direction;
            Debug.Log(facingDirection);
        }
    }

    private static Vector2 TurnVectorIntoSingleDirection(Vector2 direction)
    {
        float difference = Mathf.Abs(direction.x) - Mathf.Abs(direction.y);
        switch (difference)
        {
            case (> 0):
                direction.x = Mathf.Sign(direction.x) * 1;
                direction.y = 0;
                break;

            case (0):
                int randomValue = Random.Range(0, 2);

                if (randomValue == 0)
                {
                    direction.x = Mathf.Sign(direction.x) * 1;
                    direction.y = 0;
                }
                else
                {
                    direction.x = 0;
                    direction.y = Mathf.Sign(direction.y) * 1;
                }
                break;

            case (< 0):
                direction.x = 0;
                direction.y = Mathf.Sign(direction.y) * 1;
                break;
        }

        return direction;
    }
}
