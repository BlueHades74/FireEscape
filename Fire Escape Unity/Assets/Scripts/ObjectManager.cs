using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private ObjectManager currentlyHeldNPC = null;
    private Transform playerTransform;
    private bool isPlayerNearby = false;
    private bool isHeld = false;

    public bool IsHeld { get => isHeld; }

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    [SerializeField]
    private string action = null;
    [SerializeField]
    private Sprite imageUI;

    public string Action { get => action; }
    public Sprite ImageUI { get => imageUI; set => imageUI = value; }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
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
            transform.position = playerTransform.position + new Vector3(0, 0.2f, 0);
        }
    }

    private void TryTogglePickup(Vector3 nearbyPlayer)
    {
        if (nearbyPlayer == playerTransform?.position)
        {
            if (isHeld)
            {
                isHeld = false;
                transform.SetParent(null);
                currentlyHeldNPC = null;
                if (spriteRenderer != null) 
                {
                    spriteRenderer.color = originalColor;
                }
            }
            else if (isPlayerNearby && playerTransform != null)
            {
                if (currentlyHeldNPC != null) 
                {
                    currentlyHeldNPC.Drop();
                }

                if (playerTransform.gameObject.GetComponent<PlayerMovementScript>().PlayerMoveSpeed > 0)
                {
                    isHeld = true;
                    if (spriteRenderer != null) 
                    {
                        spriteRenderer.color = highlightColor;
                    }
                    transform.SetParent(playerTransform);
                    currentlyHeldNPC = this;
                }
            }
        }
    }

    private void Drop()
    {
        isHeld = false;
        transform.SetParent(null);
        currentlyHeldNPC = null;
        if (spriteRenderer != null) 
        {
            spriteRenderer.color = originalColor;
        }
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
            playerTransform = other.transform;
            if (!isHeld && spriteRenderer != null)
            {
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
            {
                spriteRenderer.color = originalColor;
            }
            if (!isHeld)
            {
                playerTransform = null;
            }
        }
    }
}
