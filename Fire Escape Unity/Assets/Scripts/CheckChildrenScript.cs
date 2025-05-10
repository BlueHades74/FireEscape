using UnityEngine;

public class CheckChildrenScript : MonoBehaviour
{
    private int previousChildCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Only when a child is added or removed
        if (transform.childCount != previousChildCount)
        {
            previousChildCount = transform.childCount;
            CheckForDoubleObject();
            CheckForActionItem();

        }
    }

    /// <summary>
    /// Checks if the player is holding more than one object at a time.
    /// </summary>
    private void CheckForDoubleObject()
    {
        int objectcount = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).tag == "Object" || gameObject.transform.GetChild(i).tag == "NPC")
            {
                objectcount++;
            }

            if (objectcount > 1)
            {
                transform.GetChild(i).gameObject.GetComponent<ObjectManager>().DropItem();
                objectcount--;
            }
        }
    }

    /// <summary>
    /// Checks if the new item has an attached action.
    /// </summary>
    private void CheckForActionItem()
    {
        GameObject actionItem = null;

        GetComponent<PlayerMovementScript>().SetMovementByOriginalTimesParameter(1);
        
        try
        {
            actionItem = GetComponentInChildren<ObjectManager>().gameObject;
            if (actionItem.GetComponent<ObjectManager>().Action == null)
            {
                actionItem = null;
            }
        }
        catch { }

        if (actionItem != null)
        {
            GetComponent<PlayerActionScript>().enabled = true;
            GetComponent<PlayerActionScript>().ReceiveActionItem(actionItem);
        }
        else
        {
            Debug.Log("No action item");
            GetComponent<PlayerActionScript>().enabled = false;
        }
    }
}
