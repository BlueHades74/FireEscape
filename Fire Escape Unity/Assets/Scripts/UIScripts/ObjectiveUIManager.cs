using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
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

    #if UNITY_EDITOR // only active while testing
        // Press 'K' to instantly complete level
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("DEBUG: Auto-completing level!");
            savedHumans = totalHumans;
            UpdateObjectiveUI();
    HandleLevelCompletion();
}

// Press 'H' to simulate rescuing a single human
if (Input.GetKeyDown(KeyCode.H))
{
    Debug.Log("DEBUG: Adding 1 human to saved count!");
    savedHumans++;
    UpdateObjectiveUI();

    if (savedHumans >= totalHumans)
        HandleLevelCompletion();
}
#endif
private void HandleHumanRescued(ObjectManager human)
    {
        //This will increase the count of saved humans when they hit the rescue zone
        savedHumans++;
        UpdateObjectiveUI();
        
        
        
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
