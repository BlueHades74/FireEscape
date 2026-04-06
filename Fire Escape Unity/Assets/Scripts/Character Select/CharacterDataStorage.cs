using UnityEngine;

public class CharacterDataStorage : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles

    [SerializeField]
    private int player;
    [SerializeField]
    private CharacterData currentData;

    public CharacterData CurrentData { get => currentData;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Connects to event system.
    /// </summary>
    private void OnEnable()
    {
        CharacterEvents.PlayerCharLoad += LoadData;
    }

    /// <summary>
    /// Disconnects from event system.
    /// </summary>
    private void OnDisable()
    {
        CharacterEvents.PlayerCharLoad -= LoadData;
    }

    /// <summary>
    /// Determines if the data in the call pertains to the player, if so they save it as the current data.
    /// </summary>
    /// <param name="messagedplayer"></param>
    /// <param name="data"></param>
    private void LoadData(int messagedplayer, CharacterData data)
    {
        if (messagedplayer == player)
        {
            currentData = data;
        }
    }
}
