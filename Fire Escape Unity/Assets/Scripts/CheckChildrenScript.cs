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
            actionItem = GetComponentInChildren<ActionItemScript>().gameObject;
        }
        catch { }

        if (actionItem != null)
        {
            Debug.Log(actionItem.name);
        }
        else
        {
            Debug.Log("No action item");
        }
    }
}
