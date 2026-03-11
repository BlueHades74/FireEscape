using UnityEngine;

public class CharacterDataReader : MonoBehaviour
{
    [SerializeField]
    private CharacterData data;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data = GetComponent<CharacterDataStorage>().CurrentData;
        UpdatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatePlayer()
    {
        spriteRenderer.sprite = data.CharacterSprite;
        animator.runtimeAnimatorController = data.AnimatorController;
    }
}
