using UnityEngine;

public class CharacterDataCatcher : MonoBehaviour
{
    //Created by Rafael Gonzalez Atiles

    //This script is a pair with the character settings script
    //Existing to be the listener for its events when they are called and there is no object that listens in the scene.

    private void OnEnable()
    {
        CharacterEvents.PlayerCharLoad += Listen;
    }

    private void OnDisable()
    {
        CharacterEvents.PlayerCharLoad -= Listen;
    }

    private void Listen(int messagedplayer, CharacterData data)
    {
        Debug.Log("Data Sent/Recived Successfully");
    }
}
