using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static System.Action<ObjectManager> OnHumanRescued;
    //Created by: 
    //Last Edited by: Rafael Gonzalez Atiles

    [SerializeField]
    private AudioSource soundEffectSource;
    [SerializeField]
    private AudioClip pickupSound;

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
    [SerializeField]
    private bool singlePush;

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

    public void Rescue()
    {
        Debug.Log($"[ObjectManager] {name} rescued!");
        //When a human is rescued this will tell ObjectiveUIManager that a human was saved and increase or decrease the objective count
        OnHumanRescued?.Invoke(this); //Notify Listeners
        Destroy(gameObject);
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
                transform.SetParent(null);
                currentlyHeldNPC = null;
                if (spriteRenderer != null) 
                {
                    spriteRenderer.color = originalColor;
                }
                isHeld = false;
            }
            else if (isPlayerNearby && playerTransform != null)
            {
                // Drop the NPC
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
                    PlayAudio(pickupSound);
                }
            }
        }
    }

    private void Drop()
    {
        transform.SetParent(null);
        currentlyHeldNPC = null;
        if (spriteRenderer != null) 
        {
            spriteRenderer.color = originalColor;
        }
        isHeld = false;
    }

    public void DropItem()
    {
        Drop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isHeld)
        {
            isPlayerNearby = true;
            playerTransform = other.transform;
            if (!isHeld && spriteRenderer != null)
            {
                spriteRenderer.color = highlightColor;
                other.gameObject.transform.GetChild(2).transform.GetChild(1).GetComponent<GlyphScript>().ActivatePickUpGlyph();
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
            other.gameObject.transform.GetChild(2).transform.GetChild(1).GetComponent<GlyphScript>().DisableIndicator();
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        if (soundEffectSource == null)
        {
            return;
        }
        if (clip == null)
        {
            return;
        }

        soundEffectSource.clip = clip;
        soundEffectSource.Play();
    }
    public AudioSource GetSoundEffectSource()
    {
        return soundEffectSource;
    }
}
