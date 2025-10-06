using UnityEngine;
using UnityEngine.UI;
//Author Alex
public class PlayerImageUIScript : MonoBehaviour
{
    // Author : Alex
    // The objects in the scene where the image should show up
    public Image Player1Headshot;
    public Image Player2Headshot;

    // When character/level is started it will grab the characters PNG headshot
    private void OnEnable()
    {
        CharacterEventsScript.OnCharacterChanged += UpdateCharacterUI;
    }
    // Will disable when level or different character is chosen
    private void OnDisable()
    {
        CharacterEventsScript.OnCharacterChanged -= UpdateCharacterUI;
    }
    //Updates character headshot UI based on which player selects a character Exmp: Player 1 selects Yellow Character or Player 2 Selects blue chara
    private void UpdateCharacterUI(int PlayerID, CharacterData data)
    {
        if (PlayerID == 1)
        {
            Player1Headshot.sprite = data.Headshot;
            Player1Headshot.enabled = data.Headshot != null;
        }
        else if (PlayerID == 2)
        {
            Player2Headshot.sprite = data.Headshot;
            Player2Headshot.enabled = data.Headshot != null;
        }
    }
}
