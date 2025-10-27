using UnityEngine;

public class SinglePlayerPushPickUp : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private GameObject originalParent;
    private Vector3 originalPosition;
    private BoxCollider2D boxCollider;
    private ObjectManager objectManager;
    private bool held;
    private string playerName;

    private static bool p1Hold;
    private static bool p2Hold;

    public GameObject OriginalParent { get => originalParent; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalParent = transform.parent.gameObject;
        originalPosition = transform.localPosition;
        boxCollider = GetComponent<BoxCollider2D>();
        objectManager = GetComponent<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null)
        {
            transform.SetParent(originalParent.transform);
        }
        else if (transform.parent.gameObject == originalParent)
        {
            transform.localPosition = originalPosition;

            if (held == true)
            {
                SwapPlayerActiveState(playerName, false);
                held = false;
            }
        }
        else
        {
            playerName = transform.parent.name;
            SwapPlayerActiveState(playerName, true);
            held = true;
        }
        
        boxCollider.excludeLayers = LayerMask.GetMask(GetExclusionMaskNames());

        //RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        //bool test = false;
        //for (int i = 0; i < hits.Length; i++)
        //{
            
        //    if (hits[i].collider != boxCollider && hits[i].collider.gameObject.name.Contains("SinglePlayerPushPickUp") && transform.parent.gameObject == originalParent)
        //    {
        //        test = true;
        //        gameObject.GetComponent<ObjectManager>().enabled = false;
        //    }
        //}
    }

    private void SwapPlayerActiveState(string pName, bool intendedState)
    {
        if (pName == "Player 1")
        {
            p1Hold = intendedState;
        }
        else
        {
            p2Hold = intendedState;
        }
    }

    private string[] GetExclusionMaskNames()
    {
        string[] layerNames = new string[p1Hold.GetHashCode() + p2Hold.GetHashCode()];
        int index = 0;

        if (p1Hold)
        {
            layerNames[index] = "Player 1";
            index++;
        }

        if (p2Hold)
        {
            layerNames[index] = "Player 2";
        }
        
        return layerNames;
    }
}
