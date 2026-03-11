using UnityEngine;
using UnityEngine.UI;

public class DebriefIcon : MonoBehaviour
{
    private Image image;
    private CharacterDataStorage data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        data = GetComponent<CharacterDataStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = data.CurrentData.Headshot;
    }
}
