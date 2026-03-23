using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSettings : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles

    private GameObject[] playerArray;
    [SerializeField]
    private int[] playerSetCharacters = { 0, 1 };

    [SerializeField]
    private CharacterData[] characterList;

    private void Awake()
    {
        //Ensure there is only ever one settings object.
        if (GameObject.FindGameObjectsWithTag("CharacterSettings").Count<GameObject>() > 1)
        {
            Destroy(gameObject);
        }

        //Makes it so it is a persistent object
        DontDestroyOnLoad(gameObject);
        //Finding both players and inserting them in specific slots of the array (to avoid any confusion)
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

    /// <summary>
    /// Find whenever the scene is loaded to execute some code.
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadBothPlayers;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadBothPlayers;
    }

    /// <summary>
    /// Calls the function that sends the data, passing in each player.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="sm"></param>
    private void LoadBothPlayers(Scene s, LoadSceneMode sm)
    {
        SendEvent(0);
        SendEvent(1);
    }

    /// <summary>
    /// Wraps the numbering for the characters to keep the index in bounds (0 - 3). Returns an in bounds firefighter index.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
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

    /// <summary>
    /// This function verifies that both players do not have the same character, skipping over the character already claimed by moving the index. Returns am index to a firefighter. 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    private int CheckForExisting(int value, int direction)
    {
        if (playerSetCharacters[0] == playerSetCharacters[1])
        {
            value = Increment(value, direction);
        }
        return value;
    }

    /// <summary>
    /// Increments the index of the player's chosen firefighter in a passed in direction. Returns said index.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    private int Increment(int value, int direction)
    {
        value += direction;
        value = Wrap(value);
        return value;
    }

    /// <summary>
    /// Starts the process for incrementing the player's firefighter index while also at the end updating the player data.
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <param name="direction"></param>
    private void Scroll(int playerIndex, int direction)
    {
        playerSetCharacters[playerIndex] = Increment(playerSetCharacters[playerIndex], direction);
        playerSetCharacters[playerIndex] = CheckForExisting(playerSetCharacters[playerIndex], direction);
        SendEvent(playerIndex);
    }

    /// <summary>
    /// Sends the event to update the players.
    /// </summary>
    /// <param name="playerIndex"></param>
    private void SendEvent(int playerIndex)
    {
        if (characterList.Length == 4)
        {
            CharacterEvents.PlayerCharLoad.Invoke(playerIndex, characterList[playerSetCharacters[playerIndex]]);
        }
    }    

    /// <summary>
    /// Takes in a player and scrolls said player to the left.
    /// </summary>
    /// <param name="playerIndex"></param>
    public void ScrollLeft(int playerIndex)
    {
        Scroll(playerIndex, -1);
    }

    /// <summary>
    /// Takes in a player and scrolls said player to the right.
    /// </summary>
    /// <param name="playerIndex"></param>
    public void ScrollRight(int playerIndex)
    {
        Scroll(playerIndex, 1);
    }
}
