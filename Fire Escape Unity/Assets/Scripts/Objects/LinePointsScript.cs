using UnityEngine;

public class LinePointsScript : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    private GameObject[] points;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].transform.position);
        }
    }
}
