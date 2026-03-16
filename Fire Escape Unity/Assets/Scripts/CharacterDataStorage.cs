using UnityEngine;

public class CharacterDataStorage : MonoBehaviour
{
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

    private void OnEnable()
    {
        CharacterEvents.PlayerCharLoad += LoadData;
    }

    private void OnDisable()
    {
        CharacterEvents.PlayerCharLoad -= LoadData;
    }

    private void LoadData(int messagedplayer, CharacterData data)
    {
        if (messagedplayer == player)
        {
            currentData = data;
        }
    }
}
