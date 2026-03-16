using UnityEngine;

public class GlyphTriggerScript : MonoBehaviour
{
    [SerializeField]
    private string requiredActionString;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Detects if the player triggered it, sends what item we need for the glyph to be relevant.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.GetChild(2).transform.GetChild(1).GetComponent<GlyphScript>().ActivateActionGlyph(requiredActionString);
        }
    }

    /// <summary>
    /// Detects if the Player exited it, if so it should be disabled.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.GetChild(2).transform.GetChild(1).GetComponent<GlyphScript>().DisableIndicator();
        }
    }
}
