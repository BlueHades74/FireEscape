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
    private Scene sceneto;
    [SerializeField]
    private Image overlay;

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
                SceneManager.LoadScene(sceneto.buildIndex);
            }
        }
    }

    private void FadeIn()
    {
        timeleft = totalFadeTime;
        fadingin = true;
    }
    public void FadeOutChangeScene(Scene input)
    {
        sceneto = input;
        timeleft = totalFadeTime;
        fadingin = true;
    }
}
