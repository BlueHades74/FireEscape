using UnityEngine;

//Author Alex
public class FireTracker : MonoBehaviour
{

    public static int TotalCount { get; private set; }
    public static int ExtinguishedCount { get; private set; }

    private void Start()
    {
        // Track total numbers of fires based on tag
        TotalCount = GameObject.FindGameObjectsWithTag("Fire").Length;
        ExtinguishedCount = 0;
    }

    //Call this when a fire is extinguished
    public static void RegisterFireExtinguished()
    {
        ExtinguishedCount++;
    }

    public static float GetPercentExtinguished()
    {
        if (TotalCount == 0) return 1f;
        return (float)ExtinguishedCount / TotalCount;
    }
}
