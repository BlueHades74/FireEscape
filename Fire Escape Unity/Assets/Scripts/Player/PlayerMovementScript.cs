using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private float playerMoveSpeed;

    private float originalMoveSpeed;

    private Rigidbody2D rb;

    private PlayerInputController inputs;

    private Vector2 facingDirection;

    private SpriteRenderer playerSprite;
    [SerializeField] private Sprite[] sprites;
    private int spriteIndex = 0;

    public Vector2 FacingDirection { get => facingDirection; }

    public AudioSource footstepAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set our variables
        rb = GetComponent<Rigidbody2D>();
        originalMoveSpeed = playerMoveSpeed;
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Play the footstep sound if the player is moving
        if (rb.linearVelocity != Vector2.zero)
        {
            if (footstepAudioSource != null && !footstepAudioSource.isPlaying)
                footstepAudioSource.Play();
        }
        else
        {
            if (footstepAudioSource != null && footstepAudioSource.isPlaying)
                footstepAudioSource.Stop();
        }
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
        Vector2 moveInput = context.ReadValue<Vector2>();
        SetFacingDirection(moveInput);
        rb.linearVelocity = moveInput * playerMoveSpeed;
    }

    /// <summary>
    /// This sets a direction that the user is facing.
    /// </summary>
    /// <param name="direction"></param>
    private void SetFacingDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            direction = TurnVectorIntoSingleDirection(direction);

            facingDirection = direction;

            // Use the direction vector to set sprite index
            if (direction.x < 0)
            {
                spriteIndex = 1; // Left
            }
            else if (direction.x > 0)
            {
                spriteIndex = 2; // Right
            }
            else if (direction.y < 0)
            {
                spriteIndex = 0; // Down
            }
            else if (direction.y > 0)
            {
                spriteIndex = 3; // Up
            }
            else
            {
                spriteIndex = 0;
            }

            // Set sprite index for player
            if (sprites != null && sprites.Length > spriteIndex)
                playerSprite.sprite = sprites[spriteIndex];

            Debug.Log(facingDirection);
        }
    }

    /// <summary>
    /// This does the math to choose a facing direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Changes the movement speed by multiplying the original with some other value
    /// </summary>
    /// <param name="multiplier"></param>
    public void SetMovementByOriginalTimesParameter(float multiplier)
    {
        playerMoveSpeed = originalMoveSpeed * multiplier;
    }
}
