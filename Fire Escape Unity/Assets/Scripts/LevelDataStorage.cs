using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataStorage : MonoBehaviour
{
    private bool p1Ready;
    private bool p2Ready;

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
        if (p1Ready && p2Ready)
        {
            SceneManager.LoadScene(levelInfo.LevelName);
        }
    }

    public void SetLevelInfo(LevelInfo level)
    {
        levelInfo = level;
    }

    public void FlipP1Ready()
    {
        p1Ready = !p1Ready;
    }

    public void FlipP2Ready()
    {
        p2Ready = !p2Ready;
    }
}
