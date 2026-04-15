using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Rendering;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] linesOnLoad;
    [SerializeField] private float textSpeed;
    [SerializeField] private GameObject dialogueBox;

    private string[] lines;
    private int index;

    // Every time it reads the character "^" it will pause instead of printing
    [SerializeField] private float pauseCharMultiplier = 4f;
    private const char pauseChar = '\x005E';

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (linesOnLoad.Length > 0)
        {
            StartDialogue(linesOnLoad);
        }
        else
        {
            textComponent.text = string.Empty;
            dialogueBox.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // We can put this into a separate function for the input system
        // Looks for left mouse button down to do either
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
        {
            string plaintext = lines[index];
            plaintext = plaintext.Replace(pauseChar.ToString(), "");
            // 1. jump to next line/ close text box
            if (textComponent.text == plaintext)
            {
                NextLine();
            }
            else
            {
                // 2. instant print text
                StopAllCoroutines();
                textComponent.text = plaintext;
            }
        }
    }

    void StartDialogue(string[] input)
    {
        dialogueBox.SetActive(true);
        lines = input;
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Type each character 1 by 1
        foreach(char c in lines[index].ToCharArray())
        {
            if (c == pauseChar)
            {
                yield return new WaitForSeconds(textSpeed*pauseCharMultiplier);
            }
            else
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    void NextLine()
    {
        textComponent.text = string.Empty;
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueBox.SetActive(false);
        }
    }
}
