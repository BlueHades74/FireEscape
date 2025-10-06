using UnityEngine;

public class LadderPickupLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject ladder;
    
    private SpriteRenderer spriteRenderer;

    public GameObject Ladder { get => ladder; set => ladder = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to see whether it is on the player or not
        if (transform.parent == null || transform.parent.GetComponent<PlayerActionScript>() == null)
        {
            transform.position = ladder.transform.position;
            transform.rotation = ladder.transform.rotation;
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
