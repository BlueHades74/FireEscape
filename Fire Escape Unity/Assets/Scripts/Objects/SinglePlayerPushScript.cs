using Unity.VisualScripting;
using UnityEngine;

public class SinglePlayerPushScript : MonoBehaviour
{
    private GameObject[] children;
    private GameObject[] verticalChildren;
    private GameObject[] horizontalChildren;

    private int verticalModifier;
    private int horizontalModifier;

    private int[] savedModifier;

    private Vector3 oldPosition;

    public int[] SavedModifier { get => savedModifier; }
    public Vector3 OldPosition { get => oldPosition; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        children = new GameObject[transform.childCount];

        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }

        verticalChildren = new GameObject[transform.childCount / 2];
        horizontalChildren = new GameObject[transform.childCount / 2];
        SplitChildren();
        
        verticalModifier = (int)Mathf.Sign(verticalChildren[0].transform.position.y - transform.position.y);
        horizontalModifier = (int)Mathf.Sign(horizontalChildren[0].transform.position.x - transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (children.Length > transform.childCount)
        {
            try
            {
                oldPosition = transform.position;
                GetComponent<BoxCollider2D>().enabled = false;
                GameObject missingChild = FindMissingChild();

                int[] modifier = FindModifier(missingChild);

                float difference = missingChild.transform.position.y - transform.position.y;

                Vector3 temp = missingChild.transform.position;
                temp.x += 1.1f * modifier[0];
                if (modifier[0] == 0)
                {
                    temp.x = transform.position.x;
                }
                temp.y += 1.1f * modifier[1];
                if (modifier[1] == 0)
                {
                    temp.y = transform.position.y;
                }
                transform.position = temp;

                savedModifier = modifier;
                Debug.Log(savedModifier[0] + " " + savedModifier[1]);
            }
            catch { }
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
            activateAllChildren();
        }
    }

    private void SplitChildren()
    {
        int v = 0;
        int h = 0;
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].transform.localPosition.x == 0)
            {
                verticalChildren[v] = children[i];
                v++;
            }
            else
            {
                horizontalChildren[h] = children[i];
                h++;
            }
        }
    }

    private GameObject FindMissingChild()
    {
        GameObject activeChild = null;
        for (int i = 0; i < children.Length; i++)
        {
            if (activeChild == null && children[i].transform.parent.gameObject != gameObject)
            {
                activeChild = children[i];
            }
            else
            {
                children[i].GetComponent<ObjectManager>().DropItem();
                children[i].transform.SetParent(gameObject.transform);
                children[i].GetComponent<SpriteRenderer>().enabled = false;
                children[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        return activeChild;
    }

    private int[] FindModifier(GameObject missingChild)
    {
        int[] modifier = { 0, 0 };

        for (int i = 0; i < horizontalChildren.Length; i++)
        {
            if ((horizontalChildren[i].gameObject == missingChild))
            {
                modifier[0] = (-1 * ((i + 1) % 2) * horizontalModifier) + (i * horizontalModifier);
            }
        }
        if (modifier[0] == 0)
        {
            if (verticalChildren[0] == missingChild)
            {
                modifier[1] = -verticalModifier;
            }
            else
            {
                modifier[1] = verticalModifier;
            }
        }
        return modifier;
    }

    private void activateAllChildren()
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].GetComponent<SpriteRenderer>().enabled = true;
            children[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
