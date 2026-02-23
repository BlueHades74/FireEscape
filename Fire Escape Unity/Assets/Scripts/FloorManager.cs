using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

//Author Alex
public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance;

    [Header("Starting Floor")]
    public int currentFloor = 1;

    [Header("UI Reference")]
    public TextMeshProUGUI floorText;

    private void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void GoDownFloor()
    {
        currentFloor--;
        if (currentFloor < 1)
        {
            currentFloor = 1;

            UpdateUI();
        }
    }

    public void GoUpFloor()
    {
        currentFloor++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (floorText != null)
            floorText.text = "Floor: " + currentFloor;
    }
    
}
