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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the sprite render component
        npcSpriteRender = GetComponent<SpriteRenderer>();
        // Assign the sprite to the renderer sprite
        npcSpriteRender.sprite = sprite;
    }
}
