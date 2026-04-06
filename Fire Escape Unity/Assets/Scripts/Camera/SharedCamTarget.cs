using Unity.Cinemachine;
using UnityEngine;

public class SharedCamTarget : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private GameObject vDualCamLeft;
    [SerializeField] private GameObject vDualCamRight;
    [SerializeField] private CinemachineCamera vSingleCamLeft;
    [SerializeField] private CinemachineCamera vSingleCamRight;
    [SerializeField] private float breakDistance = 15f;

    private bool canMidpoint = true;

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
        Debug.Log(canMidpoint);
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
            vDualCamLeft.SetActive(false);
            vDualCamRight.SetActive(false);
        }
        else
        {
            vDualCamLeft.SetActive(true);
            vDualCamRight.SetActive(true);
        }

        if (player1.position.x > player2.position.x && vDualCamLeft.activeSelf == true)
        {
            vSingleCamLeft.Follow = player2;
            vSingleCamRight.Follow = player1;
        }
        else if (vDualCamLeft.activeSelf == true)
        {
            vSingleCamLeft.Follow = player1;
            vSingleCamRight.Follow = player2;
        }
    }
}
