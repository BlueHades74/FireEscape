using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Sprite idleSprite;
    public Sprite moveSprite;
    public float animationSpeed = 0.15f;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private float animationTimer = 0f;
    private bool isMoving = false;
    private bool currentSpriteIsMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the sprite to the idle sprite when the player is created
        spriteRenderer.sprite = idleSprite;

    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD keys
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Normalize the movement vector to ensure the player moves at the same speed in all directions
        Vector2 movement = new Vector2(moveX, moveY).normalized;

        // Move the player
        rigidBody.linearVelocity = movement * moveSpeed;


        // Check if the player is moving
        if (movement != Vector2.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Handle walking animation
        if (isMoving)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= animationSpeed)
            {
                animationTimer -= animationSpeed; // Reset the timer

                // Toggle between moveSprite and idleSprite
                if (currentSpriteIsMove)
                {
                    spriteRenderer.sprite = idleSprite;
                    currentSpriteIsMove = false;
                }
                else
                {
                    spriteRenderer.sprite = moveSprite;
                    currentSpriteIsMove = true;
                }
            }
        }
        else
        {
            // When not moving, ensure the idle sprite is displayed
            spriteRenderer.sprite = idleSprite;
            currentSpriteIsMove = false; // Reset the animation state
        }




        // Handle sprite flipping
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }

    }
}
