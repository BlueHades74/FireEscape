using Unity.Cinemachine;
using UnityEngine;
using System;
using Unity.VisualScripting;
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
    [SerializeField] private float secondsToWait = 5;

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
        if ((player2.position - player1.position).magnitude > breakDistance)
        {
            //vDualCamLeft.SetActive(false);
            //vDualCamRight.SetActive(false);
            vSingleCamLeft.Priority = 2;
            vSingleCamRight.Priority = 2;
        }
        else
        {
            //vDualCamLeft.SetActive(true);
            //vDualCamRight.SetActive(true);
            vSingleCamLeft.Priority = 0;
            vSingleCamRight.Priority = 0;
            if (swappedDualCams) swappedDualCams = false;
        }

        if (vSingleCamLeft.Priority == 2 && !swappedDualCams && player1.position.x > player2.position.x)
        {
            vDualLeftCamOutput.OutputChannel = OutputChannels.Channel02;
            vDualRightCamOutput.OutputChannel = OutputChannels.Channel01;
            StartCoroutine(SwapDualCams());
            swappedDualCams = true;
        }
        else if (vSingleCamLeft.Priority == 2 && swappedDualCams)
        {
            vDualLeftCamOutput.OutputChannel = OutputChannels.Channel01;
            vDualRightCamOutput.OutputChannel = OutputChannels.Channel02;
            StartCoroutine(SwapDualCams());
            swappedDualCams = false;
        }

        if (player1.position.x > player2.position.x && vSingleCamLeft.Priority == 0)
        {
            vSingleCamLeft.Follow = player2;
            vSingleCamRight.Follow = player1;

            if (swappedBlackScreens == false)
            {
                swapBlackScreens?.Invoke(swappedBlackScreens);
                swappedBlackScreens = true;
            }
        }
        else if (vSingleCamLeft.Priority == 0)
        {
            vSingleCamLeft.Follow = player1;
            vSingleCamRight.Follow = player2;
            if (swappedBlackScreens == true)
            {
                swapBlackScreens?.Invoke(swappedBlackScreens);
                swappedBlackScreens = false;
            }
        }

    }

    private IEnumerator SwapDualCams()
    {
        if (player1.position.x > player2.position.x)
        {
            while (vDualLeftCamComp.Composition.ScreenPosition.x > -0.5)
            {
                yield return new WaitForSeconds(secondsToWait);
                vDualRightCamComp.Composition.ScreenPosition.x += 0.1f;
                vDualLeftCamComp.Composition.ScreenPosition.x -= 0.1f;
            }
        }
        else
        {
            while (vDualLeftCamComp.Composition.ScreenPosition.x < 0.5)
            {
                yield return new WaitForSeconds(secondsToWait);
                vDualRightCamComp.Composition.ScreenPosition.x -= 0.1f;
                vDualLeftCamComp.Composition.ScreenPosition.x += 0.1f;
            }
        }
    }
}
