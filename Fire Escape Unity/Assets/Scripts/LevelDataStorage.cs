using UnityEngine;

public class LevelDataStorage : MonoBehaviour
{
    [SerializeField]
    private bool p1Ready;
    [SerializeField]
    private bool p2Ready;
    private GameObject FadeInOut;

    private bool sent;

    [SerializeField]
    private LevelInfo levelInfo;

    public LevelInfo LevelInfo { get => levelInfo;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FadeInOut = GameObject.FindGameObjectWithTag("FadeInOut");
        sent = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (p1Ready && p2Ready && sent)
        {
            sent = false;
            FadeInOut.GetComponent<FadeInOutScript>().FadeOutChangeScene(levelInfo.LevelName);
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
