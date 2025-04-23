using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerEventSystem.current.ObjectPickedUp();
        }
    }
}


