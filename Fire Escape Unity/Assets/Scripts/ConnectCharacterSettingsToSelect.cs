using UnityEngine;

public class ConnectCharacterSettingsToSelect : MonoBehaviour
{

    private CharacterSettings characterSettings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterSettings = GameObject.FindGameObjectWithTag("CharacterSettings").GetComponent<CharacterSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScrollLeft(int playerIndex)
    {
        characterSettings.ScrollLeft(playerIndex);
    }

    public void ScrollRight(int playerIndex)
    {
        characterSettings.ScrollRight(playerIndex);
    }
}
