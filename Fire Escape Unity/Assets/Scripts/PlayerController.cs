using UnityEngine;

namespace FireEscape.Core
{
    /// <summary>
    /// Controls the player character movement, animation, and item interaction.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public float baseSpeed = 2f;

        private Rigidbody2D _rigidBody;
        private float _currentSpeed;

        /// <summary>
        /// Initializes the player controller by getting required components and setting initial state.
        /// </summary>
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _currentSpeed = baseSpeed;

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

 
        }
    }
}
