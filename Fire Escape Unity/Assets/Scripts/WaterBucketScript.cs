using UnityEngine;

public class WaterBucketScript : MonoBehaviour
{
    private bool isFilled;

    [SerializeField]
    private Sprite filledSprite;
    [SerializeField]
    private Sprite emptySprite;

    public bool IsFilled { get => isFilled; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFilled = true;
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
        GetComponent<ObjectManager>().ImageUI = emptySprite;
        ItemEventsScript.OnItemChanged(transform.parent.gameObject.GetComponent<PlayerInputController>().PlayerIndex + 1, emptySprite);
    }

    /// <summary>
    /// Fills the bucket.
    /// </summary>
    public void FillBucket()
    {
        isFilled = true;
        GetComponent<SpriteRenderer>().sprite = filledSprite;
        GetComponent<ObjectManager>().ImageUI = filledSprite;
        ItemEventsScript.OnItemChanged(transform.parent.gameObject.GetComponent<PlayerInputController>().PlayerIndex + 1, filledSprite);
    }
}
