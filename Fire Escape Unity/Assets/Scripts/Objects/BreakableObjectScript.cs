using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BreakableObjectScript : MonoBehaviour
{
    [SerializeField]
    private int healthPoints;
    private TextMeshProUGUI text; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthPoints = 3;
        text = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Removes one hp from the object and hides it if the hp is 0
    /// </summary>
    public void DamageBreakable()
    {
        healthPoints--;
        text.text = healthPoints.ToString();

        if (healthPoints <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            text.text = "";
        }
    }    
}
