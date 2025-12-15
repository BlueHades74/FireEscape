using UnityEngine;
using UnityEngine.SceneManagement;
//author alex
public class ToolTipDetection : MonoBehaviour
{
    private bool tooltipsEnabled;
    //only enable tooltips in the tutorial level
    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        tooltipsEnabled =
            sceneName == "TutorialLevel" ||
            sceneName == "Firehouse";
    }
    //If player collides with object with tooltip it will pop up the tooltip
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!tooltipsEnabled) return;

        ToolTipIdentifier tooltip = other.GetComponent<ToolTipIdentifier>();

        if (tooltip != null )
        {
            ToolTipManager.Instance.ShowToolTip(tooltip.tooltipMessage);
        }
    }

    //If player collider leaves the object it will get rid of the tooltip
    private void OnTriggerExit2D(Collider2D other)
    { 
        if (!tooltipsEnabled) return;

        ToolTipIdentifier tooltip = other.GetComponent<ToolTipIdentifier>();
        if (tooltip != null)
        {
            ToolTipManager.Instance.HideToolTip();
        }
    }
}
