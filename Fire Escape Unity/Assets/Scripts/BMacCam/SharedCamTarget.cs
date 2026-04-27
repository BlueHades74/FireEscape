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
    [SerializeField] private float breakDistanceHorizontal = 8f;
    [SerializeField] private float breakDistanceVertical = 5f;
    [SerializeField] private CinemachineCamera vDualLeftCamOutput;
    [SerializeField] private CinemachineCamera vDualRightCamOutput;
    [SerializeField] private CinemachinePositionComposer vDualLeftCamComp;
    [SerializeField] private CinemachinePositionComposer vDualRightCamComp;
    [SerializeField] private CinemachineBrain brainLeft;
    [SerializeField] private CinemachineBrain brainRight;

    // bool for preventing tracking target switches
    private bool transitioning = false;
    // bool for slowing down dual cams channel swaps
    private bool dualCamsArmed = false;

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
        if (PlayersMetBreakDistance() == true && brainLeft.IsBlending == false)
        {
            // dual at priority 1, single takes control
            vSingleCamLeft.Priority = 2;
            vSingleCamRight.Priority = 2;
            transitioning = true;
        }
        else if (PlayersMetBreakDistance() == false && brainLeft.IsBlending == false)
        {
            // dual regains control
            vSingleCamLeft.Priority = 0;
            vSingleCamRight.Priority = 0;
        }

        if (vSingleCamLeft.Priority == 2 && Player1XGreater() == true && vSingleCamLeft.Follow == player2 && dualCamsArmed)
        {
            // swapped dual cams for players swapped sides outside of break distance
            vDualLeftCamOutput.OutputChannel = OutputChannels.Channel02;
            vDualRightCamOutput.OutputChannel = OutputChannels.Channel01;
            vDualLeftCamComp.Composition.ScreenPosition.x = -0.5f;
            vDualRightCamComp.Composition.ScreenPosition.x = 0.5f;
        }
        else if (vSingleCamLeft.Priority == 2 && Player1XGreater() == false && vSingleCamLeft.Follow == player2 && dualCamsArmed)
        {
            // base dual cam position
            vDualLeftCamOutput.OutputChannel = OutputChannels.Channel01;
            vDualRightCamOutput.OutputChannel = OutputChannels.Channel02;
            vDualLeftCamComp.Composition.ScreenPosition.x = 0.5f;
            vDualRightCamComp.Composition.ScreenPosition.x = -0.5f;
        }
        else if (vSingleCamLeft.Priority == 2 && Player1XGreater() == true && vSingleCamLeft.Follow == player1 && dualCamsArmed)
        {
            // swapped dual cams for players swapped sides outside of break distance
            vDualLeftCamOutput.OutputChannel = OutputChannels.Channel02;
            vDualRightCamOutput.OutputChannel = OutputChannels.Channel01;
            vDualLeftCamComp.Composition.ScreenPosition.x = -0.5f;
            vDualRightCamComp.Composition.ScreenPosition.x = 0.5f;
        }
        else if (vSingleCamLeft.Priority == 2 && Player1XGreater() == false && vSingleCamLeft.Follow == player1 && dualCamsArmed)
        {
            // base dual cam position
            vDualLeftCamOutput.OutputChannel = OutputChannels.Channel01;
            vDualRightCamOutput.OutputChannel = OutputChannels.Channel02;
            vDualLeftCamComp.Composition.ScreenPosition.x = 0.5f;
            vDualRightCamComp.Composition.ScreenPosition.x = -0.5f;
        }
        
        if (Player1XGreater() == true && PlayersMetBreakDistance() == false)
        {
            // wait to swap tracking targets
            if (transitioning) StartCoroutine(SwapSingleTrackingTrue());
            // swap right away
            else SwapSingleCamTrackingTargets(true);
        }
        else if (PlayersMetBreakDistance() == false)
        {
            // wait to swap tracking targets
            if (transitioning) StartCoroutine(SwapSingleTrackingFalse());
            // swap right away
            else SwapSingleCamTrackingTargets(false);
        }
    }

    // checks if player 1's X position is greater than player 2's
    private bool Player1XGreater()
    {
        // player 1 has a greater x
        if (player1.position.x > player2.position.x) return true;
        // player 2 has a greater x
        return false;
    }

    private bool PlayersMetBreakDistance()
    {
        // check distances and return true if met
        if (Math.Abs(player1.position.x - player2.position.x) > breakDistanceHorizontal) return true;
        else if (Math.Abs(player1.position.y - player2.position.y) > breakDistanceVertical) return true;
        // players within range
        return false;
    }

    private void SwapSingleCamTrackingTargets(bool swap)
    {
        if (swap)
        {
            // swapped sides follow
            vSingleCamLeft.Follow = player2;
            vSingleCamRight.Follow = player1;

            transitioning = false;

            // just for stairs fading screen
            if (swappedBlackScreens == false)
            {
                swapBlackScreens?.Invoke(swappedBlackScreens);
                swappedBlackScreens = true;
            }
        }
        else
        {
            // base follow
            vSingleCamLeft.Follow = player1;
            vSingleCamRight.Follow = player2;

            transitioning = false;

            if (swappedBlackScreens == true)
            {
                swapBlackScreens?.Invoke(swappedBlackScreens);
                swappedBlackScreens = false;
            }
        }
    }

    private IEnumerator SwapSingleTrackingTrue()
    {
        // wait until the brains start AND finish blending
        yield return new WaitUntil(() => brainLeft.IsBlending || brainRight.IsBlending);

        yield return new WaitUntil(() => !brainLeft.IsBlending && !brainRight.IsBlending);


        // send bool of original targeting or not
        SwapSingleCamTrackingTargets(true);
    }

    private IEnumerator SwapSingleTrackingFalse()
    {
        // wait until the brains start AND finish blending
        yield return new WaitUntil(() => brainLeft.IsBlending || brainRight.IsBlending);

        yield return new WaitUntil(() => !brainLeft.IsBlending && !brainRight.IsBlending);


        // send bool of original targeting or not
        SwapSingleCamTrackingTargets(false);
    }

    private IEnumerator DualCamsReady()
    {
        // wait until the brains start AND finish blending
        yield return new WaitUntil(() => brainLeft.IsBlending || brainRight.IsBlending);
        yield return null;

        dualCamsArmed = true;
    }
}
