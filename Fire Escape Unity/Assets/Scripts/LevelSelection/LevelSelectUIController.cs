using UnityEngine;
using UnityEngine.UI;
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
    }

    //Sets the buttons to active within the hub based on whether or not they are unlocked
    public void UpdateLevelButtons()
    {
        foreach (var button in levelButtons)
        {
            bool unlocked = LevelUnlockManager.Instance.IsLevelUnlocked(button.levelName);
            button.buttonObject.SetActive(unlocked);
        }
    }
}
