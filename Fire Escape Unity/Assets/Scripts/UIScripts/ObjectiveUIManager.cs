using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//Author Alex
public class ObjectiveUIManger : MonoBehaviour
{
    [Header("Scene")]
    public string levelSelectSceneName = "LevelSelector";
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
            SceneManager.LoadScene(levelSelectSceneName);
        }
    }
    
    private void UpdateObjectiveUI()
    {
        objectiveText.text = $"Objectives: {savedHumans}/{totalHumans}";
    }
}
