using UnityEngine;

public class DebrisScript : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Assign the two GameObjects that act as pickup handles for this debris. Each handle must have an ObjectManager component.")]
    [SerializeField]
    private GameObject[] pickupHandles = new GameObject[2];
    [SerializeField]
    private bool horizontal = false;

    [Tooltip("Speed multiplier for players when they are carrying this debris.")]
    public float playerCarrySpeedMultiplier = 0.7f;

    private bool _isCarriedByTwoPlayers = false;
    public bool IsCarriedByTwoPlayers { get => _isCarriedByTwoPlayers; }

    private ObjectManager handle1OM;
    private ObjectManager handle2OM;

    private Transform carryingPlayer1Ref;
    private Transform carryingPlayer2Ref;

    [SerializeField]
    private Rigidbody2D player1Rb;
    [SerializeField]
    private Rigidbody2D player2Rb;

    void Start()
    {
        if (pickupHandles == null || pickupHandles.Length != 2 || pickupHandles[0] == null || pickupHandles[1] == null)
        {
            enabled = false;
            return;
        }

        handle1OM = pickupHandles[0].GetComponent<ObjectManager>();
        handle2OM = pickupHandles[1].GetComponent<ObjectManager>();

        if (handle1OM == null || handle2OM == null)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (handle1OM == null || handle2OM == null) return;

        Transform player1Transform = pickupHandles[0].transform.parent;
        Transform player2Transform = pickupHandles[1].transform.parent;

        bool handle1HeldByPlayer = player1Transform != null && player1Transform.CompareTag("Player") && handle1OM.IsHeld;
        bool handle2HeldByPlayer = player2Transform != null && player2Transform.CompareTag("Player") && handle2OM.IsHeld;

        bool currentlyAttemptingCarryByTwo = false;
        GetComponent<BoxCollider2D>().enabled = true;
        if (handle1HeldByPlayer && handle2HeldByPlayer && player1Transform != player2Transform)
        {
            currentlyAttemptingCarryByTwo = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }

        if (_isCarriedByTwoPlayers)
        {
            if (!currentlyAttemptingCarryByTwo)
            {
                SetCarriedState(false);
                if (handle1HeldByPlayer && handle1OM != null)
                {
                    //handle1OM.DropItem();
                }
                if (handle2HeldByPlayer && handle2OM != null)
                {
                    //handle2OM.DropItem();
                }
            }
            else
            {
                if (carryingPlayer1Ref != null && carryingPlayer2Ref != null)
                {
                    transform.position = (carryingPlayer1Ref.position + carryingPlayer2Ref.position) / 2f;

                    // Remove x/y movement based on if debris is horizontal
                    if (horizontal)
                    {
                        player1Rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                        player2Rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                    } 
                    else
                    {
                        player1Rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                        player2Rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                    }
                }
            }

            // Reset the constraints on the players positions
            player1Rb.constraints = RigidbodyConstraints2D.None;
            player1Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            player2Rb.constraints = RigidbodyConstraints2D.None;
            player2Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            if (currentlyAttemptingCarryByTwo)
            {
                carryingPlayer1Ref = player1Transform;
                carryingPlayer2Ref = player2Transform;
                SetCarriedState(true);
            }
        }
    }

    private void SetCarriedState(bool newCarryState)
    {
        if (_isCarriedByTwoPlayers == newCarryState) return;

        _isCarriedByTwoPlayers = newCarryState;

        if (_isCarriedByTwoPlayers)
        {
            if (carryingPlayer1Ref != null && carryingPlayer1Ref.CompareTag("Player"))
            {
                carryingPlayer1Ref.GetComponent<PlayerMovementScript>()?.SetMovementByOriginalTimesParameter(playerCarrySpeedMultiplier);
                
            }
            if (carryingPlayer2Ref != null && carryingPlayer2Ref.CompareTag("Player"))
            {
                carryingPlayer2Ref.GetComponent<PlayerMovementScript>()?.SetMovementByOriginalTimesParameter(playerCarrySpeedMultiplier);
            }
        }
        else
        {
            if (carryingPlayer1Ref != null && carryingPlayer1Ref.CompareTag("Player"))
            {
                 carryingPlayer1Ref.GetComponent<PlayerMovementScript>()?.ResetSpeedMultiplier();
            }
            if (carryingPlayer2Ref != null && carryingPlayer2Ref.CompareTag("Player"))
            {
                carryingPlayer2Ref.GetComponent<PlayerMovementScript>()?.ResetSpeedMultiplier();
            }
            carryingPlayer1Ref = null;
            carryingPlayer2Ref = null;
        }
    }

    void OnValidate()
    {
        if (pickupHandles.Length != 2)
        {
            System.Array.Resize(ref pickupHandles, 2);
        }
    }
}
