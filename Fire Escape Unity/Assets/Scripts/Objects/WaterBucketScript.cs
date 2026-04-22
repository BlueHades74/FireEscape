using UnityEngine;

public class WaterBucketScript : MonoBehaviour
{
    private bool isFilled;

    [SerializeField]
    private Sprite filledSprite;
    [SerializeField]
    private Sprite emptySprite;
    [SerializeField]
    private Sprite filledSpriteUI;
    [SerializeField]
    private Sprite emptySpriteUI;
    [SerializeField]
    private int fullCharges;
    [SerializeField]
    private int currentCharges;
    [SerializeField]
    private AudioClip waterRefillSound;
    [SerializeField]
    private AudioSource audioSource;

    public bool IsFilled { get => isFilled; }
    public int CurrentCharges { get => currentCharges; set => currentCharges = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = this.GetComponent<ObjectManager>().GetSoundEffectSource();
        isFilled = true;
        currentCharges = fullCharges;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Empties the bucket.
    /// </summary>
    public void EmptyBucket()
    {
        isFilled = false;
        GetComponent<SpriteRenderer>().sprite = emptySprite;
        GetComponent<ObjectManager>().ImageUI = emptySpriteUI;
        ItemEventsScript.OnItemChanged(transform.parent.gameObject.GetComponent<PlayerInputController>().PlayerIndex + 1, emptySpriteUI);
    }

    /// <summary>
    /// Fills the bucket.
    /// </summary>
    public void FillBucket()
    {
        isFilled = true;
        currentCharges = fullCharges;
        audioSource.clip = waterRefillSound;
        audioSource.Play();
        GetComponent<SpriteRenderer>().sprite = filledSprite;
        GetComponent<ObjectManager>().ImageUI = filledSpriteUI;
        ItemEventsScript.OnItemChanged(transform.parent.gameObject.GetComponent<PlayerInputController>().PlayerIndex + 1, filledSpriteUI);
    }
}
