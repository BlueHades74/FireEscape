using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerFaceDown faceDown = new PlayerFaceDown();
    public PlayerFaceLeft faceLeft = new PlayerFaceLeft();
    public PlayerFaceRight faceRight = new PlayerFaceRight();
    public PlayerFaceUp faceUp = new PlayerFaceUp();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // starting state
        currentState = faceDown;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;

        state.EnterState(this);
    }
}
