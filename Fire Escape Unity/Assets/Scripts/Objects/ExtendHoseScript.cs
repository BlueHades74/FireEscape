using UnityEngine;

public class ExtendHoseScript : MonoBehaviour
{
    private GameObject anchor;
    private GameObject lastSegment;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anchor = transform.GetChild(transform.childCount - 1).gameObject;
        lastSegment = transform.GetChild(transform.childCount - 2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastSegment.transform.position, anchor.transform.position) >= 2)
        {

        }
    }
}
