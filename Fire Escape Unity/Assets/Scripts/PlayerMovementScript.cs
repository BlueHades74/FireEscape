using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private float playerMoveSpeed = 5f;

    [SerializeField]
    private string map;

    private Rigidbody2D rb;
    private ControlMap inputActions;
    private bool isReady = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new ControlMap();
        isReady = true;
    }

    private void OnEnable()
    {
        if (inputActions != null)
        {
            inputActions.Enable();
        }
    }

    private void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Disable();
        }
    }

    void Update()
    {
        if (isReady)
        {
            OnPlayerMovement();
        }
    }

    private void OnPlayerMovement()
    {
        Debug.Log("Inside");

        if (inputActions != null)
        {
            Vector2 input = inputActions.Player.P1Movement.ReadValue<Vector2>();
            Debug.Log(input);
            rb.linearVelocity = input * playerMoveSpeed;
        }
        else
        {
            Debug.LogWarning("P1Movement input action not found.");
        }
    }
}
