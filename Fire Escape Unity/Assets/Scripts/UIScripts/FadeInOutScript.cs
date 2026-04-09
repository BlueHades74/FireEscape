using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOutScript : MonoBehaviour
{
    [SerializeField]
    private float totalFadeTime = 1.0f;
    private float timeleft = 0.0f;
    private bool fadingin = false;
    private bool fadingout = false;
    private string sceneto;
    [SerializeField]
    private Image overlay;

    private AsyncOperation preloadOp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingin == true)
        {
            if (timeleft > 0.0f)
            {
                overlay.color = new Color(0, 0, 0, (timeleft / totalFadeTime));
                timeleft -= Time.deltaTime;
            }
            else
            {
                overlay.color = new Color(0, 0, 0, 0);
                fadingin = false;
            }
        }
        else if (fadingout == true)
        {
            if (timeleft > 0.0f)
            {
                overlay.color = new Color(0, 0, 0, 1.0f - (timeleft / totalFadeTime));
                timeleft -= Time.deltaTime;
            }
            else
            {
                overlay.color = new Color(0, 0, 0, 1.0f);
                fadingout = false;
                preloadOp.allowSceneActivation = true;
                SceneManager.LoadScene(sceneto);
            }
        }

        if (preloadOp != null)
        {
            float progress = Mathf.Clamp01(preloadOp.progress / 0.9f);
            Debug.LogWarning($"Preload progress: {progress * 100f}%");
        }
    }

    private void FadeIn()
    {
        timeleft = totalFadeTime;
        fadingin = true;
    }
    public void FadeOutChangeScene(string input)
    {
        sceneto = input;
        timeleft = totalFadeTime;
        fadingout = true;
    }

    public void PreLoadLevel(string input)
    {
        if (input != null)
        {
            Debug.Log($"Starting preload of scene: {input}");

            preloadOp = SceneManager.LoadSceneAsync(input);
            preloadOp.allowSceneActivation = false;
            sceneto = input;
        }
        else
        {
            Debug.LogWarning("No Level assigned! Cannot preload scene.");
        }
    }

    public void CancelPreload()
    {
        if (preloadOp != null)
        {
            Debug.Log("Preload canceled.");

            preloadOp.allowSceneActivation = false;

            //unload scene if it was partially loaded
            if (sceneto != null)
            {
                SceneManager.UnloadSceneAsync(sceneto);
            }
            preloadOp = null;
        }
    }
}
