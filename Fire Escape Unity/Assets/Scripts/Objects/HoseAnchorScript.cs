using Unity.VisualScripting;
using UnityEngine;

public class HoseAnchorScript : MonoBehaviour
{
    [SerializeField]
    private GameObject hoseAnchor;
    private Vector2 distance;
    private Vector2 modifiedDistance;

    private int testTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distance = hoseAnchor.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hoseAnchor.transform.parent != null)
        {
            //if (hoseAnchor.transform.parent.GetComponent<PlayerMovementScript>().FacingDirection.x == 0)
            //{
            //    modifiedDistance = new Vector2(distance.y, distance.x) * hoseAnchor.transform.parent.GetComponent<PlayerMovementScript>().FacingDirection;
            //}
            //else
            //{
            //    modifiedDistance = distance * hoseAnchor.transform.parent.GetComponent<PlayerMovementScript>().FacingDirection;
            //}
            //transform.position = hoseAnchor.transform.position - new Vector3(modifiedDistance.x, modifiedDistance.y, 0);
            transform.rotation = hoseAnchor.transform.rotation;

            Vector3 separation = transform.TransformPoint(0, 1, 0) - transform.position;
            //transform.position = hoseAnchor.transform.position - separation;
            //GetComponent<Rigidbody2D>().MovePosition(hoseAnchor.transform.position - separation);
            Vector3 testVal = (hoseAnchor.transform.position - separation) - transform.position;
            GetComponent<Rigidbody2D>().AddForce(testVal.normalized * (Vector3.Distance(transform.position, hoseAnchor.transform.position - separation) * 300), ForceMode2D.Impulse);
            //Debug.LogWarning(Vector3.Distance(transform.position, hoseAnchor.transform.position - separation));
            if (Vector3.Distance(transform.position, hoseAnchor.transform.position - separation) < 0.25)
            {
                transform.position = hoseAnchor.transform.position - separation;
            }

            if (Vector3.Distance(transform.position, hoseAnchor.transform.position - separation) > 0.5 && testTimer !< 0)
            {
                if (testTimer == 0)
                {
                    hoseAnchor.transform.parent.transform.position = transform.position - (transform.TransformPoint(0, 1, 0) - hoseAnchor.transform.parent.transform.position);
                }
                else
                {
                    testTimer--;
                }
            }
            else
            {
                testTimer = 2;
            }

        }
        else
        {
            transform.rotation = hoseAnchor.transform.rotation;
            Vector3 separation = transform.TransformPoint(0, 1, 0) - transform.position;
            transform.position = hoseAnchor.transform.position - separation;
        }
    }
}
