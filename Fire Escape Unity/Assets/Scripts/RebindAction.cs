using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RebindAction : MonoBehaviour
{
    [Header("References")]
    public InputActionReference actionReference;
    public int bindingIndex;

    [Header("UI")]
    public TextMeshProUGUI bindingText;
    public GameObject listeningOverlay; 

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public bool isPlayer2;

    private void Start()
    {
        UpdateBindingDisplay();
    }

    public void StartRebind()
    {
        if (rebindingOperation != null)
            rebindingOperation.Cancel();

        actionReference.action.Disable();

       
        if (isPlayer2)
        {
            actionReference.action.ApplyBindingOverride(bindingIndex, "");
        }

        if (listeningOverlay != null)
            listeningOverlay.SetActive(true);

        rebindingOperation = actionReference.action
            .PerformInteractiveRebinding()
            .WithTargetBinding(bindingIndex)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                CompleteRebind();
            })
            .OnCancel(operation =>
            {
                CancelRebind();
            });

        rebindingOperation.Start();
    }

    private void CompleteRebind()
    {
        rebindingOperation.Dispose();
        rebindingOperation = null;

        actionReference.action.Enable();

        if (listeningOverlay != null)
            listeningOverlay.SetActive(false);

        UpdateBindingDisplay();
        SaveBindings();

        Debug.Log("Rebound to: " +
            actionReference.action.bindings[bindingIndex].effectivePath);
    }

    private void CancelRebind()
    {
        rebindingOperation.Dispose();
        rebindingOperation = null;

        actionReference.action.Enable();

        if (listeningOverlay != null)
            listeningOverlay.SetActive(false);
    }

    private void UpdateBindingDisplay()
    {
        if (bindingText != null)
        {
            bindingText.text = actionReference.action
                .GetBindingDisplayString(bindingIndex);
        }
    }

    private void SaveBindings()
    {
        PlayerPrefs.SetString("rebinds",
            actionReference.action.actionMap.asset.SaveBindingOverridesAsJson());
    }

    public static void LoadBindings(InputActionAsset asset)
    {
        if (PlayerPrefs.HasKey("rebinds"))
        {
            asset.LoadBindingOverridesFromJson(
                PlayerPrefs.GetString("rebinds"));
        }
    }
}
