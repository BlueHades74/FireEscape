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
        if (transform.childCount != previousChildCount)
        {
            previousChildCount = transform.childCount;
            CheckForDoubleObject();
            CheckForActionItem();
            CheckForNewHeldSprite();

        }
    }

    /// <summary>
    /// Check if the player is carrying more than one item
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
    /// Check to see if the item has an action attached
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
            //Debug.Log("No action item");
            GetComponent<PlayerActionScript>().enabled = false;
        }
    }

    /// <summary>
    /// Sees if the item has a held sprite.
    /// </summary>
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
