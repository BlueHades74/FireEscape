using TMPro;
using UnityEngine;

public class DebriefName : MonoBehaviour
{
    private TextMeshProUGUI text;

    private CharacterDataStorage data;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        data = GetComponent<CharacterDataStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = data.CurrentData.CharacterName;
    }
}
