using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField]
    private int playerIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIndex == 0)
        {
            PlayerEventSystem.current.ObjectPickedUp();
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) && playerIndex == 1)
        {
            PlayerEventSystem.current.ObjectPickedUp();
        }
    }
}


