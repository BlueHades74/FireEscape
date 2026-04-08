using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataStorage : MonoBehaviour
{
    private bool p1Ready;
    private bool p2Ready;

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
