// Author: Jacob Biles
/* Purpose & How to use
* Purpose: A base for all NPC, for other NPC implmentations to be inherient
* How to use:
* Do not use this script, use it as a base for inheritence
*/
using UnityEngine;

public class NPCBase : MonoBehaviour
{

    // The name of the NPC
    public string npcName;
    // The text option that the character will say
    public string[] npcDialog;
    // The responses based on certain characters
    public string[] playerResponseDialog;
    public float interactableDistace;

}
