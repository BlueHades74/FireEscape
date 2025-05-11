using UnityEngine;
using UnityEngine.UIElements;

public class DebrisPickup : MonoBehaviour
{
    private GameObject originalParent;

    public GameObject OriginalParent { get => originalParent; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalParent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null)
        {
            transform.SetParent(originalParent.transform);
        }

        Debug.Log(originalParent.transform.rotation.z);

        if ((originalParent.transform.rotation.z < 0.1 && originalParent.transform.rotation.z > -0.1))
        {
            if (transform.parent.gameObject == originalParent)
            {
                transform.position = new Vector3(transform.position.x, originalParent.transform.position.y, 0);
            }
        }
        else
        {
            if (transform.parent.gameObject == originalParent)
            {
                transform.position = new Vector3(originalParent.transform.position.x, transform.position.y, 0);
            }
        }
        
    }
}
