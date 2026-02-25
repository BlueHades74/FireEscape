using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GlyphScript : MonoBehaviour
{
    //Created By: Rafael Gonzalez Atiles
    //Last Edited By: Rafael Gonzalez Atiles

    [SerializeField]
    private Sprite pickUpGlyphKeyboard;
    [SerializeField]
    private Sprite actionGlyphKeyboard;
    [SerializeField]
    private Sprite pickUpGlyphController;
    [SerializeField]
    private Sprite actionGlyphController;
    [SerializeField]
    private Sprite movementGlyph;

    [SerializeField]
    private Image indicator;

    [SerializeField]
    private PlayerActionScript actionScript;

    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    private bool shouldShowMovementKeys;

    private Vector3 originPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the Components
        indicator = GetComponent<Image>();
        actionScript = transform.parent.transform.parent.GetComponent<PlayerActionScript>();
        input = transform.parent.transform.parent.GetComponent<PlayerInput>();

        if (shouldShowMovementKeys == true && input.devices[0].description.deviceClass != "")
        {
            SetImageAndActivate(movementGlyph);
            originPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShowMovementKeys == true)
        {
            if (transform.position != originPos)
            {
                shouldShowMovementKeys = false;
                DisableIndicator();
            }
        }
    }

    /// <summary>
    /// Sets the glyph sprite and activates the game object.
    /// </summary>
    /// <param name="imageToSet"></param>
    private void SetImageAndActivate(Sprite imageToSet)
    {
        indicator.enabled = true;
        indicator.sprite = imageToSet;
    }

    /// <summary>
    /// Deactivates the game object
    /// </summary>
    public void DisableIndicator()
    {
        indicator.enabled = false;
        indicator.sprite = null;
    }

    /// <summary>
    /// Sends the glyph for picking up items.
    /// </summary>
    public void ActivatePickUpGlyph()
    {
        if (input.devices[0].description.deviceClass == "")
        {
            SetImageAndActivate(pickUpGlyphController);
        }
        else
        {
            SetImageAndActivate(pickUpGlyphKeyboard);
        }
        
    }

    /// <summary>
    /// Sends the glyph to do an action if the necessary action item is equipped.
    /// </summary>
    /// <param name="requiredAction"></param>
    public void ActivateActionGlyph(string requiredAction)
    {
        if (actionScript != null)
        {
            if (requiredAction == actionScript.ReturnActionString())
            {
                if (input.devices[0].description.deviceClass == "")
                {
                    SetImageAndActivate(actionGlyphController);
                }
                else
                {
                    SetImageAndActivate(actionGlyphKeyboard);
                }
            }
        }
    }
}
