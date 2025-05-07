using UnityEngine;
using TMPro;

public class ObjectManager : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRadius = 2.5f;
    public Transform playerTransform;

    [Header("Prompt Settings")]
    public GameObject interactPromptPrefab;
    public Vector3 promptOffset = new Vector3(0, 1.5f, 0);
    private GameObject promptInstance;

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    [TextArea(2, 4)]
    public string[] dialogueLines;

    private int currentLine = 0;
    private bool isTalking = false;

    private void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        bool inRange = distance <= interactionRadius;

        // Show prompt if in range and not talking
        if (inRange && !isTalking && promptInstance == null)
        {
            ShowPrompt();
        }

        // Hide prompt if out of range
        if (!inRange && promptInstance != null)
        {
            HidePrompt();
        }

        // Keep prompt hovering above NPC
        if (promptInstance != null)
        {
            promptInstance.transform.position = transform.position + promptOffset;
        }

        // Handle input to start or continue dialogue
        if (inRange && Input.GetKeyDown(KeyCode.F))
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = null;
            HidePrompt();
            EndDialogue();
        }
    }

    private void ShowPrompt()
    {
        if (interactPromptPrefab != null && promptInstance == null)
        {
            promptInstance = Instantiate(
                interactPromptPrefab,
                transform.position + promptOffset,
                Quaternion.identity,
                transform // ✅ Attach to NPC so it moves with them
            );
        }
    }

    private void HidePrompt()
    {
        if (promptInstance != null)
        {
            Destroy(promptInstance);
            promptInstance = null;
        }
    }

    private void StartDialogue()
    {
        if (dialoguePanel != null && dialogueText != null && dialogueLines.Length > 0)
        {
            isTalking = true;
            HidePrompt();
            dialoguePanel.SetActive(true);
            dialogueText.text = dialogueLines[0];
            currentLine = 0;
        }
        else
        {
            Debug.LogWarning("DialoguePanel, dialogueText, or dialogueLines missing.");
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
            EndDialogue();

            // If still in range, show prompt again
            if (playerTransform != null &&
                Vector3.Distance(transform.position, playerTransform.position) <= interactionRadius)
            {
                ShowPrompt();
            }
        }
    }

    private void EndDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        isTalking = false;
        currentLine = 0;
    }
}
