using UnityEngine;
using TMPro;

public class ObjectManager : MonoBehaviour
{
    public Transform playerTransform;

    [Header("Interaction Prompt")]
    public GameObject interactPromptPrefab;
    private GameObject promptInstance;

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    [TextArea(2, 4)]
    public string[] dialogueLines;

    private int currentLine = 0;
    private bool isTalking = false;
    private bool isPlayerNearby = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerTransform = other.transform;

            if (interactPromptPrefab != null && promptInstance == null)
            {
                promptInstance = Instantiate(
                    interactPromptPrefab,
                    transform.position + Vector3.up * 1.5f,
                    Quaternion.identity
                );
                promptInstance.transform.SetParent(transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            if (promptInstance != null)
            {
                Destroy(promptInstance);
                promptInstance = null;
            }

            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
                isTalking = false;
                currentLine = 0;
            }
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            if (!isTalking)
            {
                StartDialogue();
            }
            else
            {
                ContinueDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        if (dialoguePanel != null && dialogueLines.Length > 0)
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = dialogueLines[0];
            currentLine = 0;
            isTalking = true;
        }
    }

    private void ContinueDialogue()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            dialoguePanel.SetActive(false);
            isTalking = false;
            currentLine = 0;
        }
    }
}
