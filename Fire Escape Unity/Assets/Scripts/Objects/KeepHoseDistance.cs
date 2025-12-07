using UnityEngine;

public class KeepHoseDistance : MonoBehaviour
{
    [SerializeField]
    private GameObject hoseNozzle;
    [SerializeField]
    private GameObject anchor;
    [SerializeField]
    private float distance;

    private Vector3 loc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distance = Vector3.Distance(hoseNozzle.transform.position, anchor.transform.position);
        loc = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (distance < Vector3.Distance(hoseNozzle.transform.position, anchor.transform.position))
        {
            if (hoseNozzle.transform.parent != null)
            {
                hoseNozzle.transform.parent.transform.position = loc;
            }
        }
        else
        {
            if (hoseNozzle.transform.parent != null)
            {
                loc = hoseNozzle.transform.parent.transform.position;
            }
        }
    }
}
