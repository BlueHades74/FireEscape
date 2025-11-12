using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBoxDialog : MonoBehaviour
{
    private List<QuestNPC> npcList = new List<QuestNPC>();
    private List<TextMeshPro> textBoxList = new List<TextMeshPro>();
    private GameObject playerOne, playerTwo;
    private GameObject playerOneTextUI, playerTwoTextUI;
    private TextMeshPro playerOneText, playerTwoText;
    public float waitTime;

    void Start()
    {
        foreach (GameObject n in GameObject.FindGameObjectsWithTag("NPC")) npcList.Add(n.GetComponent<QuestNPC>());

        foreach (QuestNPC n in npcList) textBoxList.Add(n.GetComponentInChildren<TextMeshPro>());

        foreach (TextMeshPro t in textBoxList) t.gameObject.SetActive(false);

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.name == "Player 1") playerOne = p;
            else if (p.name == "Player 2") playerTwo = p;
        }

        foreach (Transform c in playerOne.transform)if (c.gameObject.name == "Player 1 Textbox") playerOneTextUI = c.gameObject;
        foreach (Transform c in playerTwo.transform) if (c.gameObject.name == "Player 2 Textbox") playerTwoTextUI = c.gameObject;

        playerOneText = playerOneTextUI.GetComponent<TextMeshPro>();
        playerTwoText = playerTwoTextUI.GetComponent<TextMeshPro>();
        playerOneTextUI.SetActive(false);
        playerTwoTextUI.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < npcList.Count; i++)
        {
            (int, bool) withinRange = npcList[i].isPlayerWithinRange();
            switch (withinRange)
            {
                case (1, true):
                    if (npcList[i].HasTalked == false) StartCoroutine(doDialog(playerOne, playerOneText,playerOneTextUI, npcList[i], textBoxList[i], waitTime));
                    break;
                case (2, true):
                    if (npcList[i].HasTalked == false) StartCoroutine(doDialog(playerTwo, playerTwoText,playerTwoTextUI, npcList[i], textBoxList[i], waitTime));
                    break;
                case (0, false):
                    break;
                default:
                    Debug.LogWarning("How did we get here? Well its because of variable data: " + withinRange);
                    break;
            }
        }
    }


    IEnumerator doDialog(GameObject player, TextMeshPro playerTextbox, GameObject playerTextUI,QuestNPC npc, TextMeshPro npcTextbox, float time)
    {
        npc.HasTalked = true;
    
        npcTextbox.text = npc.npcDialog[0];
        npcTextbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(time);
        npcTextbox.gameObject.SetActive(false);
        npcTextbox.text = "";

        if (player.name == "Player 1") playerTextbox.text = npc.playerResponseDialog[0];
        else playerTextbox.text = npc.playerResponseDialog[1];
        playerTextUI.SetActive(true);

        yield return new WaitForSeconds(time);
        playerTextUI.SetActive(false);
        playerTextbox.text = "";

        npcTextbox.text = npc.npcDialog[1];
        npcTextbox.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(time);
        npcTextbox.gameObject.SetActive(false);
        npcTextbox.text = "";
        
    }
}

