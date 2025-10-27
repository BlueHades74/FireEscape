using UnityEngine;

public class BreakableObjectScript : MonoBehaviour
{
    [SerializeField]
    private int healthPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthPoints = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageBreakable()
    {
        healthPoints--;

        if (healthPoints <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }    
}
