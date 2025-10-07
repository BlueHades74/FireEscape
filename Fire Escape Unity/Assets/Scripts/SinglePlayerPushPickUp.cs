using UnityEngine;

public class SinglePlayerPushPickUp : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    private GameObject originalParent;
    private Vector3 originalPosition;

    public GameObject OriginalParent { get => originalParent; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalParent = transform.parent.gameObject;
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null)
        {
            transform.SetParent(originalParent.transform);
        }

        if (transform.parent.gameObject == originalParent)
        {
            transform.localPosition = originalPosition;
        }
    }
}
