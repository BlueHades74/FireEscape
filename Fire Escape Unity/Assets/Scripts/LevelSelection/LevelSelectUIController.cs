using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//Author Alex
public class LevelSelectUIController : MonoBehaviour
{

    [System.Serializable]
    public class LevelButton
    {
        public string levelName;
        public GameObject buttonObject; //Assign the buttons to this
    }

    [SerializeField] private List<LevelButton> levelButtons;

    private void OnEnable()
    {
        UpdateLevelButtons();
        AssignButtonListeners();
    }
    private void Start()
    {
        // makes sure the buttons load when the scene reloads between levels
        UpdateLevelButtons();
    }

    //Sets the buttons to active within the hub based on whether or not they are unlocked
    public void UpdateLevelButtons()
    {
        foreach (var button in levelButtons)
        {
            bool unlocked = LevelUnlockManager.Instance.IsLevelUnlocked(button.levelName);

            //Make sure tutorial is always available
            if (button.levelName == "TutorialLevel")
                unlocked = true;


            button.buttonObject.SetActive(unlocked);
        }
    }

    private void AssignButtonListeners()
    {
        foreach (var button in levelButtons)
        {
            Button btn = button.buttonObject.GetComponent<Button>();

            if (btn != null)
            {
                //Removes chances of duplicates
                btn.onClick.RemoveAllListeners();

                btn.onClick.AddListener(() => LoadLevel(button.levelName));
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        //SceneManager.LoadScene(levelName);
    }

}

