// Author: Jacob Biles
/* Purpose & How to use
* Purpose: A base for all NPC, for other NPC implmentations to be inherient
* How to use:
* Do not use this script, use it as a base for inheritence
*/
using UnityEngine;

public class NPCBase : MonoBehaviour
{
    public Sprite sprite;
    private SpriteRenderer npcSpriteRender;
    public string npcName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcSpriteRender = GetComponent<SpriteRenderer>();
        npcSpriteRender.sprite = sprite;
    }
}
