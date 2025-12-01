using UnityEngine;
//Author Alex
public class BonusTracker : MonoBehaviour
{
    public static int TotalCount { get; private set; }
    public static int CollectedCount { get; private set; }

    void Start()
    {
        TotalCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
        CollectedCount = 0;
    }

    // Call this when the player picks up a collectible
    public static void RegisterBonusCollected()
    {
        CollectedCount++;
    }
}
