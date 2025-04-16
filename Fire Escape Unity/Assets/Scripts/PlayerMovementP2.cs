using UnityEngine;

public class PlayerMovementP2 : MonoBehaviour
{
    [SerializeField]
    private float playerMoveSpeed;
    [SerializeField]
    private string map;

    private Rigidbody2D rb;

    private ControlMap inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new ControlMap();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnPlayerMovement();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnPlayerMovement()
    {
        //InputAction move = inputActions.FindAction("Movement");
        Debug.Log("Inside");
        Debug.Log(inputActions.PlayerGameplay2.Movement.ReadValue<Vector2>());
        rb.linearVelocity = inputActions.PlayerGameplay2.Movement.ReadValue<Vector2>() * playerMoveSpeed;
    }
}
