using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GlyphScript : MonoBehaviour
{
    //Created By: Rafael Gonzalez Atiles
    //Last Edited By: Rafael Gonzalez Atiles

    private int frame = 0;
    private float timeperframe = 0.5f;
    private float frametimer = 0.5f;
    [SerializeField]
    private Sprite[] pickupKeyboardGlyphFrames;
    [SerializeField]
    private Sprite[] actionKeyboardGlyphFrames;
    [SerializeField]
    private Sprite[] pickupControllerGlyphFrames;
    [SerializeField]
    private Sprite[] actionControllerGlyphFrames;
    [SerializeField]
    private Sprite[] movementGlyphFrames;

    [SerializeField]
    private Image indicator;

    [SerializeField]
    private PlayerActionScript actionScript;

    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    private bool shouldShowMovementKeys;

    private Vector3 originPos;

    private Sprite[] currentSpriteList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the Components
        indicator = GetComponent<Image>();
        actionScript = transform.parent.transform.parent.GetComponent<PlayerActionScript>();
        input = transform.parent.transform.parent.GetComponent<PlayerInput>();

        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpriteList != null)
        {
            frametimer -= Time.deltaTime;
            if (frametimer <= 0.0f)
            {
                int max = currentSpriteList.GetLength(0) - 1;
                //Debug.Log(max);
                frame += 1;
                if (frame > max)
                {
                    frame = 0;
                }
                UpdateGlyphFrame(frame);
                frametimer = timeperframe;
            }
        }
        if (shouldShowMovementKeys == true && input.devices[0].description.deviceClass != "" && indicator.enabled == false)
        {
            SetImageAndActivate(movementGlyphFrames);
        }

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
    private void SetImageAndActivate(Sprite[] imageToSet)
    {
        indicator.enabled = true;
        currentSpriteList = imageToSet;
        UpdateGlyphFrame(0);
    }
    private void UpdateGlyphFrame(int index)
    {
        indicator.sprite = currentSpriteList[index];
    }

    /// <summary>
    /// Deactivates the game object
    /// </summary>
    public void DisableIndicator()
    {
        indicator.enabled = false;
        indicator.sprite = null;
        currentSpriteList = null;
    }

    /// <summary>
    /// Sends the glyph for picking up items.
    /// </summary>
    public void ActivatePickUpGlyph()
    {
        if (input.devices[0].description.deviceClass == "")
        {
            SetImageAndActivate(pickupControllerGlyphFrames);
        }
        else
        {
            SetImageAndActivate(pickupKeyboardGlyphFrames);
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
                    SetImageAndActivate(actionControllerGlyphFrames);
                }
                else
                {
                    SetImageAndActivate(actionKeyboardGlyphFrames);
                }
            }
        }
    }
}
