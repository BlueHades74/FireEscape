using UnityEngine;
using UnityEngine.SceneManagement;

namespace FireEscape.UI
{
    /// <summary>
    /// Manages the game's menu functionality, including scene transitions and game exit.
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        /// <summary>
        /// Name of the scene to load when the player clicks the "Play" button.
        /// </summary>
        public string gameplaySceneName = "Gameplay";

        /// <summary>
        /// Loads the gameplay scene when the player clicks the "Play" button.
        /// </summary>
        public void LoadGameplayScene()
        {
            SceneManager.LoadScene(gameplaySceneName);
        }

        /// <summary>
        /// Quits the game when the player clicks the "Quit" button.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
        /// <summary>
        /// Loads the credit image when the player clicks the "Credits" button.
        /// </summary>
        public void ShowCredits()
        {

        }
    }
}
