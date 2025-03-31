using UnityEngine;
using FireEscape.Items;

namespace FireEscape.Core
{
    /// <summary>
    /// Controls the player character movement, animation, and item interaction.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public float baseSpeed = 2f;
        public Sprite idleSprite;
        public Sprite moveSprite;
        public Sprite carryingIdleSprite;
        public Sprite carryingMoveSprite;
        public float animationSpeed = 0.15f;

        private Rigidbody2D _rigidBody;
        private SpriteRenderer _spriteRenderer;
        private float _animationTimer = 0f;
        private bool _isMoving = false;
        private bool _currentSpriteIsMove = false;
        private Item _carriedItem = null;
        private float _currentSpeed;

        /// <summary>
        /// Initializes the player controller by getting required components and setting initial state.
        /// </summary>
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _currentSpeed = baseSpeed;

            // Set the sprite to the idle sprite when the player is created
            _spriteRenderer.sprite = idleSprite;
        }

        /// <summary>
        /// Handles player movement, animation, and sprite orientation each frame.
        /// </summary>
        void Update()
        {
            // Get inputs
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            // Normalize the movement vector to ensure the player moves at the same speed in all directions
            Vector2 movement = new Vector2(moveX, moveY).normalized * _currentSpeed;

            // Move the player
            _rigidBody.linearVelocity = movement;

            // Check if the player is moving
            if (movement != Vector2.zero)
            {
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }

            // Handle walking animation
            if (_isMoving)
            {
                _animationTimer += Time.deltaTime;
                if (_animationTimer >= animationSpeed)
                {
                    _animationTimer -= animationSpeed; // Reset the timer

                    // Toggle between move and idle sprites based on carrying state
                    if (_currentSpriteIsMove)
                    {
                        // Use carrying idle sprite if carrying an item, otherwise use normal idle sprite
                        _spriteRenderer.sprite = (_carriedItem != null) ? carryingIdleSprite : idleSprite;
                        _currentSpriteIsMove = false;
                    }
                    else
                    {
                        // Use carrying move sprite if carrying an item, otherwise use normal move sprite
                        _spriteRenderer.sprite = (_carriedItem != null) ? carryingMoveSprite : moveSprite;
                        _currentSpriteIsMove = true;
                    }
                }
            }
            else
            {
                // When not moving, ensure the appropriate idle sprite is displayed based on carrying state
                _spriteRenderer.sprite = (_carriedItem != null) ? carryingIdleSprite : idleSprite;
                _currentSpriteIsMove = false; // Reset the animation state
            }

            // Handle sprite flipping
            if (movement.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (movement.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

        /// <summary>
        /// Handles item pickup and drop-off when colliding with appropriate objects.
        /// </summary>
        /// <param name="other">The collider of the object being interacted with.</param>
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (_carriedItem == null && other.CompareTag("Item"))
            {
                Item item = other.GetComponent<Item>();
                item.PickUp(transform);
                _carriedItem = item;
                _currentSpeed = baseSpeed * (1f - item.speedReduction);
            }
            else if (_carriedItem != null && other.CompareTag("DropOffZone"))
            {
                _carriedItem.Drop();
                _carriedItem = null;
                _currentSpeed = baseSpeed;
            }
        }
    }
}
