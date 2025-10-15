using UnityEngine;
using Unity.Cinemachine;

public class SplitScreenManager : MonoBehaviour
{
    
    public Transform player1;
    public Transform player2;

    public Camera leftCam;
    public Camera rightCam;

    public CinemachineVirtualCameraBase vcamShared;
    public CinemachineVirtualCameraBase vcamPlayer1;
    public CinemachineVirtualCameraBase vcamPlayer2;

    public float splitDistance = 12f;
    public float mergeDistance = 8f;
    public float smoothSpeed = 5f;

    bool isSplit = false;

    void Start()
    {
        // Ensure CinemachineBrain exists on both cameras
        EnsureBrain(leftCam);
        EnsureBrain(rightCam);

        var leftBrain = leftCam.GetComponent<CinemachineBrain>();
        var rightBrain = rightCam.GetComponent<CinemachineBrain>();

        // Set each brain to listen to a different channel 
        leftBrain.ChannelMask = OutputChannels.Channel01;
        rightBrain.ChannelMask = OutputChannels.Channel02;

        // Route VCams to channels:
        vcamPlayer1.OutputChannel = OutputChannels.Channel01; // drives left brain
        vcamPlayer2.OutputChannel = OutputChannels.Channel02; // drives right brain

        // Shared VCam outputs to both channels so it can drive both brains when its priority is highest
        vcamShared.OutputChannel = OutputChannels.Channel01 | OutputChannels.Channel02;

        // initial priorities 
        vcamShared.Priority = 20;
        vcamPlayer1.Priority = 10;
        vcamPlayer2.Priority = 10;
    }

    void Update()
    {
        if (!player1 || !player2) return;

  
        float distance = Vector2.Distance(player1.position, player2.position);
        if (!isSplit && distance > splitDistance) isSplit = true;
        else if (isSplit && distance < mergeDistance) isSplit = false;

        // smooth viewport width
        float targetWidth = isSplit ? 0.5f : 1f;
        float newWidth = Mathf.Lerp(leftCam.rect.width, targetWidth, Time.deltaTime * smoothSpeed);

        if (isSplit)
        {
            leftCam.enabled = true;
            rightCam.enabled = true;

            leftCam.rect = new Rect(0f, 0f, newWidth, 1f);
            rightCam.rect = new Rect(1f - newWidth, 0f, newWidth, 1f);

            // give player VCams priority on their channels
            vcamShared.Priority = 5;
            vcamPlayer1.Priority = 20;
            vcamPlayer2.Priority = 20;
        }
        else
        {
            // merged camera
            leftCam.rect = new Rect(0f, 0f, 1f, 1f);
            rightCam.enabled = false;

            vcamShared.Priority = 20;
            vcamPlayer1.Priority = 5;
            vcamPlayer2.Priority = 5;
        }

        // move shared camera between players
        if (vcamShared != null)
        {
            Vector3 mid = (player1.position + player2.position) * 0.5f;
            vcamShared.transform.position = new Vector3(mid.x, mid.y, vcamShared.transform.position.z);
        }
    }

    void EnsureBrain(Camera cam)
    {
        if (cam == null) return;
        if (!cam.TryGetComponent<CinemachineBrain>(out var brain))
            cam.gameObject.AddComponent<CinemachineBrain>();
    }
}

