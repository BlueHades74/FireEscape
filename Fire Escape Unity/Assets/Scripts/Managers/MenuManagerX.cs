using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManagerX : MonoBehaviour
{
    Scene scene;

    private void OnEnable()
    {
        //GameEvents.LoadNextScene += LoadGameplayScene;
    }

    private void OnDisable()
    {
        //GameEvents.LoadNextScene -= LoadGameplayScene;
    }

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    IEnumerator LoadAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.buildIndex + 1);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Loads the gameplay scene when the player clicks the "Play" button.
    /// </summary>
    public void LoadGameplayScene()
    {
        StartCoroutine(LoadAsyncScene());
    }

    /// <summary>
    /// Quits the game when the player clicks the "Quit" button.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
