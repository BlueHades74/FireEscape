using Unity.VisualScripting;
using UnityEngine;

public class HoseAnchorScript : MonoBehaviour
{
    [SerializeField]
    private GameObject hoseAnchor;
    private Vector2 distance;
    private Vector2 modifiedDistance;

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
            transform.position = hoseAnchor.transform.position - separation;
        }
    }
}
