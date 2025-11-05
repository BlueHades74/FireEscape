using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Brian "Blue Guy" McLatchie

    [SerializeField]
    private float playerMoveSpeed = 10f;

    private float originalMoveSpeed = 10f;

    private Rigidbody2D rb;

    private PlayerInputController inputs;

    private Vector2 facingDirection;

    [SerializeField] private Animator animator;
    private SpriteRenderer playerSprite;
    [SerializeField] private Sprite[] sprites;
    private int spriteIndex = 0;

    public Vector2 FacingDirection { get => facingDirection; }
    public float PlayerMoveSpeed { get => playerMoveSpeed; }

    public AudioSource footstepAudioSource;

    private bool canMove = true;

    private Vector2 moveInput;

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
            // Now using animator for player movement
            if (direction.x < 0)
            {
                //spriteIndex = 1; // Left
                //playerSprite.flipX = false;
                animator.SetBool("isRightRun", false);
                animator.SetBool("isDownRun", false);
                animator.SetBool("isUpRun", false);
                animator.SetBool("isLeftRun", true); // Left
            }
            else if (direction.x > 0)
            {
                //spriteIndex = 1; // Right
                //playerSprite.flipX = true;
                animator.SetBool("isLeftRun", false);
                animator.SetBool("isDownRun", false);
                animator.SetBool("isUpRun", false);
                animator.SetBool("isRightRun", true); // Right
            }
            else if (direction.y < 0)
            {
                //spriteIndex = 0; // Down
                animator.SetBool("isLeftRun", false);
                animator.SetBool("isRightRun", false);
                animator.SetBool("isUpRun", false);
                animator.SetBool("isDownRun", true); // Down
            }
            else if (direction.y > 0)
            {
                //spriteIndex = 2; // Up
                animator.SetBool("isLeftRun", false);
                animator.SetBool("isRightRun", false);
                animator.SetBool("isDownRun", false);
                animator.SetBool("isUpRun", true); // Up
            }

            // Set sprite index for player
            //if (sprites != null && sprites.Length > spriteIndex)
            //    playerSprite.sprite = sprites[spriteIndex];
        }
        else
        {
            //spriteIndex = 0;

            // set animation to idle
            animator.SetBool("isLeftRun", false);
            animator.SetBool("isRightRun", false);
            animator.SetBool("isDownRun", false);
            animator.SetBool("isUpRun", false);
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
        if (canMove)
        {
            SetFacingDirection(moveInput);
            rb.linearVelocity = moveInput * playerMoveSpeed;
        }
    }

    /// <summary>
    /// Controls whether or not the player can move, restarts movement if they can
    /// </summary>
    /// <param name="option"></param>
    public void CanMove(bool option)
    {
        canMove = option;
        if (canMove)
        {
            SetFacingDirection(moveInput);
            rb.linearVelocity = moveInput * playerMoveSpeed;
        }
    }
}
