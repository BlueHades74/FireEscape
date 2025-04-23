using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager currentlyHeldNPC = null;
    private Transform playerTransform;
    private bool isPlayerNearby = false;
    private bool isHeld = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        if (PlayerEventSystem.current != null)
        {
            PlayerEventSystem.current.OnObjectPickedUp += TryTogglePickup;
        }
    }

    private void OnDestroy()
    {
        if (PlayerEventSystem.current != null)
        {
            PlayerEventSystem.current.OnObjectPickedUp -= TryTogglePickup;
        }
    }

    private void Update()
    {
        if (isHeld && playerTransform != null)
        {
            // Follow the player
            transform.position = playerTransform.position + new Vector3(0, 0.2f, 0);
        }
    }

    private void TryTogglePickup()
    {
        if (isHeld)
        {
            // Drop self
            isHeld = false;
            transform.SetParent(null);
            currentlyHeldNPC = null;
            spriteRenderer.color = originalColor;
            Debug.Log($"{name} DROPPED!");
        }
        else if (isPlayerNearby && playerTransform != null)
        {
            // If another NPC is held, drop it first
            if (currentlyHeldNPC != null)
            {
                currentlyHeldNPC.Drop();
            }

            // Pick up self
            isHeld = true;
            spriteRenderer.color = originalColor;
            transform.SetParent(playerTransform);
            currentlyHeldNPC = this;
            Debug.Log($"{name} PICKED UP!");
        }
    }

    private void Drop()
    {
        isHeld = false;
        transform.SetParent(null);
        currentlyHeldNPC = null;
        Debug.Log($"{name} DROPPED (swapped)!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerTransform = other.transform;

            if (!isHeld && spriteRenderer != null)
                spriteRenderer.color = highlightColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            if (!isHeld && spriteRenderer != null)
                spriteRenderer.color = originalColor;

            if (!isHeld)
                playerTransform = null;
        }
    }
}
