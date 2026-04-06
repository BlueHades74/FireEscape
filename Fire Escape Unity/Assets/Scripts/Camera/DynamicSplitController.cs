using UnityEngine;
using Unity.Cinemachine;

public class DynamicSplitController : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    [SerializeField] private CinemachineCamera sharedCam;
    [SerializeField] private CinemachineCamera player1Cam;
    [SerializeField] private CinemachineCamera player2Cam;

    [SerializeField] private float splitDistance = 12f;

    // Players distance between value
    private float distance;

    void Update()
    {
        // calculate distance between players
        distance = Vector2.Distance(player1.position, player2.position);
        
        // swap between cameras based on distance value
        if (distance > splitDistance)
        {
            EnableSplit();
        }
        else
        {
            EnableShared();
        }
    }

    void EnableShared()
    {
        sharedCam.Priority = 20;
        player1Cam.Priority = 10;
        player2Cam.Priority = 10;

        sharedCam.gameObject.SetActive(true);
        player1Cam.gameObject.SetActive(false);
        player2Cam.gameObject.SetActive(false);
    }

    void EnableSplit()
    {
        sharedCam.Priority = 10;
        player1Cam.Priority = 20;
        player2Cam.Priority = 20;

        sharedCam.gameObject.SetActive(false);
        player1Cam.gameObject.SetActive(true);
        player2Cam.gameObject.SetActive(true);

        UpdateSplitTargets();
    }

    void UpdateSplitTargets()
    {
        if (player1.position.x < player2.position.x)
        {
            player1Cam.Target.TrackingTarget = player1;
            player2Cam.Target.TrackingTarget = player2;
        }
        else
        {
            player1Cam.Target.TrackingTarget = player2;
            player2Cam.Target.TrackingTarget = player1;
        }
    }
}
