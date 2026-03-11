using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSettings : MonoBehaviour
{
    private GameObject[] playerArray;
    [SerializeField]
    private int[] playerSetCharacters = { 0, 1 };

    [SerializeField]
    private CharacterData[] characterList;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        try
        {
            playerArray = GameObject.FindGameObjectsWithTag("Player");

            if (playerArray[0].name == "Player 1")
            {
                GameObject player1 = playerArray[0];
                playerArray[0] = playerArray[1];
                playerArray[1] = player1;
            }
        }
        catch { }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadBothPlayers;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadBothPlayers;
    }

    private void LoadBothPlayers(Scene s, LoadSceneMode sm)
    {
        SendEvent(0);
        SendEvent(1);
    }

    private int Wrap(int value)
    {
        if (value < 0)
        {
            value = 3;
        }
        else if (value > 3)
        {
            value = 0;
        }

        return value;
    }

    private int CheckForExisting(int value, int direction)
    {
        if (playerSetCharacters[0] == playerSetCharacters[1])
        {
            value = Increment(value, direction);
        }
        return value;
    }

    private int Increment(int value, int direction)
    {
        value += direction;
        value = Wrap(value);
        return value;
    }

    private void Scroll(int playerIndex, int direction)
    {
        playerSetCharacters[playerIndex] = Increment(playerSetCharacters[playerIndex], direction);
        playerSetCharacters[playerIndex] = CheckForExisting(playerSetCharacters[playerIndex], direction);
        SendEvent(playerIndex);
    }

    private void SendEvent(int playerIndex)
    {
        if (characterList.Length == 4)
        {
            CharacterEvents.PlayerCharLoad.Invoke(playerIndex, characterList[playerSetCharacters[playerIndex]]);
        }
    }    

    public void ScrollLeft(int playerIndex)
    {
        Scroll(playerIndex, -1);
    }

    public void ScrollRight(int playerIndex)
    {
        Scroll(playerIndex, 1);
    }
}
