using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//Author Alex
public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;


    [Header("Menus")]
    public GameObject PauseMenu;
    public GameObject SettingsMenu;

    [Header("Main Menu Scene")]
    public string mainMenuScene = "FireHouse";

    private bool isPaused = false;

    public GameObject FirstControllerButton;
    private System.Collections.IEnumerator SelectFirstButton()
    {
        yield return null; // wait one frame
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(FirstControllerButton);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnPause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        isPaused = true;
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);

        Time.timeScale = 0f;

        EventSystem.current.SetSelectedGameObject(FirstControllerButton);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(SelectFirstButton());
    }

    public void Resume()
    {
        isPaused = false;

        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public void OpenSettings()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
