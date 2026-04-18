using Unity.Cinemachine;
using UnityEngine;
using System;
using System.Collections;

public class SharedCamTarget : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private GameObject vDualCamLeft;
    [SerializeField] private GameObject vDualCamRight;
    [SerializeField] private CinemachineCamera vSingleCamLeft;
    [SerializeField] private CinemachineCamera vSingleCamRight;
    [SerializeField] private float breakDistance = 15f;
    [SerializeField] private CinemachineCamera vDualLeftCamOutput;
    [SerializeField] private CinemachineCamera vDualRightCamOutput;
    [SerializeField] private CinemachinePositionComposer vDualLeftCamComp;
    [SerializeField] private CinemachinePositionComposer vDualRightCamComp;

    private bool swappedDualCams = false;
    private bool camsSplit;

    private bool swappedBlackScreens = false;

    private bool canMidpoint = true;

    public static event Action<bool> swapBlackScreens;

    private void OnEnable()
    {
        StaircaseScript.canMidpointFlip += MidPointBool;
    }

    private void OnDisable()
    {
        StaircaseScript.canMidpointFlip -= MidPointBool;
    }

    private void MidPointBool()
    {
        canMidpoint = !canMidpoint;
    }

    private void LateUpdate()
    {
        // create a vector3 to store difference of player positions
        Vector3 midpoint = (player1.position + player2.position) / 2f;
        // modify the Z value to keep the camera back
        midpoint.z = transform.position.z;

        if (canMidpoint)
        {
            // set camera position
            transform.position = midpoint;
        }

        // checks distance between players to decide if cameras flip to individual look at target
        if ((player2.position - player1.position).magnitude > breakDistance && !camsSplit)
        {
            // dual at priority 1, single takes control
            vSingleCamLeft.Priority = 2;
            vSingleCamRight.Priority = 2;
            camsSplit = true;
        }
        else if ((player2.position - player1.position).magnitude < breakDistance && camsSplit)
        {
            // dual regains control
            vSingleCamLeft.Priority = 0;
            vSingleCamRight.Priority = 0;
            if (swappedDualCams) swappedDualCams = false;
            // coroutine to wait for single tracking to start again
            StartCoroutine(EnableSingleTracking());
        }

        if (vSingleCamLeft.Priority == 2 && !swappedDualCams && player1.position.x > player2.position.x && vSingleCamLeft.Follow == player2)
        {
            // swapped dual cams for players swapped sides outside of break distance
            vDualLeftCamOutput.OutputChannel = OutputChannels.Channel02;
            vDualRightCamOutput.OutputChannel = OutputChannels.Channel01;
            vDualLeftCamComp.Composition.ScreenPosition.x = -0.5f;
            vDualRightCamComp.Composition.ScreenPosition.x = 0.5f;
            swappedDualCams = true;
        }
        else if (vSingleCamLeft.Priority == 2 && swappedDualCams && player1.position.x < player2.position.x && vSingleCamLeft.Follow == player2)
        {
            // base dual cam position
            vDualLeftCamOutput.OutputChannel = OutputChannels.Channel01;
            vDualRightCamOutput.OutputChannel = OutputChannels.Channel02;
            vDualLeftCamComp.Composition.ScreenPosition.x = 0.5f;
            vDualRightCamComp.Composition.ScreenPosition.x = -0.5f;
            swappedDualCams = false;
        }

        if (player1.position.x > player2.position.x && vSingleCamLeft.Priority == 0 && !camsSplit)
        {
            // swapped sides follow
            vSingleCamLeft.Follow = player2;
            vSingleCamRight.Follow = player1;

            // just for stairs fading screen
            if (swappedBlackScreens == false)
            {
                swapBlackScreens?.Invoke(swappedBlackScreens);
                swappedBlackScreens = true;
            }
        }
        else if (vSingleCamLeft.Priority == 0 && !camsSplit)
        {
            // base follow
            vSingleCamLeft.Follow = player1;
            vSingleCamRight.Follow = player2;

            if (swappedBlackScreens == true)
            {
                swapBlackScreens?.Invoke(swappedBlackScreens);
                swappedBlackScreens = false;
            }
        }

    }

    private IEnumerator EnableSingleTracking()
    {
        // the wait time for allowing single cam tracking to begin again
        yield return new WaitForSeconds(0.6f);
        camsSplit = false;
    }
}
