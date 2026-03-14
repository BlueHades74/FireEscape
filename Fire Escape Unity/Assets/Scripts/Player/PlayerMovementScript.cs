using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Brian "Blue Guy" McLatchie

    [SerializeField]
    private float playerMoveSpeed = 6f;

    private float originalMoveSpeed = 6f;

    private Rigidbody2D rb;

    private PlayerInputController inputs;

    private Vector2 facingDirection;

    [SerializeField] private Animator animator;
    private SpriteRenderer playerSprite;
    [SerializeField] private Sprite[] sprites;
    private int spriteIndex = 0;

    public Vector2 FacingDirection { get => facingDirection; }
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }

    public AudioSource footstepAudioSource;

    private bool canMove = true;

    private Vector2 moveInput;

    private int[] clampDim;

    private Vector2 addedVelocity;

    private bool SwitchFacingDirection = true;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set our variables
        rb = GetComponent<Rigidbody2D>();
        originalMoveSpeed = playerMoveSpeed;
        playerSprite = GetComponent<SpriteRenderer>();
        clampDim = new int[4];
        clampDim[0] = -1;
        clampDim[1] = 1;
        clampDim[2] = -1;
        clampDim[3] = 1;
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
        TryMove();
    }


    /// <summary>
    /// This sets a direction that the user is facing.
    /// </summary>
    /// <param name="direction"></param>
    private void SetFacingDirection(Vector2 direction)
    {
        // Sets the direction the players are facing
        animator.SetFloat(horizontal, direction.x);
        animator.SetFloat(vertical, direction.y);

        if (direction != Vector2.zero)
        {
            direction = TurnVectorIntoSingleDirection(direction);

            if (SwitchFacingDirection)
            {
                facingDirection = direction;
            }

            animator.SetFloat(lastHorizontal, direction.x);
            animator.SetFloat(lastVertical, direction.y);
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

    /// <summary>
    /// Changes the movement speed by adding the original with some other value
    /// </summary>
    public void ResetSpeedMultiplier()
    {
        playerMoveSpeed = originalMoveSpeed;
    }

    /// <summary>
    /// Control player movement based on player input
    /// </summary>
    /// <param name="context"></param>
    private void OnMovement(InputValue context)
    {
        moveInput = context.Get<Vector2>();
        TryMove();
    }

    /// <summary>
    /// Controls whether or not the player can move, restarts movement if they can
    /// </summary>
    /// <param name="option"></param>
    public void CanMove(bool option)
    {
        canMove = option;
        TryMove();
    }

    /// <summary>
    /// Limits where the x and y can go (allows us to have the player move in only one direction)
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private Vector2 ClampMove(Vector2 input)
    {
        input.x = Mathf.Clamp(input.x, clampDim[0], clampDim[1]) * Mathf.Sign(input.x);
        input.y = Mathf.Clamp(input.y, clampDim[2], clampDim[3]) * Mathf.Sign(input.y);

        return input;
    }

    /// <summary>
    /// Changes the settings of the clamp depending on the situation. Runs the movement code again to make sure that it is behaving correctly.
    /// </summary>
    /// <param name="horizontalPos"></param>
    /// <param name="horizontalNeg"></param>
    /// <param name="verticalPos"></param>
    /// <param name="verticalNeg"></param>
    public void ChangeClampMoveSettings(int horizontalPos, int horizontalNeg, int verticalPos, int verticalNeg)
    {
        clampDim[0] = Mathf.Clamp(horizontalNeg, -1, 0);
        clampDim[1] = Mathf.Clamp(horizontalPos, 0, 1);
        clampDim[2] = Mathf.Clamp(verticalNeg, -1, 0);
        clampDim[3] = Mathf.Clamp(verticalPos, 0, 1);
        TryMove();
    }

    public void ChangeAddedVelocity(Vector2 velocity)
    {
        addedVelocity = velocity;
    }

    public void SwitchFaceDirection(bool willItSwitch)
    {
        SwitchFacingDirection = willItSwitch;
    }

    private void TryMove()
    {
        if (canMove)
        {
            SetFacingDirection(moveInput);
            Vector2 modifier = ClampMove(moveInput);
            rb.linearVelocity = moveInput * playerMoveSpeed * modifier + addedVelocity;
        }
    }
}
