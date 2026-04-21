using UnityEngine;
using UnityEngine.LowLevel;

public class HoleJumpScript : MonoBehaviour
{
    //Created by: Rafael Gonzalez Atiles
    //Last Edited by: Logan Shade

    [SerializeField]
    private GameObject exitPoint;

    [SerializeField]
    private GameObject[] transitionScreens;

    [SerializeField]
    private GameObject view;

    private GameObject player;

    /// <summary>
    /// Teleports the player
    /// </summary>
    private void TeleportPlayer()
    {
        player.transform.position = exitPoint.transform.position;
    }

    /// <summary>
    /// Triggers the teleport
    /// </summary>
    public void TriggerTeleport()
    {
        TeleportPlayer();
    }

    /// <summary>
    /// Sets the split distance of the camera back to normal
    /// </summary>
    public void FixCamera()
    {
        view.GetComponent<CameraTransition>().SetDistanceToValue(10);
    }

    /// <summary>
    /// Allows an object to initiate teleporting, triggers the right black screen
    /// </summary>
    /// <param name="sender"></param>
    public void InitiateTP(GameObject sender)
    {
        player = sender;
        //view.GetComponent<CameraTransition>().SetDistanceToValue(0);
        if (player.name == "Player 1")
        {
            transitionScreens[0].SetActive(true);
            transitionScreens[0].GetComponent<ScreenFadeScript>().ReceiveTransferObject(this.gameObject);
        }
        else
        {
            transitionScreens[1].SetActive(true);
            transitionScreens[1].GetComponent<ScreenFadeScript>().ReceiveTransferObject(this.gameObject);
        }
    }

    /// <summary>
    /// Allows others to check if this hole is on a higher floor or not
    /// </summary>
    /// <returns></returns>
    public bool CanWeJump()
    {
        if (exitPoint == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            FloorManager.Instance.GoDownFloor();
        }
    }
}
