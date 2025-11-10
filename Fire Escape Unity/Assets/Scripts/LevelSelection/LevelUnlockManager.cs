using UnityEngine;
using System.Collections.Generic;

//Author Alex
public class LevelUnlockManager : MonoBehaviour
{

    public static LevelUnlockManager Instance;

    //Place level names in this list in order based on the SCENE NAMES nothing else
    [SerializeField] private List<string> levelNames;

    // will store unlock data based on player session
    private const string PREF_PREFIX = "LevelUnlocked_";

    private void Awake()
    {
        //controls the manager and making sure only one exists so it doesnt get duplicated based on save data
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadUnlockedLevels(); // ensure it reads saved data on start
    }

    private void Start()
    {
        //First level will always be unlocked
        if (!IsLevelUnlocked(levelNames[0]))
        {
            UnlockLevel(levelNames[0]);
        }
    }

    //Based on playerprefs which is bascially a mini save system that saves on local devices, 0 would mean the level is not unlocked, 1 would set the level to being unlocked
    public bool IsLevelUnlocked(string levelName)
    {
        return PlayerPrefs.GetInt(PREF_PREFIX + levelName, 0) == 1;
    }

    public void UnlockLevel(string levelName)
    {
        //
        PlayerPrefs.SetInt(PREF_PREFIX + levelName, 1);
        PlayerPrefs.Save();
    }

    //unlocks the next level based on completion of currently unlocked level

    public void UnlockNextLevel(string currentLevel)
    {
        int index = levelNames.IndexOf(currentLevel);
        if (index >= 0 && index < levelNames.Count - 1)
        {
            string nextLevel = levelNames[index + 1];
            UnlockLevel(levelNames[index + 1]);
        }
    }
    private void LoadUnlockedLevels()
    {
        foreach (string levelName in levelNames)
        {
            int unlocked = PlayerPrefs.GetInt(PREF_PREFIX + levelName, 0);
            if (unlocked == 1)
            {
                Debug.Log($"Loaded unlocked level: {levelName}");
            }
        }
    }
}
