using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBoxDialog : MonoBehaviour
{
    private List<QuestNPC> npcList = new List<QuestNPC>();
    private List<TextMeshPro> textBoxList = new List<TextMeshPro>();
    private GameObject playerOne, playerTwo;
    public float interactableDistace;
    private TextMeshPro currentTextbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject n in GameObject.FindGameObjectsWithTag("NPC")) npcList.Add(n.GetComponent<QuestNPC>());
        foreach (QuestNPC n in npcList) textBoxList.Add(gameObject.GetComponentInParent<TextMeshPro>());
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.name == "Player 1") playerOne = p;
            else if (p.name == "Player 2") playerTwo = p;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < npcList.Count; i++)
        {
            (int, bool) withinRange = npcList[i].isPlayerWithinRange();
            switch (withinRange)
            {
                case (1, true):
                    doDialog(playerOne, npcList[i], textBoxList[i]);
                    break;
                case (2, true):
                    doDialog(playerTwo, npcList[i], textBoxList[i]);
                    break;
                case (0, false):
                    break;
                default:
                    Debug.LogWarning("How did we get here? Well its because of variable data: " + withinRange);
                    break;
            }
        }
    }

    void doDialog(GameObject player, QuestNPC npc, TextMeshPro textbox)
    {
        textbox.text = npc.npcDialog[0];
        // player dialog
        // wait a few seconds
        textbox.text = npc.npcDialog[1];
        // wait a few seconds
        npc.HasTalked = true;
    }
}

