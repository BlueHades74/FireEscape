// Author: Jacob Biles
/* Purpose & How to use
* Purpose: A base for all NPC, for other NPC implmentations to be inherient
* How to use:
* Do not use this script, use it as a base for inheritence
*/
using UnityEngine;

public class NPCBase : MonoBehaviour
{

    // The sprite to be used
    public Sprite sprite;
    // The sprite Render component
    private SpriteRenderer npcSpriteRender;
    // The name of the NPC
    public string npcName;
    // The text option that the character will say
    public string[] npcDialog;
    // The responses based on certain characters
    public string[] playerResponseDialog;
    public float interactableDistace;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the sprite render component
        npcSpriteRender = GetComponent<SpriteRenderer>();

        // Assign the sprite to the renderer sprite
        npcSpriteRender.sprite = sprite;

    }
}
