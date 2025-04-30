using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerActionScript : MonoBehaviour
{
    private GameObject actionItem;
    private PlayerInputController inputs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        inputs = GetComponent<PlayerInputController>();
        if (inputs.PlayerIndex == 0)
        {
            inputs.InputActions.Player.P1Action.performed += OnAction;
        }
        else
        {
            inputs.InputActions.Player.P2Action.performed += OnAction;
        }
    }

    private void OnDisable()
    {
        inputs = GetComponent<PlayerInputController>();
        if (inputs.PlayerIndex == 0)
        {
            inputs.InputActions.Player.P1Action.performed -= OnAction;
        }
        else
        {
            inputs.InputActions.Player.P2Action.performed -= OnAction;
        }
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        
    }

    public void ReceiveActionItem(GameObject actionItemSent)
    {
        actionItem = actionItemSent;
    }

    public void AxeUse()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, GetComponent<PlayerMovementScript>().FacingDirection, 1.5f);

        if (hit)
        {
            if (hit.collider.gameObject.tag == "BreakableObject")
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }

    public void BucketUse()
    {

    }
}
