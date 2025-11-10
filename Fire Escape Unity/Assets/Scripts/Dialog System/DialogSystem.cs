using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{

    private GameObject talkingNPC;
    private Canvas dialogCanvas;
    private Text nameText;
    private Text speakingText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Canvas[] canvases = GetComponents<Canvas>();
        foreach (Canvas c in canvases) if (c.name == "Dialog System") dialogCanvas = c;

        GameObject textParent = dialogCanvas.GetComponent<GameObject>();
        Text[] textComponents = textParent.GetComponentsInChildren<Text>();
        
        foreach (Text t in textComponents)
        {
            if (t.name == "Name Text") nameText = t;
            else if(t.name == "Speaking Text") speakingText = t;
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Dialog sytem data: " + dialogCanvas.name);

    }
}
