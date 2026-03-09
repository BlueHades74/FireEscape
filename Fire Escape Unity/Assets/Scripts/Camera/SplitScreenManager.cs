using UnityEngine; 
using Unity.Cinemachine; 

  

public class SplitScreenManager : MonoBehaviour 

{ 

    public Transform player1; 
    public Transform player2; 

    public Camera leftCam; 
    public Camera rightCam; 

    public CinemachineCamera vcamShared; 
    public CinemachineCamera vcamPlayer1; 
    public CinemachineCamera vcamPlayer2; 

    public float splitDistance = 12f; 
    public float mergeDistance = 8f; 
    public float smoothSpeed = 5f; 

    float splitT = 0f;

    bool isSplit = false;

    [Header("Zoom Settings")]
    public float zoomMerged = 55f;
    public float zoomSplit = 40f;

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
        vcamPlayer1.OutputChannel = OutputChannels.Channel01; // controls left brain 
        vcamPlayer2.OutputChannel = OutputChannels.Channel02; // controls right brain 

        // Shared VCam outputs to both channels so it can control both brains when its priority is highest 
        vcamShared.OutputChannel = OutputChannels.Channel01 | OutputChannels.Channel02; 

        // Starting priorities  
        vcamShared.Priority = 20; 
        vcamPlayer1.Priority = 10; 
        vcamPlayer2.Priority = 10; 
    } 

  

    void Update()
{
       


    if (!player1 || !player2) return;

    float distance = Vector3.Distance(player1.position, player2.position);

    if (!isSplit && distance > splitDistance)
        isSplit = true;
    else if (isSplit && distance < mergeDistance)
        isSplit = false;

    // Smooth progress value
    float targetT = isSplit ? 1f : 0f;
    splitT = Mathf.MoveTowards(splitT, targetT, Time.deltaTime * smoothSpeed);

        //Smooth zoom based on splitT
        float currentFOV = Mathf.Lerp(zoomMerged, zoomSplit, splitT);

        // Apply to both Player cameras
        SetZoom(vcamPlayer1, currentFOV);
        SetZoom(vcamPlayer2, currentFOV);
        SetZoom(vcamShared, currentFOV);

        // Apply split based on progress
        if (splitT > 0f)
    {
        leftCam.enabled = true;
        rightCam.enabled = true;

        float leftWidth = Mathf.Lerp(1f, 0.5f, splitT);

        leftCam.rect = new Rect(0f, 0f, leftWidth, 1f);
        rightCam.rect = new Rect(leftWidth, 0f, 1f - leftWidth, 1f);
    }
    else
    {
        leftCam.rect = new Rect(0f, 0f, 1f, 1f);
        rightCam.enabled = false;
    }

    // Cinemachine priorities
    if (isSplit)
    {
        vcamShared.Priority = 5;
        vcamPlayer1.Priority = 20;
        vcamPlayer2.Priority = 20;
    }
    else
    {
        vcamShared.Priority = 20;
        vcamPlayer1.Priority = 5;
        vcamPlayer2.Priority = 5;
    }

    // Shared camera positioning
    if (vcamShared != null)
    {
        Vector3 mid = (player1.position + player2.position) * 0.5f;
        vcamShared.transform.position = Vector3.Lerp(
            vcamShared.transform.position,
            new Vector3(mid.x, mid.y, vcamShared.transform.position.z),
            Time.deltaTime * smoothSpeed
        );
    }
}

    void SetZoom(CinemachineCamera cam, float fov)
    {
        if (cam != null)
            cam.Lens.FieldOfView = fov;

    }

  

    void EnsureBrain(Camera cam) 

    {

        if (cam == null) return; 
        
        if (!cam.TryGetComponent<CinemachineBrain>(out var brain)) 
            cam.gameObject.AddComponent<CinemachineBrain>(); 
    } 

} 