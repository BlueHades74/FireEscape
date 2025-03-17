using UnityEngine;
using UnityEngine.SceneManagement; // Required to use SceneManager

public class MenuManager : MonoBehaviour
{
    // Name of the scene to load when the player clicks the "Play" button
    public string gameplaySceneName = "Gameplay";

    // Function to load the gameplay scene when the player clicks the "Play" button
    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    // Function to quit the game when the player clicks the "Quit" button
    public void QuitGame()
    {
        Application.Quit();
    }

}
