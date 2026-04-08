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

    private AsyncOperation preloadOp;
    public LevelInfo LevelInfo { get => levelInfo;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start loading scene in background
        if ( levelInfo != null)
        {
            Debug.Log($"[LevelDataStorage] Starting preload of scene: {levelInfo.LevelName}");

            preloadOp = SceneManager.LoadSceneAsync(levelInfo.LevelName);
            preloadOp.allowSceneActivation = false;
        }
        else
        {
            Debug.LogWarning("[LevelDataStorage] No LevelInfo assigned! Cannot preload scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (preloadOp != null)
        {
            float progress = Mathf.Clamp01(preloadOp.progress / 0.9f);
            Debug.Log($"[LevelDataStorage] Preload progress: {progress * 100f}%");
        }

        if (p1Ready && p2Ready)
        {
            preloadOp.allowSceneActivation = true;
        }
    }

    public void SetLevelInfo(LevelInfo level)
    {
        //Sets the levelinfo that determines what level to go to
        levelInfo = level;
    }

    //Readies or Unreadies player 1
    public void FlipP1Ready()
    {
        p1Ready = !p1Ready;
    }

    //Readies or Unreadies player 2
    public void FlipP2Ready()
    {
        p2Ready = !p2Ready;
    }

    public void CancelPreload()
    {
        if ( preloadOp != null )
        {
            Debug.Log("[LevelDataStorage] Preload canceled.");

            preloadOp.allowSceneActivation = false;

            //unload scene if it was partially loaded
            if (levelInfo != null)
            {
                SceneManager.UnloadSceneAsync(levelInfo.LevelName);
            }
            preloadOp = null;
        }
        p1Ready = false;
        p2Ready = false;

    }
}
