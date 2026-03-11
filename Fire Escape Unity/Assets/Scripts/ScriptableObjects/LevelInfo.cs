using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Scriptable Objects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public string LevelName;
    public int LevelNumber;
    public string LevelDescription;
    public bool AxePresent;
    public bool BucketPresent;
    public bool CrowbarPresent;
    public bool ExtinguisherPresent;
    public bool LadderPresent;
}
