using UnityEngine;
using static UnityEditor.Progress;

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
        if (transform.childCount != previousChildCount)
        {
            previousChildCount = transform.childCount;
            CheckForDoubleObject();
            CheckForActionItem();
            CheckForNewHeldSprite();

        }
    }

    private void CheckForDoubleObject()
    {
        int objectcount = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).tag == "Object")
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

    private void CheckForActionItem()
    {
        GameObject actionItem = null;
        
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

    private void CheckForNewHeldSprite()
    {
        GameObject item = null;

        try
        {
            item = GetComponentInChildren<ObjectManager>().gameObject;
            if (item.GetComponent<ObjectManager>().ImageUI == null)
            {
                item = null;
            }
        }
        catch { }

        if (item == null)
        {
            ItemEventsScript.OnItemChanged.Invoke(GetComponent<PlayerInputController>().PlayerIndex + 1, null);
        }
        else
        {
            ItemEventsScript.OnItemChanged.Invoke(GetComponent<PlayerInputController>().PlayerIndex + 1, item.GetComponent<ObjectManager>().ImageUI);
        }    
    }
}
