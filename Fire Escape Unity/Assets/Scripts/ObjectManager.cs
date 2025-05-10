using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private ObjectManager currentlyHeldNPC = null;
    private Transform playerTransform;
    private bool isPlayerNearby = false;
    private bool isHeld = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    [SerializeField]
    private string action = null;

    public string Action { get => action; }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        
    }

    private void OnDestroy()
    {
        if (PlayerEventSystem.current != null)
        {
            PlayerEventSystem.current.OnObjectPickedUp -= TryTogglePickup;
        }
    }

    private void OnEnable()
    {
        if (PlayerEventSystem.current != null)
        {
            PlayerEventSystem.current.OnObjectPickedUp += TryTogglePickup;
        }
    }

    private void OnDisable()
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

    private void TryTogglePickup(Vector3 nearbyPlayer)
    {
        if (nearbyPlayer == playerTransform?.position)
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
    }

    private void Drop()
    {
        isHeld = false;
        transform.SetParent(null);
        currentlyHeldNPC = null;
        Debug.Log($"{name} DROPPED (swapped)!");
    }

    public void DropItem()
    {
        Drop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (!isHeld && spriteRenderer != null)
            {
                playerTransform = other.transform;
                spriteRenderer.color = highlightColor;
            }
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
