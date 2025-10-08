using UnityEngine;
using Unity.Cinemachine;

public class SplitScreenController : MonoBehaviour
{
 
    public Transform player1;
    public Transform player2;

  
    public Camera mainCamLeft;
    public Camera mainCamRight;
    public Camera mainCamShared;

    
    public CinemachineCamera vcamShared;
    public CinemachineCamera vcamPlayer1;
    public CinemachineCamera vcamPlayer2;

   
    public float splitDistance = 12f;  // Distance to split
    public float mergeDistance = 8f;   // Distance to merge
    public float smoothSpeed = 5f;     // Lerp speed for rect changes

    private bool isSplit = false;

    void Start()
    {
        // Ensure Virtual Cameras are following correct targets
        vcamPlayer1.Follow = player1;
        vcamPlayer2.Follow = player2;
    }

    void Update()
    {
        float distance = Vector2.Distance(player1.position, player2.position);

        // Determine split or merge
        if (!isSplit && distance > splitDistance)
            isSplit = true;
        else if (isSplit && distance < mergeDistance)
            isSplit = false;

        // Handle viewport and camera priorities
        if (isSplit)
        {
            // Enable left/right cameras, disable shared
            mainCamLeft.enabled = true;
            mainCamRight.enabled = true;
            mainCamShared.enabled = false;

            // Set Cinemachine priorities
            vcamShared.Priority = 10;
            vcamPlayer1.Priority = 20;
            vcamPlayer2.Priority = 20;
        }
        else
        {
            // Enable shared camera, disable left/right
            mainCamLeft.enabled = false;
            mainCamRight.enabled = false;
            mainCamShared.enabled = true;

            // Set Cinemachine priorities
            vcamShared.Priority = 20;
            vcamPlayer1.Priority = 10;
            vcamPlayer2.Priority = 10;
        }

        // Optional: smooth split animation (viewport width)
        if (isSplit)
        {
            float leftWidth = Mathf.Lerp(mainCamLeft.rect.width, 0.5f, Time.deltaTime * smoothSpeed);
            mainCamLeft.rect = new Rect(0, 0, leftWidth, 1);
            mainCamRight.rect = new Rect(1f - leftWidth, 0, leftWidth, 1);
        }
        else
        {
            mainCamShared.rect = new Rect(0, 0, 1, 1);
        }
    }
}
