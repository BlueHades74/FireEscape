using UnityEngine;

namespace FireEscape.Items
{
    /// <summary>
    /// Represents an item that can be picked up and carried by the player.
    /// </summary>
    public class Item : MonoBehaviour
    {
        /// <summary>
        /// How much this item slows down the player when carried (0-1 range).
        /// </summary>
        public float speedReduction = 0.2f;
        
        private Transform _playerTransform;
        private bool _isPickedUp = false;

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        void Start()
        {
            
        }

        /// <summary>
        /// Update is called every frame. Makes the item follow the player when picked up.
        /// </summary>
        void Update()
        {
            if (_isPickedUp && _playerTransform != null)
            {
                transform.position = _playerTransform.position + new Vector3(0f, .3f, 0f);
            }
        }

        /// <summary>
        /// Makes the player pick up this item, attaching it to the player.
        /// </summary>
        /// <param name="player">The transform of the player picking up the item.</param>
        public void PickUp(Transform player)
        {
            _playerTransform = player;
            _isPickedUp = true;
            GetComponent<Collider2D>().enabled = false;
        }

        /// <summary>
        /// Drops the item and deactivates it.
        /// </summary>
        public void Drop()
        {
            _isPickedUp = false;
            gameObject.SetActive(false);
        }
    }
}
