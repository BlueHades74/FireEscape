using UnityEngine;

public class LadderHolePickup : MonoBehaviour
{
    private Transform parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        GoBackToParent();
    }

    /// <summary>
    /// Returns to its original parent
    /// </summary>
    public void GoBackToParent()
    {
        if (transform.parent == null)
        {
            transform.SetParent(parent);
        }
    }
}
