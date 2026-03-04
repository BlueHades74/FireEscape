using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class BreakableObjectScript : MonoBehaviour
{
    // making healthPoints a SerializeField will break the animation
    private int healthPoints;
    private TextMeshProUGUI text;

    // Door destroyed animation variables
    private Animator animator;

    // bool for whether door just explode or not, true = yes
    [SerializeField] private bool popDoor;

    // Place prefab for particle effect when hit here.
    [SerializeField] private VisualEffect particleWhenHit;

    private AudioSource hitSound;

    private GameObject glyphTrigger;

    [SerializeField]
    private Material borderlessMat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthPoints = 3;
        //text = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        hitSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        try
        {
            text = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        } 
        catch
        {

        }
        glyphTrigger = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (popDoor)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Removes one hp from the object and hides it if the hp is 0
    /// </summary>
    public void DamageBreakable()
    {
        healthPoints--;
        //text.text = healthPoints.ToString();

        animator.SetInteger("TimesHit", healthPoints);

        // If there is an assigned particle, create it when hit.
        if (particleWhenHit != null) {
            var hitParticle = Instantiate(particleWhenHit, transform.position, Quaternion.identity);
            Destroy(hitParticle, 1.5f);
        }

        hitSound.Play();
        //if (healthPoints <= 0)
        //{
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //text.text = "";
        //}

        if (healthPoints <= 0)
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().material = borderlessMat;
            glyphTrigger.SetActive(false);
        }
    }
}
