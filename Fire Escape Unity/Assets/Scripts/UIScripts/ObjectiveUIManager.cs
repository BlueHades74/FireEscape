using System.Collections;
using TMPro;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public int totalHumans;
    public int savedHumans;

    [SerializeField]
    private Timer timerManager;

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
        try
        {
            timerManager.Pause();
        }
        catch { }

        if (savedHumans >= totalHumans)
        {

            Debug.Log($"All humans saved in {currentLevelName}, unlocking next level: {nextLevelName}");
            if (LevelUnlockManager.Instance != null)
            {
                //unlock this level this is pretty much just a fail safe incase the save data doesnt unlock it
                LevelUnlockManager.Instance.UnlockLevel(currentLevelName);

                // Unlock next level so it appears in the hub
                LevelUnlockManager.Instance.UnlockLevel(nextLevelName);
            }

            else
            {
                Debug.LogError("LevelUnlockManager.Instance was null when trying to unlock!");
            }

            //Grab completion data at once you save all people

            LevelResultCache.Data = new LevelResultData()

            {
                levelName = currentLevelName,

                humansSaved = savedHumans,
                totalHumans = totalHumans,

                fireExtinguishedPercent = FireTracker.Instance != null
    ? FireTracker.Instance.GetPercentExtinguished()
    : 0f,
                bonusCollected = BonusTracker.CollectedCount,
                bonusTotal = BonusTracker.TotalCount

            };

            
                StartCoroutine(ReturnToResultsScene()); 
        }
    }
    
    private void UpdateObjectiveUI()
    {
        objectiveText.text = $"People Rescued: {savedHumans}/{totalHumans}";
    }

    private IEnumerator ReturnToLevelSelect()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelSelectSceneName);
    }

    private IEnumerator ReturnToResultsScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LevelReward");
    }
}
