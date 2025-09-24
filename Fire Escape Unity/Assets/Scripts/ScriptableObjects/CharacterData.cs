using UnityEngine;
using UnityEngine.UI;
//Author Alex
[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
//Parameters for a scriptable object, based on the variables will allow us to easily set future characters data
public class CharacterData : ScriptableObject
{
    public string CharacterName;
    public Sprite Headshot;
    public Sprite CharacterSprite;
}
