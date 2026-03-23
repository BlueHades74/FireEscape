using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Made by: Jacob Biles
// How to use:
/* OP 1: 
1. Create questNPC prefab
    found in > prefabs> dialog
*/
public class TextBoxDialog : MonoBehaviour
{
    private List<QuestNPC> npcList = new List<QuestNPC>();
    private List<TextMeshPro> textBoxList = new List<TextMeshPro>();
    public GameObject playerOne, playerTwo;
    private GameObject playerOneTextUI, playerTwoTextUI;
    private TextMeshPro playerOneText, playerTwoText;
    public float waitTime;

    void Start()
    {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.name == "Player 1") playerOne = p;
            else if (p.name == "Player 2") playerTwo = p;
        }

        // Get all NPCS
        foreach (GameObject n in GameObject.FindGameObjectsWithTag("NPC"))
        {
            if (n.transform.childCount > 0)
            {
                npcList.Add(n.GetComponent<QuestNPC>());
            }
        }

        // Get all NPC text boxes
        foreach (QuestNPC n in npcList) textBoxList.Add(n.GetComponentInChildren<TextMeshPro>());

        Debug.Log(textBoxList.Count);

        // Deactivate all NPC text boxes
        foreach (TextMeshPro t in textBoxList) t.transform.parent.gameObject.SetActive(false);

        // Get player Text Boxes Objects
        foreach (Transform c in playerOne.transform) if (c.gameObject.name == "Player 1 Textbox") playerOneTextUI = c.gameObject;
        foreach (Transform c in playerTwo.transform) if (c.gameObject.name == "Player 2 Textbox") playerTwoTextUI = c.gameObject;

        // Get player Textmesh
        playerOneText = playerOneTextUI.GetComponentInChildren<TextMeshPro>();
        playerTwoText = playerTwoTextUI.GetComponentInChildren<TextMeshPro>();

        // Disable Player Text boxes
        playerOneTextUI.SetActive(false);
        playerTwoTextUI.SetActive(false);

        Debug.LogWarning("Player One: " + playerOne.name);
    }

    void Update()
    {
        // Loop through all NPCS in a scence
        for (int i = 0; i < npcList.Count; i++)
        {
            try
            {
                // Call QuestNPC.isPlayerWithinRange() -- to determine which and if player is in range
                (int, bool) withinRange = npcList[i].isPlayerWithinRange();

                // Swtich based on which character & if is in range
                switch (withinRange)
                {
                    // Player 1 in range
                    case (1, true):

                        // Has Intial Dialog Triggered?
                        if (npcList[i].HasHadInitialDialogTrigger == false) StartCoroutine(doInitialDialog(playerOne, playerOneText, playerOneTextUI, npcList[i], textBoxList[i], waitTime));

                        // Has pickup Dialog Triggered?
                        else if (npcList[i].HasHadPickupDialogTrigger == false && npcList[i].GetComponent<ObjectManager>().IsHeld == true) StartCoroutine(doPickupDialog(playerOne, playerOneText, playerOneTextUI, npcList[i], textBoxList[i], waitTime));
                        break;

                    // Player 2 in range
                    case (2, true):
                        if (npcList[i].HasHadInitialDialogTrigger == false) StartCoroutine(doInitialDialog(playerTwo, playerTwoText, playerTwoTextUI, npcList[i], textBoxList[i], waitTime));
                        else if (npcList[i].HasHadPickupDialogTrigger == false && npcList[i].GetComponent<ObjectManager>().IsHeld == true) StartCoroutine(doPickupDialog(playerTwo, playerTwoText, playerTwoTextUI, npcList[i], textBoxList[i], waitTime));
                        break;

                    // No player in range
                    case (0, false):
                        break;

                    // Default -- I would be impressed if this ever happened
                    default:
                        Debug.LogWarning("How did we get here? Well its like because of because of variable data: " + withinRange);
                        break;
                }
            }
            catch
            {
                npcList.RemoveAt(i);
                textBoxList.RemoveAt(i);
            }
        }
    }

    // Handler function for range-based character-npc interactions (Range triggered)
    IEnumerator doInitialDialog(GameObject player, TextMeshPro playerTextbox, GameObject playerTextUI, QuestNPC npc, TextMeshPro npcTextbox, float time)
    {
        // Set the flag for dialog triggered (Wont reoccur)
        npc.HasHadInitialDialogTrigger = true;

        // Assing the text & activate the textbox
        npcTextbox.text = npc.npcDialog[0];
        npcTextbox.transform.parent.gameObject.SetActive(true);

        // Wait for a select time & then disable camera
        yield return new WaitForSeconds(time);
        npcTextbox.transform.parent.gameObject.SetActive(false);
        npcTextbox.text = "";

        // Check which player is the one who triggered the dialog & assign their dialog
        if (player.name == "Player 1") playerTextbox.text = npc.playerResponseDialog[0];
        else playerTextbox.text = npc.playerResponseDialog[1];
        playerTextUI.SetActive(true);

        // Wait a set amount of time and then disabler player textbox
        yield return new WaitForSeconds(time);
        playerTextUI.SetActive(false);
        playerTextbox.text = "";

    }

    // Handler function for pickup interaction (Pickup triggered)
    IEnumerator doPickupDialog(GameObject player, TextMeshPro playerTextbox, GameObject playerTextUI, QuestNPC npc, TextMeshPro npcTextbox, float time)
    {
        // set the pickedup at least once trigger - Avoids triggering multiple times
        npc.HasHadPickupDialogTrigger = true;

        // Assign the npc dialog & enable their box
        npcTextbox.text = npc.npcDialog[1];
        npcTextbox.transform.parent.gameObject.SetActive(true);

        // Wait a select amount of time
        yield return new WaitForSeconds(time);
        try
        {
            npcTextbox.transform.parent.gameObject.SetActive(false);
            npcTextbox.text = "";
        }
        catch { }

        // Determine which player triggered the pickup, assign their text, and enable the box
        if (player.name == "Player 1") playerTextbox.text = npc.playerResponseDialog[2];
        else playerTextbox.text = npc.playerResponseDialog[3];
        playerTextUI.SetActive(true);

        // Wait for a set amount of time & disable the textbox
        yield return new WaitForSeconds(time);
        playerTextUI.SetActive(false);
        playerTextbox.text = "";

    }
}

