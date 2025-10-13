using UnityEngine;
using TMPro;
//Author Alex

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager Instance;
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;

    private void Awake()
    {
        Instance = this;
        HideToolTip();
    }

    public void ShowToolTip(string message)
    {
        tooltipText.text = message;
        tooltipPanel.SetActive(true);
    }

    public void HideToolTip()
    {
        tooltipPanel.SetActive(false);
    }
}
