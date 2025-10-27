using UnityEngine;

public class SinglePlayerPushPickUp : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private GameObject originalParent;
    private Vector3 originalPosition;
    private BoxCollider2D boxCollider;
    private ObjectManager objectManager;

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
        }
        else
        {

        }

        if (boxCollider.enabled == false)
        {
            objectManager.enabled = false;
        }
        else
        {
            objectManager.enabled = true;
        }
    }
}
