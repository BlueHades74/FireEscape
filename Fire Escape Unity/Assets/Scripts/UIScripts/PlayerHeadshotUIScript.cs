using UnityEngine;
using System;
//Author Alex
public class PlayerHeadshotUIScript : MonoBehaviour
{
    //Calls scriptable object CharacterData
    [SerializeField] private CharacterData data;
    [SerializeField] private int playerID; //set the player ID current replacement until we make a character selection screen

    // this will grab the player ID and new character data if the player were to change characters
    private void Start()
    {
        if (data != null)
        {
            CharacterEventsScript.OnCharacterChanged?.Invoke(playerID, data);
        }
    }

    private void OnEnable()
    {
        CharacterEvents.PlayerCharLoad += LoadData;
    }

    private void OnDisable()
    {
        CharacterEvents.PlayerCharLoad -= LoadData;
    }

    private void LoadData(int messagedplayer, CharacterData newData)
    {
        if (messagedplayer == (playerID - 1))
        {
            data = newData;
        }
    }

    public CharacterData Data { get => data; set => data = value; }
}
