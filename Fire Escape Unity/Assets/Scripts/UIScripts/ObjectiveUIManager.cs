using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

//Author Alex
public class ObjectiveUIManger : MonoBehaviour
{
    [Header("Scene")]
    public string levelSelectSceneName = "Firehouse";

    [Header("Current Level Name")]
    [Tooltip("Name of the current level make sure this matches the level unlock manager ")]
    public string currentLevelName = "TutorialLevel";

    [Header("Next Level Name")]
    [Tooltip("Name of the next level that will unlock based on completion of current level")]
    public string nextLevelName = "Level1";


    //Affects the object in the scene so you can set specific variables for the text
    [SerializeField] private TextMeshProUGUI objectiveText;
    //For each level will have number of total humans such as 0/4 and then per saved human 1/4 etc
    private int totalHumans;
    private int savedHumans;
    
    private void OnEnable()
    {
        ObjectManager.OnHumanRescued += HandleHumanRescued;
    }
    
    private void OnDisable()
    {
        ObjectManager.OnHumanRescued -= HandleHumanRescued;
    }

    private void Start()
    {
        //Counts all humans within each level
        totalHumans = GameObject.FindGameObjectsWithTag("NPC").Length;
        savedHumans = 0;
        UpdateObjectiveUI();
    }

    private void HandleHumanRescued(ObjectManager human)
    {
        //This will increase the count of saved humans when they hit the rescue zone
        savedHumans++;
        UpdateObjectiveUI();

        if (savedHumans >= totalHumans)
        {
            if (LevelUnlockManager.Instance != null)
            {
                //unlock this level this is pretty much just a fail safe incase the save data doesnt unlock it
                LevelUnlockManager.Instance.UnlockLevel(currentLevelName);

                // Unlock next level so it appears in the hub
                LevelUnlockManager.Instance.UnlockLevel(nextLevelName);
            }

            StartCoroutine(ReturnToLevelSelect()); 
        }
    }
    
    private void UpdateObjectiveUI()
    {
        objectiveText.text = $"Objectives: {savedHumans}/{totalHumans}";
    }

    private IEnumerator ReturnToLevelSelect()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelSelectSceneName);
    }
}
