using UnityEngine;

public class LevelDataStorage : MonoBehaviour
{
    [SerializeField]
    private LevelInfo levelInfo;

    public LevelInfo LevelInfo { get => levelInfo;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelInfo(LevelInfo level)
    {
        levelInfo = level;
    }
}
