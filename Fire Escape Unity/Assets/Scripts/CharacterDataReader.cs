using UnityEngine;

public class CharacterDataReader : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles

    [SerializeField]
    private CharacterData data;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D bx;
    private BoxCollider2D childBx;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bx = GetComponent<BoxCollider2D>();
        childBx = transform.GetChild(1).GetComponent<BoxCollider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get the data from the data storage script.
        data = GetComponent<CharacterDataStorage>().CurrentData;
        UpdatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets the player's sprite and animations. (What character they are playing as).
    /// </summary>
    private void UpdatePlayer()
    {
        spriteRenderer.sprite = data.CharacterSprite;
        animator.runtimeAnimatorController = data.AnimatorController;
        bx.size = data.dynamicColliderSize;
        childBx.size = data.kinematicColliderSize;
    }
}
